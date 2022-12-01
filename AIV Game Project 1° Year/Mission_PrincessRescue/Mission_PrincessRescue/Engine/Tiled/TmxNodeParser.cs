using System.Collections.Generic;
using System.Xml;

namespace Mission_PrincessRescue
{
    class TmxNodeParser
    {
        static int attribAsInt(XmlNode node, string attrName)
        {
            return int.Parse(attribAsStr(node, attrName));
        }

        static string attribAsStr(XmlNode node, string attrName)
        {
            XmlNode attrib = node.Attributes.GetNamedItem(attrName);

            if (attrib == null)
                return null;

            return attrib.Value;
        }

        public static TmxTileset ParseTileset(XmlNode mapNode)
        {
            XmlNode tilesetNode = mapNode.SelectSingleNode("tileset");
            int tileWidth = attribAsInt(tilesetNode, "tilewidth");
            int tileHeight = attribAsInt(tilesetNode, "tileheight");
            int spacing = attribAsInt(tilesetNode, "spacing");
            int margin = attribAsInt(tilesetNode, "margin");
            XmlNode imageNode = tilesetNode.SelectSingleNode("image");
            string tilesetPath = attribAsStr(imageNode, "source");
            int textWidth = attribAsInt(imageNode, "width");
            int textHeight = attribAsInt(imageNode, "height");
            TmxTileset tileSet = new TmxTileset(tilesetPath, textWidth, textHeight, tileWidth, tileHeight, spacing, margin);

            XmlNodeList tileNodes = tilesetNode.SelectNodes("tile");
            foreach (XmlNode tile in tileNodes)
            {
                int id = attribAsInt(tile, "id");
                TmxTileType tileType = tileSet.At(id);
                tileType.Props = ParseProps(tile);
            }
            return tileSet;
        }

        public static List<TmxLayer> ParseLayers(XmlNode mapNode, TmxTileset tileSet)
        {
            List<TmxLayer> layers = new List<TmxLayer>();
            XmlNodeList layerNodes = mapNode.SelectNodes("layer");

            foreach (XmlNode layerNode in layerNodes)
                layers.Add(ParseLayer(layerNode, tileSet));

            return layers;
        }

        static TmxProperties ParseProps(XmlNode node)
        {
            TmxProperties props = new TmxProperties();
            XmlNodeList propNodes = node.SelectNodes("properties/property");
            foreach (XmlNode prop in propNodes)
            {
                string name = attribAsStr(prop, "name");
                string type = attribAsStr(prop, "type");
                string value = attribAsStr(prop, "value");

                if (type == null)
                    props.Set<string>(name, value);
                else if (type.Equals("bool"))
                    props.Set<bool>(name, bool.Parse(value));
                else if (type.Equals("int"))
                    props.Set<int>(name, int.Parse(value));
            }
            return props;
        }

        public static TmxLayer ParseLayer(XmlNode layerNode, TmxTileset tileSet)
        {
            int layerCols = attribAsInt(layerNode, "width");
            int layerRows = attribAsInt(layerNode, "height");
            XmlNode layerData = layerNode.SelectSingleNode("data");
            string csv = layerData.InnerText;
            csv = csv.Replace("\r\n", "").Replace("\n", "");

            string[] tileIds = csv.Split(',');

            TmxGrid grid = new TmxGrid(layerRows, layerCols);
            int xPos = 0;
            int yPos = 0;
            for (int row = 0; row < layerRows; row++)
            {
                for (int col = 0; col < layerCols; col++)
                {
                    int id = int.Parse(tileIds[row * layerCols + col]);
                    if (id != 0)
                    {
                        TmxTileType type = tileSet.At(id - 1);
                        TmxCell cell = new TmxCell(type, xPos, yPos);
                        grid.Set(row, col, cell);
                    }
                    xPos += tileSet.TileWidth;
                }
                xPos = 0;
                yPos += tileSet.TileHeight;
            }

            string name = attribAsStr(layerNode, "name");
            TmxLayer layer = new TmxLayer(name, grid);
            layer.Props = ParseProps(layerNode);
            return layer;
        }

    }
}