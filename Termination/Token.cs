using System.Dynamic;

namespace Termination;
public class Token
{
    public char Text {get; set;} = ' '; //TODO: perhaps change this to a string along with other code (text wrapping) to support composed characters and similar
    public ConsoleColor? Color {get; set;} = null;
    public ConsoleColor? BGColor {get; set;} = null;

    public Token()
    {

    }
    public Token(char newText)
    {
        Text = newText;
    }

    public Token(char newText, ConsoleColor? newColor)
    {
        Text = newText;
        Color = newColor;
    }

    public Token(char newText, ConsoleColor? newColor, ConsoleColor? newBGColor)
    {
        Text = newText;
        Color = newColor;
        BGColor = newBGColor;
    }
}