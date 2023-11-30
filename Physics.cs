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
        LevelModel levelModel;

        public Physics(LevelModel level) { 
            levelModel = level;
        }

        public void MoveTowards(Vector2 targetPosition, GameTime gameTime)
        {
            // Calculate the direction to the target position
            Vector2 playerPos = new Vector2(levelModel.Player.X, levelModel.Player.Y);
            Vector2 direction = Vector2.Normalize(targetPosition - playerPos);
            
            // Calculate the new position
            Vector2 newPosition = new Vector2(
                levelModel.Player.X + direction.X * levelModel.Player.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds,
                levelModel.Player.Y + direction.Y * levelModel.Player.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds
            );

            // Update the player's position
            levelModel.Player.X = newPosition.X;
            levelModel.Player.Y = newPosition.Y;
        }

        public Vector2 Raycast(Vector2 startPoint, Vector2 endPoint)
        {
            // Raycasting algorithm to check for intersections with platforms
            foreach (var platform in levelModel.Platforms)
            {
                if (SegmentIntersectsRectangle(startPoint, endPoint, platform))
                {
                    // Return the intersection point
                    return GetIntersectionPoint(startPoint, endPoint, platform);
                }
            }

            // If no intersection, return zero vector
            return Vector2.Zero;
        }

        private bool SegmentIntersectsRectangle(Vector2 startPoint, Vector2 endPoint, PlatformModel platform)
        {
            // Check if the line segment intersects with the rectangle
            float left = Math.Min(platform.X, platform.X + platform.Width);
            float right = Math.Max(platform.X, platform.X + platform.Width);
            float top = Math.Min(platform.Y, platform.Y + platform.Height);
            float bottom = Math.Max(platform.Y, platform.Y + platform.Height);

            return (
                startPoint.X <= right && endPoint.X >= left &&
                startPoint.Y <= bottom && endPoint.Y >= top
            );
        }

        private Vector2 GetIntersectionPoint(Vector2 startPoint, Vector2 endPoint, PlatformModel platform)
        {
            // Calculate the intersection point of the line segment and the rectangle
            float x = MathHelper.Clamp(endPoint.X, platform.X, platform.X + platform.Width);
            float y = MathHelper.Clamp(endPoint.Y, platform.Y, platform.Y + platform.Height);

            return new Vector2(x, y);
        }
    }
}
