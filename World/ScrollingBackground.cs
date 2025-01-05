using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NEITGameEngine.Objects;
using NEITGameEngine.Objects.Base;
using System.Diagnostics;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace NEITGameEngine.World
{
    public class ScrollingBackground:BaseGameObject
    {
        float scrollingspeed = 2.0f;
        float yPos;

        public ScrollingBackground(Texture2D texture)
        {
            _texture = texture;
            Position = new Vector2(0, 0);
            
        }

        public override void Render(SpriteBatch spriteBatch)
        {
            var viewport = spriteBatch.GraphicsDevice.Viewport;

            var sourceRectangle = new Rectangle(0, 0, _texture.Width, _texture.Height);

            // tile the textures to fill the screen. Add an extra row of tiles above the viewport so they can scroll into view and not leave a gap
            for (int nbVertical = -1; nbVertical < viewport.Height / _texture.Height + 1; nbVertical++)
            {
                var y = (int)Position.Y + nbVertical * _texture.Height;
                for (int nbHorizontal = 0; nbHorizontal < viewport.Width / _texture.Width + 1; nbHorizontal++)
                {
                    var x = (int)Position.X + nbHorizontal * _texture.Width;
                    var destinationRectangle = new Rectangle(x, y, _texture.Width, _texture.Height);
                    spriteBatch.Draw(_texture, destinationRectangle, sourceRectangle, Color.White);
                }
            }
            
            yPos  += scrollingspeed;
            Position = new(0,(int)(yPos) % _texture.Height);
            Debug.WriteLine(yPos);
        }
    }
}
