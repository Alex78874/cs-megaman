using Raylib_cs;

public class Debug {
    public static void DrawDebugingValues(Player player, Cursor cursor)
    {
        Utils.DrawText($"FPS: {Raylib.GetFPS()}", 10, 10, 20, Color.White);
        Utils.DrawText($"Player Position: {(int)player.position.X}, {(int)player.position.Y}", 10, 30, 20, Color.White);
        Utils.DrawText($"Cursor Position: {(int)cursor.position.X}, {(int)cursor.position.Y}", 10, 50, 20, Color.White);
        Utils.DrawText($"Player State: {player.currentState}", 10, 70, 20, Color.White);
        Utils.DrawText($"Player velocity: {player.velocity.X}, {player.velocity.Y}", 10, 90, 20, Color.White);
    }
}