using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using NEITGameEngine.States.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEITGameEngine.Enum;
using Microsoft.Xna.Framework;
using NEITGameEngine.Inputs.Base;
using NEITGameEngine.Inputs.GamePlayState;
using NEITGameEngine.Objects;
using Microsoft.Xna.Framework.Graphics;
using NEITGameEngine.Animation;
using NEITGameEngine.World;
using NEITGameEngine;
using NEITGameEngine.Objects.Base;
using NEITGameEngine.ObjectPooling;
using System.Diagnostics;
using NEITGameEngine.Menus;
using System.Reflection.Metadata;

namespace NEITGameEngine.States
{
    public class GamePlayState:BaseGameState
    {
      
        SpriteManager _spriteManager;
        PlayerSprite _playerSprite;
        GroundMap _map;
        WanderingEnemy wanderingEnemy;
        WaypointEnemy waypointEnemy;
        List<Vector2> wayPoints;
        public List<BaseGameObject> enemies;
        SpawnFactory spawnFactory;
        bool _pauseKeyPreviouslyPressed = false;
        PauseMenu pauseMenu;
        ScoreMenu scoreMenu;
        public int Score { get; set; }


        public override void LoadContent(ContentManager contentManager)
        {
           
            //Translation = Matrix.Identity;
            _map = new GroundMap(contentManager);
            AddGameObject(_map);
            //Add Player
            _spriteManager = new SpriteManager();
            _playerSprite = new PlayerSprite(_spriteManager, _map._groundTiles[0,0], new(Globals.WindowSize.X/2,Globals.WindowSize.Y/2), 500f, new(0,-1), contentManager.Load<Texture2D>("bulletSprite"));
            _playerSprite.SetBounds(_map.MapSize, _map.TileSize);
            Texture2D playerSpriteSheetRun = contentManager.Load<Texture2D>("run");
            Texture2D playerSpriteSheetIdle = contentManager.Load<Texture2D>("Idle");
            _spriteManager.LoadAnimation("run", playerSpriteSheetRun, 48, 48, 8, 0.1f);
            _spriteManager.LoadAnimation("idle", playerSpriteSheetIdle, 48, 48, 10, 0.1f);
            //_spriteManager.PlayAnimation("run");
            AddGameObject(_playerSprite);

            //Add wandering enemy
            wanderingEnemy = new WanderingEnemy(new(200f, 200f), 100f, 1f, contentManager.Load<Texture2D>("Circle"), _map,_playerSprite);
            AddGameObject(wanderingEnemy);

            //Add WaypointEnemy
            //Adds WaypointEnemy
            wayPoints = new List<Vector2>();
            wayPoints.Add(new Vector2(0f, 0f));
            wayPoints.Add(new Vector2(Globals.ScreenSize.X - 100f, 0f));
            wayPoints.Add(new Vector2(Globals.ScreenSize.X - 100f, Globals.ScreenSize.Y - 100f));
            wayPoints.Add(new Vector2(0f, Globals.ScreenSize.Y - 100f));
            waypointEnemy = new WaypointEnemy(wayPoints, 100, contentManager.Load<Texture2D>("Diamond"), _map, _playerSprite);
            AddGameObject(waypointEnemy);

            //enemies.Add(waypointEnemy);
            //enemies.Add(wanderingEnemy);

            //Adding SpawnFactory with wandering Enemy
            spawnFactory = new SpawnFactory(contentManager.Load<Texture2D>("Circle"), 10, 3, new(500,500), false, _map, _playerSprite);
            AddGameObject(spawnFactory);

            SpriteFont font = contentManager.Load<SpriteFont>("gameFont"); // Load your font

            
            scoreMenu = new ScoreMenu(font, new(-Globals.ScreenSize.X/2 + 20, -Globals.ScreenSize.Y/2 + 20), _playerSprite);
            AddGameObject(scoreMenu);

            Vector2 menuPosition = new Vector2(-25, Globals.ScreenSize.Y/2);
            pauseMenu = new PauseMenu(font, menuPosition,this, scoreMenu, _playerSprite);
            AddGameObject(pauseMenu);
            
            //SpriteFont font = Content.Load<SpriteFont>("MenuFont"); // Load your font
            //Vector2 menuPosition = new Vector2(400, 300);
            //_pauseMenu = new PauseMenu(font, menuPosition);
        }

 
        public override void UnloadContent(ContentManager contentManager)
        {
            
        }

