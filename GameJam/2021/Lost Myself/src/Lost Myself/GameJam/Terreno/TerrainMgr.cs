using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam
{
    enum TileType
    {
        VOID1,
        VOID2,
        BUSH1,
        BUSH2,
        TREE1,
        TREE2,
        LAST
    }

    static class TerrainMgr
    {
        public static int TerrainHeight;
        static Vector2 TileWidth;
        static int queueSize = 14;
        static Queue<Tile>[] tiles;
        static List<Tile> activeTiles;
        static int previousTile;
        public static float YPos;

        public static void Init()
        {
            TerrainHeight = 500;
            tiles = new Queue<Tile>[(int)TileType.LAST];
            for (int i = 0; i < tiles.Length; i++)
            {
                tiles[i] = new Queue<Tile>(queueSize);
                switch ((TileType)i)
                {
                    case TileType.VOID1:
                        for (int j = 0; j < queueSize; j++)
                        {
                            tiles[i].Enqueue(new VoidTile1());
                        }
                        break;
                    case TileType.VOID2:
                        for (int j = 0; j < queueSize; j++)
                        {
                            tiles[i].Enqueue(new VoidTile2());
                        }
                        break;
                    case TileType.TREE1:
                        for (int j = 0; j < queueSize; j++)
                        {
                            tiles[i].Enqueue(new TreeTile1());
                        }
                        break;
                    case TileType.TREE2:
                        for (int j = 0; j < queueSize; j++)
                        {
                            tiles[i].Enqueue(new TreeTile2());
                        }
                        break;
                    case TileType.BUSH1:
                        for (int j = 0; j < queueSize; j++)
                        {
                            tiles[i].Enqueue(new BushTile1());
                        }
                        break;
                    case TileType.BUSH2:
                        for (int j = 0; j < queueSize; j++)
                        {
                            tiles[i].Enqueue(new BushTile2());
                        }
                        break;
                    default:
                        break;
                }
            }
            activeTiles = new List<Tile>();
            YPos = 550;
            TileWidth = new Vector2(300, 0);
        }

        public static void AddTile(Tile t)
        {
            //Aggiungi un tile
            t.IsActive = true;
            if (activeTiles.Count == 0)
            {
                t.Position = new Vector2(0, YPos);
            }
            else
            {
                t.Position = activeTiles[activeTiles.Count - 1].Position + TileWidth;
            }
            activeTiles.Add(t);
        }

        public static void Update()
        {
            if (activeTiles.Count - 1 < 0)
            {
                if (Game.IsStart || Game.EndGame)
                {
                    previousTile = RandomGenerator.GetRandomInt((int)TileType.BUSH1);
                }
                else
                {
                    previousTile = RandomGenerator.GetRandomInt((int)TileType.LAST);    
                }
                AddTile(tiles[previousTile].Dequeue());
            }
            else
            {
                //Controllare se c'è un "vuoto" a destra
                if (activeTiles[activeTiles.Count - 1].PositionX < Game.Win.Width - TileWidth.X)
                {
                    //Generare un tile random fino a che non sia un burrone se lo era quello precedente
                    int queueIndex;
                    if (Game.IsStart || Game.EndGame)
                    {
                        queueIndex = RandomGenerator.GetRandomInt((int)TileType.BUSH1);
                    }
                    else
                    {
                        queueIndex = RandomGenerator.GetRandomInt((int)TileType.LAST);
                    }

                    if (previousTile == (int)TileType.TREE1 || previousTile == (int)TileType.TREE2)
                    {
                        while (queueIndex == (int)TileType.TREE1 || queueIndex == (int)TileType.TREE2)
                        {
                            queueIndex = RandomGenerator.GetRandomInt((int)TileType.LAST);
                        }
                    }

                    //Se si, aggiungere un nuovo random tile
                    AddTile(tiles[queueIndex].Dequeue());
                    previousTile = queueIndex;
                }
            }
        }

        public static void RestoreTile(Tile t)
        {
            t.IsActive = false;
            activeTiles.Remove(t);
            tiles[(int)t.Type].Enqueue(t);
        }
    }
}
