using System;
using System.Numerics;
using Raylib_cs;

class Program
{
    static void Main(string[] args)
    {
        const int screenWidth = 800;
        const int screenHeight = 450;

        Raylib.InitWindow(screenWidth, screenHeight, "Game");
        Raylib.SetTargetFPS(60);

        Player player = new Player();
        Cursor mouse = new Cursor();

        Raylib.SetMouseCursor(MouseCursor.Crosshair);

        // Main game loop
        while (!Raylib.WindowShouldClose())
        {
            Vector2 mousePos = Raylib.GetMousePosition();
            // Console.WriteLine($"X: {mousePos.X}, Y: {mousePos.Y}");          

            Input.HandleInput(player, mousePos);

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Black);
            player.Draw();
            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}
