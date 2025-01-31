using Grapple.Level;
using Grapple.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grapple.General
{
    internal static class Physics
    {
        public static LevelModel levelModel { get; set; }

        public static void MoveTowards(Vector2 targetPosition, GameTime gameTime, ref bool Moving, PlayerModel player)
        {
            if (!Moving) return; // Skip movement if not moving

            Vector2 direction = Vector2.Normalize(targetPosition - player.Position);
            Vector2 newPosition = player.Position + direction * player.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Add clamping to stop overshooting
            if (Vector2.Distance(newPosition, targetPosition) < Vector2.Distance(player.Position, newPosition))
            {
                newPosition = targetPosition; // Snap to the target
                Moving = false; // Stop movement
            }

            // Check if the player has reached the target (accounting for size)
            // Adjust the player's position to prevent snapping outside the playable area
            if (HasReachedTarget(newPosition, targetPosition, player))
            {
                Vector2 adjustedPosition = GetSnappedPosition(targetPosition, player);
                player.Position = adjustedPosition; // Snap adjusted position
                Moving = false; // Stop moving
            }
            else
            {
                player.Position = newPosition;
            }
        }

        private static Vector2 GetSnappedPosition(Vector2 targetPosition, PlayerModel player)
        {
            float adjustedX = targetPosition.X;
            float adjustedY = targetPosition.Y;

            // Check if colliding with a wall on the right or bottom side
            if (targetPosition.X + player.Width > Globals.PlayableArea.Width)
                adjustedX = Globals.PlayableArea.Width - player.Width;

            if (targetPosition.Y + player.Height > Globals.PlayableArea.Height)
                adjustedY = Globals.PlayableArea.Height - player.Height;

            // Check if colliding with a wall on the left or top side
            if (targetPosition.X < Globals.PlayableArea.X)
                adjustedX = Globals.PlayableArea.X;

            if (targetPosition.Y < Globals.PlayableArea.Y)
                adjustedY = Globals.PlayableArea.Y;

            return new Vector2(adjustedX, adjustedY);
        }

        private static bool HasReachedTarget(Vector2 newPosition, Vector2 targetPosition, PlayerModel player)
        {
            // Define the player's bounding box
            Globals.RectangleF playerBounds = new Globals.RectangleF(newPosition.X, newPosition.Y, player.Width, player.Height);

            // Create a small rectangle around the target position
            float targetTolerance = 1f; // Small tolerance to ensure precision
            Globals.RectangleF targetBounds = new Globals.RectangleF(
                targetPosition.X - targetTolerance,
                targetPosition.Y - targetTolerance,
                targetTolerance * 2,
                targetTolerance * 2
            );

            // Check for intersection
            return playerBounds.IntersectsWith(targetBounds);
        }

        public static bool CheckCollision_p_b(BalloonModel balloon)
        {
            // Collision check between player and balloons
            return levelModel.Player.Position.X < balloon.X + balloon.Width &&
                   levelModel.Player.Position.X + levelModel.Player.Width > balloon.X &&
                   levelModel.Player.Position.Y < balloon.Y + balloon.Height &&
                   levelModel.Player.Position.Y + levelModel.Player.Height > balloon.Y;
        }

        public static Vector2 CalculateTargetPosition(Vector2 startPoint, Vector2 direction)
        {
            Vector2 rayDirection = Vector2.Normalize(direction - startPoint); // Direction of the ray
            Vector2 closestIntersection = Vector2.Zero;
            float closestDistance = float.MaxValue;

            // Iterate through all platforms to find the closest intersection point
            foreach (var platform in levelModel.Platforms)
            {
                // Define rectangle edges as line segments
                Vector2 topLeft = new Vector2(platform.X, platform.Y);
                Vector2 topRight = new Vector2(platform.X + platform.Width, platform.Y);
                Vector2 bottomLeft = new Vector2(platform.X, platform.Y + platform.Height);
                Vector2 bottomRight = new Vector2(platform.X + platform.Width, platform.Y + platform.Height);

                Vector2[] edges = new Vector2[]
                {
                    LineIntersection(startPoint, rayDirection, topLeft, topRight), // Top
                    LineIntersection(startPoint, rayDirection, topRight, bottomRight), // Right
                    LineIntersection(startPoint, rayDirection, bottomLeft, bottomRight), // Bottom
                    LineIntersection(startPoint, rayDirection, topLeft, bottomLeft) // Left
                };

                foreach (var intersection in edges)
                {
                    if (intersection != Vector2.Zero) // If there's a valid intersection
                    {
                        float distance = Vector2.Distance(startPoint, intersection);
                        if (distance < closestDistance)
                        {
                            closestIntersection = intersection;
                            closestDistance = distance;
                        }
                    }
                }
            }

            return closestIntersection;
        }

        private static Vector2 LineIntersection(Vector2 rayOrigin, Vector2 rayDirection, Vector2 segmentStart, Vector2 segmentEnd)
        {
            Vector2 segmentDirection = segmentEnd - segmentStart;

            // Calculate determinant
            float cross = rayDirection.X * segmentDirection.Y - rayDirection.Y * segmentDirection.X;
            if (Math.Abs(cross) < 1e-6)
            {
                // Parallel or coincident lines
                return Vector2.Zero;
            }

            Vector2 delta = segmentStart - rayOrigin;

            float t = (delta.X * segmentDirection.Y - delta.Y * segmentDirection.X) / cross;
            float u = (delta.X * rayDirection.Y - delta.Y * rayDirection.X) / cross;

            // Check if intersection lies within the line segment
            if (u >= 0 && u <= 1 && t >= 0) // u checks segment bounds, t ensures ray direction
            {
                return rayOrigin + t * rayDirection;
            }

            return Vector2.Zero;
        }

    }
}
