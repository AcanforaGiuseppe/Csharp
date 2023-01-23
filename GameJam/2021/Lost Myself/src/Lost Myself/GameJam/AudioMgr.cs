using Aiv.Audio;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam
{
    static class AudioMgr
    {
        public static AudioSource BgAudioSource;

        public static void Init()
        {
            BgAudioSource = new AudioSource();
            BgAudioSource.Position = new Vector3(Game.Win.Width * 0.5f, Game.Win.Height * 0.5f, 0);
            BgAudioSource.Volume = 0.35f;
        }

        public static void PlayFx(string clipName, AudioSource source)
        {
            AudioClip clip = GfxMgr.GetAudioClip(clipName);
            source.Play(clip);
        }

        public static void PlayBackground(string clipName)
        {
            AudioClip clip = GfxMgr.GetAudioClip(clipName);
            BgAudioSource.Play(clip);
        }

        public static void StopBackground()
        {
            BgAudioSource.Stop();
        }

        public static void StopFx(AudioSource source)
        {
            source.Stop();
        }

        public static void StreamFx(string clipName, AudioSource source)
        {
            AudioClip clip = GfxMgr.GetAudioClip(clipName);
            source.Stream(clip, Game.Win.DeltaTime);
        }

        public static void StreamBackground(string clipName)
        {
            AudioClip clip = GfxMgr.GetAudioClip(clipName);
            BgAudioSource.Stream(clip, Game.DeltaTime);
        }


    }
}

