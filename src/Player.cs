using Raylib_cs; // Assuming you have the Raylib library for C#
using System;
using System.Numerics;

public class Player
{
    private Texture2D sprite;
    public Vector2 position;
    
    private float orientation;
    private float walkingSpeed = 5.0f;
    private float sprintingSpeed = 10.0f;
    private float speed;
    public bool isSprinting { get; set; }

    // Player movement states
    public enum MovementState
    {
        Idle,
        Walking,
        Sprinting
    }
    public MovementState movementState;


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
        try
        {
            Rectangle src = new Rectangle(0.0f, 0.0f, sprite.Width, sprite.Height);
            Rectangle dst = new Rectangle(position.X, position.Y, sprite.Width, sprite.Height);

            Vector2 origin = new Vector2(sprite.Width / 2, sprite.Height / 2);

            Raylib.DrawTexturePro(sprite, src, dst, origin, orientation, Color.White);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred in the Draw method: {ex.Message}");
        }
    }

    public void Move(int x, int y)
    {
        try
        {
            float speed = MathF.Sqrt(x * x + y * y); // Calculate speed based on x and y values

            position.X += x * Utils.DeltaTime;
            position.Y += y * Utils.DeltaTime;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred in the Move method: {ex.Message}");
        }
    }

    public void UpdateMovementState()
    {
        if (Math.Abs(position.X) > 0 || Math.Abs(position.Y) > 0)
        {
            movementState = isSprinting ? MovementState.Sprinting : MovementState.Walking;
        }
        else
        {
            movementState = MovementState.Idle;
        }
    }

    public void Orient(Cursor cursor)
    {
        try
        {
            float distX = cursor.position.X - position.X;
            float distY = cursor.position.Y - position.Y;

            orientation = -MathF.Atan2(distX, distY) * (180 / MathF.PI);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred in the Orient method: {ex.Message}");
        }
    }
}
