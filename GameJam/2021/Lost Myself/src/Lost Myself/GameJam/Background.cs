using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam
{
    class Background : GameObject
    {
        static int r = 36 - 107;
        static int g = 162 - 107;
        static int b = 197 - 107;
        //static int r = 0 - 107;
        //static int g = 255 - 107;
        //static int b = 0 - 107;
        static float mult = 0f;
        static float currentMax = 0f;

        static bool reset;

        public Background() : base("BackgroundGrey")
        {
            sprite.pivot = Vector2.Zero;
            sprite.position = new Vector2(0, -160);
            sprite.scale = new Vector2(0.5f);
            IsActive = true;
            reset = false;

            UpdateMgr.AddItem(this);
        }

        public void GainHeart()
        {
            currentMax += 0.85f / Game.HeartsRequired;
            if (currentMax >= 0.85f)
            {
                currentMax = 0.85f;
            }
        }

        public void Reset()
        {
            reset = true;
            currentMax = 0f;
        }

        public override void Update()
        {
            if (reset)
            {
                mult -= 0.55f * Game.DeltaTime;
                if (mult <= 0)
                {
                    mult = 0;
                    reset = false;
                }
            }
            else
            {
                if (mult <= currentMax)
                {
                    mult += 0.35f * Game.DeltaTime;
                }
            }
            sprite.SetAdditiveTint((int)(r * mult), (int)(g * mult), (int)(b * mult), 0);
        }
    }
}
