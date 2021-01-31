using GlitchGame.GameMain.Extensions;
using GlitchGame.GameMain.Memory;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;

namespace GlitchGame.GameMain.Graphics
{
    public class RenderEngine
    {
        private Stopwatch _stopwatch = new Stopwatch();
        private long _lastPixelDrawTicks;

        public SpriteBatch SpriteBatch { get; }
        private readonly Texture2D _systemPalette;
        private readonly RenderGun _renderGun;
        private SystemMemory _systemMemory;
        Sprite[] _scanlineSprites = new Sprite[4];


        public RenderEngine(SpriteBatch spriteBatch, Texture2D systemPalette)
        {
            SpriteBatch = spriteBatch;
            _systemPalette = systemPalette;
            _renderGun = new RenderGun();
        }

        public void RenderFrame(SystemMemory systemMemory)
        {
            _systemMemory = systemMemory;
            SpriteBatch.Begin();

            do
            {
                if (_renderGun.X == 0)
                    GetSpritesOnCurrentScanline();

                DrawNextPixel();
                _renderGun.Move();
            } while (_renderGun.Y != 0 || _renderGun.X != 0);

            SpriteBatch.End();
        }

        private void GetSpritesOnCurrentScanline()
        {
            int scanlineSpriteIndex = 0;
            for (int i = 0; i < _systemMemory.VideoMemory.Sprites.Length; i++)
            {
                var sprite = _systemMemory.VideoMemory.Sprites.Get(i);
                if (sprite != null && _renderGun.Y >= sprite.Y && _renderGun.Y < sprite.Y + sprite.Height)
                {
                    _scanlineSprites[scanlineSpriteIndex] = sprite;
                    scanlineSpriteIndex++;
                    if (scanlineSpriteIndex >= _scanlineSprites.Length)
                        break;
                }
            }

            while (scanlineSpriteIndex < _scanlineSprites.Length)
                _scanlineSprites[scanlineSpriteIndex++] = null;

        }

        private void DrawNextPixel()
        {
            var renderGunX = _renderGun.X;
            var renderGunY = _renderGun.Y;

            var bgColor = _systemMemory.VideoMemory.BgLayer.GetColorAtScreenPoint(_systemMemory.VideoMemory.Tiles, renderGunX, renderGunY);
            _renderGun.ColorAddress = _systemMemory.VideoMemory.Palettes.GetColorAddress(_systemMemory.VideoMemory.BgLayer.Palette, bgColor);

            for (int i = 0; i < _scanlineSprites.Length; i++)
            {
                if (_scanlineSprites[i] != null && _scanlineSprites[i].HitTest(renderGunX, renderGunY))
                {
                    var color = _scanlineSprites[i].GetColorAtScreenPoint(_systemMemory.VideoMemory.Tiles, renderGunX, renderGunY);
                    if (color > 0)
                    {
                        _renderGun.ColorAddress = _systemMemory.VideoMemory.Palettes.GetColorAddress(_scanlineSprites[i].Attributes.Palette, color);
                        break;
                    }
                }
            }

            var srcPixel = GetNextSourcePixel();
            var screenPixel = GetNextScreenPixel();
            SpriteBatch.Draw(_systemPalette, screenPixel, srcPixel, Color.White);         
        }

        private Rectangle GetNextSourcePixel()
        {
            var color = SystemBinaryData.ReadAt(_renderGun.ColorAddress, 6);

            if (color == 0)
                return Rectangle.Empty;

            var srcPoint = color.IndexToPoint(16);
            return srcPoint.ToRectangle(32);
        }

        private Rectangle GetNextScreenPixel()
        {
            return new Point(_renderGun.X, _renderGun.Y)
                .ToRectangle(1); 
        }
    }

    public class RenderGun
    {
        public byte X { get; private set; }
        public byte Y { get; private set; }

        public PrecisionAddress ColorAddress { get; set; }

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
