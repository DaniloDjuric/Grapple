using Grapple.Level;
using Grapple.Models;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grapple
{
    internal class Physics
    {
        LevelModel level;
        PlayerModel player;

        public Physics(LevelModel levelModel) { 
            level = levelModel;
            player = levelModel.Player;
        }

        public void MoveTowards(Vector2 direction, GameTime gameTime)
        {
            Vector2 newPosition = player.Position + direction * player.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            // If Collision();
            if(newPosition.X < 1500)
            {
                player.Position = newPosition;
            }
        }

        public bool CheckCollision_p_b(BalloonModel balloon)
        {
            // Collision check between player and balloons
            return player.Position.X+10 < balloon.X + balloon.Width &&
                   player.Position.X+10 + player.Width > balloon.X &&
                   player.Position.Y+10 < balloon.Y + balloon.Height &&
                   player.Position.Y+10 + player.Height > balloon.Y;
        }


        public Vector2 CalculateTargetPosition(Vector2 startPoint, Vector2 endPoint)
        {
            // Calculate the extended line until it hits a platform
            Vector2 intersectionPoint = Raycast(startPoint, endPoint);
            return intersectionPoint;
        }

        public Vector2 Raycast(Vector2 startPoint, Vector2 endPoint)
        {
            // Raycasting algorithm to check for intersections with platforms
            foreach (var platform in level.Platforms)
            {
                Vector2 intersectionPoint = SegmentIntersectsRectangle(startPoint, endPoint, platform);

                if (intersectionPoint != Vector2.Zero)
                {
                    // Return the intersection point
                    return intersectionPoint;
                }

            }
            return endPoint;
        }

        private Vector2 SegmentIntersectsRectangle(Vector2 startPoint, Vector2 endPoint, PlatformModel platform)
        {
            // Check if the line segment intersects with the rectangle
            float left = Math.Min(platform.X, platform.X + platform.Width);
            float right = Math.Max(platform.X, platform.X + platform.Width);
            float top = Math.Min(platform.Y, platform.Y + platform.Height);
            float bottom = Math.Max(platform.Y, platform.Y + platform.Height);

            if (LineIntersectsRectangle(startPoint, endPoint, platform))
            {
                // Return the intersection point
                return GetIntersectionPoint(startPoint, endPoint, platform);
            }

            // If no intersection, return zero vector
            return Vector2.Zero;
        }

        private bool LineIntersectsLine(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4)
        {
            // Check if two lines intersect
            float cross1 = (p4.Y - p3.Y) * (p2.X - p1.X) - (p4.X - p3.X) * (p2.Y - p1.Y);
            float cross2 = (p2.Y - p1.Y) * (p3.X - p1.X) - (p2.X - p1.X) * (p3.Y - p1.Y);

            float t1 = cross1 / (p2 - p1).LengthSquared();
            float t2 = cross2 / (p4 - p3).LengthSquared();

            return (t1 >= 0 && t1 <= 1 && t2 >= 0 && t2 <= 1);
        }

        private bool LineIntersectsRectangle(Vector2 startPoint, Vector2 endPoint, PlatformModel platform)
        {
            // Check if the line intersects with any of the sides of the rectangle
            return LineIntersectsLine(startPoint, endPoint, new Vector2(platform.X, platform.Y), new Vector2(platform.X + platform.Width, platform.Y)) || // Top side
                   LineIntersectsLine(startPoint, endPoint, new Vector2(platform.X + platform.Width, platform.Y), new Vector2(platform.X + platform.Width, platform.Y + platform.Height)) || // Right side
                   LineIntersectsLine(startPoint, endPoint, new Vector2(platform.X, platform.Y + platform.Height), new Vector2(platform.X + platform.Width, platform.Y + platform.Height)) || // Bottom side
                   LineIntersectsLine(startPoint, endPoint, new Vector2(platform.X, platform.Y), new Vector2(platform.X, platform.Y + platform.Height)); // Left side
        }

        private Vector2 GetIntersectionPoint(Vector2 startPoint, Vector2 endPoint, PlatformModel platform)
        {
            // Calculate the direction vector of the line segment
            Vector2 direction = endPoint - startPoint;

            // Calculate the intersection points with each side of the rectangle
            Vector2[] intersections = new Vector2[4];

            intersections[0] = LineIntersection(startPoint, direction, new Vector2(platform.X, platform.Y), new Vector2(platform.X + platform.Width, platform.Y)); // Top side
            intersections[1] = LineIntersection(startPoint, direction, new Vector2(platform.X + platform.Width, platform.Y), new Vector2(platform.X + platform.Width, platform.Y + platform.Height)); // Right side
            intersections[2] = LineIntersection(startPoint, direction, new Vector2(platform.X, platform.Y + platform.Height), new Vector2(platform.X + platform.Width, platform.Y + platform.Height)); // Bottom side
            intersections[3] = LineIntersection(startPoint, direction, new Vector2(platform.X, platform.Y), new Vector2(platform.X, platform.Y + platform.Height)); // Left side

            // Find the closest intersection point
            Vector2 closestIntersection = Vector2.Zero;
            float closestDistance = float.MaxValue;

            foreach (var intersection in intersections)
            {
                if (intersection != Vector2.Zero)
                {
                    float distance = Vector2.Distance(startPoint, intersection);
                    if (distance < closestDistance)
                    {
                        closestIntersection = intersection;
                        closestDistance = distance;
                    }
                }
            }

            return closestIntersection;
        }

        private Vector2 LineIntersection(Vector2 p1, Vector2 v1, Vector2 p2, Vector2 v2)
        {
            // Calculate the intersection point of two lines
            float cross = (v1.X * v2.Y - v1.Y * v2.X);

            if (cross == 0)
            {
                // Lines are parallel
                return Vector2.Zero;
            }

            float t = ((p2.X - p1.X) * v2.Y + (p1.Y - p2.Y) * v2.X) / cross;

            return p1 + t * v1;
        }
    }
}
