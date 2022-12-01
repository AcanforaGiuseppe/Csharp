using Aiv.Audio;

namespace Mission_PrincessRescue
{
    class SoundEmitter : Component
    {
        AudioSource source;
        AudioClip clip;

        public float Volume
        {
            get { return source.Volume; }
            set { source.Volume = value; }
        }
        public float Pitch
        {
            get { return source.Pitch; }
            set { source.Pitch = value; }
        }

        public SoundEmitter(object owner, string clipName) : base(owner)
        {
            source = new AudioSource();
            clip = GfxMgr.GetClip(clipName);
        }

        public void Play(float volume, float pitch = 1f)
        {
            source.Volume = volume;
            source.Pitch = pitch;
            source.Play(clip);
        }

        public void Play()
        {
            source.Play(clip);
        }

        public void Stop()
        {
            source.Stop();
        }

    }
}