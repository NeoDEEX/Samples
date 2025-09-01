// See https://aka.ms/new-console-template for more information
using NeoDEEX.Diagnostics;
using NeoDEEX.Diagnostics.Loggers;

Console.WriteLine("Simple Fox Logging Sample....");
SimpleLogging();
VeryImportantMethod("TestUser", "some data");
LoggerNameTest();
LogSourceTest();
LogFilterTest();
ExtensionMethodTest();
EnumLoggerProviders();
EnumLoggers();
EtcTest();

static void SimpleLogging()
{
    IFoxLog log = FoxLogManager.GetLogger("MyLogger");
    log.Write(FoxLogLevel.Information, "This is a log entry.");
}

static void VeryImportantMethod(string id, string arg)
{
    IFoxLog log = FoxLogManager.GetLogger("MyLogger");
    log.WriteFormat(FoxLogLevel.Information, "Method parameter: id={0} arg={1}", id, arg);
}

static void LoggerNameTest()
{
    IFoxLog log1 = FoxLogManager.GetLogger("MyLogger");
    Console.WriteLine($"log1.Name = {log1.Name}");

    // 존재하지 않는 로거 이름을 사용하였기 때문에 대체 로거가 반환된다.
    IFoxLog log2 = FoxLogManager.GetLogger("NonExistLoggerName");
    Console.WriteLine($"log2.Name = {log2.Name}");

    // MyLogger.SubLogger 로거를 요청했지만 로거 이름 레벨링에 의해 MyLogger가 반환된다.
    IFoxLog log3 = FoxLogManager.GetLogger("MyLogger.SubLogger");
    Console.WriteLine($"log3.Name = {log3.Name}");
}

static void LogSourceTest()
{
    IFoxLog log = FoxLogManager.GetLogger("MyLogger");
    log.Write(FoxLogLevel.Information, "Wihtout source parameter.");
    log.Write("MySource", FoxLogLevel.Information, "With source parameter.");
}

static void LogFilterTest()
{
    Console.WriteLine($"Currnet GlobalFilter: {FoxLogManager.GlobalFilterLevel}");
    IFoxLog log = FoxLogManager.GetLogger("MyLogger");
    log.FilterLevel = FoxLogLevel.Error;
    log.Write(FoxLogLevel.Information, "This will not be logged....");

    foreach(FoxLogLevel level in Enum.GetValues(typeof(FoxLogLevel)))
    {
        Console.WriteLine($"{level} : { (log.IsEnabled(level) == true ? "logged" : "filtered") }");
    }
    log.FilterLevel = FoxLogLevel.Information;
}

static void ExtensionMethodTest()
{
    IFoxLog log = FoxLogManager.GetLogger("MyLogger");
    log.Write(FoxLogLevel.Critical, "This is a critical method.");
    if (log.IsEnabled(FoxLogLevel.Information) == true)
    {
        log.WriteFormat(FoxLogLevel.Information, "formatting log message... x64={0}", Environment.Is64BitProcess);
    }

    log.Critical("This is a critical method.");
    if (log.InformationEnabled() == true)
    {
        log.InformationFormat("formatting log message... x64={0}", Environment.Is64BitProcess);
    }
}

static void EnumLoggerProviders()
{
    foreach(IFoxLoggerProvider provider in FoxLogManager.Providers)
    {
        Console.WriteLine($"ProviderName: {provider.Name}");
        Console.WriteLine($"  Type: {provider.GetType().FullName}");
    }
}

static void EnumLoggers()
{
    foreach (IFoxLog logger in FoxLogManager.Loggers)
    {
        Console.WriteLine($"LoggerName: {logger.Name}");
        Console.WriteLine($"  Type: {logger.GetType().FullName}");
    }
}

static void EtcTest()
{
    Console.WriteLine($"GetLogDateTime = {FoxLogManager.GetLogDateTime()}");
    Console.WriteLine(DateTime.Now.ToString(FoxLogManager.DefaultDateTimeFormat));
    Console.WriteLine(FoxLogManager.GetFormattedString("MySource", FoxLogLevel.Warning, "Log message..."));
}