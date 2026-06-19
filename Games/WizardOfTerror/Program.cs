namespace WizardOfTerror;

internal class Program
{
    static void Main()
    {
        using (new WOTGame().Run()) { }
    }
}