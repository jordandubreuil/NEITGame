using NEITGameEngine.Objects;
using Microsoft.Xna.Framework.Graphics;
using NEITGameEngine.States.Base;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using NEITGameEngine.Animation;
using NEITGameEngine.Inputs.Base;
using NEITGameEngine.Inputs.GamePlayState;
using NEITGameEngine.Enum;
using System.Collections.Generic;

namespace NEITGameEngine.States
{

    public class EnemyTestScene : BaseGameState
    {
        PatrolEnemy enemy;
        ChaseEnemy chaseEnemy;
        WaypointEnemy waypointEnemy;
        WanderingEnemy wanderingEnemy;
        SpriteManager spriteManager;
        PlayerSprite playerSprite;
        List<Vector2> wayPoints;
        public override void LoadContent(ContentManager contentManager)
        {
            Translation = Matrix.Identity;
            //Adds Patrol Enemy
            enemy = new PatrolEnemy(new(Globals.ScreenSize.X/2, Globals.ScreenSize.Y/2),100,100,100,contentManager.Load<Texture2D>("Diamond"));
            //enemy.Position = new Vector2(300f,300f);
            AddGameObject(enemy);

            //Adds Chase Enemy
            chaseEnemy = new ChaseEnemy(new(200.0f, 200.0f), 100f, 250f, contentManager.Load<Texture2D>("Circle"));
            AddGameObject(chaseEnemy);

            //Adds WaypointEnemy
            wayPoints = new List<Vector2>();
            wayPoints.Add(new Vector2(0f, 0f));
            wayPoints.Add(new Vector2(Globals.ScreenSize.X - 100f, 0f));
            wayPoints.Add(new Vector2(Globals.ScreenSize.X - 100f, Globals.ScreenSize.Y - 100f));
            wayPoints.Add(new Vector2(0f, Globals.ScreenSize.Y - 100f));
            waypointEnemy = new WaypointEnemy(wayPoints, 100, contentManager.Load<Texture2D>("Diamond"));
            AddGameObject(waypointEnemy);

            //Add Wandering Enemy
            wanderingEnemy = new WanderingEnemy(new(600f, 100f), 100, 1, contentManager.Load<Texture2D>("Circle"));
            AddGameObject(wanderingEnemy);

            //Adds Player
            spriteManager = new SpriteManager();
            playerSprite = new PlayerSprite(spriteManager, null, new(Globals.ScreenSize.X/2,Globals.ScreenSize.Y/2));
            Texture2D playerSpriteSheetRun = contentManager.Load<Texture2D>("run");
            Texture2D playerSpriteSheetIdle = contentManager.Load<Texture2D>("Idle");
            spriteManager.LoadAnimation("run", playerSpriteSheetRun, 48, 48, 8, 0.1f);
            spriteManager.LoadAnimation("idle", playerSpriteSheetIdle, 48, 48, 10, 0.1f);
            AddGameObject(playerSprite);

        }

        public override void UnloadContent(ContentManager contentManager)
        {
           // throw new System.NotImplementedException();
        }

        public override void HandleInput(GameTime gameTime)
        {
            // throw new System.NotImplementedException();
            var _inputManager = new InputManager(new GamePlayInputMapper());

            _inputManager.GetCommands(cmd =>
            {

                if (cmd is GamePlayCommand.GameExit)
                {
                    NotifyEvent(Events.GAME_QUIT);
                }

                //All other inputs would go below
                if (cmd is GamePlayCommand.moveLeft)
                {
                    playerSprite.MoveLeft();

                }
                if (cmd is GamePlayCommand.moveRight)
                {
                    playerSprite.MoveRight();

                }



                if (cmd is GamePlayCommand.moveUp)
                {
                    playerSprite.MoveUp();
                }

                if (cmd is GamePlayCommand.moveDown)
                {
                    playerSprite.MoveDown();
                }

                if (cmd is GamePlayCommand.moving)
                {
                    playerSprite.Moving();
                }

                if (cmd is GamePlayCommand.idle)
                {
                    playerSprite.Idle();
                }
            });
        }

        public override void Update(GameTime gameTime)
        {
            enemy.Update(gameTime);
            chaseEnemy.Update(gameTime, playerSprite.Position);
            waypointEnemy.Update(gameTime);
            wanderingEnemy.Update(gameTime);
            playerSprite.Update(gameTime);
        }


    }
}
