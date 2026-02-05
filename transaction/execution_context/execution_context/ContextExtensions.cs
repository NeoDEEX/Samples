using NeoDEEX.Transactions.Common;
using Spectre.Console;
using System.Reflection;
using System.Text;

namespace execution_context;

internal static class ContextExtensions
{
    public static void Dump(this FoxExecutionContext? ctx, string? header = null)
    {
        AnsiConsole.MarkupLine($"[gray]==> {header}[/]");
        if (ctx == null)
        {
            AnsiConsole.MarkupLine("  [red]No current execution context.[/]");
            return;
        }
        AnsiConsole.MarkupLine("  [green]Current execution context info:[/]");
        AnsiConsole.MarkupLine($"    [white]Context ID   = [/][blue]{ctx.Id}[/]");
        AnsiConsole.MarkupLine($"    [white]Target Object  = [/][blue]{ctx.TargetObject.GetType().FullName}[/]");
        AnsiConsole.MarkupLine($"    [white]Target Method = [/][blue]{ctx.MethodName}[/]");
        AnsiConsole.MarkupLine($"    [white]Parameters = [/][blue]{ctx.MethodCallMessage.Args.Length}[/]");
        ParameterInfo[] infos = ctx.MethodBase.GetParameters();
        StringBuilder sb = new(128);
        for (int i = 0; i < infos.Length; i++)
        {
            ParameterInfo pi = infos[i];
            string valueString = ctx.MethodCallMessage.Args[i]?.ToString() ?? "(null)";
            sb.Append("      [gray]").Append(pi.Name).Append(" : ")
                .Append(Markup.Escape(valueString))
                .AppendLine("[/]");
        }
        sb.Remove(sb.Length - Environment.NewLine.Length, Environment.NewLine.Length);
        AnsiConsole.MarkupLine(sb.ToString());
        AnsiConsole.MarkupLine($"    [white]Is AutoComplete = [/][blue]{ctx.IsAutoComplete}[/]");
        AnsiConsole.MarkupLine($"    [white]Transaction Option = [/][blue]{ctx.TransactionInfo.TransactionOption}[/]");
        AnsiConsole.MarkupLine($"    [white]Isolation Level = [/][blue]{ctx.TransactionInfo.IsolationLevel}[/]");
        AnsiConsole.MarkupLine($"    [white]Is In Transaction = [/][blue]{ctx.IsInTransaction}[/]");
    }
}
