using System.Numerics;

namespace WizardOfTerror.AnimatedObjects;

public class Campfire1 : AnimatedObject
{
    public Campfire1(Vector2 location) : base(location, "Resources/3 Animated Objects/2 Campfire/1.png", new Vector2(32, 64), 6) { }
}

public class Campfire2 : AnimatedObject
{
    public Campfire2(Vector2 location) : base(location, "Resources/3 Animated Objects/2 Campfire/2.png", new Vector2(32, 32), 6) { }
}