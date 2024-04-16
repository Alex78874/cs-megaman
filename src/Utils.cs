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
    
    public static void DrawText(string text, int x, int y, int fontSize, Color color)
    {
        Raylib.DrawText(text, x, y, fontSize, color);
    }

    public static void DrawGrid(int spacing, Color color)
    {
        for (int i = 0; i < Raylib.GetScreenWidth(); i += spacing)
        {
            Raylib.DrawLine(i, 0, i, Raylib.GetScreenHeight(), color);
        }

        for (int i = 0; i < Raylib.GetScreenHeight(); i += spacing)
        {
            Raylib.DrawLine(0, i, Raylib.GetScreenWidth(), i, color);
        }
    }

    // Function to get the placement of the mouse in the grid
    public static Vector2 GetMouseGridPosition(int spacing)
    {
        Vector2 mousePosition = Raylib.GetMousePosition();
        int x = (int)(mousePosition.X / spacing) * spacing;
        int y = (int)(mousePosition.Y / spacing) * spacing;

        return new Vector2(x, y);
    }
}