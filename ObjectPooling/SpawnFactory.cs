using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEITGameEngine.ObjectPooling
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using NEITGameEngine.Objects;
    using NEITGameEngine.Objects.Base;

    public class SpawnFactory:BaseGameObject
    {
        //private List<Enemy> enemyPool;
        private List<WanderingEnemy> enemyPool;
        private Texture2D enemyTexture;
        private int poolSize;
        private float spawnInterval;
        private float timeSinceLastSpawn;
        private Vector2 spawnPosition; // The position from which enemies spawn
        private bool randomizeSpawnX;  // Optional: allows random X offsets near spawn position

        public SpawnFactory(Texture2D enemyTexture, int poolSize, float spawnInterval, Vector2 spawnPosition, bool randomizeSpawnX = false,BaseGameObject map = null, PlayerSprite playerSprite = null)
        {
            this.enemyTexture = enemyTexture;
            this.poolSize = poolSize;
            this.spawnInterval = spawnInterval;
            this.spawnPosition = spawnPosition;
            this.randomizeSpawnX = randomizeSpawnX;
            timeSinceLastSpawn = 0f;

            // Initialize the pool with inactive enemies
            enemyPool = new List<WanderingEnemy>();
            for (int i = 0; i < poolSize; i++)
            {
                enemyPool.Add(new WanderingEnemy(spawnPosition, 100f,1f,enemyTexture, map, playerSprite));
            }
            //Debug.WriteLine(enemyPool.Count);
        }

        public override void Update(GameTime gameTime)
        {
            timeSinceLastSpawn += (float)gameTime.ElapsedGameTime.TotalSeconds;
           
            // Spawn a new enemy if the interval has passed
            if (timeSinceLastSpawn >= spawnInterval)
            {
                SpawnEnemy();
                timeSinceLastSpawn = 0f;
                //Debug.WriteLine("Spawned Enemy");
            }

            // Update all enemies in the pool
            foreach (var enemy in enemyPool)
            {
                if (enemy.IsActive)
                {
                   
                    enemy.Update(gameTime);
                }
            }
        }

        private void SpawnEnemy()
        {
            foreach (var enemy in enemyPool)
            {
                
                if (!enemy.IsActive)
                {
                    // Use the factory's spawn position, optionally randomizing the X coordinate
                    Vector2 position = spawnPosition;
                    if (randomizeSpawnX)
                    {
                        position.X += Random.Shared.Next(-100, 100); // Randomize within a range
                    }

                    float speed = Random.Shared.Next(50, 150); // Random speed
                    enemy.Activate(position, speed);
                    break; // Only activate one enemy
                }
            }
        }



        public override void Render(SpriteBatch spriteBatch)
        {
            //Debug.WriteLine("Spawner");
            foreach (var enemy in enemyPool)
            {
                if (enemy.IsActive)
                {
                    //Debug.WriteLine(enemy.Position);
                    enemy.Render(spriteBatch);
                }
            }
        }

        public List<WanderingEnemy> GetActiveEnemies()
        {
            return enemyPool.Where(e => e.IsActive).ToList();
        }
    }

}
