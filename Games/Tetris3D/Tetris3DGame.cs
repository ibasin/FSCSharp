using FSCSharp;
using Raylib_cs;
using System.Numerics;

namespace Tetris3D;

public class Tetris3DGame : Game<Tetris3DGame>
{
    #region Constrcutors
    public Tetris3DGame() : base("Tetris", WidthInSquares * SquareSide, HeightInSquares * SquareSide, Color.Black, true)
    {
        Current.PlaySound("Resources/kalinka.mp3");
        Current.ShowSplashScreen("Resources/splash.png", 7750);

        Vector3 cameraPosition = new Vector3(500, 0, 500);
        // ReSharper disable PossibleLossOfFraction
        Vector3 cameraTarget = new Vector3(WidthInSquares * SquareSide / 2, HeightInSquares * SquareSide / 2, 300);
        // ReSharper restore PossibleLossOfFraction
        var camera = new Camera3D(cameraPosition, cameraTarget, Vector3.UnitZ, 45.0f, CameraProjection.Perspective);
        var cameraGameObject = new Camera3DGameObject(camera);
        GameObjects.Add(cameraGameObject);

        GameObjects.Add(new ScoreKeeper());

        ShapesBlock = new ShapesBlock();
        GameObjects.Add(ShapesBlock);

        GameObjects.Add(new FallingShape());
    }
    #endregion

    #region Properties
    public static int Score { get; set; }
    public ShapesBlock ShapesBlock { get; set; }
    #endregion

    #region Settings
    public const int WidthInSquares = 20;
    public const int HeightInSquares = 40;

    public const int SquareSide = 30;
    #endregion
}