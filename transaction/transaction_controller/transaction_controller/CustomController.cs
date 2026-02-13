using NeoDEEX.Transactions;
using NeoDEEX.Transactions.Common;
using Spectre.Console;
using System.Reflection;

namespace transaction_controller;

internal class CustomController : IFoxTransactionControl
{
    public IFoxTransactionControl _controller;

    public CustomController()
    {
        // NeoDEEX.Transactions.Common.FoxFastTransactionController 객체를 생성한다.
        Assembly assem = typeof(IFoxTransactionControl).Assembly;
        Type type = assem.GetType("NeoDEEX.Transactions.Common.FoxFastTransactionController", false)
            ?? throw new InvalidOperationException("cannot find underline transaction controller.");
        if (Activator.CreateInstance(type, true) is not IFoxTransactionControl controller)
        {
            throw new InvalidOperationException("cannot create underline transaction controller.");
        }
        _controller = controller;
    }

    public void Initialize(FoxTransactionInfo txInfo)
    {
        // 필요한 초기화를 수행한다.
        AnsiConsole.MarkupLine($"[darkgreen]  > tx_init[/]");
        _controller.Initialize(txInfo);
    }

    public object BeginTransaction(FoxExecutionContext ctx, out bool isRoot)
    {
        // 트랜잭션을 시작하거나 참여한다.
        AnsiConsole.MarkupLine($"[darkgreen]  > tx_begin[/]");
        return _controller.BeginTransaction(ctx, out isRoot);
    }

    public void CommitTransaction(FoxExecutionContext ctx, object txData)
    {
        // 트랜잭션 성공에 투표하거나 커밋을 수행한다.
        AnsiConsole.MarkupLine($"[darkgreen]  > tx_commit[/]");
        _controller.CommitTransaction(ctx, txData);
    }

    public void AbortTransaction(FoxExecutionContext ctx, object txData)
    {
        // 트랜잭션 실패에 투표하거나 롤백을 수행한다.
        AnsiConsole.MarkupLine($"[darkgreen]  > tx_abort[/]");
        _controller.AbortTransaction(ctx, txData);
    }

    public void EndTransaction(FoxExecutionContext ctx, object txData)
    {
        // 트랜잭션을 종료하고 자원을 해제한다.
        AnsiConsole.MarkupLine($"[darkgreen]  > tx_end[/]");
        _controller.EndTransaction(ctx, txData);
    }

    public bool IsInTransaction(FoxExecutionContext ctx)
    {
        return _controller.IsInTransaction(ctx);
    }
}
