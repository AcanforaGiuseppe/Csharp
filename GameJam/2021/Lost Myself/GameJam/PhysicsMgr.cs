using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam
{
    static class PhysicsMgr
    {
        private static List<RigidBody> items;

        static PhysicsMgr()
        {
            items = new List<RigidBody>();
        }

        public static void Update()
        {
            for (int i = items.Count - 1; i >= 0; i--)
            {
                if (items[i].IsActive)
                    items[i].Update();
            }
        }

        public static void AddItem(RigidBody obj)
        {
            if (obj != null)
                items.Add(obj);
        }

        public static void RemoveItem(RigidBody obj)
        {
            if (items.Contains(obj))
                items.Remove(obj);
        }

        public static void Clear()
        {
            items.Clear();
        }
    }
}
