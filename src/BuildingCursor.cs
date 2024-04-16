using System.Numerics;
using Raylib_cs;

public class BuildingCursor
{
    private Texture2D sprite;
    public Vector2 position;

    private bool isHoveringOnSquare;
    private bool hasChangedCursor;

    private float rotationAngle;
    private float scale;

    public BuildingCursor()
    {
        string imagePath = $"res/{GetType().Name}/pointer.png";
        sprite = Raylib.LoadTexture(imagePath);

        position.X = Raylib.GetMousePosition().X;
        position.Y = Raylib.GetMousePosition().Y;
    }

    ~BuildingCursor()
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

        // Check if hovering on class Square
        if (IsHoveringOnItem() && !hasChangedCursor)
        {
            SetCursor("res/Cursor/attack.png");
            hasChangedCursor = true;
            rotationAngle = 0f; // Reset rotation angle when hovering
        }
        else if (!IsHoveringOnItem() && hasChangedCursor)
        {
            SetCursor("res/Cursor/pointer.png");
            hasChangedCursor = false;
            rotationAngle = 0f; // Reset rotation angle when stop hovering
        }

        // Animate the cursor when hovering
        if (hasChangedCursor)
        {
            // scale up dans down size slowly with cosinus
            scale = 1 + (float)(Math.Cos(Raylib.GetTime() * 2) / 10);
        }
    }

    private bool IsHoveringOnItem()
    {
        // Get the position of the cursor
        Vector2 cursorPosition = new Vector2(position.X, position.Y);

        // Iterate through all the instances of the Square class
        foreach (Item item in Program.instances)
        {
            // Get the position and size of the square
            Rectangle squareBounds = new Rectangle(item.position.X, item.position.Y, item.size, item.size);
            // Check if the cursor position is within the bounds of the square
            if (Raylib.CheckCollisionPointRec(cursorPosition, squareBounds))
            {
                return true;
            }
        }

        return false;
    }

    public void Draw()
    {
        int drawX = (int)position.X;
        int drawY = (int)position.Y;

        Vector2 origin = new Vector2(sprite.Width / 2, sprite.Height / 2);
        Rectangle sourceRec = new Rectangle(0, 0, sprite.Width * scale, sprite.Height * scale);
        Rectangle destRec = new Rectangle(drawX, drawY, sprite.Width, sprite.Height);
        
        Raylib.DrawTexturePro(sprite, sourceRec, destRec, origin, rotationAngle, Color.White);
    }
}
