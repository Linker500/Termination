// See https://aka.ms/new-console-template for more information

/*
 * TODO
 * Change titlebar text?
 * manually set console (desktop) window size?
 * catch Ctrl + C to close program and to proper cleanup, or disable at all maybe?
 */

/*
 *  Hierachy:
 *      Screen:     The Entire screen
 *          Window:     a section of a screen, a "window".
 *              Textblock:  Text optimized window
 *              Canvas:     art optimized window
 *          Token: A single character and color information.
 *
 */

using Termination;

public class Program
{
    public static Screen screen = new();
    public static Terminal term = new() {screen = screen};
    public static Screen logger = new();

    public static void Main()
    {
        TextBlock console = new(0.0,0.0,1.0,1.0);
        string Log = "Logging Start:¶";
        console.FillData(Log);
        logger.Windows.Add("Console",console);

        //Layer 1
        TextBlock padLeft   = new (0.0 , 0.0 , 0.20, 0.6 );
        Canvas    map       = new (0.20, 0.0, 0.60, 0.6);
        TextBlock padRight  = new (0.8 , 0.0 , 0.20, 0.6 );

        //Layer 2
        TextBlock info      = new (0.0 , 0.6 , 0.20, 0.25);
        TextBlock desc      = new (0.20, 0.6 , 0.60, 0.25);

        //Layer 3
        TextBlock ui        = new (0.20, 0.85, 0.60, 0.15);

        screen.Windows.Add("padLeft", padLeft);
        screen.Windows.Add("map", map);
        screen.Windows.Add("padRight", padRight);
        screen.Windows.Add("info", info);
        screen.Windows.Add("desc", desc);
        screen.Windows.Add("ui", ui);

        Assets TileMap = new("./DATA/Map-Example/");
        map.Data2 = TileMap.Images["Map"];

        desc.FillData("The vast open plains of the valley spill out in front of you. This is generic description text. How is it? I dunno. This is better than lorem ipsum I guess. Did I spell that right? I don't care.");
        info.FillData("First Day¶Noon¶Sunny¶293.7 degrees (kelvin)");
        ui.FillData("R: Rerender¶D: Enable Debug¶L: Switch to Console¶X: Quit");

        map.Alignment = 1;
        map.AlignmentVert = 1;

        desc.AlignmentVert = 1;
        info.AlignmentVert = 1;
        ui.AlignmentVert = 0;

        term.KeyPressed += OnKeyPressed;
        term.Start();
    }


    static void OnKeyPressed(object? sender, ConsoleKeyInfo key) //TODO: handle ctrl C
    {
        switch(key.Key)
        {
            case ConsoleKey.D:
            {
                term.ShowDebug = !term.ShowDebug;
                term.Render();
                break;
            }

            case ConsoleKey.L:
            {
                if(term.screen == screen)
                    term.screen = logger;
                else
                    term.screen = screen;
                
                term.Render();
                break;
            }

            case ConsoleKey.X:
            {
                term.Exit();
                break;
            }

            case ConsoleKey.R:
            {
                term.Render();
                break;
            }

        }
    }
}