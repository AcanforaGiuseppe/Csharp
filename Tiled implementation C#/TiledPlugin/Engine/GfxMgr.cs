﻿using Aiv.Fast2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiledPlugin
{
    static class GfxMgr
    {
        private static Dictionary<string, Texture> textures;

        static GfxMgr()
        {
            textures = new Dictionary<string, Texture>();
        }

        public static void AddTexture(string name, string path)
        {
            textures.Add(name, new Texture(path));
        }

        public static Texture GetTexture(string name)
        {
            Texture t = null;
            if (textures.ContainsKey(name))
                t = textures[name];

            return t;
        }

    }
}