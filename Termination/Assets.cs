namespace Termination;
public class Assets
{
    //Dictionary<string, > Texts = new();
    public Dictionary<string,List<List<Token>>> Images = new();

    String ImageDirectory;
    
    public Assets(String nImageDirectory)
    {
        ImageDirectory = nImageDirectory;
        Images.Add("Image",LoadImage());
    }

    //private <type> LoadText ()

    private List<List<Token>> LoadImage () //TODO: async in reading here we are reading a lot of stuff at once? //TODO: various exceptions for not having various parts. Perhaps have default ERRROR assets in case any files are not found (primarily/only(?) the art part).
    {
        //TODO: use better string concatanation on directories probably
        string[] Info = File.ReadAllLines(ImageDirectory + "info.txt"); //TODO: is this even useful to have? Should we just calculate this ourself?
        string[] Ascii = File.ReadAllLines(ImageDirectory + "ascii.txt");
        string[] Color = File.ReadAllLines(ImageDirectory + "color.txt");
        //string[] BGColor = File.ReadAllLines(ImageDirectory + "bgcolor.txt");

        return ProccessImage(int.Parse(Info[0]),int.Parse(Info[1]),Ascii,Color);
    }

    //public static <idk> ProccessText  ()
    //{

    //}

    private List<List<Token>> ProccessImage (int Width, int Height, string[] ascii, string[] color)
    {
        List<List<Token>> Data = new();

        for(int h=0; h<Height; h++)
        {
            Data.Add(new List<Token>());
            for(int w=0; w<Width; w++) //TODO: foreach?
            {
                Data[h].Add(new Token());
            }
        }

        for(int h=0; h<Height; h++) //TODO: probably should be foreach
        {
            for(int w=0; w<Width; w++)
            {
                Data[h][w] = new Token(ascii[h][w], TranslateColor(color[h][w])); 
            }
        }

        return Data;
    }

    // public static Token[,] ProccessImage (int Width, int Height, string[] ascii, string[] color)
    // {
    //     Token[,] Data = new Token[Height,Width];
    //     for(int h=0; h<Height; h++) //TODO: probably should be foreach
    //     {
    //         for(int w=0; w<Width; w++)
    //         {
    //             Data[h,w] = new Token(ascii[h][w], TranslateColor(color[h][w])); 
    //         }
    //     }

    //     return Data;
    // }

    private ConsoleColor? TranslateColor (char input)
    {
        switch (input)
        {
            case ' ':
                return null;

            case 'w':
                return ConsoleColor.White;

            case 'a':
                return ConsoleColor.Gray;

            case 'A':
                return ConsoleColor.DarkGray;

            case 'k':
                return ConsoleColor.Black;
            
            case 'r':
                return ConsoleColor.Red;

            case 'y':
                return ConsoleColor.Yellow;
            
            case 'g':
                return ConsoleColor.Green;

            case 'b':
                return ConsoleColor.Blue;

            case 'c':
                return ConsoleColor.Cyan;

            case 'm':
                return ConsoleColor.Magenta;

            case 'R':
                return ConsoleColor.DarkRed;
            
            case 'Y':
                return ConsoleColor.DarkYellow;
            
            case 'G':
                return ConsoleColor.DarkGreen;

            case 'B':
                return ConsoleColor.DarkBlue;

            case 'C':
                return ConsoleColor.DarkCyan;

            case 'M':
                return ConsoleColor.DarkMagenta;
            
            default: //TODO: throw exception or warning somewhere maybe?
                return null;
        }

    }

}