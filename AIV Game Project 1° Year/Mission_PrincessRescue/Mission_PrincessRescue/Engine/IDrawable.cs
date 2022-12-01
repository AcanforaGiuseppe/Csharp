namespace Mission_PrincessRescue
{
    interface IDrawable
    {
        DrawLayer Layer { get; }
        void Draw();
    }
}