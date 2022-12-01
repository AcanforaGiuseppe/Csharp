namespace Mission_PrincessRescue
{
    class Lamp : GameObject
    {
        public SoundEmitter soundEmitter;

        public Lamp(string textureName = "Lamp") : base(textureName, DrawLayer.Middleground)
        {
            soundEmitter = new SoundEmitter(this, "Fire");
            soundEmitter.Play();
        }

        public override void Update()
        {
            if (IsActive)
                base.Update();
        }

    }
}