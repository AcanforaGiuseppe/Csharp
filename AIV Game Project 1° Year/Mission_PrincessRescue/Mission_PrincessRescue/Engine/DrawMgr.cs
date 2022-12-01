using System.Collections.Generic;
using Aiv.Fast2D;

namespace Mission_PrincessRescue
{
    enum DrawLayer { Background, Middleground, Playground, Foreground, GUI, Last }

    static class DrawMgr
    {
        private static List<IDrawable>[] items;
        private static RenderTexture sceneTexture;
        private static Sprite scene;
        private static Dictionary<string, PostProcessingEffect> postFX;

        static DrawMgr()
        {
            items = new List<IDrawable>[(int)DrawLayer.Last];

            for (int i = 0; i < items.Length; i++)
            {
                items[i] = new List<IDrawable>();
            }

            sceneTexture = new RenderTexture(Game.Window.Width, Game.Window.Height);
            scene = new Sprite(Game.Window.OrthoWidth, Game.Window.OrthoHeight);

            postFX = new Dictionary<string, PostProcessingEffect>();
        }

        public static void AddFX(string name, PostProcessingEffect fx)
        {
            postFX.Add(name, fx);
        }

        public static void RemoveFX(string name)
        {
            if (postFX.ContainsKey(name))
                postFX.Remove(name);
        }

        private static void ApplyPostFX()
        {
            foreach (var item in postFX)
                sceneTexture.ApplyPostProcessingEffect(item.Value);
        }

        public static void AddItem(IDrawable item)
        {
            items[(int)item.Layer].Add(item);
        }

        public static void RemoveItem(IDrawable item)
        {
            items[(int)item.Layer].Remove(item);
        }

        public static void ClearAll()
        {
            for (int i = 0; i < items.Length; i++)
                items[i].Clear();
        }

        public static void Draw()
        {
            Game.Window.RenderTo(sceneTexture);

            for (int i = 0; i < items.Length; i++)
            {
                if ((DrawLayer)i == DrawLayer.GUI)
                {
                    ApplyPostFX();
                    Game.Window.RenderTo(null);
                    scene.DrawTexture(sceneTexture);
                }

                for (int j = 0; j < items[i].Count; j++)
                    items[i][j].Draw();
            }
        }

    }
}