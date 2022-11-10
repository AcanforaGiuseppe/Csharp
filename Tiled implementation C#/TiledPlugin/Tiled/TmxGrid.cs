using System;

namespace TiledPlugin
{
    public class TmxGrid
    {
        private int layerRows;
        private int layerCols;
        private TmxCell[] cells;

        public TmxGrid(int layerRows, int layerCols)
        {
            this.layerRows = layerRows;
            this.layerCols = layerCols;
            this.cells = new TmxCell[layerRows * layerCols];
        }

        public void Set(int row, int col, TmxCell cell)
        {
            cells[row * layerCols + col] = cell;
        }

        public int Size()
        {
            return cells.Length;
        }

        public TmxCell At(int index)
        {
            return cells[index];
        }

    }
}