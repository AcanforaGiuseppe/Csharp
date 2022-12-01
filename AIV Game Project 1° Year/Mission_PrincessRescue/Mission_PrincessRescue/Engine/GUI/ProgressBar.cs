using Aiv.Fast2D;
using OpenTK;

namespace Mission_PrincessRescue
{
    class ProgressBar : GameObject
    {
        protected Sprite barSprite;
        protected Texture barTexture;
        protected Vector2 barOffset;
        protected int barWidth;

        public override Vector2 Position { get => base.Position; set { base.Position = value; barSprite.position = value + barOffset; } }

        public ProgressBar(string frameTextureName, string barTextureName, Vector2 innerBarOffset, DrawLayer layer = DrawLayer.GUI) : base(frameTextureName, layer)
        {
            sprite.pivot = Vector2.Zero;
            IsActive = true;

            barOffset = innerBarOffset;

            barTexture = GfxMgr.GetTexture(barTextureName);
            barSprite = new Sprite(Game.PixelsToUnits(barTexture.Width), Game.PixelsToUnits(barTexture.Height));
            barWidth = (int)barSprite.Width;
        }

        public virtual void Scale(float scale)
        {
            scale = MathHelper.Clamp(scale, 0, 1);
            barSprite.scale.X = scale;
            barSprite.SetMultiplyTint((1 - scale) * 50, scale * 2, scale, 1);
        }

        public override void Draw()
        {
            if (IsActive)
            {
                base.Draw();
                barSprite.DrawTexture(barTexture, 0, 0, barWidth * (int)Game.UnitSize, (int)barSprite.Height);
            }
        }

    }
}