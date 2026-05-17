using Raylib_cs;
using System.Numerics;
using FSCSharp;

namespace Cube;

public class CubeGame : Game<CubeGame>
{
    #region Constrcutors
    public CubeGame() : base("Cube", Color.Black, true)
    {
        Vector3 cubePosition = new Vector3(500, 500, 500);
        var cube = new CubeObj(cubePosition);
        GameObjects.Add(cube);
        
        Vector3 cameraPosition = new Vector3(500, 0, 500);
        var camera = new Camera3D(cameraPosition, cubePosition, Vector3.UnitZ, 45.0f, CameraProjection.Perspective);
        var cameraGameObject = new Camera3DGameObject(camera);
        GameObjects.Add(cameraGameObject);
    }
    #endregion
}