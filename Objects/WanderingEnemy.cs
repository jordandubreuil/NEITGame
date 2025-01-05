using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEITGameEngine.Objects.Base;
using System.Diagnostics;

namespace NEITGameEngine.Objects
{
    public class WanderingEnemy:BaseGameObject
    {
        private Vector2 position;
        private Vector2 direction;
        private float speed;
        private float changeDirectionInterval;
        private float timeSinceLastChange;

        private Random random;

        private BaseGameObject parent;

        private PlayerSprite player;
        public bool IsActive { get; set; } = false;

        public WanderingEnemy(Vector2 startPosition, float speed, float changeDirectionInterval, Texture2D texture, BaseGameObject parent = null, PlayerSprite player = null)
        {
            this.position = startPosition;
            this.speed = speed;
            this.changeDirectionInterval = changeDirectionInterval;
            this.timeSinceLastChange = 0f;
            _texture = texture;
            this.random = new Random();
            this.parent = parent;
            this.player = player;
            SetRandomDirection();
        }

        public override void Update(GameTime gameTime)
        {
            timeSinceLastChange += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timeSinceLastChange > changeDirectionInterval)
            {
                SetRandomDirection();
                timeSinceLastChange = 0f;
            }

            position += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Apply parent's position if there is a parent
            if (parent != null)
            {
                position += parent.Position;
            }

            // Clamp position to keep enemy within screen bounds
            // Check if the enemy hits the screen boundaries and change direction
            if (position.X <= 0 || position.X >= Globals.ScreenSize.X - _texture.Width)
            {
                SetRandomDirection();
                position.X = MathHelper.Clamp(position.X, 0, Globals.ScreenSize.X - _texture.Width);
            }

            if (position.Y <= 0 || position.Y >= Globals.ScreenSize.Y - _texture.Height)
            {
                SetRandomDirection();
                position.Y = MathHelper.Clamp(position.Y, 0, Globals.ScreenSize.Y - _texture.Height);
            }

            if (player != null)
            {
                //Debug.WriteLine(BoxCollider);
                //Debug.WriteLine(player.BoxCollider);


                if (BoxCollider.Intersects(player.BoxCollider))
                {
                    // Handle collision (e.g., bounce off, stop movement, change direction)
                    // SetRandomDirection();
                    //Debug.WriteLine("Hit");
                }
            }

            Position = position;
        }

        //public void Draw(SpriteBatch spriteBatch, Texture2D texture)
        //{
        //    spriteBatch.Draw(texture, position, Color.White);
        //}

        private void SetRandomDirection()
        {
            direction = new Vector2((float)random.NextDouble() * 2 - 1, (float)random.NextDouble() * 2 - 1);
            if (direction.Length() > 0)
            {
                direction.Normalize();
            }
        }

        public void Activate(Vector2 spawnPosition, float speed)
        {
            Position = spawnPosition;
            this.speed = speed;
            IsActive = true;
        }

        public void Deactivate()
        {
            IsActive = false;
        }
    }



}
