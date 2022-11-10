namespace TiledPlugin
{
    interface IDrawable
    {
        DrawLayer Layer { get; set; }
        void Draw();
    }
}