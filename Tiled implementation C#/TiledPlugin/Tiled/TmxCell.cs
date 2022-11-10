namespace TiledPlugin
{
    public class TmxCell
    {
        public TmxTileType Type { get; }
        public int PosX { get; }
        public int PosY { get; }

        public TmxCell(TmxTileType type, int xPos, int yPos)
        {
            this.Type = type;
            this.PosX = xPos;
            this.PosY = yPos;
        }

    }
}