        private void CalculateTranslation()
        {
            var dx =  (Globals.WindowSize.X/2) - _playerSprite.Position.X;
            var dy =  (Globals.WindowSize.Y/2) - _playerSprite.Position.Y;
            Translation = Matrix.CreateTranslation(dx, dy, 0f);
        }

        //Old GamePlayState Input Code
        //public override void HandleInput()
        //{
        //    if (Keyboard.GetState().IsKeyDown(Keys.Escape))
        //    {
        //        //Notify Event System
        //        NotifyEvent(Events.GAME_QUIT);
        //    }

        //}

        public override void HandleInput(GameTime gameTime)
        {
            
            var _inputManager = new InputManager(new GamePlayInputMapper());
            
            _inputManager.GetCommands(cmd =>
            {
               
                if (cmd is GamePlayCommand.GameExit)
                {
                    NotifyEvent(Events.GAME_QUIT);
                }

                //Pause Command
                //if (cmd is GamePlayCommand.Pause)
                //{
                //    //Debug.WriteLine(Globals.Paused);
                //    NotifyEvent(Globals.Paused? Events.RESUMED:Events.PAUSED);
                //}

                // Handle the Pause Command
                if (Keyboard.GetState().IsKeyDown(Keys.P))
                //if (cmd is GamePlayCommand.Pause)
                {
                    // Check if the key was previously pressed
                    if (!_pauseKeyPreviouslyPressed)
                    {
                        
                        NotifyEvent(Globals.Paused ? Events.RESUMED : Events.PAUSED);
                        _pauseKeyPreviouslyPressed = true;
                    }
                    
                }
                else
                {
                    // Reset the flag when no Pause command is detected
                    _pauseKeyPreviouslyPressed = false;
                }

                if (Globals.Paused)
                {
                    return;
                }

                //All other inputs would go below
                if (cmd is GamePlayCommand.moveLeft)
                {
                    _playerSprite.MoveLeft();
                   
                }
                if (cmd is GamePlayCommand.moveRight)
                {
                    _playerSprite.MoveRight();
                   
                }

                

                if (cmd is GamePlayCommand.moveUp)
                {
                    _playerSprite.MoveUp();
                }

                if(cmd is GamePlayCommand.moveDown)
                {
                    _playerSprite.MoveDown();
                }

                if(cmd is GamePlayCommand.moving)
                {
                    _playerSprite.Moving();
                }

                if (cmd is GamePlayCommand.idle)
                {
                    _playerSprite.Idle();
                }
               
            });

            
        }
        public override void Update(GameTime gameTime) {
            
           
            if (Globals.Paused == true)
            {
                pauseMenu.HandleInput(gameTime);
                return;
            }
            _playerSprite.Update(gameTime);
            wanderingEnemy.Update(gameTime);
            waypointEnemy.Update(gameTime);
            spawnFactory.Update(gameTime);


            CheckCollisions(_playerSprite.shooting.GetActiveProjectiles(),spawnFactory.GetActiveEnemies());
            CalculateTranslation();
        
        }

        private void CheckCollisions(List<Projectile> projectiles, List<WanderingEnemy> enemies)
        {
            
            foreach (var projectile in projectiles)
            {
                if (!projectile.IsActive) continue;

                foreach (var enemy in enemies)
                {
                    //Debug.WriteLine($"{projectile.BoxCollider},{enemy.BoxCollider}");
                    if (!enemy.IsActive) continue;

                    if (projectile.BoxCollider.Intersects(enemy.BoxCollider))
                    {
                        //Debug.WriteLine("Hit");
                        // Handle collision: deactivate both objects
                        projectile.Deactivate();
                        enemy.Deactivate();
                        scoreMenu.AddScore(10);
                       
                        // Optional: Add points or trigger an explosion
                        //Debug.WriteLine("Collision detected!");
                        
                    }
                }
            }
        }

    }
}
