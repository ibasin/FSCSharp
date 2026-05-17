namespace FSCSharp;

public class GameOverException : Exception
{
    #region Constrcutors
    public GameOverException(int delayIterations = 0) : this("", delayIterations) { }
    public GameOverException(string msg, float delay = 0) : base(msg)
    {
        Delay = delay;
    }
    #endregion

    #region Properties
    public float Delay { get; protected set; }
    #endregion
}