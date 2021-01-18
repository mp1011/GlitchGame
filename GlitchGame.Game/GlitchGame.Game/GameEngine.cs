using GlitchGame.GameMain.Graphics;
using GlitchGame.GameMain.Memory;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Linq;

namespace GlitchGame.GameMain
{
    public class GameEngine : Game
    {
        private GraphicsDeviceManager _graphics;
        private RenderEngine _renderEngine;
        private SystemMemory _systemMemory;

        public GameEngine()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            _systemMemory = new SystemMemory(
                new VideoMemory(
                    new PaletteGroup(
                        new Palette(1, 5, 9, 12),
                        new Palette(4, 5, 6, 7),
                        new Palette(8, 9, 10, 11),
                        new Palette(12, 13, 14, 15)),
                     new TileSet(
                         new Tile(SolidTile(1)),
                         new Tile(SolidTile(2)),
                         new Tile(RandomTile()),
                         new Tile(RandomTile()),
                         new Tile(RandomTile()),
                         new Tile(RandomTile()),
                         new Tile(RandomTile()),
                         new Tile(RandomTile())),
                     new Sprite(0,0,1,2,3)));
        }

        private Random _rng = new Random(100);
        private byte[] RandomTile()
        {
            var b = new byte[16];
            _rng.NextBytes(b);
            return b;
        }

        private byte[] SolidTile(byte color)
        {
            var binary = string.Join("",
                Enumerable.Range(0, 64)
                .Select(c => Convert.ToString(color, 2).PadLeft(2, '0')).ToArray());

            byte[] bytes = new byte[16];
            for(int i = 0; i < 16; i++)
            {
                bytes[i] = Convert.ToByte(binary.Substring(8 * i, 8), 2);
            }

            return bytes;
        }

        protected override void LoadContent()
        {
            _renderEngine = new RenderEngine(
                spriteBatch: new SpriteBatch(GraphicsDevice),
                systemPalette: Content.Load<Texture2D>("palette"));
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _renderEngine.RenderFrame(_systemMemory.VideoMemory);
            base.Draw(gameTime);
        }
    }
}
