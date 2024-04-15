using System.Numerics;
using Raylib_cs;

public class Utils
{
    public static float DeltaTime { get; set; }

    public static void HandleException(object sender, UnhandledExceptionEventArgs e)
    {
        Exception ex = (Exception)e.ExceptionObject;

        // Log the exception, display it, or handle it as you wish
        Console.WriteLine("Unhandled exception caught: " + ex.Message);
    }
    
    public static void DrawFPS(int x, int y)
    {
        Raylib.DrawText($"FPS: {Raylib.GetFPS()}", x, y, 20, Raylib_cs.Color.White);
    }

    public static void DrawText(string text, int x, int y, int fontSize, Color color)
    {
        Raylib.DrawText(text, x, y, fontSize, color);
    }
}