using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Grapple.Ninja
{

    internal class Ninja
    {

        private static Texture2D ninjaAttack;
        private Vector2 position;
        private Animation animation;
        private SpriteBatch spriteBatch;

        public Ninja(Vector2 pos, ContentManager content, SpriteBatch spriteBatch)
        {
            ninjaAttack = content.Load<Texture2D>("ATTACK 1");
            this.animation = new(ninjaAttack, 7, 0.1f);
            this.position = pos;
            this.spriteBatch = spriteBatch;
        }

        public void Update()
        {
            animation.Update();
        }

        public void Draw()
        {
            animation.Draw(position);
        }
    }
}
