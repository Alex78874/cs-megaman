using System.Numerics;
using Raylib_cs;

public class Block
{
    public Vector2 position;
    public int size;
    public Color color;

    public Block(int size)
    {
        this.size = size;
        this.color = Color.Red;

        position.X = Raylib.GetMousePosition().X;
        position.Y = Raylib.GetMousePosition().Y;
    }

    ~Block()
    {
        // Nothing to unload
    }

    public void Update()
    {
        position.X = Raylib.GetMousePosition().X;
        position.Y = Raylib.GetMousePosition().Y;
    }

    public void Draw()
    {
        Raylib.DrawRectangle((int)position.X, (int)position.Y, size, size, color);
    }
}