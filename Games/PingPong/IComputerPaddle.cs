namespace PingPong;

public interface IComputerPaddle
{
    #region Static Methods
    public static float ReflectY(float y)
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
    public static void PredictBallYTargetImperfectly(float delta, bool isLeftPaddle, ref float yTarget, ref float decisionCooldown)
    {
        var ball = PingPongGame.Current.Ball;
        decisionCooldown -= delta;
        if (decisionCooldown <= 0)
        {
            decisionCooldown = 0.2f + (ball.Location.X - 50f) / 3750f;

            float timeToHit;
            if (isLeftPaddle)
            {
                if (ball.Velocity.X > 0)
                {
                    yTarget = PingPongGame.Current.WindowHeight / 2f;
                    return;
                }
                timeToHit = (50 - ball.Body.Size.X / 2f - ball.Location.X) / ball.Velocity.X;
            }
            else
            {
                if (ball.Velocity.X < 0)
                {
                    yTarget = PingPongGame.Current.WindowHeight / 2f;
                    return;
                }
                timeToHit = (PingPongGame.Current.WindowWidth - ball.Body.Size.X / 2f - 50 - ball.Location.X) / ball.Velocity.X;
            }

            if (timeToHit >= 0f)
            {
                float predictedY = ball.Location.Y + ball.Velocity.Y * timeToHit;

                // Account for bouncing off walls.
                predictedY = IComputerPaddle.ReflectY(predictedY);

                // Imperfect prediction.
                float error = (Random.Shared.NextSingle() * 2f - 1f) * (AimError + Math.Abs(ball.Velocity.Y) / 50f);

                yTarget = predictedY + error;
            }
        }
    }
    #endregion

    #region Properties
    public const float AimError = 60f;        // pixels
    #endregion
}
