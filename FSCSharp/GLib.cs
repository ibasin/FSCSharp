using System.Numerics;
using Raylib_cs;

namespace FSCSharp;

public static class GLib
{
    public static void DrawDashedThickLine(Vector2 start, Vector2 end, float dashLength, float gapLength, float thickness, Color color)
    {
        Vector2 delta = end - start;
        float length = delta.Length();

        if (length <= 0 || dashLength <= 0 || gapLength < 0) return;

        Vector2 direction = delta / length;

        for (float d = 0; d < length; d += dashLength + gapLength)
        {
            Vector2 p1 = start + direction * d;
            Vector2 p2 = start + direction * MathF.Min(d + dashLength, length);

            Raylib.DrawLineEx(p1, p2, thickness, color);
        }
    }
    public static void DrawTextRightAligned(string text, int rightX, int y, int fontSize, Color color)
    {
        int width = Raylib.MeasureText(text, fontSize);
        Raylib.DrawText(text, rightX - width, y, fontSize, color);
    }

    public static bool AreRectanglesColliding(Vector2 recACenter, Vector2 recASize, Vector2 recBCenter, Vector2 recBSize)
    {
        var recA = new Rectangle(recACenter.X - recASize.X / 2, recACenter.Y - recASize.Y / 2, recASize.X, recASize.Y);
        var recB = new Rectangle(recBCenter.X - recBSize.X / 2, recBCenter.Y - recBSize.Y / 2, recBSize.X, recBSize.Y);
        return Raylib.CheckCollisionRecs(recA, recB);
    }
    public static bool AreCirclesColliding(Vector2 circleACenter, float circleARadius, Vector2 circleBCenter, float circleBRadius)
    {
        return Raylib.CheckCollisionCircles(circleACenter, circleARadius, circleBCenter, circleBRadius);
    }
    public static bool AreCircleAndRectangleColliding(Vector2 circleCenter, float circleRadius, Vector2 recCenter, Vector2 recSize)
    {
        var halfRecSize = recSize / 2;
        var rec = new Rectangle(recCenter.X - halfRecSize.X, recCenter.Y - halfRecSize.Y, recSize.X, recSize.Y);
        return Raylib.CheckCollisionCircleRec(circleCenter, circleRadius, rec);
    }
}
