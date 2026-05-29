using Raylib_cs;
using System.Numerics;
using FSCSharp;

namespace Car;

public class CarGame : Game<CarGame>
{
    #region Constrcutors
    public CarGame() : base("Car", 1600, 1200, Color.Black, true)
    {
        Rlgl.SetClipPlanes(0.1f, 100000.0f);

        Vector3 carPosition = new Vector3(0, -50, 0);
        var car = new Car(carPosition);
        GameObjects.Add(car);

        Vector3 cameraPosition = new Vector3(0, 0, -200);
        var camera = new Camera3D(cameraPosition, carPosition, Vector3.UnitZ, 90.0f, CameraProjection.Perspective)
        {
            Up = Vector3.UnitY
        };
        var cameraGameObject = new Camera3DGameObject(camera);
        GameObjects.Add(cameraGameObject);
    }
    #endregion
}