using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam
{
    class Damage4 : DamageFont
    {
        public Damage4() : base("Death")
        {
            Type = FontType.DAMAGE4;
        }

        public override void Update()
        {
            if (IsActive)
            {
                if (IsDisappearing)
                {
                    counter -= Game.DeltaTime;
                    if (counter < 0)
                    {
                        counter = 0;
                        IsDisappearing = false;
                        FontMgr.LastLife = false;
                        FontMgr.AreDamageFontsDissolving = false;
                        FontMgr.RestoreFont(this);
                    }
                }
                else
                {
                    counter += Game.DeltaTime;
                    if (counter >= 1f)
                    {
                        counter = 1f;
                    }
                }
                sprite.SetAdditiveTint((int)(counter * 255), (int)(counter * 255), (int)(counter * 255), 0);
            }
        }
    }
}
