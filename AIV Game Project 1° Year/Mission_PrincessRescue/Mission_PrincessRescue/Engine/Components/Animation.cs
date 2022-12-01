using OpenTK;

namespace Mission_PrincessRescue
{
    class Animation : Component, IUpdatable
    {
        protected int numFrames;
        protected float frameDuration;
        protected bool isPlaying;
        protected int currentFrame;
        protected float elapsedTime;

        public bool Loop;
        public Vector2 Offset;
        public int FrameWidth { get; protected set; }
        public int FrameHeight { get; protected set; }

        public Animation(GameObject owner, int numFrames, int frameWidth, int frameHeight, float framesPerSecond, float offsetY, bool loop = true) : base(owner)
        {
            this.numFrames = numFrames;
            this.FrameWidth = frameWidth;
            this.FrameHeight = frameHeight;
            Loop = loop;

            Offset = new Vector2();
            Offset.Y = offsetY;

            this.frameDuration = 1 / framesPerSecond;

            UpdateMgr.AddItem(this);
        }

        public virtual void Play()
        {
            isPlaying = true;
        }

        public virtual void StopIsPlaying()
        {
            isPlaying = false;
        }

        public virtual void Restart()
        {
            currentFrame = 0;
            isPlaying = true;
        }

        public virtual void Stop()
        {
            isPlaying = false;
            currentFrame = 0;
            elapsedTime = 0;
        }

        public virtual void Pause()
        {
            isPlaying = false;
        }

        public virtual void Update()
        {
            if (IsEnabled && isPlaying)
            {
                elapsedTime += Game.DeltaTime;

                if (elapsedTime >= frameDuration)
                {
                    currentFrame++;
                    elapsedTime = 0;

                    if (currentFrame >= numFrames)
                    {
                        if (Loop)
                        {
                            currentFrame = 0;
                        }
                        else
                        {
                            OnAnimationEnd();
                            return;
                        }
                    }
                    Offset = new Vector2(FrameWidth * currentFrame, Offset.Y);
                }
            }
        }

        protected virtual void OnAnimationEnd()
        {
            isPlaying = false;
            GameObject.IsActive = false;
        }

    }
}