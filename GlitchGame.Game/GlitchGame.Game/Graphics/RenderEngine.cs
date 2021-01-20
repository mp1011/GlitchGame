using GlitchGame.GameMain.Extensions;
using GlitchGame.GameMain.Memory;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GlitchGame.GameMain.Graphics
{
    public class RenderEngine
    {
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

            for (int i = 0; i < _systemMemory.VideoMemory.Sprites.MaxSprites; i++)
            {
                var sprite = _systemMemory.VideoMemory.Sprites.Get(i);
                if(sprite != null && _renderGun.Y >= sprite.Y && _renderGun.Y < sprite.Y + sprite.Height)
                {
                    _scanlineSprites[scanlineSpriteIndex] = sprite;
                    scanlineSpriteIndex++;
                }
            }

            while(scanlineSpriteIndex < _scanlineSprites.Length)    
                _scanlineSprites[scanlineSpriteIndex++] = null;
            
        }

        private void DrawNextPixel()
        {
            var renderGunX = _renderGun.X;
            var renderGunY = _renderGun.Y;

            _renderGun.Palette = _systemMemory.VideoMemory.BgLayer.Palette;
            _renderGun.Color = _systemMemory.VideoMemory.BgLayer.GetColorAtScreenPoint(_systemMemory.VideoMemory.Tiles, renderGunX, renderGunY);

            for (int i = 0; i < _scanlineSprites.Length; i++)
            {
                if (_scanlineSprites[i] != null && _scanlineSprites[i].HitTest(renderGunX, renderGunY))
                {
                    var color = _scanlineSprites[i].GetColorAtScreenPoint(_systemMemory.VideoMemory.Tiles, renderGunX, renderGunY);
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
            SpriteBatch.Draw(_systemPalette, screenPixel, srcPixel, Color.White);
        }

        private Rectangle GetNextSourcePixel()
        {
            var color = _systemMemory.VideoMemory.Palettes
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
                .ToRectangle(1); 
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
