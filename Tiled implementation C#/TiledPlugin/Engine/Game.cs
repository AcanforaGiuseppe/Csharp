using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiledPlugin
{
    static class Game
    {
        private static Scene Scene;

        public static Window Win;
        public static bool IsRunning;

        public static float DeltaTime { get { return Win.DeltaTime; } }

        public static RandomTimer RandomTimer(int timeMin, int timeMax)
        {
            return new RandomTimer(timeMin, timeMax);
        }

        public static void Init()
        {
            Win = new Window(1280, 720, "Tiled Plugin");
            Win.SetVSync(false);
            Scene = new Scene();
        }

        public static void Play()
        {
            IsRunning = true;
            while (Win.IsOpened && IsRunning)
            {
                // Input
                if (Win.GetKey(KeyCode.Esc))
                    break;

                Scene.Update();

                // Draw
                DrawMgr.Draw();
                PhysicsMgr.ShowColliders();

                Win.Update();
            }
        }

    }
}