using OpenTK;
using Aiv.Fast2D;

namespace Mission_PrincessRescue
{
    class HouseMiddleScene : Scene
    {
        protected Sprite pauseSprite;
        protected Vector4 pauseColor;

        public Player player;

        private TextObject txt;
        private float counter;
        private float maxCounter = 20;

        public override void GetReader()
        {
            reader = new TmxReader("Assets/Tiled/houseMiddle.tmx");
        }

        public override void Start()
        {
            LoadAssets();
            actualS = new SoundEmitter(this, "Victory");
            actualS.Play();

            pauseSprite = new Sprite(Game.Window.Width, Game.Window.Height);
            pauseColor = new Vector4(0f, 0f, 0f, 0.9f);

            counter = maxCounter;

            // Writer
            FontMgr.Init();
            Font stdFont = FontMgr.AddFont("stdFont", "Assets/textSheet.png", 15, 32, 20, 20);
            txt = new TextObject(new Vector2(180, 30), "Hai salvato", stdFont, 0);
            txt = new TextObject(new Vector2(180, 50), "la principessa!", stdFont, 0);
            txt.IsActive = true;

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

            // Princess
            Princess princess = new Princess(pathfinder);
            princess.Position = new Vector2(235, 145);
            princess.IsActive = true;

            // Player
            player = new Player(Game.GetController(0), 0, pathfinder);
            player.Position = new Vector2(240, 185);
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

            if (player.Position.Y > 199 && player.Position.X > 78 && player.Position.X < 120 || player.Position.Y > 199 && player.Position.X > 168 && player.Position.X < 214 || player.Position.Y > 199 && player.Position.X > 265 && player.Position.X < 300 || player.Position.Y > 199 && player.Position.X > 360 && player.Position.X < 400)
            {
                OnExit();
                Game.PreviousScene = Game.HMS;
                Game.CurrentScene = Game.spawnScene;
                Game.spawnScene.GetReader();
                Game.GetGameReader();
                Game.CurrentScene.Start();
            }

            counter -= Game.DeltaTime;

            if (counter <= 0)
            {
                OnExit();
                Game.PreviousScene = Game.HMS;
                Game.CurrentScene = Game.GameOver;
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
            FontMgr.ClearAll();

            return base.OnExit();
        }

    }
}