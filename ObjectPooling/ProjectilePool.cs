using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using NEITGameEngine.Objects;
using System.Collections.Generic;
using System.Diagnostics;



namespace NEITGameEngine.ObjectPooling
{
    public class ProjectilePool
    {
        public List<Projectile> pool;
        private Texture2D texture;
        private int poolSize;

        public ProjectilePool(Texture2D texture, int poolSize)
        {
            this.texture = texture;
            this.poolSize = poolSize;
            pool = new List<Projectile>();

            for (int i = 0; i < poolSize; i++)
            {
                pool.Add(new Projectile(texture, Vector2.Zero, Vector2.Zero, 0f,0f, Vector2.Zero, Vector2.Zero));
                pool[i].IsActive = false; // Start all projectiles as inactive
            }
        }

        public Projectile GetProjectile(Vector2 startPosition, Vector2 direction, float speed,float rotation, Vector2 minBounds, Vector2 maxBounds)
        {
            foreach (var projectile in pool)
            {
                if (!projectile.IsActive)
                {
                    // "Activate" the projectile by setting its properties
                    projectile.Position = startPosition;
                    projectile.Direction = direction;
                    projectile.Speed = speed;
                    projectile.IsActive = true;
                    projectile.Rotation = rotation;
                    projectile.MinBounds = minBounds;
                    projectile.MaxBounds = maxBounds;
                    return projectile;
                }
            }

            // Optional: If no inactive projectiles are found, return null or handle it differently
            return null;
        }

        public void Update(GameTime gameTime)
        {
            foreach (var projectile in pool)
            {
                //Debug.WriteLine(projectile.Position);
                if (projectile.IsActive)
                {
                    projectile.Update(gameTime);

                    // Deactivate projectile if it goes off-screen
                    if (projectile.Position.X < projectile.MinBounds.X || projectile.Position.Y < projectile.MinBounds.Y ||
                        projectile.Position.X > projectile.MaxBounds.X || projectile.Position.Y > projectile.MaxBounds.Y)
                    {
                        projectile.IsActive = false;
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var projectile in pool)
            {
                if (projectile.IsActive)
                {
                    //Debug.WriteLine(projectile.Position);
                    projectile.Draw(spriteBatch);
                }
            }
        }
    }

}
