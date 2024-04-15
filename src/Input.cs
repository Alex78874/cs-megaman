using Raylib_cs;
using System.Numerics;

public class Input
{
    public static void HandlePlayerMovement(Player player, Cursor cursor)
    {
        // Orient player to mouse
        player.Orient(cursor);

        // Sprinting
        int speed = Raylib.IsKeyDown(KeyboardKey.LeftShift) ? 300 : 150;

        if (Raylib.IsKeyDown(KeyboardKey.Right) || Raylib.IsKeyDown(KeyboardKey.D))
        {
            player.Move(speed, 0);
        }
        else if (Raylib.IsKeyDown(KeyboardKey.Left) || Raylib.IsKeyDown(KeyboardKey.A))
        {
            player.Move(-speed, 0);
        }

        if (Raylib.IsKeyDown(KeyboardKey.Up) || Raylib.IsKeyDown(KeyboardKey.W))
        {
            player.Move(0, -speed);
        }
        else if (Raylib.IsKeyDown(KeyboardKey.Down) || Raylib.IsKeyDown(KeyboardKey.S))
        {
            player.Move(0, speed);
        }
    }

    public static void HandleInput(Player player, Cursor cursor)
    {
        HandlePlayerMovement(player, cursor); // Keyboard Movement
    }
}
