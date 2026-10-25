using FSCSharp;
using Raylib_cs;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Numerics;

namespace PingPong;

public class PingPongGame : Game<PingPongGame>
{
    #region Constructors
    public PingPongGame(string gameMode) : base("Ping Pong", 1500, 1000, Color.Black)
    {
        Task.Run(() =>
        {
            var delay = 320f;
            for (var i = 0; i < 11; i++)
            {
                Current.PlaySound("Resources/ball-hit.mp3");
                Thread.Sleep((int)MathF.Round(delay));
                delay *= 0.9f;
            }
        });
        
        Current.ShowSplashScreen("Resources/splash.png", 2500);

        Console.WriteLine("Welcome to Ping Pong!");

        GameObjects.Add(new Table());

        ScoreKeeper = new ScoreKeeper();
        GameObjects.Add(ScoreKeeper);

        Console.Clear();
        CLib.Focus();
        while (true)
        {
            if (gameMode == "s")
            {
                LeftPaddle = new LeftPaddleComputer(new Vector2(50, Current.WindowHeight / 2f), Color.Green);
                RightPaddle = new RightPaddle(new Vector2(Current.WindowWidth - 50, Current.WindowHeight / 2f), Color.Red);
                break;
            }
            if (gameMode == "m")
            {
                LeftPaddle = new LeftPaddle(new Vector2(50, Current.WindowHeight / 2f), Color.Green);
                RightPaddle = new RightPaddle(new Vector2(Current.WindowWidth - 50, Current.WindowHeight / 2f), Color.Red);
                break;
            }
            if (gameMode == "d")
            {
                LeftPaddle = new LeftPaddleComputer(new Vector2(50, Current.WindowHeight / 2f), Color.Green);
                RightPaddle = new RightPaddleComputer(new Vector2(Current.WindowWidth - 50, Current.WindowHeight / 2f), Color.Red);
                break;
            }
            //if (gameMode == "m2")
            //{
            //    CheckNetworkConnection();
            //    Console.WriteLine($"Your IP is: {GetLocalIpAddress()}");
            //    Console.Write("Please enter other player's IP: ");
            //    var otherPlayerIp = Console.ReadLine()!.Trim();

            //    var webServer = new WebMonk.WebServer(54548, "http://localhost:55555/");
            //    break;
            //}
            Console.Write("Invalid Input. ");
        }
        GameObjects.Add(LeftPaddle);
        GameObjects.Add(RightPaddle);

        CLib.Unfocus();

        Ball = new Ball(Vector2.Zero).InitForServing(Random.Shared.Next(2) == 0 ? LeftPaddle : RightPaddle);
        GameObjects.Add(Ball);
    }
    #endregion

    #region Methods
    public void CheckNetworkConnection()
    {
        if (!NetworkInterface.GetIsNetworkAvailable())
        {
            CLib.Unfocus();

            // ReSharper disable once VirtualMemberCallInConstructor
            ShowMessage("Network is not available", Color.White);

            while (!Raylib.WindowShouldClose())
            {
                Raylib.PollInputEvents();
            }
            Environment.Exit(0);
        }
    }
    public static string GetLocalIpAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }
        throw new Exception("No network adapters with an IPv4 address in the system!");
    }
    #endregion

    #region Properties
    public Paddle LeftPaddle { get; }
    public Paddle RightPaddle { get; }

    public Ball Ball { get; }

    public ScoreKeeper ScoreKeeper { get; }
    #endregion
}
