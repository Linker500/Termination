public class Token
{
    public char text {get; set;} //TODO: perhaps change this to a string along with other code (text wrapping) to support composed characters and similar
    public string ansi {get; set;}

    public Token()
    {
        text = ' ';
        ansi = "";
    }
    public Token(char newText)
    {
        text = newText;
        ansi = "";
    }

}