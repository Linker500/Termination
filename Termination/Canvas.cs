using System.Diagnostics;

namespace Termination;
public class Canvas
{
    public string Test()
    {
        return "it works";
    }

    public void Display()
    {
        var (x, y) = TermInfo.GetSize();
        Display(x,y);
    }

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
                columnL = '╔';
                columnR = '╗';
                row = '═';
            }
            else if(i!=y-1)
            {
                columnL = '║';
                columnR = '║';
                row = ' ';
            }
            else //if(i==y-1)
            {
                columnL = '╚';
                columnR = '╝';
                row = '═';
            }

            box.Append(columnL);
            box.Append(row,x-2);
            box.Append(columnR);
            box.AppendLine();
        }
        Console.WriteLine(box);
    }
}

/*
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
*/
