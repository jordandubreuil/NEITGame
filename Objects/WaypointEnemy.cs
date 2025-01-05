using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using NEITGameEngine.Objects.Base;
using NEITGameEngine.Enum;
using System.Diagnostics;

namespace NEITGameEngine.Objects
{
    public class WaypointEnemy:BaseGameObject
    {
        private Vector2 position;
        private List<Vector2> waypoints;
        private int currentWaypointIndex;
        private float speed;
        private BaseGameObject parent;
        private PlayerSprite player;

        public WaypointEnemy(List<Vector2> waypoints, float speed, Texture2D texture, BaseGameObject parent = null, PlayerSprite player = null)
        {
            this.position = waypoints[0];
            this.waypoints = waypoints;
            this.currentWaypointIndex = 1;
            this.speed = speed;
            this.parent = parent;
            this.player = player;
            _texture = texture;
        }

        public override void Update(GameTime gameTime)
        {
            if (waypoints.Count == 0) return;

            Vector2 targetWaypoint = waypoints[currentWaypointIndex];
            Vector2 direction = targetWaypoint - position;
            float distance = direction.Length();

            if (distance < 1f) // Close enough to target waypoint
            {
                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Count;
            }
            else
            {
                direction.Normalize();
                position += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            // Apply parent's position if there is a parent
            if (parent != null)
            {
                position += parent.Position;
            }

            if(player != null)
            {
               //Debug.WriteLine(this.BoxCollider);
               //Debug.WriteLine(player.BoxCollider);

                
                if (this.BoxCollider.Intersects(player.BoxCollider))
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
    }

}
