using NEITGameEngine.Objects.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NEITGameEngine.Animation;
using NEITGameEngine.Objects;
using System.Diagnostics;
using NEITGameEngine.Components;

namespace NEITGameEngine.Objects
{
    public class PlayerSprite:BaseGameObject
    {
        private float speed = 5.0f;
        private SpriteManager _spriteManager;
        private GroundSprite _backgroundOrigin;
        public bool IsMoving { get; set; }


        private bool _facingLeft; //Tracks player direction left or right
        Vector2 _minBounds, _maxBounds;

        public Shooting shooting;
        Vector2 movementDirection =  new(1,0);
        float projectileRotation;
        private int poolSize = 4;

        public PlayerSprite(SpriteManager spriteManager, GroundSprite groundSprite, Vector2 playerPos, float? projectileSpeed = null, Vector2? projectileDir = null, Texture2D projectileSprite = null)
        {
            _spriteManager = spriteManager;
            _backgroundOrigin = groundSprite;
            //var testWidth = new Vector2(_spriteManager._currentAnimation._frameWidth / 2, _spriteManager._currentAnimation._frameWidth / 2);
            Position = playerPos;//new Vector2((int)playerPos.X-_spriteManager._currentAnimation._frameWidth/2, (int)playerPos.Y -_spriteManager._currentAnimation._frameWidth/2);
            if(projectileSpeed.HasValue && projectileDir.HasValue)
            {
                shooting = new Shooting(projectileSprite, projectileSpeed.Value, projectileDir.Value, poolSize);
            }
            
        }

        public override Rectangle BoxCollider
        {
            get
            {
                if (_spriteManager != null)
                {
                    return new Rectangle((int)Position.X, (int)Position.Y, _spriteManager._currentAnimation._frameWidth, _spriteManager._currentAnimation._frameHeight);
                }
                return Rectangle.Empty;
            }
        }

        public void MoveLeft()
        {
            Position = new Vector2(Position.X - speed, Position.Y);
            Position = Vector2.Clamp(Position, _minBounds, _maxBounds);
            movementDirection = new Vector2(-1,0);
            projectileRotation = 180;
            _facingLeft = true;
            
        }

        

        public void MoveRight() {
            Position = new Vector2(Position.X + speed, Position.Y);
            Position = Vector2.Clamp(Position, _minBounds, _maxBounds);
            movementDirection = new Vector2(1,0);
            projectileRotation = 0;
            _facingLeft = false;

           
        }
        public void MoveUp()
        {
            Position = new Vector2(Position.X, Position.Y - speed);
            Position = Vector2.Clamp(Position, _minBounds, _maxBounds);
            movementDirection = new Vector2(0,-1);
            projectileRotation = 270;
        }

        public void MoveDown()
        {
            Position = new Vector2(Position.X, Position.Y + speed);
            Position = Vector2.Clamp(Position, _minBounds, _maxBounds);
            movementDirection = new Vector2(0,1);
            projectileRotation = 90;
        }

        public void Moving()
        {
            IsMoving = true;
        }

        public void Idle()
        {
            IsMoving = false;
        }

        public void SetBounds(Point mapSize, Point tileSize)
        {
            _minBounds = new((-tileSize.X) + _backgroundOrigin.Origin.X, (-tileSize.Y) + _backgroundOrigin.Origin.Y);
            _maxBounds = new(mapSize.X - (tileSize.X/2) - _backgroundOrigin.Origin.X,mapSize.Y - (tileSize.X/2) - _backgroundOrigin.Origin.Y);
            
        }

        public override void Update(GameTime gameTime)
        {
            if (IsMoving)
            {
               _spriteManager.PlayAnimation("run");
            }
            else
            {
                _spriteManager.PlayAnimation("idle");
            }
            _spriteManager.Update(gameTime);
            shooting.Update(gameTime, new Vector2(Position.X + _spriteManager._currentAnimation._frameWidth / 2, Position.Y + _spriteManager._currentAnimation._frameHeight / 2), movementDirection,projectileRotation, _minBounds, _maxBounds);
        }

        public void FireProjectile()
        {
            //Debug.WriteLine("This one is working");
            shooting.FireProjectile(new Vector2(Position.X + _spriteManager._currentAnimation._frameWidth/2, Position.Y + _spriteManager._currentAnimation._frameHeight/2), movementDirection, projectileRotation, _minBounds,_maxBounds);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            
            SpriteEffects flipEffect = _facingLeft ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            _spriteManager.Draw(spriteBatch, Position);
        }

        public override void Render(SpriteBatch spriteBatch)
        {
            //Debug.WriteLine(_facingLeft);
            SpriteEffects flipEffect = _facingLeft ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            _spriteManager.Draw(spriteBatch, Position, flipEffect);
            shooting.Draw(spriteBatch);
        }
    }
}
