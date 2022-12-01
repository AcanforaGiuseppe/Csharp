using System;
using System.Collections.Generic;
using Aiv.Fast2D;
using OpenTK;

namespace Mission_PrincessRescue
{
    static class Game
    {
        static List<Controller> controllers;
        static KeyboardController keyboardController;
        static bool isPausePressed;

        public static Window Window;
        public static SpawnScene spawnScene;
        public static LeftScene leftScene;
        public static RightScene rightScene;
        public static HouseMiddleScene HMS;
        public static HouseLeftScene HLS;
        public static HouseRightScene HRS;
        public static DungeonEntranceScene DES;
        public static DungeonCorridorScene DCS;
        public static Dungeon1Scene D1S;
        public static DungeonBossScene DBS;
        public static DungeonBossSceneUpdated DBSUpdated;
        public static GameOverScene GameOver;
        public static bool IsPaused;
        public static TileObj tileObj;
        public static Scene PreviousScene { get; set; }
        public static Scene CurrentScene { get; set; }
        public static float DeltaTime { get { return !IsPaused ? Window.DeltaTime : 0; } }
        public static float UnitSize { get; private set; }
        public static float OptimalScreenHeight { get; private set; }
        public static float OptimalUnitSize { get; private set; }
        public static Vector2 ScreenCenter { get; private set; }
        public static float ScreenDiagonalSquared { get; private set; }

        public static void Init()
        {
            Window = new Window(960, 640, "Mission: PrincessRescue");
            Window.SetVSync(false);

            Window.SetDefaultViewportOrthographicSize(320);
            OptimalScreenHeight = 640;
            UnitSize = Window.Height / Window.OrthoHeight;
            OptimalUnitSize = OptimalScreenHeight / Window.OrthoHeight;

            ScreenCenter = new Vector2(Window.OrthoWidth * 0.5f, Window.OrthoHeight * 0.5f);
            ScreenDiagonalSquared = ScreenCenter.LengthSquared;

            spawnScene = new SpawnScene();
            leftScene = new LeftScene();
            rightScene = new RightScene();
            HMS = new HouseMiddleScene();
            HLS = new HouseLeftScene();
            HRS = new HouseRightScene();
            DES = new DungeonEntranceScene();
            DCS = new DungeonCorridorScene();
            D1S = new Dungeon1Scene();
            DBS = new DungeonBossScene();
            DBSUpdated = new DungeonBossSceneUpdated();
            GameOver = new GameOverScene();

            List<KeyCode> keys = new List<KeyCode>();
            keys.Add(KeyCode.W);
            keys.Add(KeyCode.S);
            keys.Add(KeyCode.D);
            keys.Add(KeyCode.A);
            keys.Add(KeyCode.Space);

            KeysList keyList = new KeysList(keys);
            keyboardController = new KeyboardController(0, keyList);

            string[] joystics = Window.Joysticks;
            controllers = new List<Controller>();

            for (int i = 0; i < joystics.Length; i++)
            {
                if (joystics[i] != null && joystics[i] != "Unmapped Controller")
                    controllers.Add(new JoypadController(i));
            }

            CurrentScene = spawnScene;
            GetGameReader();
        }

        public static void GetGameReader()
        {
            CurrentScene.GetReader();

            //Tiled Domain
            TmxTileset tileSet = CurrentScene.reader.TileSet;
            List<TmxLayer> tileLayers = CurrentScene.reader.TileLayers;

            //Fast2D Domain
            GfxMgr.AddTexture(tileSet.TilesetPath, "Assets/Tiled/" + tileSet.TilesetPath);

            foreach (TmxLayer eachLayer in tileLayers)
                CurrentScene.PrepareLayer(eachLayer, tileSet);
        }

        public static float PixelsToUnits(float pixelsSize)
        {
            return pixelsSize / OptimalUnitSize;
        }

        public static Controller GetController(int index)
        {
            Controller ctrl = keyboardController;

            if (index < controllers.Count)
                ctrl = controllers[index];

            return ctrl;
        }

        public static void Play()
        {
            CurrentScene.Start();

            while (Window.IsOpened)
            {
                if (!IsPaused)
                {
                    float fps = 1f / Window.DeltaTime;
                    Window.SetTitle($"Mission: PrincessRescue                                                                                                            FPS: {(int)fps}");
                }
                else
                {
                    Window.SetTitle("Paused                                                                                                                         Mission: PrincessRescue");
                }

                if (!CurrentScene.IsPlaying)
                {
                    Scene nextScene = CurrentScene.OnExit();

                    GC.Collect();

                    if (nextScene != null)
                    {
                        CurrentScene = nextScene;
                        CurrentScene.Start();
                    }
                    else
                    {
                        return;
                    }
                }

                // Input
                if (Window.GetKey(KeyCode.Esc))
                    return;

                if (Window.GetKey(KeyCode.Space))
                {
                    if (!isPausePressed)
                    {
                        isPausePressed = true;
                        IsPaused = !IsPaused;
                    }
                }
                else
                {
                    isPausePressed = false;
                }

                CurrentScene.Input();

                // Update
                CurrentScene.Update();

                // Draw
                CurrentScene.Draw();

                Window.Update();
            }
        }

    }
}