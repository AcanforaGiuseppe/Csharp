using OpenTK;

namespace Mission_PrincessRescue
{
    class TextChar : GameObject
    {
        protected char character;
        protected Vector2 textureOffset;
        protected Font font;

        public char Character { get { return character; } set { character = value; ComputeOffset(); } }

        public TextChar(Vector2 spritePosition, char character, Font font) : base(font.TextureName, DrawLayer.GUI, font.CharacterUnitsWidth, font.CharacterUnitsHeight)
        {
            sprite.position = spritePosition;
            this.font = font;
            Character = character;
            sprite.pivot = Vector2.Zero;
        }

        protected void ComputeOffset()
        {
            textureOffset = font.GetOffset(character);
        }

        public override void Draw()
        {
            if (IsActive)
                sprite.DrawTexture(texture, (int)textureOffset.X, (int)textureOffset.Y, font.CharacterWidth, font.CharacterHeight);
        }

    }
}