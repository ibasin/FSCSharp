using System.Numerics;

namespace Breakout.Helpers;

public enum CollisionSide
{
    None, Top, Bottom, Left, Right, TopLeft, TopRight, BottomLeft, BottomRight
}

public static class MovingCircleStaticRectCollisionResolver
{
    public static CollisionSide GetCollisionDetails(
        Vector2 circleCenter,
        float circleRadius,
        Vector2 rectCenter,
        Vector2 rectSize,
        Vector2 circleVelocity,
        float deltaTime,
        out Vector2 normal,
        out float collisionTime)
    {
        normal = Vector2.Zero;
        collisionTime = float.PositiveInfinity;

        static bool IsFinite(Vector2 v) =>
            float.IsFinite(v.X) && float.IsFinite(v.Y);

        // Reject invalid input.
        if (!IsFinite(circleCenter) ||
            !IsFinite(rectCenter) ||
            !IsFinite(rectSize) ||
            !IsFinite(circleVelocity) ||
            !float.IsFinite(circleRadius) ||
            !float.IsFinite(deltaTime) ||
            circleRadius <= 0 ||
            rectSize.X <= 0 ||
            rectSize.Y <= 0 ||
            deltaTime <= 0)
            return CollisionSide.None;

        // Calculate everything in double precision.
        double dx = (double)circleVelocity.X * deltaTime;
        double dy = (double)circleVelocity.Y * deltaTime;

        double px = (double)circleCenter.X - dx;
        double py = (double)circleCenter.Y - dy;

        double minX = (double)rectCenter.X - rectSize.X * 0.5;
        double minY = (double)rectCenter.Y - rectSize.Y * 0.5;
        double maxX = (double)rectCenter.X + rectSize.X * 0.5;
        double maxY = (double)rectCenter.Y + rectSize.Y * 0.5;

        double r = circleRadius;
        double a = dx * dx + dy * dy;

        if (a == 0)
            return CollisionSide.None;

        double bestTime = double.PositiveInfinity;
        Vector2 bestNormal = Vector2.Zero;
        CollisionSide bestSide = CollisionSide.None;

        // Check straight edges.
        void CheckSide(
            double t,
            bool vertical,
            CollisionSide side,
            Vector2 candidateNormal)
        {
            if (!double.IsFinite(t) ||
                t < 0 || t > 1 || t >= bestTime)
                return;

            double coord = vertical
                ? py + dy * t
                : px + dx * t;

            bool valid = vertical
                ? coord >= minY && coord <= maxY
                : coord >= minX && coord <= maxX;

            if (!valid)
                return;

            bestTime = t;
            bestSide = side;
            bestNormal = candidateNormal;
        }

        if (dx > 0)
            CheckSide((minX - r - px) / dx,
                true, CollisionSide.Left, new(-1, 0));
        else if (dx < 0)
            CheckSide((maxX + r - px) / dx,
                true, CollisionSide.Right, new(1, 0));

        if (dy > 0)
            CheckSide((minY - r - py) / dy,
                false, CollisionSide.Top, new(0, -1));
        else if (dy < 0)
            CheckSide((maxY + r - py) / dy,
                false, CollisionSide.Bottom, new(0, 1));

        // Check rounded corners.
        void CheckCorner(
            double cx, double cy,
            int signX, int signY,
            CollisionSide side)
        {
            double rx = px - cx;
            double ry = py - cy;

            double b = rx * dx + ry * dy;
            double c = rx * rx + ry * ry - r * r;

            double discriminant = b * b - a * c;

            if (!double.IsFinite(discriminant) ||
                discriminant < 0)
                return;

            double t = (-b - Math.Sqrt(discriminant)) / a;

            if (!double.IsFinite(t) ||
                t < 0 || t > 1 || t >= bestTime)
                return;

            // Circle center relative to corner at impact.
            double nx = rx + dx * t;
            double ny = ry + dy * t;

            // Verify correct corner quadrant.
            if (nx * signX < 0 || ny * signY < 0)
                return;

            // Normalize in double precision.
            double length = Math.Sqrt(nx * nx + ny * ny);

            if (length > 0 && double.IsFinite(length))
            {
                nx /= length;
                ny /= length;
            }
            else
            {
                // Numerical fallback: opposite movement.
                double movementLength = Math.Sqrt(a);
                nx = -dx / movementLength;
                ny = -dy / movementLength;
            }

            Vector2 candidate = new((float)nx, (float)ny);

            if (!IsFinite(candidate) ||
                candidate.LengthSquared() == 0)
                return;

            bestTime = t;
            bestSide = side;
            bestNormal = candidate;
        }

        CheckCorner(minX, minY, -1, -1, CollisionSide.TopLeft);
        CheckCorner(maxX, minY, 1, -1, CollisionSide.TopRight);
        CheckCorner(minX, maxY, -1, 1, CollisionSide.BottomLeft);
        CheckCorner(maxX, maxY, 1, 1, CollisionSide.BottomRight);

        // Only valid collision results get a normal.
        if (bestSide == CollisionSide.None)
            return CollisionSide.None;

        normal = bestNormal;
        collisionTime = (float)bestTime;

        return bestSide;
    }
}
