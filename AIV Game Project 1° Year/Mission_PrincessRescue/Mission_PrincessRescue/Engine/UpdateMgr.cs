using System.Collections.Generic;

namespace Mission_PrincessRescue
{
    static class UpdateMgr
    {
        private static List<IUpdatable> items;

        static UpdateMgr()
        {
            items = new List<IUpdatable>();
        }

        public static void AddItem(IUpdatable item)
        {
            items.Add(item);
        }

        public static void RemoveItem(IUpdatable item)
        {
            items.Remove(item);
        }

        public static void ClearAll()
        {
            items.Clear();
        }

        public static void Update()
        {
            for (int i = 0; i < items.Count; i++)
                items[i].Update();
        }

    }
}