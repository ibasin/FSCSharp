using FSCSharp;
using PingPong.Helpers;
using Raylib_cs;

namespace PingPong;

using System;
using System.Numerics;

//public class PongAI
//{
//    public void Update(float deltaTime, Vector2 ballPosition, Vector2 ballVelocity, float paddleX, ref float paddleY, float paddleHeight, float ballRadius, float screenHeight)
//    {
//        _decisionTimer -= deltaTime;

//        // Reevaluate periodically, not every frame.
//        if (_decisionTimer <= 0f)
//        {
//            _decisionTimer = ReactionTime;

//            if (ballVelocity.X > 0f)
//            {
//                // Predict when ball reaches the paddle.
//                float timeToHit = (paddleX - ballRadius - ballPosition.X) / ballVelocity.X;

//                if (timeToHit >= 0f)
//                {
//                    float predictedY = ballPosition.Y + ballVelocity.Y * timeToHit;

//                    // Account for bouncing off walls.
//                    predictedY = ReflectY(predictedY, ballRadius, screenHeight - ballRadius);

//                    // Imperfect prediction.
//                    float error = (Random.Shared.NextSingle() * 2f - 1f) * AimError;

//                    _targetY = predictedY + error;
//                }
//            }
//            else
//            {
//                // Ball moving away: return to center.
//                _targetY = screenHeight / 2f;
//            }
//        }

//        // Move toward target with limited speed.
//        float maxMovement = MaxPaddleSpeed * deltaTime;

//        float movement = Math.Clamp(_targetY - paddleY, -maxMovement, maxMovement);

//        paddleY += movement;

//        // Keep paddle inside screen.
//        paddleY = Math.Clamp(paddleY, paddleHeight / 2f, screenHeight - paddleHeight / 2f);
//    }

//    private static float ReflectY(float y, float minY, float maxY)
//    {
//        float height = maxY - minY;
//        if (height <= 0f) return minY;

//        float period = 2f * height;
//        float offset = (y - minY) % period;

//        if (offset < 0f) offset += period;

//        return minY + (offset <= height ? offset : period - offset);
//    }

//    #region Properties
//    private float _decisionTimer;
//    private float _targetY;

//    // Difficulty settings
//    public const float MaxPaddleSpeed = 270f; // pixels/second
//    public const float ReactionTime = 0.20f;   // seconds
//    public const float AimError = 55f;        // pixels
//    #endregion
//}


public class LeftPaddleComputer : LeftPaddle
{
    #region Constrcutors
    public LeftPaddleComputer(Vector2 location, Color color) : base(location, color) { }
    #endregion

    #region Overrides
    public override void Update(float delta)
    {
        if (Cooldown > 0) Cooldown -= delta;

        PredictBallYTargetImperfectly(delta);
        if (Location.Y > YTarget) Location = Location with { Y = Location.Y - Velocity * delta };
        if (Location.Y < YTarget) Location = Location with { Y = Location.Y + Velocity * delta };

        var ball = PingPongGame.Current.Ball;
        if (Cooldown <= 0 && ball.Body.IsCollidingByPixelAtLocation(ball.Location, Location, Size))
        {
            MovingCircleStaticRectCollisionResolver.GetCollisionDetails(ball.Location, ball.Body.Size.X / 2, Location, Size, ball.Velocity, delta, out var normal, out _);
            ball.Velocity = Vector2.Reflect(ball.Velocity, Vector2.Normalize(normal));

            if (Game.KeyboardManager.IsKeyDown(KeyboardKey.Left)) ball.Velocity = ball.Velocity with { X = ball.Velocity.X - Velocity / 12 };
            if (Game.KeyboardManager.IsKeyDown(KeyboardKey.Right)) ball.Velocity = ball.Velocity with { X = ball.Velocity.X + Velocity / 12 };

            var locationDelta = ball.Location.X - Location.X;
            ball.Velocity = ball.Velocity with { X = ball.Velocity.X + 1.5f * locationDelta };

            PingPongGame.Current.PlaySound("Resources/ball-hit.mp3");
            Cooldown = 0.1f;
        }

        Location = Location with { Y = Math.Clamp(Location.Y, Size.Y / 2, PingPongGame.Current.WindowHeight - Size.Y / 2) };
    }

    private void PredictBallYTargetImperfectly(float delta)
    {
        var ball = PingPongGame.Current.Ball;
        DecisionCooldown -= delta;
        if (DecisionCooldown <= 0)
        {
            DecisionCooldown = 0.15f + (ball.Location.X - 50f) / 3750f;
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
                float error = (Random.Shared.NextSingle() * 2f - 1f) * AimError;

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

    public const float ReactionTime = 0.20f;   // seconds
    public const float AimError = 100f;        // pixels
    #endregion
}
