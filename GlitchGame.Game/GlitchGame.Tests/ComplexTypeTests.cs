using FluentAssertions;
using GlitchGame.GameMain.Graphics;
using GlitchGame.GameMain.Memory;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace GlitchGame.Tests
{
    class ComplexTypeTests
    {
        [SetUp]
        public void Setup()
        {
            SystemBinaryData.Reset();
            SystemBinaryData.SetIOPointer(0, 0);
        }

        [Test]
        public void CanReadTileFlipFromSprite()
        {
            new Sprite(new SpriteTiles(
                                   new TileIndex(new ByteValue(3), Flip.FlipBoth),
                                   new TileIndex(new ByteValue(4), Flip.FlipX),
                                   new TileIndex(new ByteValue(5), Flip.FlipY),
                                   new TileIndex(new ByteValue(6), Flip.FlipBoth)
                               ), new ByteValue(7), new ByteValue(8), new SpriteAttributes(new Value2(), new Value2(), new Value4()));

            SystemBinaryData.SetIOPointer(0, 0);

            var sprite = new Sprite();
            var flip1 = sprite.Tiles.GetFromCoordinates(0, 0).Flip;
            var flip2 = sprite.Tiles.GetFromCoordinates(1, 0).Flip;

            flip1.Should().Be(Flip.FlipBoth);
            flip2.Should().Be(Flip.FlipX);
        }

        [Test]
        public void CanReadTileFlipFromTileIndex()
        {
            new TileIndex(new ByteValue(0), Flip.FlipX);
            SystemBinaryData.SetIOPointer(0, 0);

            var tile = new TileIndex();
            tile.Flip.Should().Be(Flip.FlipX);
        }

        [Test]
        public void CanReadTileFromGrid()
        {
            new SpriteTiles(new TileIndex(new ByteValue(255), Flip.Normal),
                                new TileIndex(new ByteValue(255), Flip.FlipX),
                                new TileIndex(new ByteValue(255), Flip.FlipY),
                                new TileIndex(new ByteValue(255), Flip.FlipBoth)
                                );

            SystemBinaryData.SetIOPointer(0, 0);

            var grid = new SpriteTiles();
            grid.GetFromCoordinates(1, 0).Flip.Should().Be(Flip.FlipX);
        }

        [Test]
        public void CanReadPositionFromSprite()
        {
            new Sprite(new SpriteTiles(
                                   new TileIndex(new ByteValue(3), Flip.FlipBoth),
                                   new TileIndex(new ByteValue(4), Flip.FlipX),
                                   new TileIndex(new ByteValue(5), Flip.FlipY),
                                   new TileIndex(new ByteValue(6), Flip.FlipBoth)
                               ), new ByteValue(32), new ByteValue(64), new SpriteAttributes(new Value2(), new Value2(), new Value4()));

            new Sprite(new SpriteTiles(
                                 new TileIndex(new ByteValue(3), Flip.FlipBoth),
                                 new TileIndex(new ByteValue(4), Flip.FlipX),
                                 new TileIndex(new ByteValue(5), Flip.FlipY),
                                 new TileIndex(new ByteValue(6), Flip.FlipBoth)
                             ), new ByteValue(128), new ByteValue(192), new SpriteAttributes(new Value2(), new Value2(), new Value4()));

            SystemBinaryData.SetIOPointer(0, 0);

            var sprite = new Sprite();
            sprite.X.Should().Be(32);
            sprite.Y.Should().Be(64);

            var sprite2 = new Sprite();
            sprite2.X.Should().Be(128);
            sprite2.Y.Should().Be(192);

        }

    }
}
