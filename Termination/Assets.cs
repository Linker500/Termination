using System.Runtime.CompilerServices;

namespace Termination;
public class Assets
{
    //Dictionary<string, > Texts = new();
    public Dictionary<string,List<List<Token>>> Images = new();

    string AssetPath;
    public Assets(string nAssetPath)    //TODO: exception handling for file not found
    {
        AssetPath = nAssetPath;
        // string[] textPaths  = Directory.GetDirectories(Path.Combine(AssetPath, "TXT"));
        // foreach(string i in textPaths)
        // {
        //     LoadText(i);    
        // }


        string[] imagePaths = Directory.GetDirectories(Path.Combine(AssetPath, "IMG"));
        foreach(string i in imagePaths)
        {
            LoadImage(i);    
        }
    }
    //private <type> LoadText ()

    private void LoadImage (string image) //TODO: async in reading here we are reading a lot of stuff at once? //TODO: various exceptions for not having various parts. Perhaps have default ERRROR assets in case any files are not found (primarily/only(?) the art part).
    {
        //The Image directory is "./<AssetPath>/IMG/<ImageName>"

        string[] info  = File.ReadAllLines(Path.Combine(image, "info.txt")); //TODO: is this even useful to have? Should we just calculate this ourself?
        string[] ascii = File.ReadAllLines(Path.Combine(image, "ascii.txt"));
        string[] color = File.ReadAllLines(Path.Combine(image, "color.txt"));
        //string[] BGColor = File.ReadAllLines(ImageDirectory + "bgcolor.txt");

        Images.Add(Path.GetFileName(image), ProccessImage(int.Parse(info[0]),int.Parse(info[1]),ascii,color));
    }

    //private static <idk> ProccessText  ()
    //{

    //}

    private List<List<Token>> ProccessImage (int Width, int Height, string[] ascii, string[] color)
    {
        List<List<Token>> Data = new();


        //TODO: this is all gross
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

private (byte,byte,byte)? TranslateColor (char input) //TODO: this translation is TEMPORARY until my file format is updated for ansi!
    {
        switch (input)
        {
            case ' ':
                return null;

            case 'w': //White
                return (242,242,242);

            case 'a': //Gray
                return (204,204,204);

            case 'A': //Dark Gray
                return (118,188,188);

            case 'k': //Black
                return (12,12,12);
            
            case 'r': //Red
                return (231,72,86);

            case 'y': //Yellow
                return (249,241,165);
            
            case 'g': //Green
                return (22,198,12);

            case 'b': //Blue
                return (59,120,255);

            case 'c': //Cyan
                return (97,214,214);

            case 'm': //Magenta
                return (180,0,158);

            case 'R': //Dark Red
                return (197,15,31);
            
            case 'Y': //Dark Yellow
                return (193,156,0);
            
            case 'G': //Dark Green
                return (19,161,14);

            case 'B': //Dark Blue
                return (0,55,218);

            case 'C': //Dark Cyan
                return (58,150,221);

            case 'M': //Dark Magenta
                return (136,23,152);
            
            default: //TODO: throw exception or warning somewhere maybe?
                return null;
        }

    }
    private ConsoleColor? OLD_TranslateColor (char input)
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