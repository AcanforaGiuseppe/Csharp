using System;
using System.Collections.Generic;

namespace TiledPlugin
{
    public class TmxTileType
    {
        public int Width { get; }
        public int Height { get; }
        public int OffX { get; }
        public int OffY { get; }
        public int Id { get; }

        public TmxProperties Props { get; set; }

        public TmxTileType(int id, int tileWidth, int tileHeight, int xOff, int yOff)
        {
            Width = tileWidth;
            Height = tileHeight;
            OffX = xOff;
            OffY = yOff;
            Id = id;

            Props = new TmxProperties();
        }

    }
}