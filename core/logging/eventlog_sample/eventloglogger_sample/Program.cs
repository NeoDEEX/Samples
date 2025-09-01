// See https://aka.ms/new-console-template for more information
using NeoDEEX.Diagnostics;
using NeoDEEX.Extensions.Diagnostics.Loggers;

Console.WriteLine("FoxEventLogLogger Sample!");

IFoxLog log = FoxLogManager.GetLogger("EventLogLoggerSample");
if (log is FoxEventLogLogger logger)
{
    Console.WriteLine($"Event Log Name: {logger.EventLogName}");
    Console.WriteLine($"Event Source Name: {logger.EventSourceName}");
    Console.WriteLine($"Create Log: {logger.CreateLog}");

    Console.WriteLine($"Max KB: {logger.EventLog.MaximumKilobytes}");
    Console.WriteLine($"Overflow Action: {logger.EventLog.OverflowAction}");
}
log.Information("NeoDEEX FoxEventLogLogger Test...");