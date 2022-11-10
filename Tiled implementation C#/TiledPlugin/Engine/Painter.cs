using System;
using Aiv.Fast2D;
using System.Collections.Generic;

namespace TiledPlugin
{
    class Cachable
    {
        public Texture Texture { get; set; }
        public Sprite Sprite { get; set; }
    }

    class Painter
    {
        private static Dictionary<string, Cachable> cache = new Dictionary<string, Cachable>();

        private static void SetPoint(byte[] bitmap, int w, int x, int y)
        {
            int v = (y * w + x) * 4;
            bitmap[v + 0] = 255;
            bitmap[v + 1] = 0;
            bitmap[v + 2] = 0;
            bitmap[v + 3] = 255;
        }

        public static void DrawRect(int posX, int posY, int w, int h)
        {
            string key = "rect_" + w + "x" + h;
            if (!cache.ContainsKey(key))
            {
                Cachable cachable = new Cachable();

                Texture newTexture = new Texture(w, h);
                cachable.Texture = newTexture;
                cachable.Sprite = new Sprite(w, h);
                cache[key] = cachable;

                byte[] bitmap = new byte[w * h * 4];

                for (int x = 0; x < newTexture.Width; x++)
                {
                    for (int y = 0; y < newTexture.Height; y++)
                    {
                        if (x == 0 || y == 0 || x == newTexture.Width - 1 || y == newTexture.Height - 1)
                            SetPoint(bitmap, w, x, y);
                    }

                }
                newTexture.Update(bitmap);
            }

            Texture texture = cache[key].Texture;
            Sprite sprite = cache[key].Sprite;
            sprite.position.X = posX;
            sprite.position.Y = posY;
            sprite.DrawTexture(texture);
        }

        static public void DrawCircle(int centerX, int centerY, int ray)
        {
            string key = "circle_" + ray;
            if (!cache.ContainsKey(key))
            {
                Cachable cachable = new Cachable();
                int w = ray * 2 + 1; // +1 to avoid circle drawn on the edge of the texture causing array index out of bound on bitmap array
                int h = ray * 2 + 1; // +1 to avoid circle drawn on the edge of the texture causing array index out of bound on bitmap array
                Texture newTexture = new Texture(w, h);

                cachable.Texture = newTexture;
                cachable.Sprite = new Sprite(w, h);

                cache[key] = cachable;

                byte[] bitmap = new byte[w * h * 4];

                int x = 0, y = ray;
                int d = 3 - 2 * ray;
                int cX = w / 2;
                int cY = h / 2;

                while (y >= x)
                {
                    SetPoint(bitmap, w, cX + x, cY + y);
                    SetPoint(bitmap, w, cX - x, cY + y);
                    SetPoint(bitmap, w, cX + x, cY - y);
                    SetPoint(bitmap, w, cX - x, cY - y);
                    SetPoint(bitmap, w, cX + y, cY + x);
                    SetPoint(bitmap, w, cX - y, cY + x);
                    SetPoint(bitmap, w, cX + y, cY - x);
                    SetPoint(bitmap, w, cX - y, cY - x);

                    if (d > 0)
                    {
                        d = d + 4 * (x - y) + 10;
                        y--;
                        x++;
                    }
                    else
                    {
                        d = d + 4 * x + 6;
                        x++;
                    }
                }
                newTexture.Update(bitmap);
            }
            Texture texture = cache[key].Texture;
            Sprite sprite = cache[key].Sprite;
            sprite.position.X = centerX - (sprite.Width / 2);
            sprite.position.Y = centerY - (sprite.Height / 2);
            sprite.DrawTexture(texture);
        }

    }
}