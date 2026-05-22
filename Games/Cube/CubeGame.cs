using Raylib_cs;
using System.Numerics;
using FSCSharp;

namespace Cube;

public class CubeGame : Game<CubeGame>
{
    #region Constrcutors
    public CubeGame() : base("Cube", Color.Black, true)
    {
        Vector3 cubePosition = new Vector3(0, 0, 0);
        var cube = new CubeObj(cubePosition);
        GameObjects.Add(cube);
        
        Vector3 cameraPosition = new Vector3(0, -200, 0);
        var camera = new Camera3D(cameraPosition, cubePosition, Vector3.UnitZ, 90.0f, CameraProjection.Perspective)
        {
            //Up = Vector3.UnitY
        };
        var cameraGameObject = new Camera3DGameObject(camera);
        GameObjects.Add(cameraGameObject);
    }
    #endregion
}