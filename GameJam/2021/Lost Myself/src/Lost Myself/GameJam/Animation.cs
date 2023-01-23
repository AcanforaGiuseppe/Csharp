using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam
{
    class Animation
    {
        int numFrames;
        int width;
        float frameDuration;
        int currentFrameIndex;
        float counter;

        public bool Loop { get; set; }
        public bool IsPlaying { get; private set; }

        private int currentFrame
        {
            get
            {
                return currentFrameIndex;
            }
            set
            {
                currentFrameIndex = value;

                if (currentFrameIndex >= numFrames)
                    OnAnimationEnds();
            }
        }


        public Animation(int numFrames, int width, float fps)
        {
            Loop = true;
            IsPlaying = true;
            this.numFrames = numFrames;
            this.width = width;

            if (fps > 0.0f)
                frameDuration = 1 / fps;
            else
                frameDuration = 0.0f;
        }



        private void OnAnimationEnds()
        {
            if (Loop)
                currentFrame = 0;
            else
                Pause();
        }

        public void Update()
        {

            if (IsPlaying)
            {
                if (frameDuration != 0.0f)
                {
                    counter += Game.DeltaTime;

                    if (counter >= frameDuration)
                    {
                        counter = 0;
                        currentFrame = (currentFrame + 1);// % numFrames;
                    }
                }
                else
                {
                    currentFrame = 0;
                }
            }
        }

        public int GetCurrentFrameOffset()
        {
            return currentFrame * width;
        }

        public void Start()
        {
            IsPlaying = true;
        }

        public void Restart()
        {
            currentFrame = 0;
            IsPlaying = true;
        }

        public void Stop()
        {
            currentFrame = 0;
            IsPlaying = false;
        }
        public void Pause()
        {
            IsPlaying = false;
        }
    }
    //class Animation
    //{
    //    private Texture[] frames;
    //    private int currentFrameIndex;
    //    private float frameDuration;
    //    private int numFrames;
    //    private float counter;
    //    private bool isPlaying;
    //    private bool loop;

    //    public Texture CurrentTexture;

    //    public Animation(string[] textures, float fps)
    //    {
    //        frames = new Texture[textures.Length];
    //        for (int i = 0; i < frames.Length; i++)
    //        {
    //            frames[i] = new Texture(textures[i]);
    //        }
    //        currentFrameIndex = 0;
    //        CurrentTexture = frames[currentFrameIndex];
    //        numFrames = frames.Length;

    //        if (fps <= 0)
    //        {
    //            fps = 1;
    //        }

    //        frameDuration = 1 / fps;

    //        isPlaying = true;
    //        loop = true;
    //    }

    //    public void Update()
    //    {
    //        if (isPlaying)
    //        {
    //            counter += Game.Win.DeltaTime;
    //            if (counter >= frameDuration)
    //            {
    //                //currentFrameIndex = (currentFrameIndex + 1) % numFrames;
    //                currentFrameIndex++;
    //                if (currentFrameIndex >= numFrames)
    //                {
    //                    if (loop)
    //                    {
    //                        currentFrameIndex = 0;
    //                    }
    //                    else
    //                    { // freeze animation to last frame
    //                        currentFrameIndex = numFrames - 1;
    //                        isPlaying = false;
    //                    }
    //                }
    //                counter = 0;
    //                CurrentTexture = frames[currentFrameIndex];
    //            }
    //        }
    //    }

    //    public bool IsPlaying()
    //    {
    //        return isPlaying;
    //    }

    //    public void SetLoop(bool value)
    //    {
    //        loop = value;
    //    }

    //    public bool GetLoop()
    //    {
    //        return loop;
    //    }

    //    public void Play()
    //    {
    //        isPlaying = true;
    //    }
    //    public void Pause()
    //    {
    //        isPlaying = false;
    //    }

    //    public void Restart()
    //    {

    //    }

    //    public void Stop()
    //    {
    //        isPlaying = false;
    //        currentFrameIndex = 0;
    //        CurrentTexture = frames[currentFrameIndex];
    //    }

    //}
}
