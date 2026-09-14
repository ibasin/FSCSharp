using FSCSharp;
using System.Numerics;

namespace Breakout
{
    public class Ball : TangibleGameObject
    {
        #region Overrides
        public override void Update(float delta)
        {
            throw new NotImplementedException();
        }
        public override void Draw(float delta)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Properties
        public bool IsServing { get; protected set; }

        public Vector2 Speed { get; set; }
        public Vector2 Size { get; protected set; }

        public Vector2 PositionClampMin { get; protected set; }
        public Vector2 PositionClampMax { get; protected set; }
        #endregion
    }
}
