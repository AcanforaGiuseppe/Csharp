namespace Mission_PrincessRescue
{
    public class TmxGrid
    {
        public int Rows { get; }
        public int Cols { get; }

        private TmxCell[] cells;

        public TmxGrid(int layerRows, int layerCols)
        {
            this.Rows = layerRows;
            this.Cols = layerCols;
            this.cells = new TmxCell[layerRows * layerCols];
        }

        public void Set(int row, int col, TmxCell cell)
        {
            cells[row * Cols + col] = cell;
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