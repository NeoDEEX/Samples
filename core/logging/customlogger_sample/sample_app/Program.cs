// See https://aka.ms/new-console-template for more information
using LoggerLib;
using NeoDEEX.Diagnostics;

Console.WriteLine("Custom Logger Sample");

IFoxLog log = FoxLogManager.GetLogger("CustomLogger");
if (log is CustomLogger logger)
{
    Console.WriteLine($"Type: {logger.GetType().FullName}");
    Console.WriteLine($"Console Logger Name: {logger.ConsoleLogerName}");
    Console.WriteLine($"File Logger Name: {logger.FileLoggerName}");
}
log.Information("Writing a log with CustomLogger");
