using Aiv.Fast2D;

namespace Mission_PrincessRescue
{
    class TileObj : GameObject
    {
        private int xOff;
        private int yOff;

        public TileObj(string texture, int posX, int posY, int tOffX, int tOffY, int width, int height) : base(texture, DrawLayer.Background)
        {
            sprite = new Sprite(width, height);
            sprite.position.X = posX;
            sprite.position.Y = posY;
            xOff = tOffX;
            yOff = tOffY;
            IsActive = true;
        }

        public override void Draw()
        {
            if (!IsActive)
                return;

            sprite.DrawTexture(texture, xOff, yOff, Width, Height);
        }

    }
}