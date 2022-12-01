namespace Mission_PrincessRescue
{
    class Chest : GameObject
    {
        private SoundEmitter chestOpens;

        public Chest(string textureName = "Chest") : base(textureName, DrawLayer.Middleground)
        {
            RigidBody = new RigidBody(this);
            RigidBody.Type = RigidBodyType.Item;
            RigidBody.Collider = ColliderFactory.CreateBoxFor(this);
            RigidBody.AddCollisionType(RigidBodyType.Player);
        }

        public override void Update()
        {
            if (IsActive)
                base.Update();
        }

        public override void OnCollide(Collision collisionInfo)
        {
            base.OnCollide(collisionInfo);

            IsActive = false;

            if (Game.CurrentScene == Game.spawnScene)
                ((SpawnScene)Game.CurrentScene).ShowChest = false;


            if (Game.CurrentScene == Game.leftScene)
                ((LeftScene)Game.CurrentScene).ShowChest = false;


            if (Game.CurrentScene == Game.rightScene)
                ((RightScene)Game.CurrentScene).ShowChest = false;


            if (Game.CurrentScene == Game.HLS)
                ((HouseLeftScene)Game.CurrentScene).ShowChest = false;


            if (Game.CurrentScene == Game.D1S)
                ((Dungeon1Scene)Game.CurrentScene).ShowChest = false;


            if (Game.CurrentScene == Game.DES)
                ((DungeonEntranceScene)Game.CurrentScene).ShowChest = false;


            if (Game.CurrentScene == Game.DCS)
                ((DungeonCorridorScene)Game.CurrentScene).ShowChest = false;


            if (Game.CurrentScene == Game.DBSUpdated)
                ((DungeonBossSceneUpdated)Game.CurrentScene).ShowChest = false;

            chestOpens = new SoundEmitter(this, "ChestOpening");
            chestOpens.Play();
        }

    }
}