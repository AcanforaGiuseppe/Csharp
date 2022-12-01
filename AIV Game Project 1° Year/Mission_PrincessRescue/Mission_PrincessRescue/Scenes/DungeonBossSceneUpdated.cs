using System;
using Aiv.Fast2D;
using OpenTK;

namespace Mission_PrincessRescue
{
    class DungeonBossSceneUpdated : Scene
    {
        public float blockUnitWidth;
        public float blockUnitHeight;
        public Player player;
        public Key key;
        public bool updated;
        public bool takenKey = false;
        public bool ShowChest = true;

        protected Sprite pauseSprite;
        protected Vector4 pauseColor;

        private Lamp lamp1;
        private Lamp lamp2;
        private Lamp lamp3;
        private Lamp lamp4;
        private Lamp lamp5;
        private Blood b3;
        private Blood b4;
        private Blood b2;
        private Blood b1;
        private Fire fire1;
        private Fire fire2;
        private Skull sk1;
        private Skull sk2;
        private Skull sk3;
        private Skull sk4;
        private Chest chest;
        private Bow bow1;
        private Bow bow2;
        private Bow bow3;
        private Bones bones1;
        private Bones bones2;
        private Bones bones3;
        private Bones bones4;
        private Bones bones5;
        private Bow bow4;
        private Enemy e1;
        private Enemy e2;
        private Enemy e3;
        private Enemy e4;
        private Enemy e5;
        private Enemy e6;

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
            b1 = new Blood();
            b1.Position = new Vector2(140, 170);
            b1.Sprite.scale = new Vector2(0.6f);
            b2 = new Blood();
            b2.Position = new Vector2(350, 170);
            b2.Sprite.scale = new Vector2(0.5f);
            b3 = new Blood();
            b3.Position = new Vector2(350, 60);
            b3.Sprite.scale = new Vector2(0.4f);
            b4 = new Blood();
            b4.Position = new Vector2(350, 270);
            b4.Sprite.scale = new Vector2(0.2f);
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
            fire1 = new Fire();
            fire1.Position = new Vector2(350, 20);
            fire2 = new Fire();
            fire2.Position = new Vector2(380, 230);
            fire2.Sprite.Rotation = 30;
            sk1 = new Skull();
            sk1.Position = new Vector2(150, 170);
            sk2 = new Skull();
            sk2.Position = new Vector2(40, 230);
            sk3 = new Skull();
            sk3.Position = new Vector2(60, 100);
            sk4 = new Skull();
            sk4.Position = new Vector2(360, 60);
            sk4.Sprite.scale = new Vector2(2);
            bow1 = new Bow();
            bow1.Position = new Vector2(390, 70);
            bow1.Sprite.scale = new Vector2(2);
            bow2 = new Bow();
            bow2.Position = new Vector2(50, 200);
            bow3 = new Bow();
            bow3.Position = new Vector2(100, 260);
            bones1 = new Bones();
            bones1.Position = new Vector2(30, 50);
            bones2 = new Bones();
            bones2.Position = new Vector2(150, 50);
            bones3 = new Bones();
            bones3.Position = new Vector2(223, 162);
            bones3.Sprite.scale = new Vector2(3);
            bones4 = new Bones();
            bones4.Position = new Vector2(300, 200);
            bones4.Sprite.scale = new Vector2(0.6f);
            bones5 = new Bones();
            bones5.Position = new Vector2(98, 250);
            bones5.Sprite.scale = new Vector2(1.5f);
            bow4 = new Bow();
            bow4.Position = new Vector2(300, 250);
            bow4.Sprite.scale = new Vector2(2.2f);

            if (ShowChest)
            {
                chest = new Chest();
                chest.Position = new Vector2(100, 25);
                chest.Sprite.scale = new Vector2(1.5f);
            }

            // Player
            player = new Player(Game.GetController(0), 0, pathfinder);
            if (Game.PreviousScene == Game.DCS)
                player.Position = new Vector2(40, 160);
            else
                player.Position = new Vector2(352, 160);
            player.IsActive = true;

            // Enemies
            e1 = new Enemy("Enemy", pathfinder);
            e1.Position = new Vector2(350, -50);
            e2 = new Enemy("Enemy", pathfinder);
            e2.Position = new Vector2(350, 350);
            e3 = new Enemy("Enemy", pathfinder);
            e3.Position = new Vector2(-50, -25);
            e4 = new Enemy("Enemy", pathfinder);
            e4.Position = new Vector2(-50, 175);
            e5 = new Enemy("Enemy", pathfinder);
            e5.Position = new Vector2(-50, 350);
            e6 = new Enemy("Enemy", pathfinder);
            e6.Position = new Vector2(500, 160);

            UpdateWorld();

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
            lamp1.IsActive = true;
            lamp2.IsActive = true;
            lamp3.IsActive = true;
            lamp4.IsActive = true;
            lamp5.IsActive = true;
            fire1.IsActive = true;
            fire2.IsActive = true;
            sk1.IsActive = true;
            sk2.IsActive = true;
            sk3.IsActive = true;
            sk4.IsActive = true;
            chest.IsActive = true;
            bow1.IsActive = true;
            bow2.IsActive = true;
            bow3.IsActive = true;
            bow4.IsActive = true;
            bones1.IsActive = true;
            bones2.IsActive = true;
            bones3.IsActive = true;
            bones4.IsActive = true;
            bones5.IsActive = true;
            e1.IsActive = true;
            e2.IsActive = true;
            e3.IsActive = true;
            e4.IsActive = true;
            e5.IsActive = true;
            e6.IsActive = true;

            updated = true;
        }

        public override void Update()
        {
            PhysicsMgr.Update();
            UpdateMgr.Update();
            PhysicsMgr.CheckCollisions();

            // Follow Player
            if (player != null)
            {
                e1.HeadToPlayer();
                e2.HeadToPlayer();
                e3.HeadToPlayer();
                e4.HeadToPlayer();
                e5.HeadToPlayer();
                e6.HeadToPlayer();
            }

            // Enemies DMG
            if (e1.Position.X > player.Position.X - 1 && e1.Position.X < player.Position.X + 1)
                player.AddDamage(1 * (int)Game.DeltaTime + 1);
            if (e2.Position.X > player.Position.X - 1 && e2.Position.X < player.Position.X + 1)
                player.AddDamage(1 * (int)Game.DeltaTime + 1);
            if (e3.Position.X > player.Position.X - 1 && e3.Position.X < player.Position.X + 1)
                player.AddDamage(1 * (int)Game.DeltaTime + 1);
            if (e4.Position.X > player.Position.X - 1 && e4.Position.X < player.Position.X + 1)
                player.AddDamage(1 * (int)Game.DeltaTime + 1);
            if (e5.Position.X > player.Position.X - 1 && e5.Position.X < player.Position.X + 1)
                player.AddDamage(1 * (int)Game.DeltaTime + 1);
            if (e6.Position.X > player.Position.X - 1 && e6.Position.X < player.Position.X + 1)
                player.AddDamage(1 * (int)Game.DeltaTime + 1);

            // Fire Collision
            if (player.Position.X > 325 && player.Position.X < 375 && player.Position.Y < 45)
                player.AddDamage(1 * (int)Game.DeltaTime + 1);
            if (player.Position.X > 360 && player.Position.X < 405 && player.Position.Y > 200 && player.Position.Y < 260)
                player.AddDamage(1 * (int)Game.DeltaTime + 1);

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