using Supermodel.Client.CodeGen;

namespace PingPongModelGenerator;

internal class Program
{
    static void Main()
    {
        var modelGenerator = new ModelGen([typeof(PingPong.Program).Assembly]);
        var sb = modelGenerator.GenerateModels();
        var code = sb.ToString();

        File.WriteAllText(@"..\..\..\..\..\PingPong\ApiClient\Supermodel.Mobile.ModelsForRuntime.cs", code);

        Console.WriteLine("All done!");
    }
}
