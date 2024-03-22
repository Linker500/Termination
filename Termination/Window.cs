namespace Termination;
//TODO: is panel a better name than window?

/*
 * The window class is the base abstract class for all Windows.
 * TextBlock and Canvas extend it, proccessing input data differently.
 * A window has two coordinate offsets, defining it's location in the screen, and a height and width, defining it's size.
 * Each window has a FrameBuffer of Tokens. This framebuffer is a copy of the entire data of the window.
 */

public abstract class Window
{
    // public int MinWidth {get; set;} //Absolute minimum width of window       //TODO: implement
    // public int MinHeight {get; set;} //Absolute minimum height of window

    public int Alignment {get; set;} //Alignment of body. 0 = left, 1 = center, 2 = right   //TODO: make enum?

    // //X and Y offset from 0,0 on the screen. The coordinate of the window. 
    public double XOffset {get; set;}
    public double YOffset {get; set;}
    
    //Relative height and width to total screen size
    public double Width {get; set;}
    public double Height {get; set;}

    //TODO: these should be read anyone but not writable by anything other than screen?
    public int aWidth;
    public int aHeight;
    public int aXOffset;
    public int aYOffset;

    //public string Data {get; set;} //TODO: if color data is going to be implemented, this needs to be an unformatted array of tokens
    public List<Token> Data; //Unformatted raw window content
    public List<List<Token>> FrameBuffer {get; set;} //Window content formatted for window size
    
    public Window(double x, double y, double w, double h)
    {
        FrameBuffer = new();
        XOffset = x;
        YOffset = y;
        Width = w;
        Height = h;
    }

    public abstract void FillData(string input);
    public abstract void GenFrameBuffer();

    public void GenerateBlank()
    {
        FrameBuffer = new();
        for(int y=0; y<aHeight; y++)
        {
            List<Token> line = new();
            for(int x=0; x<aWidth; x++)
            {
                line.Add(new(' '));
            }
            FrameBuffer.Add(line);
        }
    }
}