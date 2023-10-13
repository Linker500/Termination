using System.Runtime.CompilerServices;

namespace Termination;

public class Canvas
{
    public int xOffset {get;}
    public int yOffset {get;}
    public int width {get;}
    public int height {get;}

    public List<List<Token>> frameBuffer = new();

    public Canvas(int x, int y, int w, int h)
    {
        xOffset = x;
        yOffset = y;
        width = w;
        height = h;

        GenerateBlank();
    }
    public void FillFrame(string text)
    {
        List<string> textNew = new();

        for(int h=0; h<height && text.Length != 0; h++)
        {
            
            if(width > text.Length)
            {
                textNew.Add(text);
                text = "";
            }

            else
            {
                int lastSpace = -1;
                for(int i=0; i<width && i<text.Length; i++)
                {
                    if(text[i] == ' ')
                        lastSpace = i;
                }

                if(lastSpace == -1)
                {
                    textNew.Add(text.Substring(0,width));
                    text = text.Substring(width);
                }
                else 
                {
                    textNew.Add(text.Substring(0,lastSpace));
                    text = text.Substring(lastSpace+1);
                }
            }
        }

        for(int y=0; y< textNew.Count; y++)
        {
            for(int x=0; x< textNew[y].Length; x++)
            {
                frameBuffer[y][x].text = textNew[y][x];
            }
        }
    }

    private void GenerateBlank()
    {
        for(int y=0; y<height; y++)
        {
            List<Token> line = new();
            for(int x=0; x<width; x++)
            {
                line.Add(new(' '));
            }
            frameBuffer.Add(line);
        }
    }
}