using NeoDEEX.Transactions;
using Spectre.Console;

namespace execution_extension;

[MyLogging]
internal class BizClass : FoxBizBase, IBizClass
{
    [MyLogging("Console")]
    public string Method1(string param1)
    {
        AnsiConsole.MarkupLine($"[blue]BizClass.Method1() called with param1='{param1}'[/]");
        return $"Result from Method1: param1='{param1}'";
    }

    public void Method2()
    {
        AnsiConsole.MarkupLine($"[blue]BizClass.Method2() called[/]");
        throw new ApplicationException("Exception from Method2");
    }
}

internal interface IBizClass : IDisposable
{
    string Method1(string param1);
    void Method2();
}
