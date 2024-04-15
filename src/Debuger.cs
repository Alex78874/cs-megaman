using System.Numerics;

public class Debuger
{
    public static void HandleException(object sender, UnhandledExceptionEventArgs e)
    {
        Exception ex = (Exception)e.ExceptionObject;

        // Log the exception, display it, or handle it as you wish
        Console.WriteLine("Unhandled exception caught: " + ex.Message);
    }
}