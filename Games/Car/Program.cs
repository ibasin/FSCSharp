namespace Car;

public class Program
{
    static void Main()
    {
        using var game = new CarGame();
        game.Run();
    }
}