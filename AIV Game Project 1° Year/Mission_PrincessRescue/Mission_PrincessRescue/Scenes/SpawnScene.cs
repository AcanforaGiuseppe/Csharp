using Aiv.Fast2D;
using OpenTK;

namespace Mission_PrincessRescue
{
    class SpawnScene : Scene
    {
        private Chest chest;

        protected Sprite pauseSprite;
        protected Vector4 pauseColor;

        public Player player;
        public bool ShowChest = true;
        public float blockUnitWidth;
        public float blockUnitHeight;

        public override void GetReader()
        {
            if (Game.DBS.takenKey)
                reader = new TmxReader("Assets/Tiled/spawn-open.tmx");
            else
                reader = new TmxReader("Assets/Tiled/spawn-closed.tmx");
        }

        public override void Start()
        {
            LoadAssets();
            actualS = new SoundEmitter(this, "City");
            actualS.Play(0.9f);

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

            if (ShowChest)
            {
                chest = new Chest();
                chest.Position = new Vector2(343, 168);
                chest.Sprite.scale = new Vector2(1.5f);
                chest.IsActive = true;
            }

            // Player
            player = new Player(Game.GetController(0), 0, pathfinder);
            if (Game.PreviousScene == Game.leftScene)
                player.Position = new Vector2(25, 155);
            else if (Game.PreviousScene == Game.rightScene)
                player.Position = new Vector2(455, 155);
            else if (Game.PreviousScene == Game.HMS)
                player.Position = new Vector2(239, 56);
            else if (Game.PreviousScene == Game.HRS)
                player.Position = new Vector2(425, 56);
            else if (Game.PreviousScene == Game.HLS)
                player.Position = new Vector2(136, 56);
            else
                player.Position = new Vector2(240, 157);
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

            if (player.Position.X < 137 && player.Position.Y < 55)
            {
                OnExit();
                Game.PreviousScene = Game.spawnScene;
                Game.CurrentScene = Game.HLS;
                Game.HLS.GetReader();
                Game.GetGameReader();
                Game.CurrentScene.Start();
            }

            if (player.Position.X > 214 && player.Position.X < 267 && player.Position.Y < 50)
            {
                OnExit();
                Game.PreviousScene = Game.spawnScene;
                Game.CurrentScene = Game.HMS;
                Game.HMS.GetReader();
                Game.GetGameReader();
                Game.CurrentScene.Start();
            }

            if (player.Position.X > 297 && player.Position.X < 425 && player.Position.Y < 55)
            {
                OnExit();
                Game.PreviousScene = Game.spawnScene;
                Game.CurrentScene = Game.HRS;
                Game.HRS.GetReader();
                Game.GetGameReader();
                Game.CurrentScene.Start();
            }

            if (player.Position.X > 465)
            {
                OnExit();
                Game.PreviousScene = Game.spawnScene;
                Game.CurrentScene = Game.rightScene;
                Game.rightScene.GetReader();
                Game.GetGameReader();
                Game.CurrentScene.Start();
            }

            if (player.Position.X < 10)
            {
                OnExit();
                Game.PreviousScene = Game.spawnScene;
                Game.CurrentScene = Game.leftScene;
                Game.leftScene.GetReader();
                Game.GetGameReader();
                Game.CurrentScene.Start();
            }
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