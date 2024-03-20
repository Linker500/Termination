using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace Termination;

/*
 * Textblock is a Window type.
 * It specializes in displaying long blocks of text.
 * While other Windows merely hide data that goes out of the window bounds, Textblock dynamically wraps them.
 * As you'd expect a text to do.
 * This differentiates it from other windows, where wrapping is not ideal.
 * This also allows text to be auto formatted, the user not having to know what size and shape the panel will be before hand.
 */

public class TextBlock : Window
{
    public TextBlock(double x, double y, double w, double h) : base(x,y,w,h)
    {

    }

    //TODO: add support for paragraph breaking. And possibly center and right aligning.
    //Fills textblock with passed string. Wrapping text around spaces to fit into the limited area cleanly.
    public override void GenFrameBuffer()
    {
        GenerateBlank(); //Generates a new blank token for the entire Window, as we will be possibly be leaving large parts of it blank.

        string text = Data; //Text to manipulate; Copy of the Windows Data
        List<string> textNew = new(); //List of text lines that are made of split up text

        //Split the input string into wrapped lines.
        for(int h=0; h<aHeight && text.Length != 0; h++) //Loop for every vertical line
        {
            
            //First check if the absolute width of the Window is longer than the remaining text.
            //If so, then just use the entire remaining text.
            if(aWidth > text.Length)
            {
                textNew.Add(text);
                text = "";
            }

            //Next, figure out where to wrap text
            else
            {
                int lastSpace = -1; //The last space character found. Default to -1, for if there is no space found.
                for(int i=0; i<aWidth && i<text.Length; i++) //Loop to find last space
                {
                    if(text[i] == ' ')
                        lastSpace = i;
                }

                if(lastSpace == -1) //If no space, then wrap at the end, splitting the word. TODO: perhaps add logic to add a dash to show text was cut? Like th- ¶ is
                {
                    textNew.Add(text.Substring(0,aWidth));
                    text = text.Substring(aWidth);
                }
                else //Otherwise, wrap at the space
                {
                    textNew.Add(text.Substring(0,lastSpace));
                    text = text.Substring(lastSpace+1);
                }
            }
        }

        //Format the split strings into to a FrameBuffer
        for(int y=0; y< textNew.Count; y++)
        {
            for(int x=0; x< textNew[y].Length; x++)
            {
                FrameBuffer[y][x].Text = textNew[y][x];
            }
        }
    }
}