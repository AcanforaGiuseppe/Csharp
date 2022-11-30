using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam
{
    class Lore2 : Font
    {
        public bool IsWaitingAppearing;
        public bool IsAppearing;
        public bool IsIdle;
        public bool IsDisappearing;
        float timerCounter = 1f;

        public Lore2() : base("Lore2")
        {
            Type = FontType.LORE2;
        }

        public override void Update()
        {
            if (IsActive)
            {
                if (IsWaitingAppearing)
                {
                    timerCounter -= Game.DeltaTime;
                    if (timerCounter <= 0)
                    {
                        IsWaitingAppearing = false;
                        IsAppearing = true;
                    }
                }
                else if (IsAppearing)
                {
                    counter += Game.DeltaTime;
                    if (counter >= 1f)
                    {
                        counter = 1f;
                        IsAppearing = false;
                        IsIdle = true;
                        timerCounter = 8f;
                    }
                }
                else if (IsIdle)
                {
                    timerCounter -= Game.DeltaTime;
                    if (timerCounter <= 0)
                    {
                        IsIdle = false;
                        IsDisappearing = true;
                    }
                }
                else if (IsDisappearing)
                {
                    counter -= Game.DeltaTime;
                    if (counter <= 0)
                    {
                        counter = 0;
                        IsDisappearing = false;
                        FontMgr.RestoreFont(this);
                        FontMgr.IsLore2OnScreen = false;
                    }
                }
                sprite.SetAdditiveTint((int)(counter * 255), (int)(counter * 255), (int)(counter * 255), 0);
            }
        }
    }
}
