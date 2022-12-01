using Aiv.Fast2D;
using OpenTK;

namespace Mission_PrincessRescue
{
    class DungeonBossScene : Scene
    {
        public float blockUnitWidth;
        public float blockUnitHeight;
        public Player player;
        public Key key;
        public bool takenKey = false;
        public bool updated = false;
        public bool ShowChest = true;

        protected Sprite pauseSprite;
        protected Vector4 pauseColor;

        private Lamp lamp1;
        private Lamp lamp2;
        private Lamp lamp3;
        private Lamp lamp4;
        private Lamp lamp5;

        public override void GetReader()
        {
            reader = new TmxReader("Assets/Tiled/dungeon2.tmx");
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
            lamp1 = new Lamp();
            lamp1.Position = new Vector2(150);
            lamp1.Sprite.Rotation = 30;
            lamp2 = new Lamp();
            lamp2.Position = new Vector2(200);
            lamp2.Sprite.Rotation = 60;
            lamp3 = new Lamp();
            lamp3.Position = new Vector2(70, 260);
            lamp3.Sprite.Rotation = 90;
            lamp4 = new Lamp();
            lamp4.Position = new Vector2(400, 200);
            lamp4.Sprite.Rotation = 90;
            lamp5 = new Lamp();
            lamp5.Position = new Vector2(300, 70);
            lamp5.Sprite.Rotation = 90;

            // Player
            player = new Player(Game.GetController(0), 0, pathfinder);
            if (Game.PreviousScene == Game.DBS)
                player.Position = new Vector2(332, 168);
            else
                player.Position = new Vector2(40, 160);
            player.IsActive = true;

            if (!takenKey)
            {
                key = new Key();
                key.Position = new Vector2(352, 160);
                key.IsActive = true;
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

            if (player.Position.X > 340 && player.Position.X < 361 && player.Position.Y > 150 && player.Position.Y < 169)
                takenKey = true;

            if (takenKey)
            {
                OnExit();
                Game.PreviousScene = Game.DBS;
                Game.CurrentScene = Game.DBSUpdated;
                Game.DBSUpdated.GetReader();
                Game.GetGameReader();
                Game.CurrentScene.Start();
            }

            if (player.Position.X < 10)
            {
                OnExit();
                Game.PreviousScene = Game.DBS;
                Game.CurrentScene = Game.DCS;
                Game.DCS.GetReader();
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
            lamp1.soundEmitter.Stop();
            lamp2.soundEmitter.Stop();
            lamp3.soundEmitter.Stop();
            lamp4.soundEmitter.Stop();
            lamp5.soundEmitter.Stop();

            UpdateMgr.ClearAll();
            DrawMgr.ClearAll();
            PhysicsMgr.ClearAll();
            GfxMgr.ClearAll();

            return base.OnExit();
        }

    }
}