using System.Collections.Generic;
using Aiv.Fast2D;
using Aiv.Audio;

namespace Mission_PrincessRescue
{
    static class GfxMgr
    {
        private static Dictionary<string, Texture> textures;
        private static Dictionary<string, AudioClip> clips;

        static GfxMgr()
        {
            textures = new Dictionary<string, Texture>();
            clips = new Dictionary<string, AudioClip>();
        }

        public static AudioClip AddClip(string name, string path)
        {
            AudioClip c = new AudioClip(path);

            if (c != null)
                clips[name] = c;

            return c;
        }

        public static AudioClip GetClip(string name)
        {
            AudioClip c = null;

            if (clips.ContainsKey(name))
                c = clips[name];

            return c;
        }

        public static Texture AddTexture(string name, string path)
        {
            Texture t = new Texture(path);

            if (t != null)
                textures[name] = t;

            return t;
        }

        public static Texture GetTexture(string name)
        {
            Texture t = null;

            if (textures.ContainsKey(name))
                t = textures[name];

            return t;
        }

        public static void ClearAll()
        {
            textures.Clear();
            clips.Clear();
        }

    }
}