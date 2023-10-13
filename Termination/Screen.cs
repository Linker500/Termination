//TODO: Console.Clear does not do a full clear on linux systems

namespace Termination;
using System.Diagnostics;
using Microsoft.VisualBasic;

public class Screen
{
    public readonly Dictionary<string,Canvas> canvases = new();
    
    public Screen()
    {
        canvases.Add("one",new Canvas(0,0,40,20));
        canvases.Add("two",new Canvas(50,20,100,100));
        canvases.Add("three",new Canvas(175,30,30,30));
    }

    //Renders all canvases
    public void Render()
    {
        foreach(var (key, val) in canvases)
            Render(val);
    }

    //Renders specific canvas by name
    public void Render(string name) {Render(canvases[name]);}

    //Renders specific canvas
    public void Render(Canvas canvas)
    {
        int xo = canvas.xOffset;
        int yo = canvas.yOffset;

        for(int y=0; y<canvas.height; y++) //TODO: can this be a nested foreach? idk
        {
            for(int x=0; x<canvas.width; x++)
            {
                Console.SetCursorPosition(x+xo,y+yo);
                Console.Write(canvas.frameBuffer[y][x].text);
            }
        }
    }

    public void Display()
    {
        //Draw border Outline
        DisplayScreen();
        
        //Cycle through Canvases, displaying them one by one.
        foreach(KeyValuePair<string, Canvas> entry in canvases)
            DisplayCanvas(entry.Value, true);
    }
    public void DisplayScreen()
    {
        var (w, h) = TermInfo.GetSize();
        DisplayBox(0,0,w,h,false);
    }

    private void DisplayCanvas(Canvas canvas, bool bold)
    {
        int x = canvas.xOffset;
        int y = canvas.yOffset;
        int w = canvas.width;
        int h = canvas.height;

        DisplayBox(x, y, w, h, bold);
    }

    //Moves cursor to draw an arbitrarily sized border.
    private void DisplayBox(int x, int y, int w, int h, bool bold)
    {
        char[] b; //Border lines

        if(bold)
            b = new char[] {'╔','╗','═','║','╚','╝'}; 
        else
            b = new char[] {'┌','┐','─','│','└','┘'};

        /* Prints top row and right column
         * Starting at top left corner.
         * ​ ╔══╗
         *     ║
         */
        Console.SetCursorPosition(x, y);
        Console.Write(b[0]);
        for(int i = 1; i < w-1; i++)
            Console.Write(b[2]);

        Console.Write(b[1]);
        Console.SetCursorPosition(x+w-1, y+1);
        for(int i = 1; i < h; i++)
        {
            Console.Write(b[3]);
            Console.SetCursorPosition(x+w-1, y+i);
        }

        /* Prints left column and bottom row
         * Starting again at top left corner.
         *  ║
         * ​ ╚══╝
         */
        Console.SetCursorPosition(x, y+1);
        for(int i = 1; i < h; i++)
        {
            Console.Write(b[3]);
            Console.SetCursorPosition(x, y+i);
        }
        Console.Write(b[4]);
        for(int i = 1; i < w-1; i++)
            Console.Write(b[2]);
        Console.Write(b[5]);
    }
}

 /*\
​╔═╦═╗
║ ║ ║
╠═╬═╣
║ ║ ║
╚═╩═╝
┌─┬─┐
│ │ │
├─┼─┤
│ │ │
└─┴─┘
 \*/

/* Code Graveyard *\

    public void Display(int x, int y)
    {
        var box = new System.Text.StringBuilder("");

        for(int i=0; i<y; i++)
        {
            char columnL;
            char columnR;
            char row;
            if(i==0)
            {
                columnL = '┌';
                columnR = '┐';
                row = '─';
            }
            else if(i!=y-1)
            {
                columnL = '│';
                columnR = '│';
                row = ' ';
            }
            else //if(i==y-1)
            {
                columnL = '└';
                columnR = '┘';
                row = '─';
            }

            box.Append(columnL);
            box.Append(row,x-2);
            box.Append(columnR);

            if(i<y-1)
                box.AppendLine();
        }
        Console.Write(box);
    }


\* */