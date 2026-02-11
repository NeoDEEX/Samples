using Spectre.Console;

namespace transaction_scope;

public static class Program
{
    public static void Main()
    {
        AnsiConsole.MarkupLine("[green]Fox Transactions Propagation Sample[/]\n");

        AnsiConsole.MarkupLine("[yellow]Transacation scenario A[/]");
        using var obj1 = new Class1();
        var itf1 = obj1.CreateExecution<IClass1>();
        itf1.TxMethod_A1(1);
        AnsiConsole.WriteLine();

        AnsiConsole.MarkupLine("[yellow]Transacation scenario B[/]");
        using var obj2 = new Class1();
        var itf2 = obj2.CreateExecution<IClass1>();
        itf2.TxMethod_B1(1);
        AnsiConsole.WriteLine();

        AnsiConsole.MarkupLine("[yellow]Transacation scenario C & D[/]");
        using var obj3 = new Class1();
        var itf3 = obj3.CreateExecution<IClass1>();
        itf3.TxMethod_C1(1);
        AnsiConsole.WriteLine();
    }
}
