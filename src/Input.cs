using Raylib_cs;
using System.Numerics;

public class Input
{
    public static void HandlePlayerMovement(Player player, Vector2 mousePos)
    {
        // Orient player to mouse
        // player.Orient(mousePos.X, mousePos.Y);

        // Sprinting
        if (Raylib.IsKeyDown(KeyboardKey.LeftShift))
        {
            player.isSprinting = true;
        }
        else
        {
            player.isSprinting = false;
        }

        if (Raylib.IsKeyDown(KeyboardKey.Right) || Raylib.IsKeyDown(KeyboardKey.D))
        {
            player.Move(1, 0);
        }
        else if (Raylib.IsKeyDown(KeyboardKey.Left) || Raylib.IsKeyDown(KeyboardKey.A))
        {
            player.Move(-1, 0);
        }

        if (Raylib.IsKeyDown(KeyboardKey.Up) || Raylib.IsKeyDown(KeyboardKey.W))
        {
            player.Move(0, -1);
        }
        else if (Raylib.IsKeyDown(KeyboardKey.Down) || Raylib.IsKeyDown(KeyboardKey.S))
        {
            player.Move(0, 1);
        }
    }

    public static void HandleInput(Player player, Vector2 mousePos)
    {
        HandlePlayerMovement(player, mousePos); // Keyboard Movement
    }
}
