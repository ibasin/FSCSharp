using System.Numerics;

namespace FSCSharp;

public static class Vector2Ext
{
    #region Embedded Types
    public readonly struct Violations
    {
        #region Constructors
        public Violations(bool xMinusViolation, bool xPlusViolation, bool yMinusViolation, bool yPlusViolation)
        {
            XMinusViolation = xMinusViolation;
            XPlusViolation = xPlusViolation;
            YMinusViolation = yMinusViolation;
            YPlusViolation = yPlusViolation;
        }
        public Violations(Violations other)
        {
            XMinusViolation = other.XMinusViolation;
            XPlusViolation = other.XPlusViolation;
            YMinusViolation = other.YMinusViolation;
            YPlusViolation = other.YPlusViolation;
        }
        public Violations(Vector2 vect) : this(false, false, false, false)
        {
            if (vect.X > Game.CurrentInternal.WindowWidth - 1) XPlusViolation = true;
            else if (vect.X < 0) XMinusViolation = true;

            if (vect.Y > Game.CurrentInternal.WindowHeight - 1) YPlusViolation = true;
            else if (vect.Y < 0) YMinusViolation = true;
        }
        #endregion

        #region Properties
        public bool IsInViolation => XMinusViolation || XPlusViolation || YMinusViolation || YPlusViolation;
        public bool XMinusViolation { get; }
        public bool XPlusViolation { get; }
        public bool YMinusViolation { get; }
        public bool YPlusViolation { get; }
        #endregion
    }
    #endregion

    extension(Vector2 me)
    {
        #region Methods
        public Vector2 CorrectViolations()
        {
            int x, y;

            if (me.IntX > Game.CurrentInternal.WindowWidth - 1) x = Game.CurrentInternal.WindowWidth - 1;
            else if (me.IntX < 0) x = 0;
            else x = me.IntX;

            if (me.IntY > Game.CurrentInternal.WindowHeight - 1) y = Game.CurrentInternal.WindowHeight - 1;
            else if (me.IntY < 0) y = 0;
            else y = me.IntY;

            return new Vector2(x, y);
        }
        public Vector2 Move(Go direction, int distance = 1)
        {
            return me.Move(direction, (float)distance);
        }
        public Vector2 Move(Go direction, float distance)
        {
            Vector2 vect;
            if (direction == Go.Up) vect = me with { Y = me.Y - distance };
            else if (direction == Go.Down) vect = me with { Y = me.Y + distance };
            else if (direction == Go.Right) vect = me with { X = me.X + distance };
            else if (direction == Go.Left) vect = me with { X = me.X - distance };
            else throw new Exception("Invalid direction");
            return vect;
        }
        public static Vector2 CreateRandom()
        {
            return new Vector2(Random.Shared.Next(Game.CurrentInternal.WindowWidth), Random.Shared.Next(Game.CurrentInternal.WindowHeight));
        }
        public static Vector2 WindowCenter
        {
            get
            {
                // ReSharper disable PossibleLossOfFraction
                _windowCenter ??= new Vector2(Game.CurrentInternal.WindowWidth / 2, Game.CurrentInternal.WindowHeight / 2);
                // ReSharper restore PossibleLossOfFraction

                return _windowCenter.Value;
            }
        }
        #endregion

        #region Properties 
        public int IntX => (int)Math.Round(me.X);
        public int IntY => (int)Math.Round(me.Y);

        public Violations Violations => new(me);
        public bool IsValid => !me.Violations.IsInViolation;
        #endregion
    }
    private static Vector2? _windowCenter;
}