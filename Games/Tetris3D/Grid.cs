using FSCSharp;
using Raylib_cs;

namespace Tetris3D;

public class Grid : TangibleGameObject
{
    #region Overrides
    public override void Update(float delta)
    {
        if (Game.KeyboardManager.IsKeyPressed(KeyboardKey.G)) Enabled = !Enabled;
    }
    public override void Draw()
    {
        var camera = Tetris3DGame.Current.Camera3DGameObject;
        if (Enabled && camera.Camera.Position == Tetris3DGame.Current.Camera3DDefaultPosition)
        {
            //horizontal lines
            for (var i = 1; i < Tetris3DGame.HeightInSquares; i++)
            {
                var y = i * Tetris3DGame.SquareSide;
                Raylib.DrawLine(0, y, Tetris3DGame.WidthInSquares * Tetris3DGame.SquareSide, y, Color.DarkGray);
            }

            //vertical lines
            for (var i = 1; i < Tetris3DGame.WidthInSquares; i++)
            {
                var x = i * Tetris3DGame.SquareSide;
                Raylib.DrawLine(x, 0, x, Tetris3DGame.HeightInSquares * Tetris3DGame.SquareSide, Color.DarkGray);
            }
        }
    }
    #endregion

    #region Properties
    public bool Enabled { get; set; }
    #endregion
}