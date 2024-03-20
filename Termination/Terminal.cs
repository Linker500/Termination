namespace Termination;
public class Terminal
{
    
    public Screen screen;
    //private readonly ConsoleInput input = new();
    public event EventHandler<ConsoleKeyInfo>? KeyPressed; //Event for when key is pressed


    public bool EnableInput {get; set;} = true; //Whether to recieve input at all
    public bool EchoInput {get; set;} = false; //Whether to print the user input
    private bool ShowDebug = false; //Display cursor and window boundaries?

    public Terminal()
    {
        KeyPressed += OnKeyPressed;
        screen = new();

        screen.Windows["thr"].Data = "What the fuck did you just fucking say about me, you little bitch? I'll have you know I graduated top of my class in the Navy Seals, and I've been involved in numerous secret raids on Al-Quaeda, and I have over 300 confirmed kills. I am trained in gorilla warfare and I'm the top sniper in the entire US armed forces. You are nothing to me but just another target. I will wipe you the fuck out with precision the likes of which has never been seen before on this Earth, mark my fucking words. You think you can get away with saying that shit to me over the Internet? Think again, fucker. As we speak I am contacting my secret network of spies across the USA and your IP is being traced right now so you better prepare for the storm, maggot. The storm that wipes out the pathetic little thing you call your life. You're fucking dead, kid. I can be anywhere, anytime, and I can kill you in over seven hundred ways, and that's just with my bare hands. Not only am I extensively trained in unarmed combat, but I have access to the entire arsenal of the United States Marine Corps and I will use it to its full extent to wipe your miserable ass off the face of the continent, you little shit. If only you could have known what unholy retribution your little \"clever\" comment was about to bring down upon you, maybe you would have held your fucking tongue. But you couldn't, you didn't, and now you're paying the price, you goddamn idiot. I will shit fury all over you and you will drown in it. You're fucking dead, kiddo.";
        screen.Windows["one"].Data = "The FitnessGram Pacer test is a multistage aerobic capacity test that progressively gets more difficult as it continues. The 20 meter Pacer test will begin in 30 seconds. Line up at the start. The running speed starts slowly, but gets faster each minute after you hear this signal *boop*. A single lap should be completed each time you hear this sound *ding*. Remember to run in a straight line, and run as long as possible. The second time you fail to complete a lap before the sound, your test is over. The test will begin on the word start. On your mark, get ready, start.";
        screen.Windows["two"].Data = "I'd just like to interject for a moment. What you're refering to as Linux, is in fact, GNU/Linux, or as I've recently taken to calling it, GNU plus Linux. Linux is not an operating system unto itself, but rather another free component of a fully functioning GNU system made useful by the GNU corelibs, shell utilities and vital system components comprising a full OS as defined by POSIX. Many computer users run a modified version of the GNU system every day, without realizing it. Through a peculiar turn of events, the version of GNU which is widely used today is often called Linux, and many of its users are not aware that it is basically the GNU system, developed by the GNU Project. There really is a Linux, and these people are using it, but it is just a part of the system they use. Linux is the kernel: the program in the system that allocates the machine\'s resources to the other programs that you run. The kernel is an essential part of an operating system, but useless by itself; it can only function in the context of a complete operating system. Linux is normally used in combination with the GNU operating system: the whole system is basically GNU with Linux added, or GNU/Linux. All the so-called Linux distributions are really distributions of GNU/Linux!";
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

    void Render() //TODO: make cross platform detector of window size change to automate rerendering
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

        Console.CursorVisible = ShowDebug;
        Console.Clear();
        var (x, y) = TermInfo.GetSize();
        screen.SolveWindows();
        screen.Render();

        if(ShowDebug)
        {
            screen.Display();
        }
    }

    void OnKeyPressed(object? sender, ConsoleKeyInfo key)
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
}