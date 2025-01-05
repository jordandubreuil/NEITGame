using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NEITGameEngine.ObjectPooling;
using NEITGameEngine.Objects;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace NEITGameEngine.Components
{
    
    public class Shooting
    {
        //private List<Projectile> projectiles;
        private ProjectilePool projectiles;
        private Texture2D projectileTexture;
        private float projectileSpeed;
        private Vector2 shootDirection;
        private float cooldown;           
        private float timeSinceLastShot;
        

        public Shooting(Texture2D projectileTexture, float speed, Vector2 direction, int poolSize, float cooldown = 0.5f)
        {
            this.projectileTexture = projectileTexture;
            this.projectileSpeed = speed;
            this.shootDirection = direction;
            //projectiles = new List<Projectile>();
            projectiles = new ProjectilePool(projectileTexture, poolSize);
            this.cooldown = cooldown;
            this.timeSinceLastShot = cooldown;
        }

        public void Update(GameTime gameTime, Vector2 projectilePos, Vector2 playerDir, float rot, Vector2 minBounds, Vector2 maxBounds)
        {
            timeSinceLastShot += (float)gameTime.ElapsedGameTime.TotalSeconds;
            //Debug.WriteLine(minBounds);
            //Debug.WriteLine(maxBounds); 
            // Check for shooting input without modifying player movement code
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && timeSinceLastShot > cooldown)
            {
                Debug.WriteLine($"{projectilePos}{playerDir}{rot}{minBounds}{maxBounds}");
                FireProjectile(projectilePos,playerDir,rot, minBounds, maxBounds);
                timeSinceLastShot = 0;
            }
            projectiles.Update(gameTime);
            // Update all projectiles
            //for (int i = projectiles.Count - 1; i >= 0; i--)
            //{
            //    projectiles[i].Update(gameTime);
               
            //    if (!projectiles[i].IsActive)
            //    {
            //        projectiles.RemoveAt(i); // Remove inactive projectiles
            //    }
            //}
        }

        

        public void FireProjectile(Vector2 playerPosition, Vector2 playerDirection, float rotation, Vector2 minBounds, Vector2 maxBounds)
        {
            // Create a new projectile at the player's current position
            //Projectile newProjectile = new Projectile(projectileTexture, playerPosition, playerDirection, projectileSpeed, rotation, minBounds, maxBounds);
            projectiles.GetProjectile(playerPosition,playerDirection, this.projectileSpeed,rotation,minBounds,maxBounds);
            Debug.WriteLine("Added bullet");
           //projectiles.Add(newProjectile);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //foreach (var projectile in projectiles)
            //{
            //    projectile.Draw(spriteBatch);
            //}

            projectiles.Draw(spriteBatch);
        }

        public List<Projectile> GetActiveProjectiles()
        {
            return projectiles.pool.Where(p => p.IsActive).ToList();
        }
    }
}
