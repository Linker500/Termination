// See https://aka.ms/new-console-template for more information
using Termination;

var canvas = new Canvas();
//Console.WriteLine(canvas.Test());



while(true)
{
    Console.Clear();
    var (x, y) = TermInfo.GetSize();
    canvas.Display(x,y);
    
    while(true)
    {
        var (nX, nY) = TermInfo.GetSize();
        if(x != nX || y != nY)
            break;
    }
}

