using System.Runtime.CompilerServices;

namespace Termination;

/*
 * Canvas is a type of window that's data is defined and addressed absolutely.
 * It is designed to display things like pixel art.
 * A canvas does not wrap. It merely cuts off the displayed data where appropiate.
 * If we wrapped instead, it would completely destroy the any art of structure you make of text.
 */
public class Canvas : Window
{
    public Canvas(double x, double y, double w, double h) : base(x,y,w,h)
    {
        
    }

    public override void FillData(string input)
    {
        throw new NotImplementedException();
    }
    public override void GenFrameBuffer()
    {
        FrameBuffer = new(Data2);

        if(Alignment == 0) //Left aligned
        {
            for(int i=0; i<FrameBuffer.Count; i++) //TODO: foreach
                AlignLeft(FrameBuffer[i]);
        }

        else if(Alignment == 1) //Center aligned
        {
            for(int i=0; i<FrameBuffer.Count; i++) //TODO: foreach
                AlignCenter(FrameBuffer[i]);
        }

        else if(Alignment == 2) //Right aligned
        {
            for(int i=0; i<FrameBuffer.Count; i++) //TODO: foreach
                AlignRight(FrameBuffer[i]);
        }

        if(AlignmentVert == 0) //Top aligned
            AlignTop(FrameBuffer);
        else if(AlignmentVert == 1) //Middle aligned
            AlignMiddle(FrameBuffer);
        else if(AlignmentVert == 2) //Bottom aligned
            AlignBottom(FrameBuffer);
    }

    private void AlignLeft(List<Token> list)
    {
        while(list.Count < aWidth)
            list.Add(new Token(' '));
    }
    private void AlignCenter(List<Token> list)
    {
        bool odd = true;
        while(list.Count < aWidth)
        {
            if(odd)
                list.Add(new Token(' '));
            else
                list.Insert(0, new Token(' '));
            
            odd = !odd;
        }
    }
    private void AlignRight(List<Token> list)
    {
        while(list.Count < aWidth)
            list.Insert(0, new Token(' '));
    }

    //Vertical Alignment functions
    private void AlignTop(List<List<Token>> list)
    {
        while(list.Count < aHeight)
            list.Add(GenerateBlankLine(aWidth));
    }
    private void AlignMiddle(List<List<Token>> list)
    {
        bool odd = true;
        while(list.Count < aHeight)
        {
            if(odd)
                list.Add(GenerateBlankLine(aWidth));
            else
                list.Insert(0,GenerateBlankLine(aWidth));
            odd = !odd;
        }

        //list.Insert(0,GenerateBlankLine(aWidth));
    }
    private void AlignBottom(List<List<Token>> list)
    {
        while(list.Count < aHeight)
            list.Insert(0,GenerateBlankLine(aWidth));
    }

        // FrameBuffer = new();

        // for(int h=0; h<aHeight; h++)
        // {
        //     FrameBuffer.Add(new List<Token>());
        //     FrameBuffer[h] = GenerateBlankLine(aWidth);
        // }

        // for(int h=0; h<aHeight && h<Data2.GetLength(0); h++)
        // {
        //     for(int w=0; w<aWidth && w<Data2.GetLength(1); w++)
        //     {
        //         FrameBuffer[h][w] = Data2[h,w];
        //     }
        // }
}