using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grapple.General
{
    public class Camera
    {
        public Vector2 Position { get; private set; } = Vector2.Zero;
        private float shakeDuration = 0f;
        private float shakeIntensity = 0f;
        private Random random = new Random();

        public Matrix GetViewMatrix()
        {
            return Matrix.CreateTranslation(new Vector3(-Position, 0));
        }

        public void StartShake(float intensity, float duration)
        {
            shakeIntensity = intensity;
            shakeDuration = duration;
        }

        public void Update(GameTime gameTime)
        {
            if (shakeDuration > 0)
            {
                shakeDuration -= (float)gameTime.ElapsedGameTime.TotalSeconds;

                float shakeOffsetX = (float)(random.NextDouble() * 2 - 1) * shakeIntensity;
                float shakeOffsetY = (float)(random.NextDouble() * 2 - 1) * shakeIntensity;

                Position = new Vector2(shakeOffsetX, shakeOffsetY);
            }
            else
            {
                Position = Vector2.Zero;
            }
        }
    }
}
