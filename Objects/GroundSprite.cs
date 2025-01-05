using NEITGameEngine.Objects.Base;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace NEITGameEngine.Objects
{
    public class GroundSprite
    {
        Texture2D _groundSprite;
        public Vector2 Origin { get; set; }
        public Vector2 Position { get; set; }

        public GroundSprite(Texture2D texture, Vector2 position)
        {
            _groundSprite = texture;
            Position = position;
            Origin = new(_groundSprite.Width/2, _groundSprite.Height/2);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_groundSprite,Position,null,Color.White, 0f, Origin, 1f, SpriteEffects.None, 0f);
        }

    }
}
