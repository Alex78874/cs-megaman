public class Debug {
    public static void DrawDebugingValues(Player player, Cursor cursor)
    {
        Utils.DrawFPS(10, 10);
        Utils.DrawText($"Player Position: {(int)player.position.X}, {(int)player.position.Y}", 10, 30, 20, Raylib_cs.Color.White);
        Utils.DrawText($"Cursor Position: {(int)cursor.position.X}, {(int)cursor.position.Y}", 10, 50, 20, Raylib_cs.Color.White);
        Utils.DrawText($"Player State: {player.movementState}", 10, 70, 20, Raylib_cs.Color.White);
    }
}