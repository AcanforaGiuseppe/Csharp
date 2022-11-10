namespace TiledPlugin
{
    public class TmxLayer
    {
        public string Name { get; }
        public TmxGrid Grid { get; }
        public TmxProperties Props { get; set; }

        public TmxLayer(string name, TmxGrid grid)
        {
            Name = name;
            Grid = grid;
            Props = new TmxProperties();
        }

    }
}