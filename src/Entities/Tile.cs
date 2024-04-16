using System.Numerics;
using Raylib_cs;

public class Tile
{
    public Vector2 position;
    public Texture2D sprite;
    public int size;
    public Color color;
    public int type;

    private string tileDirectory;

    public Tile(int tileSize, int type)
    {
        this.type = type;
        tileDirectory = $"res/{GetType().Name}/{GetType().Name}_{type}.png";
        sprite = Raylib.LoadTexture(tileDirectory);
        size = tileSize;
        color = Color.White;
    }

    ~Tile()
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