using GlitchGame.GameMain.Extensions;
using GlitchGame.GameMain.Memory;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GlitchGame.GameMain.Graphics
{
    public class RenderEngine
    {
        private readonly SpriteBatch _spriteBatch;
        private readonly Texture2D _systemPalette;
        private readonly RenderGun _renderGun;
        private VideoMemory _videoMemory;
        Sprite[] _scanlineSprites = new Sprite[8];

        public RenderEngine(SpriteBatch spriteBatch, Texture2D systemPalette)
        {
            _spriteBatch = spriteBatch;
            _systemPalette = systemPalette;
            _renderGun = new RenderGun();
        }

        public void RenderFrame(VideoMemory videoMemory)
        {
            _videoMemory = videoMemory;
            _spriteBatch.Begin();

            do
            {
                if (_renderGun.X == 0)
                    GetSpritesOnCurrentScanline();

                DrawNextPixel();
                _renderGun.Move();
            } while (_renderGun.Y != 0 || _renderGun.X != 0);

            _spriteBatch.End();
        }

        private void GetSpritesOnCurrentScanline()
        {
            var s = _videoMemory.Sprite;
            if(_renderGun.Y >= s.Y && _renderGun.Y < s.Y + s.Height)
                _scanlineSprites[0] = _videoMemory.Sprite;
            else
                _scanlineSprites[0] = null;
        }

        private void DrawNextPixel()
        {
            _renderGun.Palette = _videoMemory.BgLayer.Palette;
            _renderGun.Color = _videoMemory.BgLayer.GetColorAtScreenPoint(_videoMemory.Tiles, _renderGun.X, _renderGun.Y);

            for (int i = 0; i < 8; i++)
            {
                if (_scanlineSprites[i] != null && _scanlineSprites[i].HitTest(_renderGun.X, _renderGun.Y))
                {
                    var color = _scanlineSprites[i].GetColorAtScreenPoint(_videoMemory.Tiles, _renderGun.X, _renderGun.Y);
                    if (color.Value > 0)
                    {
                        _renderGun.Palette = _scanlineSprites[i].Palette;
                        _renderGun.Color = color;
                        break;
                    }
                }
            }

            var srcPixel = GetNextSourcePixel();
            var screenPixel = GetNextScreenPixel();
            _spriteBatch.Draw(_systemPalette, screenPixel, srcPixel, Color.White);
        }

        private Rectangle GetNextSourcePixel()
        {
            var color =_videoMemory.Palettes
                .Get(_renderGun.Palette)
                .Get(_renderGun.Color);

            if (color.Value == 0)
                return Rectangle.Empty;

            var srcPoint = color.Value.IndexToPoint(16);
            return srcPoint.ToRectangle(32);
        }

        private Rectangle GetNextScreenPixel()
        {
            return new Point(_renderGun.X, _renderGun.Y)
                .ToRectangle(4); //todo, scale to screen size
        }
    }

    public class RenderGun
    {
        public byte X { get; private set; }
        public byte Y { get; private set; }

        public Value4 Palette { get; set; }

        public Value4 Color { get; set; }

        public void Move()
        {
            if (X < 255)
                X++;
            else if(Y < 255)
            {
                X = 0;
                Y++;
            }
            else
            {
                X = 0;
                Y = 0;
            }
        }
    }
}
