namespace Termination;
public class Terminal
{
    public Screen screen {get; set;}

    public int SizeX, SizeY; //Size of terminal

    //TODO: derive default colors from user's terminal at start? Also make work for non true color!! Truecolor absolute white and black are temporary
    public (byte,byte,byte) DefFG = (255,255,255); //User's default foreground color. 
    //public ConsoleColor DefFG = ConsoleColor.White;
    public (byte,byte,byte) DefBG = (0,0,0); //User's default background color.
    //public ConsoleColor DefBG = ConsoleColor.Black;

    public bool EnableInput {get; set;} = true; //Whether to recieve input at all
    public bool EchoInput {get; set;} = false; //Whether to print the user input
    
    public event EventHandler<ConsoleKeyInfo>? KeyPressed; //Event for when key is pressed
    private bool ShowDebug = false; //Display cursor and window boundaries?

    public Terminal()
    {
        (SizeX, SizeY) = TermInfo.GetSize();
        KeyPressed += OnKeyPressed;
        screen = new();
    }

    public void Start()
    {
        Task.Factory.StartNew(this.Render, TaskCreationOptions.LongRunning);
        
        while(true)
        {
            var keyInfo = Console.ReadKey(true);

            if(!EnableInput) continue; //Stop if input is not enabled

            if(EchoInput)
            {
                Console.Write(keyInfo.KeyChar);
            }

            KeyPressed?.Invoke(null, keyInfo); //Trigger event
        }
    }

    //Render Screen
    private void Render() //TODO: make cross platform detector of window size change to automate rerendering
    {
        // while(true)
        // {
        //     Console.Clear();
        //     var (x, y) = TermInfo.GetSize();
        //     screen.SolveWindows();
        //     screen.Display();
        //     screen.Render("one");
        //     screen.Render("two");
        //     screen.Render("thr");
        //     //screen.Display();

        //     while(true) //TODO: this is for testing and a spam read loop is NOT good.
        //     {
        //         var (nX, nY) = TermInfo.GetSize();
        //         if(x != nX || y != nY)
        //             break;
        //     }
        // }
        (SizeX, SizeY) = TermInfo.GetSize();
        Console.CursorVisible = ShowDebug;
        Console.Clear();
        screen.SolveWindows(SizeX,SizeY);
        foreach(var (key, val) in screen.Windows)
            RenderWindow(val);
        
        if(ShowDebug)
        {
            Border();
        }
    }

    //Render given window
    private void RenderWindow(Window Rendering) //TODO: if frame buffer somehow ends up with missing tokens then do an exception handler to just output them blank
    {
        Rendering.GenFrameBuffer();

        //Set cursor to top left of window
        //Move cursor along line by line printing character by character via it's frame buffer   
        for(int y=0; y<Rendering.aHeight; y++)
        {
            Console.SetCursorPosition(Rendering.aXOffset,Rendering.aYOffset+y);
            for(int x=0; x<Rendering.aWidth; x++)
            {
                var token = Rendering.FrameBuffer[y][x];
                
                (byte,byte,byte) NewFGColor = DefFG;
                (byte,byte,byte) NewBGColor = DefBG;
                // ConsoleColor NewColor = DefFG;
                // ConsoleColor NewBGColor = DefBG;

                // if(token.Color != null) //TODO: perhaps wait for and catch an exception for the typecast being null than check like this.
                //     NewColor = (ConsoleColor)token.Color;
                // if(token.BGColor != null)
                //     NewBGColor = (ConsoleColor)token.BGColor;
                
                // Console.ForegroundColor = NewColor;
                // Console.BackgroundColor = NewBGColor;

                string? ansi = token.GetAnsi(); //TODO: !!! Check if next character has different properties and colors and if so don't change it, since ansi code changes are probably slow!
                
                //TODO: concatanation like this is gross right? possibly use something else and just combine the string into one final string that is printed
                if(ansi != null)
                    Console.Write(token.GetAnsi() + token.Text + "\x1b[0m");  //TODO: this hard coded ansi reset is NOT CORRECT and is temporary. It should restet to the terminals DEFAULT colors, but that is not implemented yet
                else
                    Console.Write(token.Text);
            }
        }

        Console.ForegroundColor = screen.FGColor;
        Console.BackgroundColor = screen.BGColor;
    }

    private void OnKeyPressed(object? sender, ConsoleKeyInfo key) //TODO: handle ctrl C
    {
        switch(key.Key)
        {
            case ConsoleKey.D:
            {
                ShowDebug = !ShowDebug;
                break;
            }

            case ConsoleKey.X:
            {
                CleanUp();
                Environment.Exit(0);
                break;
            }

            case ConsoleKey.R:
            {
                Render();
                break;
            }

        }
    }

    //Draws border of Everything
    public void Border()
    {
        //Draw border Outline
        BorderScreen();
        
        //Cycle through Windows, displaying them one by one.
        foreach(KeyValuePair<string, Window> entry in screen.Windows)
            BorderWindow(entry.Value, false);
    }

    //Draws border of the Screen
    public void BorderScreen()
    {
        DrawBox(0,0,SizeX,SizeY,true);
    }

    //Draws border of specific Window
    public static void BorderWindow(Window window, bool bold)
    {
        int x = window.aXOffset;
        int y = window.aYOffset;
        int w = window.aWidth;
        int h = window.aHeight;

        DrawBox(x, y, w, h, bold);
    }

    //Moves cursor to draw an arbitrarily sized border.
    private static void DrawBox(int x, int y, int w, int h, bool bold)
    {
        char[] b; //Border lines

        if(bold)
            b = new char[] {'╔','╗','═','║','╚','╝'}; 
        else
            b = new char[] {'┌','┐','─','│','└','┘'};

        /* Prints top row and right column
         * Starting at top left corner.
         * ​ ╔══╗
         *     ║
         */
        Console.SetCursorPosition(x, y);
        Console.Write(b[0]);
        for(int i = 1; i < w-1; i++)
            Console.Write(b[2]);

        Console.Write(b[1]);
        Console.SetCursorPosition(x+w-1, y+1);
        for(int i = 1; i < h; i++)
        {
            Console.Write(b[3]);
            Console.SetCursorPosition(x+w-1, y+i);
        }

        /* Prints left column and bottom row
         * Starting again at top left corner.
         *  ║
         * ​ ╚══╝
         */
        Console.SetCursorPosition(x, y+1);
        for(int i = 1; i < h; i++)
        {
            Console.Write(b[3]);
            Console.SetCursorPosition(x, y+i);
        }
        Console.Write(b[4]);
        for(int i = 1; i < w-1; i++)
            Console.Write(b[2]);
        Console.Write(b[5]);
    }

    private static void CleanUp() //Cleans up terminal. Used before exiting
    {
        Console.ResetColor();
        Console.CursorVisible = true;
        Console.Clear();
    }
}