using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Grapple
{

    internal class Animation
    {        
        private readonly Texture2D _texture;
        private readonly List<Rectangle> _sourceRectangles = new();

        private GameTime gameTime;

        public int _totalFrames;
        public int _frame;
        public float _frameTime;
        public float _frameTimeLeft;

        public Animation(Texture2D texture, int framesX, float frameTime)
        {
            _texture = texture;
            _frameTime = frameTime;
            _frameTimeLeft = _frameTime;
            _totalFrames = framesX;
            gameTime = new GameTime();

            var frameWidth = (_texture.Width / framesX);
            var frameHeight = _texture.Height;

            for (int i = 0; i < _totalFrames; i++)
            {
                _sourceRectangles.Add(new Rectangle(i * frameWidth + 30, 30, frameWidth - 30, frameHeight - 30));
            }
        }


        public void Update()
        {
            float ellapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _frameTimeLeft -= 0.02f; ;

            if (_frameTimeLeft <= 0) {
                _frameTimeLeft += _frameTime;
                _frame = (_frame + 1) % _totalFrames;
            }

        }


        public void Draw(Vector2 pos, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, pos, _sourceRectangles[_frame], Color.White, 0f, new Vector2(), new Vector2(2, 2), new SpriteEffects(), 0f) ;
        }
    }
}
