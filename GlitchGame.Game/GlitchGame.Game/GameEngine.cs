using GlitchGame.GameMain.GameLogic;
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
        private RenderTarget2D _renderTarget;
        private GameLogicController _gameLogicController;

        public GameEngine()
        {
            _graphics = new GraphicsDeviceManager(this);
            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += Window_ClientSizeChanged;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        private void Window_ClientSizeChanged(object sender, EventArgs e)
        {
            _graphics.PreferredBackBufferWidth = Window.ClientBounds.Width;
            _graphics.PreferredBackBufferHeight = Window.ClientBounds.Height;
            _graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            IsFixedTimeStep = true;
            TargetElapsedTime = TimeSpan.FromMilliseconds(33);

            SystemBinaryData.SetIOPointer(2,0); //todo
            _systemMemory = new SystemMemory(
                new VideoMemory(
                    new PaletteGroup(
                        new Palette(0, 5, 9, 12),
                        new Palette(1, 4, 8, 12),
                        new Palette(8, 9, 10, 11),
                        new Palette(12, 13, 14, 15)),
                     new TileSet(
                         new Tile("00000000",
                                  "00001111",
                                  "00011111",
                                  "00111111",
                                  "01011122",
                                  "11111222",
                                  "11111222",
                                  "11111222"),
                         new Tile(SolidTile(2)),
                         new Tile(SolidTile(3)),
                         new Tile("11001100",
                                  "11001100",
                                  "00110011",
                                  "00110011",
                                  "11001100",
                                  "11001100",
                                  "00110011",
                                  "00110011"),                                  
                         new Tile(RandomTile()),
                         new Tile(RandomTile()),
                         new Tile(RandomTile()),
                         new Tile(RandomTile()))
                     ));

            for (int i = 0; i < _systemMemory.VideoMemory.Sprites.Length; i++)
            {
                _systemMemory.VideoMemory.Sprites.SetWritePointer(i);
                new Sprite(new SpriteTiles(
                                       new TileIndex(new ByteValue(0), Flip.Normal),
                                       new TileIndex(new ByteValue(0), Flip.FlipX),
                                       new TileIndex(new ByteValue(0), Flip.FlipY),
                                       new TileIndex(new ByteValue(0), Flip.FlipBoth)
                                   ), new ByteValue(128), new ByteValue((byte)(32*i)), new SpriteAttributes(new Value2(0), new Value2(0), new Value4(0)));
            }


            _systemMemory.VideoMemory.BgLayer.Palette = new Value2(1);
            _systemMemory.VideoMemory.BgLayer.TileMap.SetAll(3);

            _systemMemory.VideoMemory.BgLayer.TileMap.Set(0, 0, 4);
            _systemMemory.VideoMemory.BgLayer.TileMap.Set(1, 1, 4);
            _systemMemory.VideoMemory.BgLayer.TileMap.Set(2, 2, 4);
            _systemMemory.VideoMemory.BgLayer.TileMap.Set(3, 3, 4);

            //_systemMemory.VideoMemory.BgLayer.TileMap.Set(30, 0, 4);
            //_systemMemory.VideoMemory.BgLayer.TileMap.Set(30, 1, 4);
            //_systemMemory.VideoMemory.BgLayer.TileMap.Set(30, 2, 4);
            //_systemMemory.VideoMemory.BgLayer.TileMap.Set(31, 0, 4);
            //_systemMemory.VideoMemory.BgLayer.TileMap.Set(31, 1, 4);
            //_systemMemory.VideoMemory.BgLayer.TileMap.Set(31, 2, 4);



            _graphics.PreferredBackBufferWidth = Window.ClientBounds.Width;
            _graphics.PreferredBackBufferHeight = Window.ClientBounds.Height;
            _graphics.ApplyChanges();

            _gameLogicController = new GameLogicController(_systemMemory);
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

            _renderTarget = new RenderTarget2D(GraphicsDevice, SystemConstants.ScreenPixelWidth, SystemConstants.ScreenPixelHeight);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            var clock = _systemMemory.Clock;
            if (clock[1] < 255)
                clock[1]++;
            else
            {
                clock[0]++;
                clock[1] = 0;
            }
            _systemMemory.Clock = clock;

            _gameLogicController.UpdateFrame();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            GraphicsDevice.SetRenderTarget(_renderTarget);
            _renderEngine.RenderFrame(_systemMemory);

            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Clear(Color.Black);
            _renderEngine.SpriteBatch.Begin();
            _renderEngine.SpriteBatch.Draw(_renderTarget, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight),
                new Rectangle(0, 0, SystemConstants.ScreenPixelWidth, SystemConstants.ScreenPixelHeight), Color.White);
            _renderEngine.SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
