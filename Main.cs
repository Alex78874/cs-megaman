using System;
using System.Numerics;
using Raylib_cs;

class Program
{
    public static List<Block> instances = new List<Block>();

    static void Main(string[] args)
    {
        AppDomain.CurrentDomain.UnhandledException += Utils.HandleException;

        const int screenWidth = 800;
        const int screenHeight = 450;

        Raylib.InitWindow(screenWidth, screenHeight, "Game");
        // Adapt the screen to the monitor's refresh rate
        if (Raylib.IsWindowReady()) {
            Raylib.SetTargetFPS(Raylib.GetMonitorRefreshRate(0));
        }

        // Hide the cursor
        Raylib.HideCursor();

        // Create the entities
        Player player = new Player();
        Cursor cursor = new Cursor();

        Block block = new Block(30);
        block.position = new Vector2(400, 225);

        // Add a block to the list of instances
        instances.Add(block);

        // Main game loop
        while (!Raylib.WindowShouldClose())
        {   
            // Get current fps
            Utils.DeltaTime = Raylib.GetFrameTime();
            
            // Handle input
            Input.HandleInput(player, cursor);
            cursor.Update();
            player.UpdateMovementState();

            Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.Black);

                // Draw all entities
                player.Draw();
                block.Draw();
                // Draw the cursor
                cursor.Draw();

                // Draw all the debuging values
                Debug.DrawDebugingValues(player, cursor);
            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}