using System.Numerics;

namespace Breakout.Helpers;

public enum CollisionSide
{
    None, Top, Bottom, Left, Right, TopLeft, TopRight, BottomLeft, BottomRight
}

public static class MovingCircleStaticRectCollisionResolver
{
    public static CollisionSide GetCollisionDetails(Vector2 circleCenter, float circleRadius,
                                                    Vector2 rectCenter, Vector2 rectSize,
                                                    Vector2 circleVelocity, float deltaTime,
                                                    out Vector2 normal, out float collisionTime)
    {
        normal = Vector2.Zero;
        collisionTime = 1f;

        // Validate inputs.
        if (!IsFinite(circleCenter) || 
            !IsFinite(rectCenter) ||
            !IsFinite(rectSize) ||
            !IsFinite(circleVelocity) ||
            float.IsNaN(circleRadius) ||
            float.IsInfinity(circleRadius) ||
            float.IsNaN(deltaTime) ||
            float.IsInfinity(deltaTime) ||
            circleRadius <= 0 ||
            deltaTime < 0 ||
            rectSize.X <= 0 ||
            rectSize.Y <= 0)
        {
            return CollisionSide.None;
        }

        double minX = rectCenter.X - rectSize.X / 2.0;
        double maxX = rectCenter.X + rectSize.X / 2.0;
        double minY = rectCenter.Y - rectSize.Y / 2.0;
        double maxY = rectCenter.Y + rectSize.Y / 2.0;

        double moveX = (double)circleVelocity.X * deltaTime;
        double moveY = (double)circleVelocity.Y * deltaTime;

        double startX = circleCenter.X - moveX;
        double startY = circleCenter.Y - moveY;

        // Small tolerance for floating-point rounding.
        double radius = circleRadius + Math.Max(0.00001, circleRadius * 0.000001);

        double radiusSquared = radius * radius;

        // Verify overlap at current position.
        if (!Overlaps(circleCenter.X, circleCenter.Y, minX, minY, maxX, maxY, radiusSquared)) return CollisionSide.None;

        double impactX;
        double impactY;

        if (Overlaps(startX, startY, minX, minY, maxX, maxY, radiusSquared))
        {
            // Already overlapping at the beginning.
            collisionTime = 0f;
            impactX = startX;
            impactY = startY;
        }
        else
        {
            // Find the first contact along the movement.
            double low = 0.0;
            double high = 1.0;

            for (int i = 0; i < 32; i++)
            {
                double mid = (low + high) * 0.5;

                double x = startX + moveX * mid;
                double y = startY + moveY * mid;

                if (Overlaps(x, y, minX, minY, maxX, maxY, radiusSquared)) high = mid;
                else low = mid;
            }

            collisionTime = (float)high;

            impactX = startX + moveX * high;
            impactY = startY + moveY * high;
        }

        // Find nearest point on rectangle.
        double closestX = Math.Max(minX, Math.Min(maxX, impactX));
        double closestY = Math.Max(minY, Math.Min(maxY, impactY));

        double nx = impactX - closestX;
        double ny = impactY - closestY;

        double lengthSquared = nx * nx + ny * ny;

        if (lengthSquared > 0)
        {
            double length = Math.Sqrt(lengthSquared);

            normal = new Vector2((float)(nx / length), (float)(ny / length));

            bool left = impactX < minX;
            bool right = impactX > maxX;
            bool top = impactY < minY;
            bool bottom = impactY > maxY;

            if (left && top) return CollisionSide.TopLeft;
            if (right && top) return CollisionSide.TopRight;
            if (left && bottom) return CollisionSide.BottomLeft;
            if (right && bottom) return CollisionSide.BottomRight;

            if (left) return CollisionSide.Left;
            if (right) return CollisionSide.Right;
            if (top) return CollisionSide.Top;

            return CollisionSide.Bottom;
        }

        // Center inside rectangle or exactly on boundary.
        // Choose nearest edge to obtain a valid unit normal.
        double leftDist = impactX - minX;
        double rightDist = maxX - impactX;
        double topDist = impactY - minY;
        double bottomDist = maxY - impactY;

        double minDist = Math.Min(Math.Min(leftDist, rightDist), Math.Min(topDist, bottomDist));

        if (Math.Abs(minDist - leftDist) < 0.0001)
        {
            normal = new Vector2(-1f, 0f);
            return CollisionSide.Left;
        }

        if (Math.Abs(minDist - rightDist) < 0.0001)
        {
            normal = new Vector2(1f, 0f);
            return CollisionSide.Right;
        }

        if (Math.Abs(minDist - topDist) < 0.0001)
        {
            normal = new Vector2(0f, -1f);
            return CollisionSide.Top;
        }

        normal = new Vector2(0f, 1f);
        return CollisionSide.Bottom;
    }

    private static bool Overlaps(double x, double y, double minX, double minY, double maxX, double maxY, double radiusSquared)
    {
        double closestX = Math.Max(minX, Math.Min(maxX, x));
        double closestY = Math.Max(minY, Math.Min(maxY, y));

        double dx = x - closestX;
        double dy = y - closestY;

        return dx * dx + dy * dy <= radiusSquared;
    }

    private static bool IsFinite(Vector2 v)
    {
        return !float.IsNaN(v.X) && !float.IsNaN(v.Y) && !float.IsInfinity(v.X) && !float.IsInfinity(v.Y);
    }
}