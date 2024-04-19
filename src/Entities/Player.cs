using Raylib_cs; // Assuming you have the Raylib library for C#
using System;
using System.Linq.Expressions;
using System.Numerics;
using System.Reflection;

public class Player
{

    // Main variables
    private Texture2D sprite;
    public Vector2 position;
    public Vector2 velocity;

    // Animation and states
    public enum State
    {
        Spawning,
        Idle,
        Walking,
        Jumping,
        Landing,
    }
    public State currentState;
    private Dictionary<State, Animation> animations;

    private bool loop = false;


    // Other variables
    private float scale = 2.0f;
    private float orientation = 0; // Angle in degrees
    public int direction = 1; // 1 = right, -1 = left
    private float jumpSpeed = 1000f;
    private bool isJumping = false;
    private float moveSpeed = 5f;
    private float gravity = 28f;

    public Hitbox hitbox => new Hitbox(new Vector2(28, 32), position);


    public bool isSprinting { get; set; }

    public Player()
    {
        sprite = Raylib.LoadTexture($"res/{GetType().Name}/player_sprites.png");

        animations = new Dictionary<State, Animation>
        {
            { State.Spawning, new Animation(sprite, 13, 47, 54, [
                new Rectangle(47*0, 97, 47, 54),
                new Rectangle(47*0, 97, 47, 54),
                new Rectangle(47*0, 97, 47, 54),
                new Rectangle(47*0, 97, 47, 54),
                new Rectangle(47*0, 97, 47, 54),
                new Rectangle(47*0, 97, 47, 54),
                new Rectangle(47*0, 97, 47, 54),
                new Rectangle(47*0, 97, 47, 54), // Megaman beam

                new Rectangle(47*1, 97, 47, 54),
                new Rectangle(47*2, 97, 47, 54),
                new Rectangle(47*3, 97, 47, 54),
                new Rectangle(47*4, 97, 47, 54),
                new Rectangle(47*5, 97, 47, 54)
            ]) },
            { State.Idle, new Animation(sprite, 2, 32, 32, [
                new Rectangle(0, 32, 32, 32),
                new Rectangle(0, 32, 32, 32),
                new Rectangle(0, 32, 32, 32),
                new Rectangle(0, 32, 32, 32),
                new Rectangle(0, 32, 32, 32),
                new Rectangle(0, 32, 32, 32),
                new Rectangle(32, 32, 32, 32) // Eye blinking
            ]) },
            { State.Walking, new Animation(sprite, 13, 32, 32, [
                new Rectangle(32*0, 0, 32, 32),
                new Rectangle(32*1, 0, 32, 32),
                new Rectangle(32*3, 0, 32, 32),
                new Rectangle(32*4, 0, 32, 32),
                new Rectangle(32*5, 0, 32, 32),
                new Rectangle(32*6, 0, 32, 32),
                new Rectangle(32*7, 0, 32, 32),
                new Rectangle(32*8, 0, 32, 32),
            ]) },
            { State.Jumping, new Animation(sprite, 13, 32, 32, [
                new Rectangle(32*0, 64, 32, 32),
                new Rectangle(32*1, 64, 32, 32),
                new Rectangle(32*2, 64, 32, 32),
            ]) },
            { State.Landing, new Animation(sprite, 13, 32, 32, [
                new Rectangle(32*4, 64, 32, 32),
                new Rectangle(32*5, 64, 32, 32),
                new Rectangle(32*6, 64, 32, 32),
            ]) },
            // Add more animations as needed
        };

        currentState = State.Spawning;
        position = new Vector2(100, 100);
    }

    ~Player()
    {
        Raylib.UnloadTexture(sprite);
    }

    public void Draw()
    {  
        // Idle animation after spawning animation
        if (currentState == State.Spawning && animations[currentState].stop) {
            currentState = State.Idle;
        }

        // Idle animation after landing animation
        if (currentState == State.Landing && animations[currentState].stop) {
            currentState = State.Idle;
        }

        // Dont loop theses animations
        loop = currentState != State.Spawning &&
                currentState != State.Jumping &&
                currentState != State.Landing;

        animations[currentState].DrawAnimationPro(
            new Rectangle(position.X, position.Y, animations[currentState].width, animations[currentState].height),
            new Vector2(animations[currentState].width, animations[currentState].height - hitbox.Size.Y/2),
            0,
            Color.White,
            direction,
            1,
            loop
        );
        
        // Draw the bounding box
        //Raylib.DrawRectangleLinesEx(BoundingBox, 1, Color.Red);

        // Draw a circle at the player's position
        //Raylib.DrawCircle((int)position.X, (int)position.Y, 5, Color.Green);
    }

    public void Update(float deltaTime, Map map, Cursor cursor)
    {
        // Orient player to mouse
        Orient(cursor);

        // Apply gravity
        velocity.Y += gravity * deltaTime;

        // Sprinting
        moveSpeed = Raylib.IsKeyDown(KeyboardKey.LeftShift) ? 300 : 150;

        // Check for jump input
        if (currentState != State.Spawning) {
            if (Raylib.IsKeyPressed(KeyboardKey.Space) && !isJumping)
            {
                velocity.Y = -jumpSpeed * deltaTime;
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
        }

        // Apply velocity to position
        position += velocity;

        // Check for collisions with tiles
        foreach (Tile tile in map.blocks) {
            if (tile != null && tile.collidable && Raylib.CheckCollisionRecs(hitbox.BoundingBox, tile.BoundingBox)) {

                // If the player is falling, stop the fall
                if (tile.position.Y - hitbox.Size.Y < hitbox.Position.Y && velocity.Y > 0) {
                    Raylib.DrawCircle((int)tile.position.X, (int)tile.position.Y, 5, Color.Red);
                    position.Y = tile.position.Y - hitbox.Size.Y/2;
                    velocity.Y = 0;
                    isJumping = false;
                }
            }
        }

        // Update the animation
        animations[currentState].Update();
    }

    public void Orient(Cursor cursor)
    {
        float distX = cursor.position.X - position.X;
        float distY = cursor.position.Y - position.Y;

        orientation = -MathF.Atan2(distX, distY) * (180 / MathF.PI);
    }
}
