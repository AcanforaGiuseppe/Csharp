using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiledPlugin
{
    class Scene
    {
        public Scene()
        {
            //Tiled Domain 
            TmxReader reader = new TmxReader("Assets/map8x8.tmx");
            TmxTileset tileSet = reader.TileSet;
            List<TmxLayer> tileLayers = reader.TileLayers;

            //Fast2D Domain
            GfxMgr.AddTexture(tileSet.TilesetPath, "Assets/" + tileSet.TilesetPath);

            foreach (TmxLayer eachLayer in tileLayers)
            {
                PrepareLayer(eachLayer, tileSet);
            }
        }

        private void PrepareLayer(TmxLayer tileLayer, TmxTileset tileSet)
        {
            for (int i = 0; i < tileLayer.Grid.Size(); i++)
            {
                TmxCell cell = tileLayer.Grid.At(i);
                if (cell == null)
                    continue;

                TileObj tileObj = new TileObj(tileSet.TilesetPath, cell.PosX, cell.PosY, cell.Type.OffX, cell.Type.OffY, cell.Type.Width, cell.Type.Height);

                //Props.TryBool("collidable")
                if (cell.Type.Props.Has("collidable") && cell.Type.Props.GetBool("collidable"))
                {
                    tileObj.RigidBody = new RigidBody(tileObj);
                    tileObj.RigidBody.Collider = ColliderFactory.CreateBoxFor(tileObj);
                    tileObj.RigidBody.Type = RigidBodyType.TileObj;
                }

                //Props.GetString("drawLayer", "Background")
                string drawLayer = tileLayer.Props.GetString("drawLayer");
                if (drawLayer != null)
                    tileObj.Layer = (DrawLayer)Enum.Parse(typeof(DrawLayer), drawLayer);
            }
        }

        public void Update()
        { }

    }
}