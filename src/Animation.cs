using Raylib_cs;
using System.Numerics;

public class Animation
{
    public Texture2D Atlas { get; set; }
    public int FramesPerSecond { get; set; }
    public double TimeStarted { get; set; }
    public Rectangle[] Rectangles { get; set; }

    public Animation(Texture2D atlas, int framesPerSecond, Rectangle[] rectangles)
    {
        Atlas = atlas;
        FramesPerSecond = framesPerSecond;
        TimeStarted = Raylib.GetTime();
        Rectangles = new Rectangle[rectangles.Length];
        rectangles.CopyTo(Rectangles, 0);
    }

    public void DrawAnimationPro(Rectangle dest, Vector2 origin, float rotation, Color tint)
    {
        int index = (int)((Raylib.GetTime() - TimeStarted) * FramesPerSecond) % Rectangles.Length;
        Rectangle source = Rectangles[index];
        Raylib.DrawTexturePro(Atlas, source, dest, origin, rotation, tint);
    }
}