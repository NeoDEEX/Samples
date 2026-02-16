using NeoDEEX.Diagnostics;
using NeoDEEX.Transactions.Common;
using Spectre.Console;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace execution_extension;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
internal class MyLoggingAttribute : Attribute, IFoxExecutionExtension
{
    public string? LoggerName { get; private set; }

    public bool ShouldOverride => true;

    public MyLoggingAttribute()
    {
        this.LoggerName = null;
    }

    public MyLoggingAttribute(string loggerName)
    {
        this.LoggerName = loggerName;
    }

    // 최초 1회만 호출되는 초기화 메서드
    public int Initialize(Type type, MethodBase? method)
    {
        AnsiConsole.MarkupLine($"[gray]MyLogging extension Initialize() called for {type.FullName}.{method?.Name}[/]");
        this.LoggerName ??= type.FullName;
        return 0;
    }

    public void PreProcess(FoxExecutionContext ctx)
    {
        AnsiConsole.MarkupLine($"[gray]MyLogging extension PreProcess() called for {ctx.MethodBase.DeclaringType?.FullName}.{ctx.MethodBase.Name}[/]");

        Debug.Assert(this.LoggerName != null);
        IFoxLog log = FoxLogManager.GetLogger(this.LoggerName);

        StringBuilder sb = new(1024);
        sb.Append("Method invoked: ")
            .Append(ctx.MethodBase.DeclaringType?.FullName).Append(':')
            .Append(ctx.MethodBase.Name).Append("  ");
        ParameterInfo[] infos = ctx.MethodBase.GetParameters();
        for (int i = 0; i < infos.Length; i++)
        {
            ParameterInfo pi = infos[i];
            string valueString = ctx.MethodCallMessage.Args[i]?.ToString() ?? "(null)";
            sb.Append(pi.Name).Append('=').Append(Markup.Escape(valueString)).Append("  ");
        }
        log.Write("MyLogging", FoxLogLevel.Verbose, sb.ToString());
    }

    public void PostProcess(FoxExecutionContext ctx)
    {
        AnsiConsole.MarkupLine($"[gray]MyLogging extension PostProcess() called for {ctx.MethodBase.DeclaringType?.FullName}.{ctx.MethodBase.Name}[/]");

        Debug.Assert(this.LoggerName != null);
        IFoxLog log = FoxLogManager.GetLogger(this.LoggerName);
        if (ctx.Exception == null)
        {
            string valueString = ctx.MethodReturnMessage.Result?.ToString() ?? "(null)";
            log.Write("MyLogging", FoxLogLevel.Verbose, $"  Return value: {Markup.Escape(valueString)}");
        }
        else
        {
            log.Write("MyLogging", FoxLogLevel.Verbose, $"  Exception: {ctx.Exception.GetType().FullName}");
        }
    }
}
