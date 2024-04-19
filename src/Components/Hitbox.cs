using System.Numerics;
using Raylib_cs;

public class Hitbox
{
    public Vector2 Size { get; private set; }
    public Vector2 Position { get; set; }

    public Hitbox(Vector2 size, Vector2 position)
    {
        Size = size;
        Position = position;
    }

    public Rectangle BoundingBox
    {
        get
        {
            return new Rectangle(Position.X - (Size.X / 2), Position.Y - (Size.Y / 2), Size.X, Size.Y);
        }
    }

    public void Update(Vector2 position)
    {
        Position = position;
    }

    public void Draw(Color color)
    {
        Raylib.DrawRectangleLinesEx(BoundingBox, 1, Color.Red);
    }
}