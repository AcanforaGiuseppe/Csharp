using Aiv.Fast2D;
using OpenTK;

namespace Mission_PrincessRescue
{
    class HouseLeftScene : Scene
    {
        protected int alivePlayers;
        protected int aliveEnemies;
        protected Sprite pauseSprite;
        protected Vector4 pauseColor;

        public Player player;
        public bool ShowChest = true;

        private Chest chest1;

        public override void GetReader()
        {
            reader = new TmxReader("Assets/Tiled/houseLeft.tmx");
        }

        public override void Start()
        {
            LoadAssets();
            actualS = new SoundEmitter(this, "City");
            actualS.Play(0.3f);
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

            float blockUnitWidth = Game.Window.OrthoWidth / grid.GetLength(1);
            float blockUnitHeight = Game.Window.OrthoHeight / grid.GetLength(0);

            GridPathfinder pathfinder = new GridPathfinder(grid, blockUnitWidth, blockUnitHeight);

            // Player
            player = new Player(Game.GetController(0), 0, pathfinder);
            player.Position = new Vector2(225, 184);
            player.IsActive = true;

            if (ShowChest)
            {
                chest1 = new Chest();
                chest1.Position = new Vector2(225, 136);
                chest1.Sprite.scale = new Vector2(1.5f);
                chest1.IsActive = true;
            }

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

            if (player.Position.X < 238 && player.Position.X > 205 && player.Position.Y > 190)
            {
                OnExit();
                Game.PreviousScene = Game.HLS;
                Game.CurrentScene = Game.spawnScene;
                Game.spawnScene.GetReader();
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