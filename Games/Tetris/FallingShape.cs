using FSCSharp;
using Raylib_cs;
using System.Numerics;

namespace Tetris;

public class FallingShape : TangibleGameObject
{
    #region Embedded Types
    public struct Rect
    {
        #region Constructiors
        public Rect(int x1, int y1, int x2, int y2)
        {
            X1 = x1;
            Y1 = y1;
            X2 = x2;
            Y2 = y2;
        }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return $"({X1},{Y1})-({X2},{Y2})";
        }
        #endregion

        #region Properties
        public int X1 { get; }
        public int Y1 { get; }
        public int X2 { get; }
        public int Y2 { get; }
        #endregion
    }
    #endregion


    #region Constructors
    public FallingShape()
    {
        // ReSharper disable once PossibleLossOfFraction
        var locationInGrid = new Vector2((TetrisGame.WidthInSquares - 4) / 2, 0);
        Location = locationInGrid * TetrisGame.SquareSide;
        Shape = Random.Shared.Next(Data.NumOfShapes);
        Orientation = Random.Shared.Next(4);
    }
    #endregion

    #region Overrides
    public override void Update(float delta)
    {
        Vector2 location = Location;
        var orientation = Orientation;
        if (Game.KeyboardManager.IsKeyPressed(KeyboardKey.Left))
        {
            location = Location.Move(Go.Left, TetrisGame.SquareSide);
            //if (IsCollidingWithSomething()) Location = Location.Move(Go.Right, TetrisGame.SquareSide);
        }
        if (Game.KeyboardManager.IsKeyPressed(KeyboardKey.Right))
        {
            location = Location.Move(Go.Right, TetrisGame.SquareSide);
            //if (IsCollidingWithSomething()) Location = Location.Move(Go.Left, TetrisGame.SquareSide);
        }
        if (Game.KeyboardManager.IsKeyPressed(KeyboardKey.Up))
        {
            orientation--;
            //if (IsCollidingWithSomething()) Orientation++;
        }
        if (Game.KeyboardManager.IsKeyPressed(KeyboardKey.Down))
        {
            orientation++;
            //if (IsCollidingWithSomething()) Orientation--;
        }
        //if (key == KeyboardKey.Space)
        //{
        //    while (!ToDelete) MoveDown();
        //    return;
        //}
        if (!location.Violations.XMinusViolation && 
            !location.Move(Go.Left, TetrisGame.SquareSide * ShapeRect.X1).Move(Go.Right, TetrisGame.SquareSide * ShapeRect.X2).Violations.XPlusViolation)
        {
            Location = location;
            if (Orientation != orientation) Orientation = orientation;
        }

        //Location = Location.Move(Go.Down, delta * 200);
        //if (Location.Move(Go.Down, TetrisGame.SquareSide * ShapeRect.Y2).Violations.YPlusViolation)
        //{
        //    ToDelete = true;
        //    TetrisGame.Current.GameObjects.Add(new FallingShape());
        //}
    }
    public override void Draw()
    {
        for (var x = 0; x < Data.Shapes.GetLength(2); x++)
        {
            for (var y = 0; y < Data.Shapes.GetLength(3); y++)
            {
                var color = Data.Shapes[Shape, Orientation, x, y];
                if (color.HasValue)
                {
                    var xx = (int)Math.Round(Location.X + x * TetrisGame.SquareSide);
                    var yy = (int)Math.Round(Location.Y + y * TetrisGame.SquareSide);
                    Raylib.DrawRectangle(xx, yy, TetrisGame.SquareSide, TetrisGame.SquareSide, color.Value);

                }
            }
        }
    }
    #endregion

    #region Properties
    public Vector2 Location { get; set; }

    public int Shape { get; }
    //public int Shape
    //{
    //    get;
    //    set
    //    {
    //        _currentSizeInBlocks = null;
    //        if (value < 0) value = Data.NumOfShapes - 1;
    //        if (value >= Data.NumOfShapes) value = 0;
    //        field = value;
    //    }
    //}
    public int Orientation
    {
        get;
        set
        {
            _shapeRect = null;
            if (value < 0) value = 4 - 1;
            if (value >= 4) value = 0;
            field = value;
        }
    }

    public Rect ShapeRect
    {
        get
        {
            if (_shapeRect == null)
            {
                int minX = Data.Shapes.GetLength(2);
                int minY = Data.Shapes.GetLength(3);

                int maxX = -1;
                int maxY = -1;

                for (var x = 0; x < Data.Shapes.GetLength(2); x++)
                {
                    for (var y = 0; y < Data.Shapes.GetLength(3); y++)
                    {
                        if (Data.Shapes[Shape, Orientation, x, y].HasValue)
                        {
                            if (x < minX) minX = x;
                            if (y < minY) minY = y;
                            if (x > maxX) maxX = x;
                            if (y > maxY) maxY = y;
                        }
                    }
                }
                _shapeRect = new Rect(minX, minY, maxX, maxY);
                Console.WriteLine($"Shape = {Shape}, Orientation = {Orientation}, Rect = {_shapeRect}");
            }
            return _shapeRect.Value;
        }
    }
    private Rect? _shapeRect;

    #endregion
}