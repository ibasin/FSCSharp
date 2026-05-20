using FSCSharp;
using Raylib_cs;

namespace Tetris;

public static class Data
{
    #region Constructors
    static Data()
    {
        Shapes = new Color?[NumOfShapes, Enum.GetNames(typeof(Go)).Length, 4, 4];
        InitShapesToNull();

        int shape = -1;

        #region Line
        {
            shape++;
            Color color = Color.Purple;

            Shapes[shape, (int)Go.Up, 1, 0] = color;
            Shapes[shape, (int)Go.Up, 1, 1] = color;
            Shapes[shape, (int)Go.Up, 1, 2] = color;
            Shapes[shape, (int)Go.Up, 1, 3] = color;

            Shapes[shape, (int)Go.Right, 0, 1] = color;
            Shapes[shape, (int)Go.Right, 1, 1] = color;
            Shapes[shape, (int)Go.Right, 2, 1] = color;
            Shapes[shape, (int)Go.Right, 3, 1] = color;

            Shapes[shape, (int)Go.Down, 1, 0] = color;
            Shapes[shape, (int)Go.Down, 1, 1] = color;
            Shapes[shape, (int)Go.Down, 1, 2] = color;
            Shapes[shape, (int)Go.Down, 1, 3] = color;

            Shapes[shape, (int)Go.Left, 0, 2] = color;
            Shapes[shape, (int)Go.Left, 1, 2] = color;
            Shapes[shape, (int)Go.Left, 2, 2] = color;
            Shapes[shape, (int)Go.Left, 3, 2] = color;
        }
        #endregion

        #region Square
        {
            shape++;
            Color color = Color.Blue;

            Shapes[shape, (int)Go.Up, 1, 1] = color;
            Shapes[shape, (int)Go.Up, 2, 1] = color;
            Shapes[shape, (int)Go.Up, 1, 2] = color;
            Shapes[shape, (int)Go.Up, 2, 2] = color;

            Shapes[shape, (int)Go.Right, 1, 1] = color;
            Shapes[shape, (int)Go.Right, 2, 1] = color;
            Shapes[shape, (int)Go.Right, 1, 2] = color;
            Shapes[shape, (int)Go.Right, 2, 2] = color;

            Shapes[shape, (int)Go.Down, 1, 1] = color;
            Shapes[shape, (int)Go.Down, 2, 1] = color;
            Shapes[shape, (int)Go.Down, 1, 2] = color;
            Shapes[shape, (int)Go.Down, 2, 2] = color;

            Shapes[shape, (int)Go.Left, 1, 1] = color;
            Shapes[shape, (int)Go.Left, 2, 1] = color;
            Shapes[shape, (int)Go.Left, 1, 2] = color;
            Shapes[shape, (int)Go.Left, 2, 2] = color;
        }
        #endregion

        #region L-Shape
        {
            shape++;
            Color color = Color.Red;

            Shapes[shape, (int)Go.Up, 0, 0] = color;
            Shapes[shape, (int)Go.Up, 1, 0] = color;
            Shapes[shape, (int)Go.Up, 1, 1] = color;
            Shapes[shape, (int)Go.Up, 1, 2] = color;

            Shapes[shape, (int)Go.Right, 0, 1] = color;
            Shapes[shape, (int)Go.Right, 1, 1] = color;
            Shapes[shape, (int)Go.Right, 2, 1] = color;
            Shapes[shape, (int)Go.Right, 2, 0] = color;

            Shapes[shape, (int)Go.Down, 1, 0] = color;
            Shapes[shape, (int)Go.Down, 1, 1] = color;
            Shapes[shape, (int)Go.Down, 1, 2] = color;
            Shapes[shape, (int)Go.Down, 2, 2] = color;

            Shapes[shape, (int)Go.Left, 0, 1] = color;
            Shapes[shape, (int)Go.Left, 1, 1] = color;
            Shapes[shape, (int)Go.Left, 2, 1] = color;
            Shapes[shape, (int)Go.Left, 0, 2] = color;
        }
        #endregion

        #region J-Shape
        {
            shape++;
            Color color = Color.Green;

            Shapes[shape, (int)Go.Up, 1, 0] = color;
            Shapes[shape, (int)Go.Up, 1, 1] = color;
            Shapes[shape, (int)Go.Up, 1, 2] = color;
            Shapes[shape, (int)Go.Up, 2, 0] = color;

            Shapes[shape, (int)Go.Right, 0, 1] = color;
            Shapes[shape, (int)Go.Right, 1, 1] = color;
            Shapes[shape, (int)Go.Right, 2, 1] = color;
            Shapes[shape, (int)Go.Right, 2, 2] = color;

            Shapes[shape, (int)Go.Down, 1, 0] = color;
            Shapes[shape, (int)Go.Down, 1, 1] = color;
            Shapes[shape, (int)Go.Down, 1, 2] = color;
            Shapes[shape, (int)Go.Down, 0, 2] = color;

            Shapes[shape, (int)Go.Left, 0, 0] = color;
            Shapes[shape, (int)Go.Left, 0, 1] = color;
            Shapes[shape, (int)Go.Left, 1, 1] = color;
            Shapes[shape, (int)Go.Left, 2, 1] = color;
        }
        #endregion

        #region T-Shape
        {
            shape++;
            Color color = Color.Magenta;

            Shapes[shape, (int)Go.Up, 1, 0] = color;
            Shapes[shape, (int)Go.Up, 0, 1] = color;
            Shapes[shape, (int)Go.Up, 1, 1] = color;
            Shapes[shape, (int)Go.Up, 2, 1] = color;

            Shapes[shape, (int)Go.Right, 1, 0] = color;
            Shapes[shape, (int)Go.Right, 1, 1] = color;
            Shapes[shape, (int)Go.Right, 1, 2] = color;
            Shapes[shape, (int)Go.Right, 2, 1] = color;

            Shapes[shape, (int)Go.Down, 0, 1] = color;
            Shapes[shape, (int)Go.Down, 1, 1] = color;
            Shapes[shape, (int)Go.Down, 2, 1] = color;
            Shapes[shape, (int)Go.Down, 1, 2] = color;

            Shapes[shape, (int)Go.Left, 1, 0] = color;
            Shapes[shape, (int)Go.Left, 0, 1] = color;
            Shapes[shape, (int)Go.Left, 1, 1] = color;
            Shapes[shape, (int)Go.Left, 1, 2] = color;
        }
        #endregion

        #region Z-Shape
        {
            shape++;
            Color color = Color.White;

            Shapes[shape, (int)Go.Up, 1, 0] = color;
            Shapes[shape, (int)Go.Up, 0, 1] = color;
            Shapes[shape, (int)Go.Up, 1, 1] = color;
            Shapes[shape, (int)Go.Up, 0, 2] = color;

            Shapes[shape, (int)Go.Right, 0, 0] = color;
            Shapes[shape, (int)Go.Right, 1, 0] = color;
            Shapes[shape, (int)Go.Right, 1, 1] = color;
            Shapes[shape, (int)Go.Right, 2, 1] = color;

            Shapes[shape, (int)Go.Down, 2, 0] = color;
            Shapes[shape, (int)Go.Down, 1, 1] = color;
            Shapes[shape, (int)Go.Down, 2, 1] = color;
            Shapes[shape, (int)Go.Down, 1, 2] = color;

            Shapes[shape, (int)Go.Left, 0, 1] = color;
            Shapes[shape, (int)Go.Left, 1, 1] = color;
            Shapes[shape, (int)Go.Left, 1, 2] = color;
            Shapes[shape, (int)Go.Left, 2, 2] = color;
        }
        #endregion

        #region S-Shape
        {
            shape++;
            Color color = Color.Yellow;

            Shapes[shape, (int)Go.Up, 0, 0] = color;
            Shapes[shape, (int)Go.Up, 0, 1] = color;
            Shapes[shape, (int)Go.Up, 1, 1] = color;
            Shapes[shape, (int)Go.Up, 1, 2] = color;

            Shapes[shape, (int)Go.Right, 1, 0] = color;
            Shapes[shape, (int)Go.Right, 2, 0] = color;
            Shapes[shape, (int)Go.Right, 0, 1] = color;
            Shapes[shape, (int)Go.Right, 1, 1] = color;

            Shapes[shape, (int)Go.Down, 1, 0] = color;
            Shapes[shape, (int)Go.Down, 1, 1] = color;
            Shapes[shape, (int)Go.Down, 2, 1] = color;
            Shapes[shape, (int)Go.Down, 2, 2] = color;

            Shapes[shape, (int)Go.Left, 1, 1] = color;
            Shapes[shape, (int)Go.Left, 2, 1] = color;
            Shapes[shape, (int)Go.Left, 0, 2] = color;
            Shapes[shape, (int)Go.Left, 1, 2] = color;
        }
        #endregion
    }
    static void InitShapesToNull()
    {
        for (var shape = 0; shape < Shapes.GetLength(0); shape++)
        {
            for (var orientation = 0; orientation < Shapes.GetLength(1); orientation++)
            {
                for (var x = 0; x < Shapes.GetLength(2); x++)
                {
                    for (var y = 0; y < Shapes.GetLength(3); y++)
                    {
                        Shapes[shape, orientation, x, y] = null;
                    }
                }
            }
        }
    }
    #endregion

    #region Properties
    public static Color?[,,,] Shapes { get; } //[shape, orientation, x, y]
    public const int NumOfShapes = 7;
    #endregion
}