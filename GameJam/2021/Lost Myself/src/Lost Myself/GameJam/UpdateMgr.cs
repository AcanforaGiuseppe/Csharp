using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam
{
    static class UpdateMgr
    {
        public static List<IUpdatable> Objects;

        static UpdateMgr()
        {
            Objects = new List<IUpdatable>();
        }

        public static void Update()
        {
            for (int i = 0; i < Objects.Count; i++)
            {
                Objects[i].Update();
            }
        }

        public static void AddItem(IUpdatable obj)
        {
            if (obj != null)
            {
                Objects.Add(obj);
            }
        }

        public static void RemoveItem(IUpdatable obj)
        {
            if (Objects.Contains(obj))
            {
                Objects.Remove(obj);
            }
        }

        public static void Clear()
        {
            Objects.Clear();
        }

        public static void SetSpeedAll(float speed)
        {
            for (int i = 0; i < Objects.Count; i++)
            {
                if (Objects[i] is Tile)
                {
                    ((Tile)Objects[i]).Speed = speed;
                }
                if (Objects[i] is Obstacle)
                {
                    ((Obstacle)Objects[i]).Speed = speed;
                }
                if (Objects[i] is HeartPiece)
                {
                    ((HeartPiece)Objects[i]).Speed = speed;
                }
            }
        }
    }
}
