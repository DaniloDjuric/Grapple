﻿using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Grapple.General
{

    internal class Animation
    {
        private readonly Texture2D _texture;
        private readonly List<Rectangle> _sourceRectangles = new();

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

            var frameWidth = _texture.Width / framesX;
            var frameHeight = _texture.Height;

            for (int i = 0; i < _totalFrames; i++)
            {
                _sourceRectangles.Add(new Rectangle(i * frameWidth + 35, 45, frameWidth - 30, frameHeight - 30));
            }
        }


        public void Update()
        {
            _frameTimeLeft -= Globals.TotalSeconds;

            if (_frameTimeLeft <= 0)
            {
                _frameTimeLeft += _frameTime;
                _frame = (_frame + 1) % _totalFrames;
            }

        }


        public void Draw(Vector2 pos, bool isFacingRight)
        {
            Globals.SpriteBatch.Draw(
                _texture,
                pos,
                _sourceRectangles[_frame],
                Color.White,
                0f,
                isFacingRight ? new Vector2(10, 0) : new Vector2(30, 0),
                new Vector2(2.5f, 2.5f),
                isFacingRight ? SpriteEffects.None : SpriteEffects.FlipHorizontally // Fliping the sprite
                , 0f);


        }
    }
}
