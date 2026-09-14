using FSCSharp;
using Raylib_cs;
using System.Numerics;

namespace Breakout
{
    public class Block : TangibleGameObject
    {
        #region Constructors
        public Block(Vector2 location, Color color)
        {
            Location = location;
            Color = color;
        }
        #endregion
        
        #region Overrides
        public override void Update(float delta)
        {
            //do nothing, blocks are static
        }
        public override void Draw(float delta)
        {
            Raylib.DrawRectangle((int)Location.X, (int)Location.Y, (int)Size.X, (int)Size.Y, Color);
        }
        #endregion

        #region Properties
        public static readonly Vector2 Size = new Vector2(96, 25);
        public Vector2 Location { get; set; }
        public Color Color { get; protected set; }
        #endregion
    }
}
