//TODO: Console.Clear does not do a full clear on linux systems

namespace Termination;
using System.Diagnostics;
using System.Security.Cryptography;
using Microsoft.VisualBasic;

/* 
 * The screen class is the "screen" that we made out of the terminal. 
 * It contains multiple Windows. These function as vaguely as windows in a graphical desktop environment do.
 * They contain a coordinate position, a length, height, and then display data within it.
 */

public class Screen
{
    public readonly Dictionary<string,Window> Windows = new();
    
    public Screen()
    {
        Windows.Add("one",new TextBlock(0,0,40,20));
        Windows.Add("two",new TextBlock(50,20,100,100));
        Windows.Add("three",new TextBlock(175,30,30,30));
    }

    //Renders all windows
    public void Render()
    {
        foreach(var (key, val) in Windows)
            Render(val);
    }

    //Renders specific canvas by name
    public void Render(string name) {Render(Windows[name]);}

    //Renders specific canvas
    public void Render(Window window)
    {
        int xo = window.xOffset;
        int yo = window.yOffset;

        for(int y=0; y<window.height; y++) //TODO: can this be a nested foreach? idk
        {
            for(int x=0; x<window.width; x++)
            {
                Console.SetCursorPosition(x+xo,y+yo);

                Token token = window.FrameBuffer[y][x];

                Console.ForegroundColor = token.Color;
                Console.BackgroundColor = token.BGColor;
                Console.Write(token.Text);
            }
        }
    }

    public void Display()
    {
        //Draw border Outline
        DisplayScreen();
        
        //Cycle through Canvases, displaying them one by one.
        foreach(KeyValuePair<string, Window> entry in Windows)
            DisplayWindow(entry.Value, true);
    }
    public void DisplayScreen()
    {
        var (w, h) = TermInfo.GetSize();
        DisplayBox(0,0,w,h,false);
    }

    private void DisplayWindow(Window window, bool bold)
    {
        int x = window.xOffset;
        int y = window.yOffset;
        int w = window.width;
        int h = window.height;

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