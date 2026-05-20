using FSCSharp;
using Raylib_cs;
using System.Numerics;

namespace Tetris;

public class FallingShape : TangibleGameObject
{
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
        if (Game.KeyboardManager.IsKeyPressed(KeyboardKey.Left))
        {
            Location = Location.Move(Go.Left, TetrisGame.SquareSide);
            //if (IsCollidingWithSomething()) Location = Location.Move(Go.Right, TetrisGame.SquareSide);
        }
        if (Game.KeyboardManager.IsKeyPressed(KeyboardKey.Right))
        {
            Location = Location.Move(Go.Right, TetrisGame.SquareSide);
            //if (IsCollidingWithSomething()) Location = Location.Move(Go.Left, TetrisGame.SquareSide);
        }
        if (Game.KeyboardManager.IsKeyPressed(KeyboardKey.Up))
        {
            Orientation--;
            //if (IsCollidingWithSomething()) Orientation++;
        }
        if (Game.KeyboardManager.IsKeyPressed(KeyboardKey.Down))
        {
            Orientation++;
            //if (IsCollidingWithSomething()) Orientation--;
        }
        //if (key == KeyboardKey.Space)
        //{
        //    while (!ToDelete) MoveDown();
        //    return;
        //}
        Location = Location.Move(Go.Down, delta * 200);

        if (!Location.IsValid || !Location.Move(Go.Down, TetrisGame.SquareSide).Move(Go.Right, TetrisGame.SquareSide).IsValid)
        {
            ToDelete = true;
            TetrisGame.Current.GameObjects.Add(new FallingShape());
        }
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
            _currentSizeInBlocks = null;
            if (value < 0) value = 4 - 1;
            if (value >= 4) value = 0;
            field = value;
        }
    }

    public (int, int) CurrentSizeInBlocks
    {
        get
        {
            if (_currentSizeInBlocks == null)
            {
                int maxX = 0;
                int maxY = 0;

                for (var x = 0; x < Data.Shapes.GetLength(2); x++)
                {
                    for (var y = 0; y < Data.Shapes.GetLength(3); y++)
                    {
                        if (Data.Shapes[Shape, Orientation, x, y].HasValue)
                        {
                            if (x > maxX) maxX = x;
                            if (y > maxY) maxY = x;
                        }
                    }
                }
                _currentSizeInBlocks = (maxX, maxY);
            }
            return _currentSizeInBlocks.Value;
        }
    }
    private (int, int)? _currentSizeInBlocks;

    #endregion
}