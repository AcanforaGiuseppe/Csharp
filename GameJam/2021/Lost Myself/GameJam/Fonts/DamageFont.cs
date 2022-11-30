using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam
{
    abstract class DamageFont : Font
    {
        public bool IsDisappearing;

        public DamageFont(string textureName) : base(textureName)
        {
        }

        public void Dissolve()
        {
            IsDisappearing = true;
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
