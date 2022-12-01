using Aiv.Fast2D;
using OpenTK;

namespace Mission_PrincessRescue
{
    class LeftScene : Scene
    {
        public bool ShowChest = true;
        public Player player;

        protected Sprite pauseSprite;
        protected Vector4 pauseColor;

        private Dog dog;
        private Umbrella umb1;
        private Chest chest1;
        private Falo falo1;
        private Falo falo2;
        private Falo falo3;
        private Falo falo4;
        private Ball ball;

        public override void GetReader()
        {
            reader = new TmxReader("Assets/Tiled/beach.tmx");
        }

        public override void Start()
        {
            LoadAssets();
            actualS = new SoundEmitter(this, "Beach");
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
            player.Position = new Vector2(440, 152);
            player.IsActive = true;

            // Dog
            dog = new Dog(pathfinder);
            dog.Position = new Vector2(250, 100);
            dog.soundEmitter.Play(0.05f);
            dog.IsActive = true;

            // Items
            Shovel s1 = new Shovel();
            s1.Position = new Vector2(328, 215);
            s1.IsActive = true;
            Shovel s2 = new Shovel();
            s2.Position = new Vector2(215, 120);
            s2.IsActive = true;
            Shovel s3 = new Shovel();
            s3.Position = new Vector2(423, 40);
            s3.IsActive = true;
            Bucket b1 = new Bucket();
            b1.Position = new Vector2(420, 50);
            b1.Sprite.scale = new Vector2(0.015f);
            b1.IsActive = true;
            Bucket b2 = new Bucket();
            b2.Position = new Vector2(180, 248);
            b2.Sprite.scale = new Vector2(0.015f);
            b2.IsActive = true;
            falo1 = new Falo();
            falo1.Position = new Vector2(120, 200);
            falo1.IsActive = true;
            falo2 = new Falo();
            falo2.Position = new Vector2(150, 200);
            falo2.IsActive = true;
            falo3 = new Falo();
            falo3.Position = new Vector2(135, 220);
            falo3.IsActive = true;
            falo4 = new Falo();
            falo4.Position = new Vector2(135, 180);
            falo4.IsActive = true;
            Fish fish1 = new Fish();
            fish1.Position = new Vector2(95, 190);
            fish1.IsActive = true;
            Fish fish2 = new Fish();
            fish2.Position = new Vector2(95, 205);
            fish2.IsActive = true;
            Fish fish3 = new Fish();
            fish3.Position = new Vector2(95, 220);
            fish3.IsActive = true;
            umb1 = new Umbrella();
            umb1.Position = new Vector2(310, 150);
            umb1.Sprite.Rotation = 14;
            umb1.IsActive = true;
            Umbrella umb2 = new Umbrella();
            umb2.Position = new Vector2(280, 200);
            umb2.Sprite.Rotation = 51;
            umb2.IsActive = true;
            ball = new Ball();
            ball.Position = new Vector2(200, 168);
            ball.IsActive = true;

            if (ShowChest)
            {
                chest1 = new Chest();
                chest1.Position = new Vector2(360, 8);
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

            // Fire Collision
            if (player.Position.X > 118 && player.Position.X < 153 && player.Position.Y > 180 && player.Position.Y < 230)
                player.AddDamage(1 * (int)Game.DeltaTime + 1);

            if (player.Position.X > 465)
            {
                OnExit();
                Game.PreviousScene = Game.leftScene;
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
            dog.soundEmitter.Stop();

            UpdateMgr.ClearAll();
            DrawMgr.ClearAll();
            PhysicsMgr.ClearAll();
            GfxMgr.ClearAll();

            return base.OnExit();
        }

    }
}