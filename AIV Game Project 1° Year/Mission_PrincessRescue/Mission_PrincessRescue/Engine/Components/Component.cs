namespace Mission_PrincessRescue
{
    enum ComponentType { SoundEmitter, RandomizeSoundEmitter, Animation }
    abstract class Component
    {
        protected bool isEnabled;

        public GameObject GameObject { get; protected set; }
        public object Obj { get; protected set; }
        public bool IsEnabled
        {
            get { return isEnabled && GameObject.IsActive; }
            set { isEnabled = value; }
        }

        public Component(GameObject owner)
        {
            GameObject = owner;
        }

        public Component(object owner)
        {
            Obj = owner;
        }

    }
}