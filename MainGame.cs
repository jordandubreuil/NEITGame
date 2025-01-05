//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Input;
//using NEITGameEngine.States;
//using NEITGameEngine.States.Base;
//using NEITGameEngine;
//using System.Diagnostics;
//using NEITGameEngine.SaveDataSystem;
//using System;

//namespace NEITGameEngine
//{
//    public class MainGame : Game
//    {
//        private BaseGameState _currentGameState;

//        private GraphicsDeviceManager _graphics;
//        private SpriteBatch _spriteBatch;
//        public SaveSystem _saveSystem;
//        private bool paused = false;

//        private Matrix _scaleMatrix;
//        private int _virtualWidth = 800; // Your desired internal resolution width
//        private int _virtualHeight = 480; // Your desired internal resolution height
//        private int _screenWidth;
//        private int _screenHeight;


//        public MainGame()
//        {
//            _graphics = new GraphicsDeviceManager(this);
//            Globals.Graphics = _graphics; // Assign graphics to Globals
//            Content.RootDirectory = "Content";
//            IsMouseVisible = true;
//        }

        
//        private void RecalculateScale()
//        {
//            _screenWidth = GraphicsDevice.Viewport.Width;
//            _screenHeight = GraphicsDevice.Viewport.Height;

//            float scaleX = (float)_screenWidth / _virtualWidth;
//            float scaleY = (float)_screenHeight / _virtualHeight;
//            float scale = Math.Min(scaleX, scaleY); // Uniform scaling

//            _scaleMatrix = Matrix.CreateScale(new Vector3(scale, scale, 1));
//        }

//        protected override void Initialize()
//        {
//            // TODO: Add your initialization logic here
//            Globals.WindowSize = new Point(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
//            Globals.ScreenSize = new Point(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
//            _saveSystem = new SaveSystem("gamesave.json");
//            Globals.GlobalSaveSystem = _saveSystem;

//            //_graphics.PreferredBackBufferWidth = 1920; // Width
//            //_graphics.PreferredBackBufferHeight = 1080; // Height
//            //_graphics.IsFullScreen = true;
//            //_graphics.ApplyChanges();

//            //RecalculateScale();
//            base.Initialize();
//        }

//        protected override void LoadContent()
//        {
//            _spriteBatch = new SpriteBatch(GraphicsDevice);

//            // TODO: use this.Content to load your game content here
//            //SwitchGameState(new EnemyTestScene());
//            //SwitchGameState(new GamePlayState());
//            //SwitchGameState(new LoadExample());
//            SwitchGameState(new MainMenu());
//            //SwitchGameState(new RotationTest());
//        }

//        private void CurrentGameState_OnStateSwitched(object sender, BaseGameState e) {
//            SwitchGameState(e);
//        }

//        private void SwitchGameState(BaseGameState gameState)
//        {
//            if (_currentGameState != null)
//            {
//                // Unload/Switch state
//                _currentGameState.OnStateSwitched -= CurrentGameState_OnStateSwitched;
//                _currentGameState.OnEventNotification -= _currentGameState_OnEventNotification;
//                _currentGameState.UnloadContent(Content);
//            }

//            _currentGameState = gameState;
//            _currentGameState.LoadContent(Content);
//            _currentGameState.OnStateSwitched += CurrentGameState_OnStateSwitched;
//            _currentGameState.OnEventNotification += _currentGameState_OnEventNotification;

//        }

//        private void _currentGameState_OnEventNotification(object sender, Enum.Events e)
//        {
//            switch (e)
//            {
//                case Enum.Events.GAME_QUIT:
//                    Exit();
//                    break;
//                case Enum.Events.PAUSED:
//                    Debug.WriteLine("paused");
//                    Globals.Paused = true;
//                    break;
//                case Enum.Events.RESUMED:
//                    Debug.WriteLine("unpaused");
//                    Globals.Paused = false;
//                    break;
//            }
//        }

//        protected override void UnloadContent()
//        {
//            _currentGameState?.UnloadContent(Content);
//        }

//        protected override void Update(GameTime gameTime)
//        {
            
//            // TODO: Add your update logic here
//            _currentGameState.HandleInput(gameTime);
//            //if (Globals.Paused == true)
//            //{
//            //    return;
//            //}
//            _currentGameState.Update(gameTime);
            
//            base.Update(gameTime);
//        }

//        protected override void Draw(GameTime gameTime)
//        {
//            GraphicsDevice.Clear(Color.Gray);

//            // TODO: Add your drawing code here
//            _spriteBatch.Begin(transformMatrix: _currentGameState.Translation);
//            _currentGameState.Render(_spriteBatch);
//            _spriteBatch.End();

//            base.Draw(gameTime);
//        }
//    }
//}
