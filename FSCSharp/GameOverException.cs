namespace FSCSharp;

public class GameOverException : Exception
{
    #region Constrcutors
    public GameOverException(int delayIterations = 0) : this("", delayIterations) { }
    public GameOverException(string msg, float delaySec = 0) : base(msg)
    {
        DelaySec = delaySec;
    }
    #endregion

    #region Properties
    public float DelaySec { get; protected set; }
    #endregion
}