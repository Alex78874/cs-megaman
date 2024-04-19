using Raylib_cs;
using System.Numerics;

public class Animation
{
    public Texture2D Atlas { get; set; }
    public int FramesPerSecond { get; set; }
    public int width { get; set; }
    public int height { get; set; }

    public double TimeStarted { get; set; }
    public Rectangle[] Rectangles { get; set; }

    private int currentIndex = 0;
    public bool stop = false;

    public Animation(Texture2D atlas, int framesPerSecond, int width, int height, Rectangle[] rectangles)
    {
        Atlas = atlas;
        this.width = width;
        this.height = height;
        FramesPerSecond = framesPerSecond;
        TimeStarted = Raylib.GetTime();
        Rectangles = new Rectangle[rectangles.Length];
        rectangles.CopyTo(Rectangles, 0);
    }

    public void Update()
    {
        
    }

    public void DrawAnimationPro(Rectangle dest, Vector2 origin, float rotation, Color tint, int direction, float scale = 1, bool loop = true)
    {
        double elapsedTime = Raylib.GetTime() - TimeStarted;

        if (stop) {
            currentIndex = Rectangles.Length - 1;
        } else {
            currentIndex = (int)(elapsedTime * FramesPerSecond) % Rectangles.Length;
        }

        Rectangle source = Rectangles[currentIndex];
        dest = new Rectangle(
            dest.X  + source.Width/2,
            dest.Y + dest.Height - source.Height * scale,
            source.Width * scale,
            source.Height * scale
        );

        source.Width *= direction;

        // Draw the last frame and stop if the animation is not looping
        if (currentIndex == (Rectangles.Length - 1) && !loop) {
            Raylib.DrawTexturePro(Atlas, source, dest, origin, rotation, tint);
            stop = true;
            return;
        }

        Raylib.DrawTexturePro(Atlas, source, dest, origin, rotation, tint);
    }
}