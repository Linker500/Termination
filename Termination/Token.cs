namespace Termination;
public class Token
{
    public char Text {get; set;} //TODO: perhaps change this to a string along with other code (text wrapping) to support composed characters and similar
    public ConsoleColor Color {get; set;}
    public ConsoleColor BGColor {get; set;}

    public Token()
    {
        Text = ' ';
        Color = ConsoleColor.White;
        BGColor = ConsoleColor.Black;
    }
    public Token(char newText)
    {
        Text = newText;
        Color = ConsoleColor.White;
        BGColor = ConsoleColor.Black;
    }

}