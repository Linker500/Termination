namespace Termination;

public class Canvas
{
    public int xOffset {get;}
    public int yOffset {get;}
    public int width {get;}
    public int height {get;}

    public string frameBuffer {get; set;}

    public Canvas(int x, int y, int w, int h)
    {
        xOffset = x;
        yOffset = y;
        width = w;
        height = h;
    }
}