using System;
using System.Numerics;
using Raylib_cs;

class Program
{
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

        Player player = new Player();
        Cursor mouse = new Cursor();

        // Raylib.SetMouseCursor(MouseCursor.Crosshair);

        // Main game loop
        while (!Raylib.WindowShouldClose())
        {   
            // Get current fps
            Utils.DeltaTime = Raylib.GetFrameTime();
            
            mouse.Update();

            // Draw all the debuging values
            Debug.DrawDebugingValues(player, mouse);

            player.UpdateMovementState();
            Input.HandleInput(player, mouse);

            Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.Black);
                player.Draw();
                Console.WriteLine($"Player is {player.movementState}");
            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}