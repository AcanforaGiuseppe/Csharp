using Aiv.Fast2D;
using OpenTK;

namespace Mission_PrincessRescue
{
    class Dungeon1Scene : Scene
    {
        protected Sprite pauseSprite;
        protected Vector4 pauseColor;

        public Player player;
        public float blockUnitWidth;
        public float blockUnitHeight;
        public bool updated;
        public bool ShowChest = true;

        private Lamp lamp1;
        private Lamp lamp2;
        private Skull s1;
        private Skull s2;
        private Skull s3;
        private Skull s4;
        private Skull s5;
        private Fire fire1;
        private Fire fire2;
        private Fire fire3;
        private Fire fire4;
        private Blood blood1;
        private Blood blood2;
        private Blood blood3;
        private Chest chest;
        private Bow bow1;
        private Bow bow2;
        private Bones b1;
        private Bones b2;
        private Bones b3;
        private Bones b4;
        private Bones b5;
        private Bones b6;
        private Bones b7;
        private Bones b8;
        private Bones b9;
        private Enemy e1;
        private Enemy e2;
        private Enemy e3;

        public override void GetReader()
        {
            reader = new TmxReader("Assets/Tiled/dungeon1.tmx");
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
            b1 = new Bones();
            b1.Position = new Vector2(392, 40);
            b1.Sprite.scale = new Vector2(1.7f);
            b2 = new Bones();
            b2.Position = new Vector2(168, 72);
            b3 = new Bones();
            b3.Position = new Vector2(136, 168);
            b3.Sprite.scale = new Vector2(0.4f);
            b4 = new Bones();
            b4.Position = new Vector2(156, 168);
            b4.Sprite.scale = new Vector2(0.4f);
            b5 = new Bones();
            b5.Position = new Vector2(180, 152);
            b5.Sprite.scale = new Vector2(0.4f);
            b6 = new Bones();
            b6.Position = new Vector2(310, 160);
            b6.Sprite.scale = new Vector2(1.4f);
            b7 = new Bones();
            b7.Position = new Vector2(330, 160);
            b7.Sprite.scale = new Vector2(1.4f);
            b8 = new Bones();
            b8.Position = new Vector2(320, 170);
            b8.Sprite.scale = new Vector2(1.4f);
            b9 = new Bones();
            b9.Position = new Vector2(320, 150);
            b9.Sprite.scale = new Vector2(1.4f);
            bow1 = new Bow();
            bow1.Position = new Vector2(300, 290);
            bow2 = new Bow();
            bow2.Position = new Vector2(310, 275);
            bow2.Sprite.scale = new Vector2(0.6f);
            blood1 = new Blood();
            blood1.Position = new Vector2(240, 190);
            blood1.Sprite.scale = new Vector2(0.5f);
            blood2 = new Blood();
            blood2.Position = new Vector2(40, 70);
            blood2.Sprite.scale = new Vector2(0.2f);
            blood3 = new Blood();
            blood3.Position = new Vector2(390, 290);
            blood3.Sprite.scale = new Vector2(0.1f);
            lamp1 = new Lamp();
            lamp1.Position = new Vector2(300, 160);
            lamp1.Sprite.Rotation = 90;
            lamp2 = new Lamp();
            lamp2.Position = new Vector2(50, 180);
            lamp2.Sprite.Rotation = 45;
            fire1 = new Fire();
            fire1.Position = new Vector2(290, 150);
            fire2 = new Fire();
            fire2.Position = new Vector2(300, 180);
            fire3 = new Fire();
            fire3.Position = new Vector2(300, 130);
            fire4 = new Fire();
            fire4.Position = new Vector2(55, 190);
            s1 = new Skull();
            s1.Position = new Vector2(50, 290);
            s2 = new Skull();
            s2.Position = new Vector2(100, 246);
            s3 = new Skull();
            s3.Position = new Vector2(300, 273);
            s3.Sprite.scale = new Vector2(0.2f);
            s4 = new Skull();
            s4.Position = new Vector2(25, 72);
            s4.Sprite.scale = new Vector2(1.4f);
            s5 = new Skull();
            s5.Position = new Vector2(320, 160);
            s5.Sprite.scale = new Vector2(1.4f);

            if (ShowChest)
            {
                chest = new Chest();
                chest.Position = new Vector2(134, 296);
                chest.Sprite.scale = new Vector2(1.5f);
            }

            // Enemies
            e1 = new Enemy("Enemy", pathfinder);
            e1.Position = new Vector2(245, 90);
            e2 = new Enemy("Enemy", pathfinder);
            e2.Position = new Vector2(168, 250);
            e3 = new Enemy("Enemy", pathfinder);
            e3.Position = new Vector2(330, 168);

            if (Game.DBS.takenKey)
                UpdateWorld();

            // Player
            player = new Player(Game.GetController(0), 0, pathfinder);
            if (Game.PreviousScene == Game.DES)
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

        private void UpdateWorld()
        {
            b1.IsActive = true;
            b2.IsActive = true;
            b3.IsActive = true;
            b4.IsActive = true;
            b5.IsActive = true;
            b6.IsActive = true;
            b7.IsActive = true;
            b8.IsActive = true;
            b9.IsActive = true;
            bow1.IsActive = true;
            bow2.IsActive = true;
            blood1.IsActive = true;
            blood2.IsActive = true;
            blood3.IsActive = true;
            lamp1.IsActive = true;
            lamp2.IsActive = true;
            fire1.IsActive = true;
            fire2.IsActive = true;
            fire3.IsActive = true;
            fire4.IsActive = true;
            s1.IsActive = true;
            s2.IsActive = true;
            s3.IsActive = true;
            s4.IsActive = true;
            s5.IsActive = true;
            chest.IsActive = true;
            e1.IsActive = true;
            e2.IsActive = true;
            e3.IsActive = true;

            updated = true;
        }

        public override void Update()
        {
            PhysicsMgr.Update();
            UpdateMgr.Update();
            PhysicsMgr.CheckCollisions();

            if (Game.DBS.takenKey)
            {
                if (player.Position.X > 260 && player.Position.X < 330 && player.Position.Y > 115 && player.Position.Y < 200)
                    player.AddDamage(1 * (int)Game.DeltaTime + 1);

                if (player.Position.X > 25 && player.Position.X < 70 && player.Position.Y > 170 && player.Position.Y < 203)
                    player.AddDamage(1 * (int)Game.DeltaTime + 1);

                // Enemies
                if (e1.Position.X > player.Position.X - 1 && e1.Position.X < player.Position.X + 1)
                    player.AddDamage(1 * (int)Game.DeltaTime + 1);
                if (e2.Position.X > player.Position.X - 1 && e2.Position.X < player.Position.X + 1)
                    player.AddDamage(1 * (int)Game.DeltaTime + 1);
                if (e3.Position.X > player.Position.X - 1 && e3.Position.X < player.Position.X + 1)
                    player.AddDamage(1 * (int)Game.DeltaTime + 1);
            }

            if (player != null)
            {
                e1.HeadToPlayer();
                e2.HeadToPlayer();
                e3.HeadToPlayer();
            }

            if (player.Position.X < 10)
            {
                OnExit();
                Game.PreviousScene = Game.D1S;
                Game.CurrentScene = Game.DES;
                Game.DES.GetReader();
                Game.GetGameReader();
                Game.CurrentScene.Start();
            }

            if (player.Position.X > 465)
            {
                OnExit();
                Game.PreviousScene = Game.D1S;
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
            lamp1.soundEmitter.Stop();
            lamp2.soundEmitter.Stop();
            actualS.Stop();

            UpdateMgr.ClearAll();
            DrawMgr.ClearAll();
            PhysicsMgr.ClearAll();
            GfxMgr.ClearAll();

            return base.OnExit();
        }

    }
}