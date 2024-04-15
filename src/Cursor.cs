using System.Numerics;
using Raylib_cs;

public class Cursor
{
    private Texture2D sprite;
    public Vector2 position;

    public Cursor()
    {
        string imagePath = $"res/{GetType().Name}/pointer.png";
        sprite = Raylib.LoadTexture(imagePath);

        position.X = Raylib.GetMousePosition().X;
        position.Y = Raylib.GetMousePosition().Y;
    }

    ~Cursor()
    {
        Raylib.UnloadTexture(sprite);
    }

    public void SetCursor(string path)
    {
        sprite = Raylib.LoadTexture(path);
    }

    public void Update()
    {
        position.X = Raylib.GetMousePosition().X;
        position.Y = Raylib.GetMousePosition().Y;
    }

    public void Draw()
    {
        Raylib.DrawTexture(sprite, (int)position.X, (int)position.Y, Raylib_cs.Color.White);
    }
}
