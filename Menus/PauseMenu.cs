using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using NEITGameEngine.Objects.Base;
using NEITGameEngine.Enum;
using NEITGameEngine.States.Base;
using NEITGameEngine.SaveDataSystem;
using System.Collections.Generic;
using System.Diagnostics;
using NEITGameEngine;
using NEITGameEngine.Objects;



namespace NEITGameEngine.Menus
{
    public class PauseMenu:BaseGameObject
    {
        public enum PauseMenuOption
        {
            Resume,
            SaveGame,
            ExitGame
        }
        private PauseMenuOption _currentMenuOption = PauseMenuOption.Resume;
        private SpriteFont _font;
        private Vector2 _menuPosition;
        private bool _isKeyPressed = false;
        private BaseGameState _currentGameState;
        ScoreMenu _scoreMenu;
        PlayerSprite _playerSprite;

        public PauseMenu(SpriteFont font, Vector2 menuPosition, BaseGameState currentGameState, ScoreMenu scoreMenu, PlayerSprite playerSprite)
        {
            _font = font;
            _menuPosition = menuPosition;
            _currentGameState = currentGameState;
            _scoreMenu = scoreMenu; 
            _playerSprite = playerSprite;
        }

        public void HandleInput(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Up) && !_isKeyPressed)
            {
                _currentMenuOption = _currentMenuOption == PauseMenuOption.Resume
                    ? PauseMenuOption.ExitGame
                    : _currentMenuOption - 1;
                _isKeyPressed = true;
            }

            if (keyboardState.IsKeyDown(Keys.Down) && !_isKeyPressed)
            {
                _currentMenuOption = _currentMenuOption == PauseMenuOption.ExitGame
                    ? PauseMenuOption.Resume
                    : _currentMenuOption + 1;
                _isKeyPressed = true;
            }

            if (keyboardState.IsKeyUp(Keys.Up) && keyboardState.IsKeyUp(Keys.Down))
            {
                _isKeyPressed = false;
            }

            if (keyboardState.IsKeyDown(Keys.Enter))
            {
                ExecuteMenuOption();
            }
        }

        public override void Render(SpriteBatch spriteBatch)
        {
            if (!Globals.Paused) { return; }
            Vector2 adjustedPosition = _menuPosition - _playerSprite.Position;
            string[] menuOptions = { "Resume", "Save Game", "Exit Game" };
            int selectedIndex = (int)_currentMenuOption;
            Color titleColor = Color.White;
            Vector2 titlePosition = _playerSprite.Position + new Vector2(_menuPosition.X, -40);
            spriteBatch.DrawString(_font, "Paused", titlePosition, titleColor);
            for (int i = 0; i < menuOptions.Length; i++)
            {
                Color color = i == selectedIndex ? Color.Yellow : Color.White;
                Vector2 position = _playerSprite.Position + new Vector2(_menuPosition.X, i * 40);
                spriteBatch.DrawString(_font, menuOptions[i], position, color);
            }
        }

      

        private void ExecuteMenuOption()
        {
            switch (_currentMenuOption)
            {
                case PauseMenuOption.Resume:
                    Globals.Paused = false;
                    break;
                case PauseMenuOption.SaveGame:
                    SaveGame();
                    break;
                case PauseMenuOption.ExitGame:
                    ExitGame();
                    break;
            }
        }

        private void SaveGame()
        {
            // Add save game logic
            Console.WriteLine("Game Saved!");
            //Get the score and save it
            SaveData saveData = new SaveData
            {
                PlayerScore = _scoreMenu.GetScore(),
                PlayerPosition = new Vector2(200, 300),
                EnemyPositions = new List<Vector2>
                {
                    new Vector2(100, 150),
                    new Vector2(300, 400)
                }
            };
            Debug.WriteLine(saveData.PlayerScore);
            Globals.GlobalSaveSystem.SaveGame(saveData);
        }

        private void ExitGame()
        {
            // Add exit game logic
            Console.WriteLine("Game Exited!");
            // Example: Exit game using MonoGame Game class
            //Game.Exit();
            _currentGameState.NotifyEvent(Events.GAME_QUIT);
        }
    }
}
