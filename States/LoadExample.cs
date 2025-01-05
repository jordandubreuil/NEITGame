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
using static System.Formats.Asn1.AsnWriter;
using System.Diagnostics;

namespace NEITGameEngine.States
{
    public class LoadExample : BaseGameState
    {
        SaveSystem _saveSystem;
        int _playerScore;
        SaveData _playerSave;
        private SpriteFont _font;
        //public Matrix Translation = new(Matrix.Identity);

        public override void LoadContent(ContentManager contentManager)
        {
            Translation = Matrix.Identity;
            _saveSystem = Globals.GlobalSaveSystem;
            _playerSave = _saveSystem.LoadGame();
            _playerScore = _playerSave.PlayerScore;
            _font = contentManager.Load<SpriteFont>("gameFont"); // Load your font


        }

        public override void UnloadContent(ContentManager contentManager)
        {
            contentManager.Unload();
        }

      
        public override void HandleInput(Microsoft.Xna.Framework.GameTime gameTime)
        {
            var _inputManager = new InputManager(new DevInputMapper());

            _inputManager.GetCommands(cmd => { 
            if (cmd is DevInputCommand.GameStart) 
                {
                SwitchState(new GamePlayState());
                }
            if (cmd is DevInputCommand.GameExit) 
                {
                    NotifyEvent(Events.GAME_QUIT);
                }
            if(cmd is DevInputCommand.Shoot)

                {
                    //call some audio
                    //_bulletInstance.Play();
                   
                }
            });
        }

        public override void Update(GameTime gameTime)
        {
            Debug.WriteLine(_playerScore);
            base.Update(gameTime);  
        }

        // Method to draw the score on the screen
        public override void Render(SpriteBatch spriteBatch)
        {
            string scoreText = $"Player Loaded Score: {_playerScore}\n    Press Enter to Start";
            spriteBatch.DrawString(_font, scoreText,new Vector2(Globals.ScreenSize.X/2 - 100, Globals.ScreenSize.Y / 2), Color.White);
        }


    }
}
