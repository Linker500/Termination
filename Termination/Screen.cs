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
    public ConsoleColor FGColor {get; set;} //
    public ConsoleColor BGColor {get; set;} //

    public readonly Dictionary<string,Window> Windows = new(); //Dictionary of windows for this Screen
    //public int SizeX, SizeY; //Size of this screen
    
    public Screen()
    {
        FGColor = ConsoleColor.White;
        BGColor = ConsoleColor.Black;
    }

    public void SolveWindows(int SizeX, int SizeY) //TODO: throw exceptions for overlapping / out of bound / impossible screens
    {
        //TODO: this math is off by one. It can lose one column and one row.
        foreach(var (key, val) in Windows)
        {
            Window window = val;
            window.aXOffset = (int)(SizeX * window.XOffset);
            window.aYOffset = (int)(SizeY * window.YOffset);
            window.aWidth = (int)(SizeX * window.Width);
            window.aHeight = (int)(SizeY * window.Height);
        }
    }
}