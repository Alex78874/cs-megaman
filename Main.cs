using System;
using System.Numerics;
using Raylib_cs;

class Program
{
    public static List<Item> instances = new List<Item>();
    public static List<Tile> tiles = new List<Tile>();

    static void Main(string[] args)
    {
        AppDomain.CurrentDomain.UnhandledException += Utils.HandleException;

        // const int screenWidth = 1920;
        // const int screenHeight = 1080;
        const int screenWidth = 1280;
        const int screenHeight = 720;

        // Calculate how many tiles can fit in the screen
        (int tilesX, int tilesY) = Utils.CalculateTiles(screenWidth, screenHeight, 32);
        Console.WriteLine($"Tiles in X: {tilesX}, Tiles in Y: {tilesY}");

        bool grid = false;

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

        // Create the 2D camera
        Camera2D camera = new Camera2D();
        Raylib.BeginMode2D(camera);

        Item crate = new Item(30, 1);
        crate.position = new Vector2(400, 225);
        // Add the crate to the list of instances
        instances.Add(crate);

        // Create the map from example_stage JSON file
        int[,] stage = Map.ConvertJsonFileToMap("res/stage/example_stage.json") ?? new int[0, 0];
        Map map = new Map(stage);

        // Main game loop
        while (!Raylib.WindowShouldClose())
        {   
            // Get current fps
            Utils.DeltaTime = Raylib.GetFrameTime();
            
            // Update the camera target to follow the player
            camera.Target = new Vector2(player.position.X, player.position.Y);

            // Updates
            Update.UpdateGame(player, cursor, map, instances);

            // Check for collisions with the player and the tiles
            foreach (Tile tile in tiles) {
                if (tile.collidable && Raylib.CheckCollisionRecs(player.BoundingBox, tile.BoundingBox)) {
                    Console.WriteLine("Collision detected");
                }
            }

            Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.Black);

                // Draw the map
                map.Draw();
                // Draw the player
                player.Draw();
                // Draw all the instances
                foreach (Item instance in instances) {
                    instance.Draw();
                }

                // Toggle the grid
                if (Raylib.IsKeyPressed(KeyboardKey.G)) {
                    grid = !grid;
                }
                if (grid) {
                    Utils.DrawGrid(32, Color.DarkGray);
                }

                // Draw the cursor
                cursor.Draw();

                // Draw all the debuging values
                Debug.DrawDebugingValues(player, cursor);
            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}