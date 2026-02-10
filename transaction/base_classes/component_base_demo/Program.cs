using Microsoft.Extensions.Configuration;
using NeoDEEX.Data;
using NeoDEEX.Diagnostics;
using NeoDEEX.Security;
using Spectre.Console;

namespace component_base_demo;

internal class Program
{
    static void Main()
    {
        AnsiConsole.MarkupLine("[green]Fox Transactions FoxComponentBase Class Sample[/]");

        // user-secrets에서 DB 연결정보를 로드하도록 구성 설정 지정.
        var assembly = typeof(Program).Assembly;
        FoxDatabaseConfig.ExternalConfiguration = new ConfigurationBuilder().AddUserSecrets(assembly).Build();

        // 사용자 인증을 시뮬레이션하기 위해 사용자 정보 생성
        FoxUserInfoContext userInfo = new("TestUser");
        // 인증이 성공적이면 앱은 SetCallContext 메서드를 호출하여
        // Fox Biz/Data Service 및 Fox Transactions 에 인증된 사용자 정보(FoxUserInfoContext 객체)를
        // 전달하게 됨.
        userInfo["DEPT"] = "부서코드";
        userInfo.SetCallContext();

        using TxComp comp = new();
        ITxComp itf = comp.CreateExecution<ITxComp>();
        try
        {
            //itf.NonTxMethod();
            itf.TxMethod();
            //itf.TxMethod(true);

            FoxPerformanceActivity activity = new("MyTrace");
            using (activity.Enter())
            {
                AnsiConsole.MarkupLine("[gray]Delaying 200 msec...[/]");
                System.Threading.Thread.Sleep(200);
                //itf.InvokeSub(true);
                comp.Invoke("InvokeSub", true);
            }
            AnsiConsole.MarkupLine($"[blue]{Markup.Escape(activity.ActivityInfo.ToString())}[/]");
        }
        catch (ApplicationException ex)
        {
            AnsiConsole.MarkupLine($"[red]Caught ApplicationException: {ex.Message}[/]");
        }
    }
}
