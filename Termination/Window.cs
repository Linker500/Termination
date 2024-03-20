using System.Security.Cryptography.X509Certificates;

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
    // //X and Y offset from 0,0 on the screen. The coordinate of the window. 
    // public int xOffset {get; set;}
    // public int yOffset {get; set;}

    // //Width and height of the window itself

    // public int MinWidth {get; set;} //Absolute minimum width of window
    // public int MinHeight {get; set;} //Absolute minimum height of window

    public int Alignment {get; set;} //Alignment of body. 0 = left, 1 = center, 2 = right   
    public double XOffset {get; set;}

    public double YOffset {get; set;}
    
    public double Width {get; set;} //Size width to screen width
    public double Height {get; set;} //Relative height to screen height

    //Todo: these should be read anyone but not writable by anything other than screen?
    public int aWidth;
    public int aHeight;
    public int aXOffset;
    public int aYOffset;

    public string Data {get; set;}
    public List<List<Token>> FrameBuffer {get; set;}
    public Window(double x, double y, double w, double h)
    {
        FrameBuffer = new();
        XOffset = x;
        YOffset = y;
        Width = w;
        Height = h;
    }


    public abstract void GenFrameBuffer();

// public void Fill(string text)
//     {
//         List<string> textNew = new();

//         for(int h=0; h<height && text.Length != 0; h++)
//         {
            
//             if(width > text.Length)
//             {
//                 textNew.Add(text);
//                 text = "";
//             }

//             else
//             {
//                 int lastSpace = -1;
//                 for(int i=0; i<width && i<text.Length; i++)Min
//                 {
//                     if(text[i] == ' ')
//                         lastSpace = i;
//                 }

//                 if(lastSpace == -1)
//                 {
//                     textNew.Add(text.Substring(0,width));
//                     text = text.Substring(width);
//                 }
//                 else 
//                 {
//                     textNew.Add(text.Substring(0,lastSpace));
//                     text = text.Substring(lastSpace+1);
//                 }
//             }
//         }

//         for(int y=0; y< textNew.Count; y++)
//         {
//             for(int x=0; x< textNew[y].Length; x++)
//             {
//                 FrameBuffer[y][x].text = textNew[y][x];
//             }
//         }
//     }

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