using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam
{
    static class Game
    {
        public static Window Win;
        public static float DeltaTime { get { return Win.DeltaTime; } }
        public static Player Player;
        public static Background bg;
        public static bool IsStart;
        public static bool EndGame;
        public static bool Exit;
        static float counter = 0;

        //GLOBAL SPEED PER LA MODIFICA (-700 DI DEFAULT)
        public static float GlobalSpeed = -500;
        public static float CrouchingGlobalSpeed = -425;

        //GLOBAL CUORI DA PRENERE PER VINCERE
        public static int HeartsRequired = 10;

        //GLOBAL LIFE COUNT
        public static int Lifes = 4;

        public static void Init()
        {
            IsStart = true;
            EndGame = false;
            Exit = false;
            Win = new Window(1280, 720, "GameJam");
            Win.SetVSync(false);
            LoadAssets();
            AudioMgr.Init();
            RandomGenerator.Init();
            FontMgr.Init();
            TerrainMgr.Init();
            ObstacleMgr.Init();
            Player = new Player(new Vector2(200, TerrainMgr.YPos));
            bg = new Background();
        }

        public static void LoadAssets()
        {
            var assetsDir = new DirectoryInfo(@"Assets/");
            var audioDir = new DirectoryInfo(@"Assets/Audio/");

            foreach (FileInfo file in assetsDir.GetFiles())
            {
                GfxMgr.AddTexture(file.Name);
            }

            foreach (FileInfo file in audioDir.GetFiles())
            {
                GfxMgr.AddClip(file.Name);
            }
        }

        public static void Play()
        {
            while (Win.IsOpened)
            {
                PlayBgMusic(0.8f);
                //Win.SetTitle($"{1f / DeltaTime}");
                //INPUTS =============================================
                if (Win.GetKey(KeyCode.Esc) || Exit)
                {
                    break;
                }
                Player.Input();

                //UPDATES ============================================
                PhysicsMgr.Update();
                TerrainMgr.Update();
                ObstacleMgr.Update();
                FontMgr.Update();
                UpdateMgr.Update();

                //PhysicsMgr.CheckCollisions();

                //DRAWS ==============================================
                DrawMgr.Draw();

                Win.Update();
            }
        }

        public static void PlayBgMusic(float playafter)
        {
            counter += DeltaTime;
            if (counter >= playafter)
            {
                AudioMgr.StreamBackground("BgMusic_2");
            }
        }
    }
}
