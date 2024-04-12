using Raylib_cs; // Assuming you have the Raylib library for C#
using System;
using System.Numerics;

public class Player
{
    private Texture2D sprite;
    private Vector2 position;
    private float orientation;
    private float walkingSpeed = 5.0f;
    private float sprintingSpeed = 10.0f;
    private float speed;
    public bool isSprinting { get; set; }

    public Player()
    {
        string imagePath = $"res/{GetType().Name}/body.png";
        sprite = Raylib.LoadTexture(imagePath);

        position.X = 100;
        position.Y = 100;
    }

    ~Player()
    {
        Raylib.UnloadTexture(sprite);
    }

    public void Draw()
    {
        Rectangle src = new Rectangle(0.0f, 0.0f, sprite.Width, sprite.Height);
        Rectangle dst = new Rectangle(position.X, position.Y, sprite.Width, sprite.Height);

        Vector2 origin = new Vector2(sprite.Width / 2, sprite.Height / 2);

        Raylib.DrawTexturePro(sprite, src, dst, origin, orientation, Color.White);
    }

    public void Move(int x, int y)
    {
        if (isSprinting)
        {
            speed = sprintingSpeed;
        }
        else
        {
            speed = walkingSpeed;
        }

        position.X += speed * x;
        position.Y += speed * y;
    }

    public void Orient(Cursor cursor)
    {
        float distX = cursor.position.X - position.X;
        float distY = cursor.position.Y - position.Y;

        orientation = -MathF.Atan2(distX, distY) * (180 / MathF.PI);
    }
}
