namespace Termination;

/*
 * Textblock is a Window type.
 * It specializes in displaying long blocks of text.
 * While other Windows merely hide data that goes out of the window bounds, Textblock dynamically wraps them.
 * As you'd expect a text to do.
 * This differentiates it from other windows, where wrapping is not ideal.
 * This also allows text to be auto formatted, the user not having to know what size and shape the panel will be before hand.
 */

public class TextBlock : Window
{
    public TextBlock(double x, double y, double w, double h) : base(x,y,w,h)
    {
        Alignment = 0;
        AlignmentVert = 0;
    }

    public override void FillData(string input) //Converts input string into array of tokens. //TODO: standardize color formatting or something idk
    {
        for(int i=0; i<input.Length; i++)
        {
            Data.Add(new Token(input[i]));
        }
    }

    //TODO: add support for paragraph breaking. And possibly center and right aligning.
    //Fills textblock with passed string. Wrapping text around spaces to fit into the limited area cleanly.
    public override void GenFrameBuffer()
    {
        FrameBuffer = new();

        List<Token> text = new(Data); //Text to manipulate; Copy of the Windows Data TODO: there is no reason we need to copy the array atm. Can probably just use a counting int to offset the values that I am deleting. I cannot be bothered to make that optimization right now.

        //Split the input string into wrapped lines.
        for(int h=0; h<aHeight && text.Count != 0; h++) //Loop for every vertical line
        {
            FrameBuffer.Add(new List<Token>());
            int nextBreak = -1; //Location of next break, either wrap or paragraph. If none is found, left at -1
            bool paragraph = false; //whether the next break is a text wrap or a paragraph break.

            for(int i=0; i<aWidth+1 && i<text.Count; i++) //Loop to find either the last space (and thus) paragraph breaks //TODO: foreach?
            {
                if(text[i].Text == ' ') //If a space is found, update the next break (we want the last space)
                    nextBreak = i;
                
                else if(text[i].Text == '¶') //If a paragraph break is found, then mark it, and immediately exit loop
                {
                    nextBreak = i;
                    paragraph = true;
                    i=aWidth+1;
                }
            }
            
            //First check if the absolute width of the Window is longer than the remaining text, and that there is no paragraph break
            //If so, then just use the entire remaining text.
            if(aWidth > text.Count && !paragraph)
            {
                for(int i=0; i<text.Count; i++) //TODO: foreach loop?
                {
                    FrameBuffer[h].Add(text[i]);
                }
                text.Clear();
            }

            //Next, figure out where to wrap text
            else
            {
                if(nextBreak == -1) //If no break, then wrap at the end, splitting the word. TODO: perhaps add logic to add a dash to show text was cut? Like th- ¶ is
                {
                    for(int i=0; i<aWidth; i++) //TODO: foreach loop?
                    {
                        FrameBuffer[h].Add(text[0]);
                        text.Remove(text[0]);
                    }
                }

                else //Otherwise, wrap at the space
                {
                    for(int i=0; i<nextBreak; i++) //TODO: foreach?
                    {
                        FrameBuffer[h].Add(text[0]);
                        text.Remove(text[0]);
                    }
                    text.Remove(text[0]); //The last next character will be a space or paragraph break. Since we are wrapping we can delete it.
                }
            }
        }

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

    //TODO: all of these functions add padding. Make sure padding follows default window colors or w/e
    //Horizontal Alignment functions
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

    /*
    private void AlignLeft(List<Token> list) //Sanity pass to remove left white space. There shouldn't be any, unless the end user makes a mistake
    {
        int deletions = 0; //How many spaces need to be deleted and moved to the end for given line

        for(int i=0; i<list.Count; i++) //Count how many sequential spaces, if any, the line starts with. //TODO foreach
        {
            if(list[i].Text == ' ')
                deletions++;
            else
                break;
        }

        if(deletions > 0) //Deletes any found left white space and replace them at the end
        {
            for(int i=0; i<deletions; i++) //TODO foreach
            {
                list.Add(list[0]);
                list.Remove(list[0]);
            }
        }
    }

    */
}