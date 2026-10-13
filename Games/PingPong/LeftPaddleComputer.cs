using FSCSharp;
using PingPong.Helpers;
using Raylib_cs;

namespace PingPong;

using System;
using System.Numerics;

public class LeftPaddleComputer : LeftPaddle
{
    #region Constrcutors
    public LeftPaddleComputer(Vector2 location, Color color) : base(location, color) { }
    #endregion

    #region Overrides
    public override void Update(float delta)
    {
        PredictBallYTargetImperfectly(delta);
        var isUp = Location.Y - YTarget > 10;
        var isDown = Location.Y - YTarget < -10;
        UpdateInternal(delta, isUp, isDown);
    }

    private void PredictBallYTargetImperfectly(float delta)
    {
        var ball = PingPongGame.Current.Ball;
        DecisionCooldown -= delta;
        if (DecisionCooldown <= 0)
        {
            DecisionCooldown = 0.2f + (ball.Location.X - 50f) / 3750f;
            if (ball.Velocity.X > 0)
            {
                YTarget = PingPongGame.Current.WindowHeight / 2f;
                return;
            }

            // Predict when ball reaches the paddle.
            float timeToHit = (50 - ball.Body.Size.X/2f - ball.Location.X) / ball.Velocity.X;

            if (timeToHit >= 0f)
            {
                float predictedY = ball.Location.Y + ball.Velocity.Y * timeToHit;

                // Account for bouncing off walls.
                predictedY = ReflectY(predictedY);

                // Imperfect prediction.
                float error = (Random.Shared.NextSingle() * 2f - 1f) * (AimError + Math.Abs(ball.Velocity.Y) / 50f);

                YTarget = predictedY + error;
            }

        }
    }
    private static float ReflectY(float y)
    {
        var ballRadius = PingPongGame.Current.Ball.Body.Size.Y;

        var minY = ballRadius;
        var maxY = PingPongGame.Current.WindowHeight - ballRadius;

        float height = maxY - minY;
        if (height <= 0f) return minY;

        float period = 2f * height;
        float offset = (y - minY) % period;

        if (offset < 0f) offset += period;

        return minY + (offset <= height ? offset : period - offset);
    }

    #endregion

    #region Properties
    public float YTarget { get; set; }
    public float DecisionCooldown { get; set; }

    public const float AimError = 60f;        // pixels
    #endregion
}
