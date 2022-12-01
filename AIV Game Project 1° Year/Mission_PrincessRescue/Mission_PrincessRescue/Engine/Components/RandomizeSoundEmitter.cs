using System.Collections.Generic;
using Aiv.Audio;

namespace Mission_PrincessRescue
{
    class RandomizeSoundEmitter : Component
    {
        AudioSource source;
        List<AudioClip> clips;

        public float Volume
        {
            get { return source.Volume; }
            set { source.Volume = value; }
        }

        public RandomizeSoundEmitter(GameObject owner) : base(owner)
        {
            source = new AudioSource();
            clips = new List<AudioClip>();
        }

        public void AddClip(string clipName)
        {
            clips.Add(GfxMgr.GetClip(clipName));
        }

        public void Play(float volume)
        {
            source.Volume = volume;
            RandomizePitch();
            source.Play(GetRandomClip());
        }

        public void Play()
        {
            RandomizePitch();
            source.Play(GetRandomClip());
        }

        protected void RandomizePitch()
        {
            source.Pitch = RandomGenerator.GetRandomFloat() * 0.4f + 0.8f;
        }

        protected AudioClip GetRandomClip()
        {
            return clips[RandomGenerator.GetRandomInt(0, clips.Count)];
        }

    }
}