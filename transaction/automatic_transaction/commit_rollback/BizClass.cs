using NeoDEEX.Transactions;
using Spectre.Console;

namespace commit_rollback;

[FoxTransaction(TransactionOption = FoxTransactionOption.Required)]
public class BizClass : FoxBizBase, IBizClass
{
    [FoxAutoComplete]
    public void AutoCompleteMethod(int id, bool forceRollback = false)
    {
        //
        // 공통 코드를 사용하였으나 직접 구현한 코드도 주석으로 남겨둠
        //
        //using DacClass dac = new();
        //IDacClass itf = dac.CreateExecution<IDacClass>();
        //dac.AutoCompleteInsertData(id);

        //if (forceRollback == false)
        //{
        //    AnsiConsole.MarkupLine("[blue]Transaction will be committed.[/]");
        //}
        //else
        //{
        //    AnsiConsole.MarkupLine("[red]Transaction will be aborted.[/]");
        //    throw new InvalidOperationException("Force rollback.");
        //}

        DoInvokeDacMethod(id, forceRollback);
    }

    [FoxAutoComplete(false)]
    public void ManualCompleteMethod(int id, bool forceRollback = false)
    {
        //
        // 공통 코드를 사용하였으나 직접 구현한 코드도 주석으로 남겨둠
        //
        //using DacClass dac = new();
        //IDacClass itf = dac.CreateExecution<IDacClass>();
        //dac.ManualCompleteInsertData(id);

        //if (forceRollback == false)
        //{
        //    AnsiConsole.MarkupLine("[blue]Transaction will be committed.[/]");
        //    if (!FoxContextUtil.IsAutoComplete)
        //    {
        //        FoxContextUtil.SetComplete();
        //    }
        //}
        //else
        //{
        //    AnsiConsole.MarkupLine("[red]Transaction will be aborted.[/]");
        //    if (!FoxContextUtil.IsAutoComplete)
        //    {
        //        FoxContextUtil.SetAbort();
        //    }
        //}

        DoInvokeDacMethod(id, forceRollback);
    }

    private static void DoInvokeDacMethod(int id, bool forceRollback)
    {
        using DacClass dac = new();
        IDacClass itf = dac.CreateExecution<IDacClass>();
        if (FoxContextUtil.IsAutoComplete)
        {
            dac.AutoCompleteInsertData(id);
        }
        else
        {
            dac.ManualCompleteInsertData(id);
        }

        if (forceRollback == false)
        {
            AnsiConsole.MarkupLine("[blue]Transaction will be committed.[/]");
            if (!FoxContextUtil.IsAutoComplete)
            {
                FoxContextUtil.SetComplete();
            }
        }
        else
        {
            AnsiConsole.MarkupLine("[red]Transaction will be aborted.[/]");
            if (FoxContextUtil.IsAutoComplete)
            {
                throw new InvalidOperationException("Force rollback.");
            }
            else
            {
                FoxContextUtil.SetAbort();
            }
        }
    }

    [FoxAutoComplete(false)]
    public void DefaultTransactionStatus()
    {
        AnsiConsole.MarkupLine($"[cyan]Default transaction vote : {FoxContextUtil.MyTransactionVote}[/]");
    }
}

public interface IBizClass
{
    void AutoCompleteMethod(int id, bool forceRollback = false);
    void ManualCompleteMethod(int id, bool forceRollback = false);
    void DefaultTransactionStatus();
}
