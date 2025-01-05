using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NEITGameEngine.Objects.Base;
using System.Diagnostics;

namespace NEITGameEngine.Objects
{
    public class PatrolEnemy:BaseGameObject
    {
        private Vector2 position;
        private float speed;
        private float leftBound;
        private float rightBound;
        private bool movingRight;

        public PatrolEnemy(Vector2 startPosition, float speed, float leftBound, float rightBound, Texture2D texture)
        {
            this.position = new(startPosition.X - texture.Width/2, startPosition.Y - texture.Height/2);
            this.speed = speed;
            this.leftBound = position.X - leftBound;
            this.rightBound = rightBound + position.X;
            this.movingRight = true;
            _texture = texture;
        }

        public override void Update(GameTime gameTime)
        {
            if (movingRight)
            {
               
                position.X += speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                //position = position;
                if (position.X >= rightBound)
                {
                    movingRight = false;
                }
            }
            else
            {
               
                position.X -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (position.X <= leftBound)
                {
                    movingRight = true;
                }
            }
            Position = position;

        }

        public void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

       
    }

}
