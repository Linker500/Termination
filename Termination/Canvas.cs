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
    public Canvas(int x, int y, int w, int h) : base(x,y,w,h)
    {
        
    }

    public override void Fill(string text)
    {

    }
}