using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NEITGameEngine.Objects;
using NEITGameEngine.Objects.Base;

namespace NEITGameEngine.Menus
{
 
    public class ScoreMenu:BaseGameObject
    {
        private int _score; // The player's current score
        private Vector2 _position; // Position of the score on the screen
        private SpriteFont _font; // Font used to display the score
        private PlayerSprite _playerSprite;

        public ScoreMenu(SpriteFont font, Vector2 position, PlayerSprite playerSprite)
        {
            _score = 0;
            _font = font;
            _position = position;
            _playerSprite = playerSprite;
        }

        // Method to increase the score
        public void AddScore(int points)
        {
            _score += points;
        }

        // Method to reset the score
        public void ResetScore()
        {
            _score = 0;
        }

        public int GetScore()
        {
            return _score;
        }

        // Method to draw the score on the screen
        public override void Render(SpriteBatch spriteBatch)
        {
            string scoreText = $"Score: {_score}";
            spriteBatch.DrawString(_font, scoreText, _playerSprite.Position + _position, Color.White);
        }
    }
}
