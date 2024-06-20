namespace Termination;
public struct Token
{
    public char Text {get; set;} = ' '; //TODO: perhaps change this to a string along with other code (text wrapping) to support composed characters and similar

    public (byte Red, byte Green, byte Blue)? ForegroundColor;
    public (byte Red, byte Green, byte Blue)? BackgroundColor;

    public bool Bold {get; set;} = false;           //1
    public bool Faint {get; set;} = false;          //2
    public bool Italic {get; set;} = false;         //3
    public bool Underline {get; set;} = false;      //4
    public bool OverLine {get; set;} = false;       //53
    public bool Strikethrough {get; set;} = false;  //9
    public bool Blink {get; set;} = false;          //5

    public Token()
    {
        
    }
    public Token(char nText)
    {
        Text = nText;
    }

    public Token(char nText,  (byte, byte, byte)? nForeGroundColor)
    {
        Text = nText;
        ForegroundColor = nForeGroundColor;
    }

    public Token(char nText, (byte, byte, byte)? nForeGroundColor, (byte, byte, byte)? nBackGroundColor)
    {
        Text = nText;
        ForegroundColor = nForeGroundColor;
        BackgroundColor = nBackGroundColor;
    }

    public string? GetAnsi() //TODO: test if this function can be borked with garbage data) //TODO: string concatanation as here is bad probably?
    {
        //\x1b[<stuff>m
        List<byte> parameters = new();

        if(ForegroundColor != null) //38;2;r;g;b
        {
            parameters.Add(38);
            parameters.Add(2);
            parameters.Add(ForegroundColor.Value.Red);
            parameters.Add(ForegroundColor.Value.Green);
            parameters.Add(ForegroundColor.Value.Blue);
        }

        if(BackgroundColor != null) //48;2;r;g;b
        {
            parameters.Add(48);
            parameters.Add(2);
            parameters.Add(BackgroundColor.Value.Red);
            parameters.Add(BackgroundColor.Value.Green);
            parameters.Add(BackgroundColor.Value.Blue);
        }

        if(Bold)            {parameters.Add(1);} //1
        if(Faint)           {parameters.Add(2);} //2
        if(Italic)          {parameters.Add(3);} //3
        if(Underline)       {parameters.Add(4);} //4
        if(OverLine)        {parameters.Add(53);} //53
        if(Strikethrough)   {parameters.Add(9);} //9
        if(Blink)           {parameters.Add(5);} //5

        string ansi = "";
        foreach(var i in parameters)
        {
            ansi += i + ";";
        }

        if(ansi.Length == 0)
        {
            return null;
        }
        else
        {
            ansi = ansi.Remove(ansi.Length-1);
            return "\x1b["+ansi+"m";
        }
    }
}