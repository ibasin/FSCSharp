using System.Numerics;

namespace PingPong.Api;

public static class Vector2Ex
{
    #region Methods
    public static Vector2 ToVector2(this ApiServer.Vector2AsClass me)
    {
        return new Vector2(me.X, me.Y);
    }
    public static Vector2 ToVector2(this ApiClient.Vector2AsClass me)
    {
        return new Vector2(me.X, me.Y);
    }
    #endregion
}