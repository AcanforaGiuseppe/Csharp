using System;
using Aiv.Fast2D;
using OpenTK;

namespace Mission_PrincessRescue
{
    class DungeonCorridorScene : Scene
    {
        private Blood blood2;
        private Blood blood1;
        private Chest chest;

        protected Sprite pauseSprite;
        protected Vector4 pauseColor;

        public float blockUnitWidth;
        public float blockUnitHeight;
        public Player player;
        public bool updated;
        public bool ShowChest = true;

        public override void GetReader()
        {
            reader = new TmxReader("Assets/Tiled/dungeonTunnel.tmx");
        }

        public override void Start()
        {
            LoadAssets();
            actualS = new SoundEmitter(this, "Dungeon");
            actualS.Play();

            pauseSprite = new Sprite(Game.Window.Width, Game.Window.Height);
            pauseColor = new Vector4(0f, 0f, 0f, 0.9f);

            TmxTileset tileset = reader.TileSet;
            TmxGrid tmxGrid = reader.TileLayers[0].Grid;

            int[,] grid = new int[tmxGrid.Rows, tmxGrid.Cols];
            for (int row = 0; row < tmxGrid.Rows; row++)
            {
                for (int col = 0; col < tmxGrid.Cols; col++)
                {
                    int index = row * tmxGrid.Cols + col;
                    TmxCell cell = tmxGrid.At(index);
                    int cost = 1;

                    if (cell != null && cell.Type.Props.Has("cost"))
                        cost = cell.Type.Props.Get<int>("cost");

                    grid[row, col] = cost;
                }
            }

            blockUnitWidth = Game.Window.OrthoWidth / grid.GetLength(1);
            blockUnitHeight = Game.Window.OrthoHeight / grid.GetLength(0);

            GridPathfinder pathfinder = new GridPathfinder(grid, blockUnitWidth, blockUnitHeight);

            // Items
            blood1 = new Blood();
            blood1.Position = new Vector2(70, 170);
            blood1.Sprite.scale = new Vector2(0.6f);
            blood2 = new Blood();
            blood2.Position = new Vector2(370, 150);
            blood2.Sprite.scale = new Vector2(0.5f);
            blood2.Sprite.Rotation = 90;

            if (ShowChest)
            {
                chest = new Chest();
                chest.Position = new Vector2(260, 152);
                chest.Sprite.scale = new Vector2(1.5f);
            }

            if (Game.DBS.takenKey)
                UpdateWorld();

            // Player
            player = new Player(Game.GetController(0), 0, pathfinder);
            if (Game.PreviousScene == Game.D1S)
                player.Position = new Vector2(40, 160);
            else
                player.Position = new Vector2(440, 160);
            player.IsActive = true;

            // Map Rendering
            GfxMgr.AddTexture("tileset", "Assets/Tiled/" + tileset.TilesetPath);

            int size = tmxGrid.Rows * tmxGrid.Cols;
            for (int index = 0; index < size; index++)
            {
                TmxCell cell = tmxGrid.At(index);

                if (cell == null)
                    continue;

                float pixelToUnitW = blockUnitWidth / cell.Type.Width;
                float pixelToUnitH = blockUnitHeight / cell.Type.Height;

                float posX = cell.PosX * pixelToUnitW + blockUnitWidth / 2;
                float posY = cell.PosY * pixelToUnitH + blockUnitHeight / 2;
            }
            base.Start();
        }

        public override void Update()
        {
            PhysicsMgr.Update();
            UpdateMgr.Update();
            PhysicsMgr.CheckCollisions();

            if (player.Position.X < 10)
            {
                OnExit();
                Game.PreviousScene = Game.DCS;
                Game.CurrentScene = Game.D1S;
                Game.D1S.GetReader();
                Game.GetGameReader();
                Game.CurrentScene.Start();
            }

            if (player.Position.X > 465 && Game.DBS.takenKey)
            {
                OnExit();
                Game.PreviousScene = Game.DCS;
                Game.CurrentScene = Game.DBSUpdated;
                Game.DBSUpdated.GetReader();
                Game.GetGameReader();
                Game.CurrentScene.Start();
            }
            else if (player.Position.X > 465)
            {
                OnExit();
                Game.PreviousScene = Game.DCS;
                Game.CurrentScene = Game.DBS;
                Game.DBS.GetReader();
                Game.GetGameReader();
                Game.CurrentScene.Start();
            }
        }

        private void UpdateWorld()
        {
            blood1.IsActive = true;
            blood2.IsActive = true;
            chest.IsActive = true;

            updated = true;
        }

        public override void Draw()
        {
            DrawMgr.Draw();

            if (Game.IsPaused)
                pauseSprite.DrawColor(pauseColor);
        }

        public override void Input()
        {
            player.Input();
        }

        public override Scene OnExit()
        {
            actualS.Stop();

            UpdateMgr.ClearAll();
            DrawMgr.ClearAll();
            PhysicsMgr.ClearAll();
            GfxMgr.ClearAll();

            return base.OnExit();
        }

    }
}