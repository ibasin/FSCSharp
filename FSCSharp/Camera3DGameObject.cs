using Raylib_cs;

namespace FSCSharp;

public class Camera3DGameObject : GameObject
{
    #region Constrcutors
    public Camera3DGameObject(Camera3D camera)
    {
        Camera = camera;
    }
    #endregion

    #region Overrides
    public override void Update(float delta)
    {
        //do nothing
    }
    #endregion

    #region Properties
    // ReSharper disable once InconsistentNaming
    public Camera3D Camera;
    #endregion
}