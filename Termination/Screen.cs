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
    public readonly Dictionary<string,Window> Windows = new(); //Dictionary of windows for this Screen
    public int SizeX, SizeY; //Size of this screen
    
    public Screen()
    {
        (SizeX, SizeY) = TermInfo.GetSize();
        Windows.Add("one",new TextBlock(0.0, 0.0, 0.3, 0.4));
        Windows.Add("two",new TextBlock(0.05, 0.4, 0.6, 0.6));
        Windows.Add("thr",new TextBlock(0.4, 0.04, 0.6, 0.35));
        //Windows.Add("fou",new TextBlock(0.0, 0.4, 0.0, 0.0));

        SolveWindows();
    }

    public void SolveWindows() //TODO: throw exceptions for overlapping / impossible screens
    {
        (SizeX, SizeY) = TermInfo.GetSize();

        //TODO: this math is off by one. It loses one column and one row.
        foreach(var (key, val) in Windows)
        {
            Window window = val;
            window.aXOffset = (int)(SizeX * window.XOffset);
            window.aYOffset = (int)(SizeY * window.YOffset);
            window.aWidth = (int)(SizeX * window.Width);
            window.aHeight = (int)(SizeY * window.Height);
        }
    }

    //Renders all Windows
    public void Render()
    {
        foreach(var (key, val) in Windows)
            Render(val);
    }

    //Render specific Window by name
    public void Render(string WinName)
    {
        Render(Windows[WinName]);
    }

    //Render given window
    private void Render(Window Rendering) //TODO: if frame buffer somehow ends up with missing tokens then do an exception handler to just output them blank
    {
        Rendering.GenFrameBuffer();

        //Set cursor to top left of window
        //Move cursor along line by line printing character by character via it's frame buffer   
        for(int y=0; y<Rendering.aHeight; y++)
        {
            Console.SetCursorPosition(Rendering.aXOffset,Rendering.aYOffset+y);
            for(int x=0; x<Rendering.aWidth; x++)
            {
                Console.Write(Rendering.FrameBuffer[y][x].Text);
            }
        }
    }

    /*
    //Renders all Windows
    public void Render()
    {
        foreach(var (key, val) in Windows)
            Render(val);
    }
    //Renders specific Window by name
    public void Render(string name) {Render(Windows[name]);}

    //Renders specific Window
    public void Render(Window window)
    {
        int xo = window.xOffset;
        int yo = window.yOffset;

        for(int y=0; y<window.MinHeight; y++) //TODO: can this be a nested foreach? idk
        {
            for(int x=0; x<window.MinWidth; x++)
            {
                Console.SetCursorPosition(x+xo,y+yo);

                Token token = window.FrameBuffer[y][x];

                Console.ForegroundColor = token.Color;
                Console.BackgroundColor = token.BGColor;
                Console.Write(token.Text);
            }
        }
    }

    */

    //Draws border of Everything
    public void Display()
    {
        //Draw border Outline
        DisplayScreen();
        
        //Cycle through Windows, displaying them one by one.
        foreach(KeyValuePair<string, Window> entry in Windows)
            DisplayWindow(entry.Value, true);
    }

    //Draws border of the Screen
    public void DisplayScreen()
    {
        DisplayBox(0,0,SizeX,SizeY,false);
    }

    //Draws border of specific Window
    private void DisplayWindow(Window window, bool bold)
    {
        int x = window.aXOffset;
        int y = window.aYOffset;
        int w = window.aWidth;
        int h = window.aHeight;

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