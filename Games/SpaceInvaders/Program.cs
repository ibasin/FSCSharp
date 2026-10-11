namespace SpaceInvaders;

public class Program
{
    static void Main()
    {
        using var game = new SpaceInvadersGame();
        game.Run();
    }
}