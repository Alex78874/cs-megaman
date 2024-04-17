using Raylib_cs;
using System.Numerics;

public class Animation
{
    public Texture2D Atlas { get; set; }
    public int FramesPerSecond { get; set; }
    public double TimeStarted { get; set; }
    public Rectangle[] Rectangles { get; set; }

    private int currentIndex = 0;
    private bool stop = false;

    public Animation(Texture2D atlas, int framesPerSecond, Rectangle[] rectangles)
    {
        Atlas = atlas;
        FramesPerSecond = framesPerSecond;
        TimeStarted = Raylib.GetTime();
        Rectangles = new Rectangle[rectangles.Length];
        rectangles.CopyTo(Rectangles, 0);
    }

    public void DrawAnimationPro(Rectangle dest, Vector2 origin, float rotation, Color tint, bool loop = true)
    {
        double elapsedTime = Raylib.GetTime() - TimeStarted;

        if (stop) {
            currentIndex = Rectangles.Length - 1;
        } else {
            currentIndex = (int)(elapsedTime * FramesPerSecond) % Rectangles.Length;
        }

        Rectangle source = Rectangles[currentIndex];

        Raylib.DrawTexturePro(Atlas, source, dest, origin, rotation, tint);

        // Draw the last frame and stop if the animation is not looping
        if (currentIndex == (Rectangles.Length - 1) && !loop) {
            Console.WriteLine("Drawing last frame");
            Raylib.DrawTexturePro(Atlas, source, dest, origin, rotation, tint);
            stop = true;
        }
    }
}