using NEITGameEngine.States.Base;
using NEITGameEngine.Objects;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using NEITGameEngine.Inputs;
using NEITGameEngine.Inputs.Base;
using NEITGameEngine.Inputs.DevState;
using NEITGameEngine.Enum;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using NEITGameEngine.Sound;
using NEITGameEngine.Animation;
using Microsoft.Xna.Framework;
using NEITGameEngine.SaveDataSystem;
using System.Diagnostics;

namespace NEITGameEngine.States
{
    public class MainMenu : BaseGameState
    {
        private SpriteFont _font;
        private int _selectedIndex;
        private string[] _menuItems = { "Play Game", "Options", "Exit" };
        private InputManager _inputManager;
        private SaveSystem _saveSystem;
        private int _playerScore;
        private SaveData _playerSave;
        private KeyboardState _previousKeyboardState;
        private TimeSpan _keyPressDelay = TimeSpan.FromMilliseconds(150);
        private TimeSpan _elapsedTime;


        public override void LoadContent(ContentManager contentManager)
        {
            Translation = Matrix.Identity;
            _font = contentManager.Load<SpriteFont>("gameFont");
            _saveSystem = Globals.GlobalSaveSystem;
            _playerSave = _saveSystem.LoadGame();
            _playerScore = _playerSave.PlayerScore;
            _selectedIndex = 0;
            _inputManager = new InputManager(new DevInputMapper());
        }

        public override void UnloadContent(ContentManager contentManager)
        {
            contentManager.Unload();
        }

        public override void HandleInput(GameTime gameTime)
        {
            // Get current keyboard state
            KeyboardState currentKeyboardState = Keyboard.GetState();
            _elapsedTime += gameTime.ElapsedGameTime;

            // Check for up/down key presses with debounce
            if (_elapsedTime >= _keyPressDelay)
            {
                if (currentKeyboardState.IsKeyDown(Keys.Up) && !_previousKeyboardState.IsKeyDown(Keys.Up))
                {
                    _selectedIndex--;
                    if (_selectedIndex < 0) _selectedIndex = _menuItems.Length - 1;
                    _elapsedTime = TimeSpan.Zero; // Reset timer
                }

                if (currentKeyboardState.IsKeyDown(Keys.Down) && !_previousKeyboardState.IsKeyDown(Keys.Down))
                {
                    _selectedIndex++;
                    if (_selectedIndex >= _menuItems.Length) _selectedIndex = 0;
                    _elapsedTime = TimeSpan.Zero; // Reset timer
                }

                if (currentKeyboardState.IsKeyDown(Keys.Enter) && !_previousKeyboardState.IsKeyDown(Keys.Enter))
                {
                    switch (_selectedIndex)
                    {
                        case 0: // Play Game
                            SwitchState(new GamePlayState());
                            break;
                        case 1: // Options
                            SwitchState(new OptionsMenuState());
                            break;
                        case 2: // Exit
                            NotifyEvent(Events.GAME_QUIT);
                            break;
                    }
                    _elapsedTime = TimeSpan.Zero; // Reset timer
                }
            }

            // Save the current keyboard state for the next frame
            _previousKeyboardState = currentKeyboardState;
        }


        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Render(SpriteBatch spriteBatch)
        {
            string title = "Main Menu";
            Vector2 titlePosition = new Vector2(Globals.ScreenSize.X / 2 - 100, 100);
            spriteBatch.DrawString(_font, title, titlePosition, Color.White);

            for (int i = 0; i < _menuItems.Length; i++)
            {
                Color color = i == _selectedIndex ? Color.Yellow : Color.White;
                Vector2 position = new Vector2(Globals.ScreenSize.X / 2 - 100, 200 + i * 50);
                spriteBatch.DrawString(_font, _menuItems[i], position, color);
            }
        }
    }

    public class OptionsMenuState : BaseGameState
    {
        private SpriteFont _font;
        private bool _isFullscreen;
        private InputManager _inputManager;
        private KeyboardState _previousKeyboardState;
        private TimeSpan _keyPressDelay = TimeSpan.FromMilliseconds(150);
        private TimeSpan _elapsedTime;


        public override void LoadContent(ContentManager contentManager)
        {
            Translation = Matrix.Identity;
            _font = contentManager.Load<SpriteFont>("gameFont");
            _isFullscreen = Globals.Graphics.IsFullScreen;
            _inputManager = new InputManager(new DevInputMapper());
        }

        public override void UnloadContent(ContentManager contentManager)
        {
            contentManager.Unload();
        }

        public override void HandleInput(GameTime gameTime)
        {
            KeyboardState currentKeyboardState = Keyboard.GetState();
            _elapsedTime += gameTime.ElapsedGameTime;

            if (_elapsedTime >= _keyPressDelay)

            {
                if (currentKeyboardState.IsKeyDown(Keys.Enter) && !_previousKeyboardState.IsKeyDown(Keys.Enter))
                {
                    _isFullscreen = !_isFullscreen;
                    Globals.Graphics.IsFullScreen = _isFullscreen;
                    Globals.Graphics.ApplyChanges();
                    _elapsedTime = TimeSpan.Zero;
                }

                if (currentKeyboardState.IsKeyDown(Keys.Back) && !_previousKeyboardState.IsKeyDown(Keys.Back))
                {
                    SwitchState(new MainMenu());
                    _elapsedTime = TimeSpan.Zero;
                }
            }

            _previousKeyboardState = currentKeyboardState;
        }


        public override void Render(SpriteBatch spriteBatch)
        {
            string optionsText = $"Options Menu\nFullscreen: {_isFullscreen}\nPress Enter to Toggle\nPress Backspace to Return";
            spriteBatch.DrawString(_font, optionsText, new Vector2(Globals.ScreenSize.X / 2 - 200, Globals.ScreenSize.Y / 2), Color.White);
        }
    }
}
