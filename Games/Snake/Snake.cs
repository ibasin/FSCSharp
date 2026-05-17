using System.Numerics;
using FSCSharp;
using Raylib_cs;

namespace Snake;

public class Snake : TangibleGameObject
{
    #region Constructors
    public Snake()
    {
        Direction = Go.Left;

        Body = new Queue<Point>();
        Body.Enqueue(new Point(SnakeGame.Current.BoardSize.X/2 + 4, SnakeGame.Current.BoardSize.Y/2));
        Body.Enqueue(new Point(SnakeGame.Current.BoardSize.X/2 + 3, SnakeGame.Current.BoardSize.Y/2));
        Body.Enqueue(new Point(SnakeGame.Current.BoardSize.X/2 + 2, SnakeGame.Current.BoardSize.Y/2));
        Body.Enqueue(new Point(SnakeGame.Current.BoardSize.X/2 + 1, SnakeGame.Current.BoardSize.Y/2));
        Body.Enqueue(new Point(SnakeGame.Current.BoardSize.X/2, SnakeGame.Current.BoardSize.Y/2));

        Priority = 2000;
    }
    #endregion
    
    #region Overrides
    public override void Update(float delta)
    {
        StepCooldown -= delta;
        if (StepCooldown < 0)
        {
            var key = Game.KeyboardManager.ReadKey();
            if (key == KeyboardKey.Left && Direction != Go.Right) Direction = Go.Left;
            if (key == KeyboardKey.Right && Direction != Go.Left) Direction = Go.Right;
            if (key == KeyboardKey.Up && Direction != Go.Down) Direction = Go.Up;
            if (key == KeyboardKey.Down && Direction != Go.Up) Direction = Go.Down;
            Game.KeyboardManager.ClearBuffer();

            GrowingCooldown--;
            if (GrowingCooldown <= 0)
            {
                Body.Dequeue();
                GrowingCooldown = 0;
            }

            var head = Body.Last();
            Point newPoint;
            if (Direction == Go.Left) newPoint = new Point(head.X - 1, head.Y);
            else if (Direction == Go.Right) newPoint = new Point(head.X + 1, head.Y);
            else if (Direction == Go.Up) newPoint = new Point(head.X, head.Y - 1);
            else if (Direction == Go.Down) newPoint = new Point(head.X, head.Y + 1);
            else throw new Exception("This should never happen!");

            if (newPoint.X < 0 || newPoint.Y < 0 || newPoint.X >= SnakeGame.Current.BoardSize.X || newPoint.Y >= SnakeGame.Current.BoardSize.Y)
            {
                ToDelete = true;
                SnakeGame.Current.PlaySound("Resources/gong.mp3");
                throw new GameOverException($"Snake ran into a wall! Score: {ScoreKeeper.ApplesCount}", 0.25f);
            }
            if (Body.Any(p => p.X == newPoint.X && p.Y == newPoint.Y))
            {
                ToDelete = true;
                SnakeGame.Current.PlaySound("Resources/self-bite.mp3");
                throw new GameOverException($"Snake ate itself! Score: { ScoreKeeper.ApplesCount }", 0.25f);
            }

            if (head.X == SnakeGame.Current.Apple.Position.X && head.Y == SnakeGame.Current.Apple.Position.Y)
            {
                SnakeGame.Current.PlaySound("Resources/apple-bite.mp3");

                SnakeGame.Current.Apple.ToDelete = true;

                SnakeGame.Current.Apple = new Apple();
                SnakeGame.Current.GameObjects.Add(SnakeGame.Current.Apple);

                ScoreKeeper.ApplesCount++;

                GrowingCooldown = 5;
            }

            Body.Enqueue(newPoint);

            StepCooldown = 0.2f;
        }
    }
    public override void Draw()
    {
        foreach (var bodyPart in Body)
        {
            Raylib.DrawCircle(SnakeGame.BoardCellSize /2 + bodyPart.X*SnakeGame.BoardCellSize,
                              SnakeGame.BoardCellSize /2 + bodyPart.Y*SnakeGame.BoardCellSize,
                              SnakeGame.BoardCellSize /2f, 
                              new Color(251, 200, 1));
        }

        var head = Body.Last();
        var headCenter = new Vector2(SnakeGame.BoardCellSize/2f + head.X * SnakeGame.BoardCellSize,
                                     SnakeGame.BoardCellSize/2f + head.Y * SnakeGame.BoardCellSize);

        var eyeColor = Color.Red;
        if (Direction == Go.Left)
        {
            var eye1 = headCenter.Move(Go.Left, SnakeGame.BoardCellSize / 5).Move(Go.Up, SnakeGame.BoardCellSize / 5);
            Raylib.DrawCircle((int)Math.Round(eye1.X), (int)Math.Round(eye1.Y), 3, eyeColor);

            var eye2 = headCenter.Move(Go.Left, SnakeGame.BoardCellSize / 5).Move(Go.Down, SnakeGame.BoardCellSize / 5);
            Raylib.DrawCircle((int)Math.Round(eye2.X), (int)Math.Round(eye2.Y), 3, eyeColor);
        }
        if (Direction == Go.Right)
        {
            var eye1 = headCenter.Move(Go.Right, SnakeGame.BoardCellSize / 5).Move(Go.Up, SnakeGame.BoardCellSize / 5);
            Raylib.DrawCircle((int)Math.Round(eye1.X), (int)Math.Round(eye1.Y), 3, eyeColor);

            var eye2 = headCenter.Move(Go.Right, SnakeGame.BoardCellSize / 5).Move(Go.Down, SnakeGame.BoardCellSize / 5);
            Raylib.DrawCircle((int)Math.Round(eye2.X), (int)Math.Round(eye2.Y), 3, eyeColor);
        }
        if (Direction == Go.Up)
        {
            var eye1 = headCenter.Move(Go.Up, SnakeGame.BoardCellSize / 5).Move(Go.Left, SnakeGame.BoardCellSize / 5);
            Raylib.DrawCircle((int)Math.Round(eye1.X), (int)Math.Round(eye1.Y), 3, eyeColor);

            var eye2 = headCenter.Move(Go.Up, SnakeGame.BoardCellSize / 5).Move(Go.Right, SnakeGame.BoardCellSize / 5);
            Raylib.DrawCircle((int)Math.Round(eye2.X), (int)Math.Round(eye2.Y), 3, eyeColor);
        }
        if (Direction == Go.Down)
        {
            var eye1 = headCenter.Move(Go.Down, SnakeGame.BoardCellSize / 5).Move(Go.Left, SnakeGame.BoardCellSize / 5);
            Raylib.DrawCircle((int)Math.Round(eye1.X), (int)Math.Round(eye1.Y), 3, eyeColor);

            var eye2 = headCenter.Move(Go.Down, SnakeGame.BoardCellSize / 5).Move(Go.Right, SnakeGame.BoardCellSize / 5);
            Raylib.DrawCircle((int)Math.Round(eye2.X), (int)Math.Round(eye2.Y), 3, eyeColor);
        }
    }
    #endregion 

    #region Properties
    public Queue<Point> Body { get; }
    public Go Direction { get; set; }

    public float StepCooldown { get; set; }
    public int GrowingCooldown { get; set; }
    #endregion
}