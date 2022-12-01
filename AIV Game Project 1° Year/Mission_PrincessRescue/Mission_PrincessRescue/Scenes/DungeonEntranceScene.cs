using System;
using Aiv.Fast2D;
using OpenTK;

namespace Mission_PrincessRescue
{
    class DungeonEntranceScene : Scene
    {
        protected Sprite pauseSprite;
        protected Vector4 pauseColor;

        public Player player;
        public float blockUnitWidth;
        public float blockUnitHeight;
        public bool ShowChest = true;
        public bool updated;

        private Lamp lamp;
        private Fire fire;
        private Blood blood1;
        private Blood blood2;
        private Bow bow1;
        private Bow bow2;
        private Bones b1;
        private Bones b2;
        private Bones b3;
        private Bones b4;
        private Bones b5;
        private Chest chest;
        private Enemy e1;
        private Enemy e2;
        private Enemy e3;
        private Enemy e4;
        private Enemy e5;
        private Enemy e6;

        public override void GetReader()
        {
            reader = new TmxReader("Assets/Tiled/dungeon.tmx");
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
            b4 = new Bones();
            b4.Position = new Vector2(376, 264);
            b4.Sprite.scale = new Vector2(1.3f);
            b5 = new Bones();
            b5.Position = new Vector2(24, 295);
            bow1 = new Bow();
            bow1.Position = new Vector2(40, 295);
            bow1.Sprite.scale = new Vector2(2);
            bow2 = new Bow();
            bow2.Position = new Vector2(300, 100);
            blood1 = new Blood();
            blood1.Position = new Vector2(100, 240);
            blood1.Sprite.scale = new Vector2(0.3f);
            blood2 = new Blood();
            blood2.Position = new Vector2(350, 80);
            blood2.Sprite.scale = new Vector2(0.3f);
            lamp = new Lamp();
            lamp.Position = new Vector2(263, 232);
            lamp.Sprite.Rotation = 30;
            fire = new Fire();
            fire.Position = new Vector2(260, 228);

            if (ShowChest)
            {
                chest = new Chest();
                chest.Position = new Vector2(450, 25);
                chest.Sprite.scale = new Vector2(1.5f);
            }

            // Enemies
            e1 = new Enemy("Enemy", pathfinder);
            e1.Position = new Vector2(500, -50);
            e2 = new Enemy("Enemy", pathfinder);
            e2.Position = new Vector2(250, -50);
            e3 = new Enemy("Enemy", pathfinder);
            e3.Position = new Vector2(-50, -50);
            e4 = new Enemy("Enemy", pathfinder);
            e4.Position = new Vector2(500, 350);
            e5 = new Enemy("Enemy", pathfinder);
            e5.Position = new Vector2(250, 350);
            e6 = new Enemy("Enemy", pathfinder);
            e6.Position = new Vector2(-50, 350);

            if (Game.DBS.takenKey)
                UpdateWorld();

            // Player
            player = new Player(Game.GetController(0), 0, pathfinder);
            if (Game.PreviousScene == Game.rightScene)
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

            // Enemies DMG
            if (Game.DBS.takenKey)
            {
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
            }

            if (player != null)
            {
                e1.HeadToPlayer();
                e2.HeadToPlayer();
                e3.HeadToPlayer();
                e4.HeadToPlayer();
                e5.HeadToPlayer();
                e6.HeadToPlayer();
            }

            // Fire Collision
            if (Game.DBS.takenKey)
                if (player.Position.X > 230 && player.Position.X < 280 && player.Position.Y > 210 && player.Position.Y < 235)
                    player.AddDamage(1 * (int)Game.DeltaTime + 1);

            if (player.Position.X < 10)
            {
                OnExit();
                Game.PreviousScene = Game.DES;
                Game.CurrentScene = Game.rightScene;
                Game.rightScene.GetReader();
                Game.GetGameReader();
                Game.CurrentScene.Start();
            }

            if (player.Position.X > 465)
            {
                OnExit();
                Game.PreviousScene = Game.DES;
                Game.CurrentScene = Game.D1S;
                Game.D1S.GetReader();
                Game.GetGameReader();
                Game.CurrentScene.Start();
            }
        }

        private void UpdateWorld()
        {
            b1.IsActive = true;
            b2.IsActive = true;
            b3.IsActive = true;
            b4.IsActive = true;
            b5.IsActive = true;
            bow1.IsActive = true;
            bow2.IsActive = true;
            blood1.IsActive = true;
            blood2.IsActive = true;
            lamp.IsActive = true;
            fire.IsActive = true;
            chest.IsActive = true;
            e1.IsActive = true;
            e2.IsActive = true;
            e3.IsActive = true;
            e4.IsActive = true;
            e5.IsActive = true;
            e6.IsActive = true;

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
            lamp.soundEmitter.Stop();
            actualS.Stop();

            UpdateMgr.ClearAll();
            DrawMgr.ClearAll();
            PhysicsMgr.ClearAll();
            GfxMgr.ClearAll();

            return base.OnExit();
        }

    }
}