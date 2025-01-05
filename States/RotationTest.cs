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
using NEITGameEngine.World;
using System.Reflection.Metadata;


namespace NEITGameEngine.States
{
    public class RotationTest : BaseGameState
    {

        //SoundManager _soundManager;
        SpriteManager _spriteManager;
        PlayerSprite _playerSprite;
        //public Matrix Translation = new(Matrix.Identity);
        Turret turret;
        public ScrollingBackground myBackground;

        public override void LoadContent(ContentManager contentManager)
        {
            Translation = Matrix.Identity;
            myBackground = new ScrollingBackground(contentManager.Load<Texture2D>("groundtile"));
            AddGameObject(myBackground);
            turret = new Turret(contentManager.Load<Texture2D>("arrow"));
            turret.Position = new Vector2(30f,12f);
            //turret = new Turret(contentManager.Load<Texture2D>("arrow"));
            //AddGameObject(new Turret(contentManager.Load<Texture2D>("arrow")));
            AddGameObject(turret);
            
            //Translation = Matrix.Identity;
            //AddGameObject(new TestSprite(contentManager.Load<Texture2D>("Diamond")));
            //BGM = contentManager.Load<SoundEffect>("FutureAmbient_2");
            //bullet = contentManager.Load<SoundEffect>("bullet");
            //_bgmInstance = BGM.CreateInstance();
            //_bulletInstance = bullet.CreateInstance();
            //_bgmInstance.IsLooped = true;
            //_bgmInstance.Play();


            //BGM2 = contentManager.Load<Song>("FutureAmbient_1");
            //MediaPlayer.Play(BGM2);
            //MediaPlayer.IsRepeating = true;
            //_soundManager = new SoundManager();
            //_soundManager.LoadSoundEffect("BGM", BGM);
            //_soundManager.CreateSoundEffectInstance("BGM");
            // _soundManager.PlaySoundEffectInstance("BGM");
            //_soundManager.LoadSoundEffect("bullet", bullet);
            //_soundManager.CreateSoundEffectInstance("bullet");
            _spriteManager = new SpriteManager();
            _playerSprite = new PlayerSprite(_spriteManager,null, new(100f,100f));
            Texture2D playerSpriteSheetRun = contentManager.Load<Texture2D>("idle");
            _spriteManager.LoadAnimation("idle", playerSpriteSheetRun, 48, 48, 8, 0.1f);
            _spriteManager.PlayAnimation("idle");
            AddGameObject(_playerSprite);
        }

        public override void UnloadContent(ContentManager contentManager)
        {
            contentManager.Unload();
        }

        //public override void Update(GameTime gameTime)
        //{
        //    myBackground.Update(gameTime);
            

        //}

        //public override void HandleInput()
        //{
        //    if (Keyboard.GetState().IsKeyDown(Keys.Enter))
        //    {
        //        SwitchState(new GamePlayState());
        //    }

        //}
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
                    //_soundManager.PlaySoundEffectInstance("bullet");
                }
            });
        }

        //protected override void SetInputManager()
        //{

        //}

        public override void Update(GameTime gameTime)
        {
            _playerSprite.Update(gameTime);
        }
    }
}
