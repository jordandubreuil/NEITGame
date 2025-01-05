using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace NEITGameEngine.Editor
{
    public class Editor : MainGame
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // Basic Components
        private Texture2D _tileset;
        private List<Rectangle> _tileRectangles;
        private int _tileSize = 32;

        private int[,] _grid;
        private int _gridWidth = 20;
        private int _gridHeight = 15;

        private Point _selectedTile = new Point(-1, -1);

        public Editor()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // Initialize grid
            _grid = new int[_gridWidth, _gridHeight];

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load tileset
            _tileset = Content.Load<Texture2D>("tileset");
            GenerateTileRectangles();
        }

        private void GenerateTileRectangles()
        {
            _tileRectangles = new List<Rectangle>();
            for (int y = 0; y < _tileset.Height / _tileSize; y++)
            {
                for (int x = 0; x < _tileset.Width / _tileSize; x++)
                {
                    _tileRectangles.Add(new Rectangle(x * _tileSize, y * _tileSize, _tileSize, _tileSize));
                }
            }
        }

        protected override void Update(GameTime gameTime)
        {
            var mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                PlaceTile(mouseState);
            }

            base.Update(gameTime);
        }

        private void PlaceTile(MouseState mouseState)
        {
            int x = mouseState.X / _tileSize;
            int y = mouseState.Y / _tileSize;

            if (x >= 0 && x < _gridWidth && y >= 0 && y < _gridHeight && _selectedTile.X != -1)
            {
                _grid[x, y] = _selectedTile.Y * (_tileset.Width / _tileSize) + _selectedTile.X;
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            // Draw grid
            for (int y = 0; y < _gridHeight; y++)
            {
                for (int x = 0; x < _gridWidth; x++)
                {
                    int tileIndex = _grid[x, y];
                    if (tileIndex != 0)
                    {
                        var sourceRectangle = _tileRectangles[tileIndex];
                        _spriteBatch.Draw(_tileset, new Rectangle(x * _tileSize, y * _tileSize, _tileSize, _tileSize), sourceRectangle, Color.White);
                    }
                }
            }

            // Draw tile palette (for simplicity, at the bottom of the screen)
            for (int i = 0; i < _tileRectangles.Count; i++)
            {
                var sourceRectangle = _tileRectangles[i];
                _spriteBatch.Draw(_tileset, new Rectangle(i * _tileSize, _gridHeight * _tileSize, _tileSize, _tileSize), sourceRectangle, Color.White);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

