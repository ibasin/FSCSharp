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

        if (TetrisGame.Current.ShapesBlock.IsShapeAtLocationColliding(Shape, Orientation, Location))
        {
            TetrisGame.Current.PlaySound("Resources/game_over.wav");
            throw new GameOverException($"Game Over! Score: {TetrisGame.Score}.");
        }
    }
    #endregion

    #region Overrides
    public override void Update(float delta)
    {
        if (IsFreeFall)
        {
            MoveDown(delta * 12);
            return;
        }
        
        //Save current location and orientation
        Vector2 oldLocation = Location;
        var oldOrientation = Orientation;
        
        //Move or rotate in response to keyboard inputs
        if (Game.KeyboardManager.IsKeyPressed(KeyboardKey.Left))
            Location = Location.Move(Go.Left, TetrisGame.SquareSide);
        if (Game.KeyboardManager.IsKeyPressed(KeyboardKey.Right))
            Location = Location.Move(Go.Right, TetrisGame.SquareSide);
        if (Game.KeyboardManager.IsKeyPressed(KeyboardKey.Up)) Orientation--;
        if (Game.KeyboardManager.IsKeyPressed(KeyboardKey.Down)) Orientation++;
        if (Game.KeyboardManager.IsKeyPressed(KeyboardKey.Space)) IsFreeFall = true;

        //Check if we did not move or rotate out of the screen 
        if (Location.Move(Go.Right, TetrisGame.SquareSide * ShapeRect.X1).Violations.XMinusViolation ||
            Location.Move(Go.Right, TetrisGame.SquareSide * ShapeRect.X2).Violations.XPlusViolation ||
            TetrisGame.Current.ShapesBlock.IsShapeAtLocationColliding(Shape, Orientation, Location))
        {
            if (Location != oldLocation)
            {
                TetrisGame.Current.PlaySound("Resources/cant_sound.wav");
                Location = oldLocation;
            }

            if (Orientation != oldOrientation)
            {
                TetrisGame.Current.PlaySound("Resources/cant_sound.wav");
                Orientation = oldOrientation;
            }
        }
        else
        {
            if (Location != oldLocation) TetrisGame.Current.PlaySound("Resources/move_piece.wav");
            if (Orientation != oldOrientation) TetrisGame.Current.PlaySound("Resources/rotate_piece.wav");
        }

        MoveDown(delta);
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

    #region Methods
    public void MoveDown(float delta)
    {
        Location = Location.Move(Go.Down, delta * VerticalSpeed);

        if (TetrisGame.Current.ShapesBlock.IsShapeAtLocationColliding(Shape, Orientation, Location))
        {
            TetrisGame.Current.PlaySound("Resources/piece_landed.wav");

            TetrisGame.Current.ShapesBlock.MergeShapeAtLocation(Shape, Orientation, Location);
            var lines = TetrisGame.Current.ShapesBlock.RemoveFullLines();

            //update score
            if (lines == 1) TetrisGame.Score += 40;
            else if (lines == 2) TetrisGame.Score += 100;
            else if (lines == 3) TetrisGame.Score += 300;
            else if (lines == 4) TetrisGame.Score += 1200;

            ToDelete = true;

            var newShape = new FallingShape();
            newShape.VerticalSpeed = VerticalSpeed + 0.25f;
            TetrisGame.Current.GameObjects.Add(newShape);
        }
    }
    #endregion

    #region Properties
    public Vector2 Location { get; set; }
    public float VerticalSpeed { get; protected set; } = 110;
    public bool IsFreeFall { get; set; }

    public int Shape { get; }
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
                //Console.WriteLine($"Shape = {Shape}, Orientation = {Orientation}, Rect = {_shapeRect}");
            }
            return _shapeRect.Value;
        }
    }
    private Rect? _shapeRect;
    #endregion
}