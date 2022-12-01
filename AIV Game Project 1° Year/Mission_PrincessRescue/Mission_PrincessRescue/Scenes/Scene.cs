using System;

namespace Mission_PrincessRescue
{
    abstract class Scene
    {
        public Scene NextScene;
        public Scene PreviousScene;
        public TmxReader reader;
        public TmxCell cell;
        public SoundEmitter actualS;
        public bool IsPlaying { get; protected set; }

        public virtual void GetReader()
        {
            reader = new TmxReader("Assets/Tiled/ExamProject.tmx");
        }

        public virtual void Start()
        {
            IsPlaying = true;
        }

        public virtual Scene OnExit()
        {
            IsPlaying = false;
            return NextScene;
        }

        public void PrepareLayer(TmxLayer tileLayer, TmxTileset tileSet)
        {
            for (int i = 0; i < tileLayer.Grid.Size(); i++)
            {
                cell = tileLayer.Grid.At(i);

                if (cell == null)
                    continue;

                TileObj tileObj = new TileObj(tileSet.TilesetPath, cell.PosX, cell.PosY, cell.Type.OffX, cell.Type.OffY, cell.Type.Width, cell.Type.Height);

                string drawLayer = tileLayer.Props.Get<string>("drawLayer");
                if (drawLayer != null)
                    tileObj.Layer = (DrawLayer)Enum.Parse(typeof(DrawLayer), drawLayer);
            }
        }

        public void LoadAssets()
        {
            // Player
            GfxMgr.AddTexture("Hero", "Assets/Heroes/Hero.png");
            GfxMgr.AddTexture("Princess", "Assets/Heroes/Princess.png");
            GfxMgr.AddTexture("Dog", "Assets/Heroes/Dog.png");

            // Enemy
            GfxMgr.AddTexture("Enemy", "Assets/Heroes/Enemy.png");

            // Target
            GfxMgr.AddTexture("Target", "Assets/Cross.png");

            // Items
            GfxMgr.AddTexture("Shovel", "Assets/Items/shovel.png");
            GfxMgr.AddTexture("Key", "Assets/Items/key.png");
            GfxMgr.AddTexture("Bones", "Assets/Items/bones.png");
            GfxMgr.AddTexture("Bow", "Assets/Items/bow.png");
            GfxMgr.AddTexture("Chest", "Assets/Items/chest.png");
            GfxMgr.AddTexture("Lamp", "Assets/Items/lamp.png");
            GfxMgr.AddTexture("Leaf", "Assets/Items/leaf.png");
            GfxMgr.AddTexture("Skull", "Assets/Items/skull.png");
            GfxMgr.AddTexture("Blood", "Assets/Items/blood.png");
            GfxMgr.AddTexture("Fire", "Assets/Items/fire.png");
            GfxMgr.AddTexture("Bucket", "Assets/Items/bucket.png");
            GfxMgr.AddTexture("Falo", "Assets/Items/falo.png");
            GfxMgr.AddTexture("Fish", "Assets/Items/fish.png");
            GfxMgr.AddTexture("Ball", "Assets/Items/ball.png");
            GfxMgr.AddTexture("Umbrella", "Assets/Items/umbrella.png");

            // Music
            GfxMgr.AddClip("Beach", "Assets/Music/beach.ogg");
            GfxMgr.AddClip("City", "Assets/Music/city.ogg");
            GfxMgr.AddClip("Dungeon", "Assets/Music/dungeon.ogg");
            GfxMgr.AddClip("Fire", "Assets/Music/fire.ogg");
            GfxMgr.AddClip("Forest", "Assets/Music/forest.ogg");
            GfxMgr.AddClip("Victory", "Assets/Music/victory.ogg");
            GfxMgr.AddClip("ChestOpening", "Assets/Music/chest_opening.ogg");
            GfxMgr.AddClip("Dog_Sound", "Assets/Music/Dog_Sound.ogg");

            // Bar frames
            GfxMgr.AddTexture("barFrame", "Assets/loadingBar_frame.png");
            GfxMgr.AddTexture("blueBar", "Assets/loadingBar_bar.png");
        }

        public virtual void Update()
        { }

        public abstract void Input();
        public abstract void Draw();

    }
}