using Raylib_cs; // Assuming you have the Raylib library for C#
using System;
using System.Numerics;

public class Player
{
    private Texture2D sprite;
    public Vector2 position;

    // public SpriteAnimation animation;
    public Animation animation;

    private float scale = 2.0f;

    private float orientation;
    private float walkingSpeed = 5.0f;
    private float sprintingSpeed = 10.0f;
    private int speed;

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
        string imagePath = $"res/{GetType().Name}/spritesheet.png";

        sprite = Raylib.LoadTexture(imagePath);
        animation = new Animation(sprite, 12, new Rectangle[] {
            new Rectangle(0, 3, 14, 48),
            new Rectangle(14, 23, 28, 28),
            new Rectangle(45, 26, 40, 25),
            new Rectangle(88, 26, 34, 25),
            new Rectangle(125, 25, 26, 26),
            new Rectangle(154, 23, 24, 28)
        });
        // animation = new SpriteAnimation(sprite, 9, 0.1f);

        position = new Vector2(100, 100);
    }

    ~Player()
    {
        Raylib.UnloadTexture(sprite);
    }

    public void Draw()
    {
        animation.DrawAnimationPro(new Rectangle(position.X, position.Y, 32 * scale, 32 * scale), new Vector2(16 * scale, 16 * scale), 0, Color.White);
        // Raylib.DrawTexturePro(sprite, src, dst, origin, orientation, Color.White);
    }

    public void Move(int x, int y)
    {
        speed = Convert.ToInt32(Math.Sqrt(x * x + y * y));

        position.X += x * Utils.DeltaTime;
        position.Y += y * Utils.DeltaTime;
    }

    public void UpdateMovementState()
    {
        Console.WriteLine($"Abs X: {Math.Abs(position.X)}, Abs Y: {Math.Abs(position.Y)}");

        if (Math.Abs(position.X) > 0 || Math.Abs(position.Y) > 0) {
            movementState = isSprinting ? MovementState.Sprinting : MovementState.Walking;
        } else {
            movementState = MovementState.Idle;
        }
    }

    public void Orient(Cursor cursor)
    {
        float distX = cursor.position.X - position.X;
        float distY = cursor.position.Y - position.Y;

        orientation = -MathF.Atan2(distX, distY) * (180 / MathF.PI);
    }
}
