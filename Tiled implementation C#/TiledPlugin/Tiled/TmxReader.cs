using System.Xml;
using System;
using System.Collections.Generic;

namespace TiledPlugin
{
    public class TmxReader
    {
        private string tmxPath;

        public TmxTileset TileSet { get; internal set; }
        public List<TmxLayer> TileLayers { get; internal set; }

        public TmxReader(string tmxPath)
        {
            this.tmxPath = tmxPath;

            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load(tmxPath);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            XmlNode mapNode = doc.SelectSingleNode("map");
            TileSet = TmxNodeParser.ParseTileset(mapNode);
            TileLayers = TmxNodeParser.ParseLayers(mapNode, TileSet);
        }

    }
}