using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam
{
    class Lore1 : Font
    {
        public bool IsAppearing;
        public bool IsIdle;
        public bool IsDisappearing;
        float timerCounter;

        public Lore1() : base("Lore1")
        {
            Type = FontType.LORE1;
            timerCounter = 0f;
        }

        public override void Update()
        {
            if (IsActive)
            {
                if (IsAppearing)
                {
                    counter += Game.DeltaTime;
                    if (counter >= 1f)
                    {
                        counter = 1f;
                        IsAppearing = false;
                        IsIdle = true;
                    }
                }
                else if (IsIdle)
                {
                    timerCounter += Game.DeltaTime;
                    if (timerCounter >= 7.5f)
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
                        FontMgr.RemoveStartScreen();
                        FontMgr.IsLore1OnScreen = false;
                        Game.IsStart = false;
                    }
                }
                sprite.SetAdditiveTint((int)(counter * 255), (int)(counter * 255), (int)(counter * 255), 0);
            }
        }
    }
}
