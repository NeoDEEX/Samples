// See https://aka.ms/new-console-template for more information
using NeoDEEX.Diagnostics;
using NeoDEEX.Diagnostics.Loggers;

Console.WriteLine("FoxTextFileLogger Sample");

// 추가 인코딩 등록
System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

IFoxLog log1 = FoxLogManager.GetLogger("MyLogger");
log1.Information("This is MY log entry...");
if (log1 is FoxTextFileLogger logger1)
{
    Console.WriteLine($"FilePrefix: {logger1.FilePrefix}");
    Console.WriteLine($"DirectoryPath: {logger1.DirectoryPath}");
    Console.WriteLine($"CreationSchedule: {logger1.CreationSchedule}");
    Console.WriteLine($"MaxFileSize: {logger1.MaxFileSize}");
    Console.WriteLine($"EffectiveDirectory: {logger1.EffectiveDirectory}");
    Console.WriteLine($"FilePath: {logger1.FilePath}");
    Console.WriteLine($"Encoding: {logger1.EncodingName}");
    Console.WriteLine($"Code Page: {logger1.Encoding.CodePage}");
    Console.WriteLine($"ExclusiveLock: {logger1.ExclusiveLock}");
}
Console.WriteLine();

IFoxLog log2 = FoxLogManager.GetLogger("YourLogger");
log2.Information("This is YOUR log entry...");
if (log2 is FoxTextFileLogger logger2)
{
    Console.WriteLine($"FilePrefix: {logger2.FilePrefix}");
    Console.WriteLine($"DirectoryPath: {logger2.DirectoryPath}");
    Console.WriteLine($"CreationSchedule: {logger2.CreationSchedule}");
    Console.WriteLine($"MaxFileSize: {logger2.MaxFileSize}");
    Console.WriteLine($"EffectiveDirectory: {logger2.EffectiveDirectory}");
    Console.WriteLine($"FilePath: {logger2.FilePath}");
    Console.WriteLine($"Encoding: {logger2.EncodingName}");
    Console.WriteLine($"Code Page: {logger2.Encoding.CodePage}");
    Console.WriteLine($"ExclusiveLock: {logger2.ExclusiveLock}");
}
Console.WriteLine();

Dictionary<string, string> dic = new()
{
    { "filePrefix", "HisLog"},
    { "directory", "~/Logs"},
    { "maxSize", "64MB"}
};
FoxLoggerPropertyCollection properties = new(dic); 
FoxTextFileLoggerProvider provider = new("theProvider", properties);
FoxTextFileLogger logger3 = (FoxTextFileLogger)provider.CreateLogger("HerLogger", FoxLoggerPropertyCollection.Empty);
logger3.Warning("This is warning log.");
Console.WriteLine($"FilePath: {logger3.FilePath}");
Console.WriteLine($"MaxFileSize: {logger3.MaxFileSize}");

