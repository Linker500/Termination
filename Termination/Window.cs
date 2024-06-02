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
    // public int MinWidth {get; set;} //Absolute minimum width of window       //TODO: implement?
    // public int MinHeight {get; set;} //Absolute minimum height of window

    public int Alignment {get; set;} //Alignment of body. 0 = left, 1 = center, 2 = right   //TODO: make enum?
    public int AlignmentVert {get; set;} //Allignment of vertical body. 0 = up, 1 = center 3 = bottom //TODO: make enum? Also change name


    //X and Y offset from 0,0 on the screen. The coordinate of the window. 
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

    public List<Token> Data {get; set;} //Unformatted raw window content

    public List<List<Token>> Data2 {get; set;} //temporary
    public List<List<Token>> FrameBuffer {get; set;} //Window content formatted for window size
    
    public Window(double x, double y, double w, double h)
    {
        Data = new();
        FrameBuffer = new();
        XOffset = x;
        YOffset = y;
        Width = w;
        Height = h;
    }
    public abstract void FillData(string input);
    public abstract void GenFrameBuffer();

    public List<Token> GenerateBlankLine(int width)
    {
        List<Token> BlankLine = new();
        
        while(BlankLine.Count < width)
            BlankLine.Add(new Token(' '));
            
        return BlankLine;
    }
}