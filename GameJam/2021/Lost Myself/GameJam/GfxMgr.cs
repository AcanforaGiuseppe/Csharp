using Aiv.Audio;
using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam
{
    static class GfxMgr
    {
        private static Dictionary<string, Texture> textures;
        private static Dictionary<string, AudioClip> audioClips;

        static GfxMgr()
        {
            textures = new Dictionary<string, Texture>();
            audioClips = new Dictionary<string, AudioClip>();
        }

        // TEXTURES
        public static void AddTexture(string textureName)
        {
            textures.Add(Path.GetFileNameWithoutExtension(textureName), new Texture($"Assets/{textureName}"));
        }

        // AUDIO
        public static void AddClip(string clipName)
        {
            audioClips.Add(Path.GetFileNameWithoutExtension(clipName), new AudioClip($"Assets/Audio/{clipName}"));
        }

        public static Texture GetTexture(string key)
        {
            if (textures.ContainsKey(key))
                return textures[key];
            else
                return null;
        }

        public static AudioClip GetAudioClip(string key)
        {
            if (audioClips.ContainsKey(key))
                return audioClips[key];
            else
                return null;
        }

        public static void Clear()
        {
            textures.Clear();
            audioClips.Clear();
        }
    }
}
