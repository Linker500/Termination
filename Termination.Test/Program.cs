// See https://aka.ms/new-console-template for more information
using Termination;

var screen = new Screen();
//Console.WriteLine(board.Test());

while(false)
{
    Console.Clear();
    var (x, y) = TermInfo.GetSize();
    screen.Display();
    
    while(true) //TODO: this is for testing and a spam read loop is NOT good.
    {
        var (nX, nY) = TermInfo.GetSize();
        if(x != nX || y != nY)
            break;
    }
}

screen.Display();
//screen.DisplayCanvas(screen.canvases.First(),true);