using NeoDEEX.Diagnostics;
using NeoDEEX.Transactions;
using NeoDEEX.Transactions.Common;
using Spectre.Console;
using System.Reflection;

namespace execution_context;

[FoxTransaction(FoxTransactionOption.Required)]
public class BizClass : FoxBizBase, IBizClass
{
    protected override void Activate()
    {
        FoxExecutionContext ctx = this.Context;
        AnsiConsole.MarkupLine($"[darkgreen]> obj_id={ctx.TargetObject.GetHashCode()} {ctx.MethodName} activated...[/]");
    }

    protected override void Deactivate()
    {
        FoxExecutionContext ctx = this.Context;
        AnsiConsole.MarkupLine($"[darkred]> obj_id={ctx.TargetObject.GetHashCode()} {ctx.MethodName} deactivated...[/]");
        if (ctx.Exception != null)
        {
            var log = FoxLogManager.GetLogger<BizClass>();
            log.Error(ctx.Exception);
            AnsiConsole.MarkupLine($"[darkred]> Exception: {ctx.Exception?.GetType().FullName}[/]");
        }
        if (ctx.MethodBase is MethodInfo mi && mi.ReturnType != typeof(void))
        {
            AnsiConsole.MarkupLine($"[darkyellow]> obj_id={ctx.TargetObject.GetHashCode()} {ctx.MethodName} has a return value : {ctx.MethodReturnMessage.Result}[/]");
        }
    }

    [FoxAutoComplete]
    public void InsertMany(int[] ids, bool commit)
    {
        this.Context.Dump("In transactional method ...");

        using DacClass dac = new();
        IDacClass itf = dac.CreateExecution<IDacClass>();
        foreach (int id in ids)
        {
            itf.InsertData(id, id, "STR #" + id);
        }
        if (!commit)
        {
            throw new ApplicationException("Simulated error to rollback this transaction.");
        }
    }

    [FoxAutoComplete]
    [FoxTransaction(FoxTransactionOption.Supported)]
    public void ShowContextInfo()
    {
        FoxExecutionContext ctx = this.Context;
        ctx.Dump();
    }
}

public interface IBizClass
{
    void InsertMany(int[] ids,bool commit);
    void ShowContextInfo();
}
