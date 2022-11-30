using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam
{
    class DrawMgr
    {
        public static List<IDrawable> Objects;

        static DrawMgr()
        {
            Objects = new List<IDrawable>();
        }

        public static void Draw()
        {
            for (int i = Objects.Count - 1; i >= 0; i--)
            {
                if (Objects[i] is Background)
                {
                    Objects[i].Draw();
                }
            }

            for (int i = Objects.Count - 1; i >= 0; i--)
            {
                if (Objects[i] is Tile)
                {
                    Objects[i].Draw();
                }
            }

            for (int i = Objects.Count - 1; i >= 0; i--)
            {
                if (Objects[i] is Obstacle)
                {
                    Objects[i].Draw();
                }
            }

            for (int i = Objects.Count - 1; i >= 0; i--)
            {
                if (Objects[i] is HeartPiece)
                    Objects[i].Draw();
            }

            for (int i = Objects.Count - 1; i >= 0; i--)
            {
                if (Objects[i] is Player)
                {
                    Objects[i].Draw();
                }
            }

            for (int i = Objects.Count - 1; i >= 0; i--)
            {
                if (Objects[i] is BoundBox)
                {
                    Objects[i].Draw();
                }
            }

            for (int i = Objects.Count - 1; i >= 0; i--)
            {
                if (Objects[i] is Font)
                {
                    Objects[i].Draw();
                }
            }

            //for (int i = Objects.Count - 1; i >= 0; i--)
            //{
            //    if (Objects[i] is CircleCollider)
            //        Objects[i].Draw();
            //}
        }

        public static void AddItem(IDrawable obj)
        {
            if (obj != null)
                Objects.Add(obj);
        }

        public static void RemoveItem(IDrawable obj)
        {
            if (Objects.Contains(obj))
                Objects.Remove(obj);
        }

        public static void Clear()
        {
            Objects.Clear();
        }
    }
}
