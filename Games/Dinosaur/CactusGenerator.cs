using FSCSharp;

namespace Dinosaur;

public class CactusGenerator : GameObject
{
    #region Overrides
    public override void Update(float delta)
    {
        NewCactusCooldown -= delta;
        if (NewCactusCooldown <= 0)
        {
            NewCactusCooldown = 0;
            if (Random.Shared.NextSingle() <= delta * 0.8f)
            {
                NewCactusCooldown = 1.5f;
                DinosaurGame.Current.GameObjects.Add(new Cactus());
            }
        }
    }
    #endregion

    #region Properties
    public float NewCactusCooldown { get; set; }
    #endregion
}