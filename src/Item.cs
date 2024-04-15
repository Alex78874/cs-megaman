using System.Numerics;
using Raylib_cs;

public class Item
{
    public Vector2 position;
    public Texture2D sprite;
    public int size;
    public Color color;

    private string tileDirectory;

    public Item(int tileSize, int type)
    {
        tileDirectory = $"res/{GetType().Name}/{GetType().Name}_{type}.png";
        sprite = Raylib.LoadTexture(tileDirectory);
        size = tileSize;
        color = Color.White;
    }

    ~Item()
    {
        Raylib.UnloadTexture(sprite);
    }

    public void Update()
    {
        // Nothing to update
    }

    public void Draw()
    {
        Raylib.DrawTextureV(sprite, position, color);
    }
}