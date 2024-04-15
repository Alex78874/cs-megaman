using System;
using System.Numerics;
using Raylib_cs;

class Program
{
    public static List<Item> instances = new List<Item>();

    static void Main(string[] args)
    {
        AppDomain.CurrentDomain.UnhandledException += Utils.HandleException;

        const int screenWidth = 800;
        const int screenHeight = 450;

        Raylib.InitWindow(screenWidth, screenHeight, "Game");
        // Raylib.ToggleFullscreen();
        // Adapt the screen to the monitor's refresh rate
        if (Raylib.IsWindowReady()) {
            Raylib.SetTargetFPS(Raylib.GetMonitorRefreshRate(0));
        }

        // Hide the cursor
        Raylib.HideCursor();

        // Create the entities
        Player player = new Player();
        Cursor cursor = new Cursor();


        Item crate = new Item(30, 1);
        crate.position = new Vector2(400, 225);
        // Add the crate to the list of instances
        instances.Add(crate);


        Map map = new Map(10, 10, 32);

        // Main game loop
        while (!Raylib.WindowShouldClose())
        {   
            // Get current fps
            Utils.DeltaTime = Raylib.GetFrameTime();
            
            // Handle input
            Input.HandleInput(player, cursor);
            cursor.Update();
            player.UpdateMovementState();
            map.Update();

            Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.Black);

                // Draw the map
                map.Draw();
                // Draw all entities
                player.Draw();
                crate.Draw();
                // Draw the cursor
                cursor.Draw();

                // Draw all the debuging values
                Debug.DrawDebugingValues(player, cursor);
            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}