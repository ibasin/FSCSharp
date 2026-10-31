using System.Numerics;

namespace PingPong.Api;

public static class Vector2AsClassEx
{
    #region Methods
    public static ApiServer.Vector2AsClass ToVector2AsClass(this Vector2 me)
    {
        return new ApiServer.Vector2AsClass { X = me.X, Y = me.Y };
    }
    public static ApiClient.Vector2AsClass ToVector2AsClass(this Vector2 me)
    {
        return new ApiClient.Vector2AsClass { X = me.X, Y = me.Y };
    }
    #endregion
}