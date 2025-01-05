using NEITGameEngine.Objects.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace NEITGameEngine.Objects
{
    public class ChaseEnemy : BaseGameObject
    {

        private Vector2 position;
        private float speed;
        private float detectionRadius;

        public ChaseEnemy(Vector2 startPosition, float speed, float detectionRadius, Texture2D texture)
        {
            this.position = startPosition;
            this.speed = speed;
            this.detectionRadius = detectionRadius;
            _texture = texture;
        }

        public void Update(GameTime gameTime, Vector2 playerPosition)
        {
            Vector2 direction = playerPosition - position;
            float distance = direction.Length();
            Debug.WriteLine(distance);
            if (distance < detectionRadius)
            {
                direction.Normalize();
                position += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            Position = position;
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }


    }
}
