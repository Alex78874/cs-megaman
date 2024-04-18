using Raylib_cs; // Assuming you have the Raylib library for C#
using System;
using System.Linq.Expressions;
using System.Numerics;
using System.Reflection;

public class Player
{

    // Space variables
    private Texture2D sprite;
    private float scale = 1.0f;
    public Vector2 position;
    public Vector2 velocity;

    // 1 = right -1 = left
    public int direction = 1;

    // Animation and states
    public enum State
    {
        Spawning,
        Idle,
        Walking,
        Jumping,
    }
    public State currentState;
    private Dictionary<State, Animation> animations;
    private bool loop = false;


    private float orientation;
    private float walkingSpeed = 5.0f;
    private float sprintingSpeed = 10.0f;


    private float jumpSpeed = 10f;
    private bool isJumping = false;
    private float moveSpeed = 5f;
    private float gravity = 0.5f;

    private int speed;

    public bool isSprinting { get; set; }

    public Rectangle BoundingBox {
        get {
            // Adjust the size to match your player's size
            return new Rectangle(position.X, position.Y, 32, 32);
        }
    }

    public Player()
    {
        sprite = Raylib.LoadTexture($"res/{GetType().Name}/spritesheet.png");

        animations = new Dictionary<State, Animation>
        {
            { State.Spawning, new Animation(sprite, 10, [
                new Rectangle(0, 3, 14, 48),
                new Rectangle(14, 23, 28, 28),
                new Rectangle(45, 26, 40, 25),
                new Rectangle(88, 26, 34, 25),
                new Rectangle(125, 25, 26, 26),
                new Rectangle(154, 23, 24, 28)
            ]) },
            { State.Idle, new Animation(sprite, 2, [
                new Rectangle(3, 54, 24, 28),
                new Rectangle(3, 54, 24, 28),
                new Rectangle(3, 54, 24, 28),
                new Rectangle(3, 54, 24, 28),
                new Rectangle(3, 54, 24, 28),
                new Rectangle(3, 54, 24, 28),
                new Rectangle(3, 54, 24, 28),
                new Rectangle(30, 54, 24, 28) // Eye blinking
            ]) },
            { State.Walking, new Animation(sprite, 10, [
                new Rectangle(3, 85, 23, 28),
                new Rectangle(29, 87, 26, 26),
                new Rectangle(58, 86, 21, 27),
                new Rectangle(82, 85, 18, 28),
                new Rectangle(103, 85, 20, 28),
                new Rectangle(126, 87, 28, 26),
                new Rectangle(156, 86, 21, 27),
                new Rectangle(180, 85, 18, 28),
                new Rectangle(201, 85, 21, 28),
            ]) },
            // Add more animations as needed
        };

        currentState = State.Idle;
        position = new Vector2(100, 100);
    }

    ~Player()
    {
        Raylib.UnloadTexture(sprite);
    }

    public void Draw()
    {
        // Loop animation if not idle
        loop = currentState != State.Spawning;

        animations[currentState].DrawAnimationPro(new Rectangle(position.X, position.Y, 32 * scale, 32 * scale), new Vector2(16 * scale, 16 * scale), 0, Color.White, direction, loop);
        // Raylib.DrawTexturePro(sprite, src, dst, origin, orientation, Color.White);
    }

    public void Update(float deltaTime, Map map, Cursor cursor)
    {
        // Orient player to mouse
        Orient(cursor);

        // Apply gravity
        velocity.Y += gravity;

        // Sprinting
        moveSpeed = Raylib.IsKeyDown(KeyboardKey.LeftShift) ? 300 : 150;

        // Check for jump input
        if (Raylib.IsKeyPressed(KeyboardKey.Space) && !isJumping)
        {
            velocity.Y = -jumpSpeed;
            isJumping = true;
            currentState = State.Jumping;
        }

        // Check for horizontal movement input
        if (Raylib.IsKeyDown(KeyboardKey.Left) || Raylib.IsKeyDown(KeyboardKey.A))
        {
            velocity.X = -moveSpeed * deltaTime;
            currentState = State.Walking;
            direction = -1;
        }
        else if (Raylib.IsKeyDown(KeyboardKey.Right) || Raylib.IsKeyDown(KeyboardKey.D))
        {
            velocity.X = moveSpeed * deltaTime;
            currentState = State.Walking;
            direction = 1;
        }
        else {
            velocity.X = 0;
            currentState = State.Idle;
        }

        // Apply velocity to position
        position += velocity;

        // Check for collisions with tiles
        foreach (Tile tile in map.blocks) {
            if (tile != null && tile.collidable && Raylib.CheckCollisionRecs(BoundingBox, tile.BoundingBox)) {

                // If the player is falling, stop the fall
                if (tile.position.Y - (BoundingBox.Height / 2) < position.Y && velocity.Y > 0) {
                    Raylib.DrawCircle((int)tile.position.X, (int)tile.position.Y, 5, Color.Red);
                    position.Y = tile.position.Y + (BoundingBox.Height / 2) - BoundingBox.Height;
                    velocity.Y = 0;
                    isJumping = false;
                }
            }
        }

        // Update the animation
        animations[currentState].Update();
    }

    public void Move(int x, int y)
    {
        speed = Convert.ToInt32(Math.Sqrt(x * x + y * y));

        position.X += x * Utils.DeltaTime;
        position.Y += y * Utils.DeltaTime;
    }

    public void Orient(Cursor cursor)
    {
        float distX = cursor.position.X - position.X;
        float distY = cursor.position.Y - position.Y;

        orientation = -MathF.Atan2(distX, distY) * (180 / MathF.PI);
    }
}
