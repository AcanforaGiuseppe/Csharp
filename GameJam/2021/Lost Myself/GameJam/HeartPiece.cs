using Aiv.Fast2D;
using Aiv.Audio;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam
{
    class HeartPiece : GameObject
    {
        private Animation animation;
        private AudioSource source;
        public float Speed
        {
            set { rigidBody.Velocity.X = value; }
        }

        public HeartPiece() : base("star_spritesheet", 512, 512)
        {
            source = new AudioSource();
            source.Volume = 0.1f;
            sprite.position.Y = TerrainMgr.YPos - 100;
            sprite.scale = new Vector2(0.1f);
            rigidBody = new RigidBody(this);
            rigidBody.Velocity.X = Game.GlobalSpeed;
            IsActive = false;
            animation = new Animation(8, 512, 25);
            sprite.SetAdditiveTint(255, 230, 0, 0);
            UpdateMgr.AddItem(this);
        }

        public void Spawn(Vector2 heartPos)
        {
            sprite.position = heartPos;
        }

        public override void Update()
        {
            if (IsActive)
            {
                if (sprite.position.X <= Game.Player.Position.X + 50)
                {
                    AudioMgr.PlayFx("Collectible", source);
                    ObstacleMgr.RestoreHeart(this);
                    Game.bg.GainHeart();
                    ObstacleMgr.HeartsAquired++;
                    if (ObstacleMgr.HeartsAquired >= Game.HeartsRequired)
                    {
                        Game.EndGame = true;
                        ObstacleMgr.DeleteAllActiveObstacles();
                        FontMgr.DissolveDamageTexts();
                        FontMgr.Lore2();
                    }
                }
                animation.Update();
            }
        }

        public override void Draw()
        {
            if (IsActive)
                sprite.DrawTexture(texture, animation.GetCurrentFrameOffset(), 0, 512, 512);
        }
    }
}
