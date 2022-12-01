namespace Mission_PrincessRescue
{
    public class TmxTileset
    {
        public string TilesetPath { get; internal set; }
        public int TileWidth { get; internal set; }
        public int TileHeight { get; internal set; }

        private int textWidth;
        private int textHeight;
        private int spacing;
        private int margin;
        private int rows;
        private int cols;
        private TmxTileType[] tiles;

        public TmxTileset(string tilesetPath, int textWidth, int textHeight, int tileWidth, int tileHeight, int spacing, int margin)
        {
            this.TilesetPath = tilesetPath;
            this.textWidth = textWidth;
            this.textHeight = textHeight;
            this.TileWidth = tileWidth;
            this.TileHeight = tileHeight;
            this.spacing = spacing;
            this.margin = margin;

            int virtualHeight = textHeight - 2 * margin + spacing;
            int virtualTileHeight = tileHeight + spacing;

            int virtualWidth = textWidth - 2 * margin + spacing;
            int virtualTileWidth = tileWidth + spacing;

            this.rows = virtualHeight / virtualTileHeight;
            this.cols = virtualWidth / virtualTileWidth;
            this.tiles = new TmxTileType[rows * cols];

            int xOff = margin;
            int yOff = margin;
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    int id = row * cols + col;

                    TmxTileType tile = new TmxTileType(id, tileWidth, tileHeight, xOff, yOff);
                    tiles[row * cols + col] = tile;
                    xOff += virtualTileWidth;
                }
                xOff = margin;
                yOff += virtualTileHeight;
            }
        }

        public TmxTileType At(int indexAndId)
        {
            return tiles[indexAndId];
        }

    }
}