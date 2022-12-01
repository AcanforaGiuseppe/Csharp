using Aiv.Fast2D;
using OpenTK;

namespace Mission_PrincessRescue
{
    class RightScene : Scene
    {
        protected int alivePlayers;
        protected int aliveEnemies;
        protected Sprite pauseSprite;
        protected Vector4 pauseColor;

        public Player player;
        public bool ShowChest = true;

        public override void GetReader()
        {
            reader = new TmxReader("Assets/Tiled/forest.tmx");
        }

        public override void Start()
        {
            LoadAssets();
            actualS = new SoundEmitter(this, "Forest");
            actualS.Play(0.8f);

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
            if (Game.PreviousScene == Game.spawnScene)
                player.Position = new Vector2(40, 152);
            else
                player.Position = new Vector2(280, 168);
            player.IsActive = true;

            if (ShowChest)
            {
                Chest chest = new Chest();
                chest.Position = new Vector2(235, 56);
                chest.Sprite.scale = new Vector2(1.5f);
                chest.IsActive = true;
            }

            // Items
            Leaf l1 = new Leaf();
            l1.Position = new Vector2(100, 145);
            l1.IsActive = true;
            Leaf l2 = new Leaf();
            l2.Position = new Vector2(183);
            l2.IsActive = true;
            Leaf l3 = new Leaf();
            l3.Position = new Vector2(40, 94);
            l3.IsActive = true;
            Leaf l4 = new Leaf();
            l4.Position = new Vector2(300, 183);
            l4.IsActive = true;
            Leaf l5 = new Leaf();
            l5.Position = new Vector2(46, 71);
            l5.IsActive = true;
            Leaf l6 = new Leaf();
            l6.Position = new Vector2(166, 232);
            l6.IsActive = true;
            Leaf l7 = new Leaf();
            l7.Position = new Vector2(235, 120);
            l7.IsActive = true;
            Leaf l8 = new Leaf();
            l8.Position = new Vector2(99, 235);
            l8.IsActive = true;
            Leaf l9 = new Leaf();
            l9.Position = new Vector2(100, 258);
            l9.IsActive = true;
            Leaf l10 = new Leaf();
            l10.Position = new Vector2(135, 221);
            l10.IsActive = true;
            Leaf l11 = new Leaf();
            l11.Position = new Vector2(156, 260);
            l11.IsActive = true;
            Leaf l12 = new Leaf();
            l12.Position = new Vector2(135, 213);
            l12.IsActive = true;
            Leaf l13 = new Leaf();
            l13.Position = new Vector2(163, 225);
            l13.IsActive = true;
            Leaf l14 = new Leaf();
            l14.Position = new Vector2(200, 210);
            l14.IsActive = true;
            Leaf l15 = new Leaf();
            l15.Position = new Vector2(325, 150);
            l15.IsActive = true;
            Leaf l16 = new Leaf();
            l16.Position = new Vector2(43, 158);
            l16.IsActive = true;
            Leaf l17 = new Leaf();
            l17.Position = new Vector2(80);
            l17.IsActive = true;
            Leaf l18 = new Leaf();
            l18.Position = new Vector2(22, 91);
            l18.IsActive = true;
            Leaf l19 = new Leaf();
            l1.Position = new Vector2(256, 90);
            l1.IsActive = true;
            Leaf l20 = new Leaf();
            l20.Position = new Vector2(200);
            l20.IsActive = true;
            Leaf l21 = new Leaf();
            l21.Position = new Vector2(136, 152);
            l21.IsActive = true;
            Leaf l22 = new Leaf();
            l22.Position = new Vector2(80, 168);
            l22.IsActive = true;
            Leaf l23 = new Leaf();
            l23.Position = new Vector2(90);
            l23.IsActive = true;

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

            if (player.Position.X > 295 && player.Position.Y >= 167 && player.Position.Y <= 168)
            {
                OnExit();
                Game.PreviousScene = Game.rightScene;
                Game.CurrentScene = Game.DES;
                Game.DES.GetReader();
                Game.GetGameReader();
                Game.CurrentScene.Start();
            }

            if (player.Position.X < 20)
            {
                OnExit();
                Game.PreviousScene = Game.rightScene;
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