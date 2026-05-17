using FSCSharp;
using Raylib_cs;

namespace Snake;

public class SnakeGame : Game<SnakeGame>
{
    #region Constrcutors
    public SnakeGame() : base("Snake", 1600, 1000, new Color(56, 106, 28))
    {
        if (Current.WindowWidth % BoardCellSize != 0) throw new Exception("Board size must be divisible by board cell size!");
        if (Current.WindowHeight % BoardCellSize != 0) throw new Exception("Board size must be divisible by board cell size!");

        BoardSize = new(Current.WindowWidth / BoardCellSize, Current.WindowHeight / BoardCellSize);

        Current.ShowSplashScreen("Resources/splash.png", 2000);

        AppleTexture = Raylib.LoadTexture("Resources/apple.png");

        GameObjects.Add(new ScoreKeeper());

        Snake = new Snake();
        GameObjects.Add(Snake);

        Apple = new Apple();
        GameObjects.Add(Apple);
    }
    #endregion

    #region Properties
    public Snake Snake { get; set; }
    public Apple Apple { get; set; }

    public Point BoardSize { get; }
    public const int BoardCellSize = 40;

    public Texture2D AppleTexture { get; set; }
    #endregion

}