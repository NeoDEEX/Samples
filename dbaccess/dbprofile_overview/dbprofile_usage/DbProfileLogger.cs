using NeoDEEX.Data.Diagnostics;
using NeoDEEX.Diagnostics;

namespace dbprofile_usage;

public class DbProfileLogger(string name) : FoxLoggerBase(name)
{
    protected override void WriteLog(string source, FoxLogLevel level, object? data)
    {
        if (data is not FoxDbProfileInfo info)
        {
            throw new ArgumentException("Log entry data is not of type FoxDbProfileInfo.", nameof(data));
        }
        string json = info.ToJson();
        if (level == FoxLogLevel.Error)
        {
            Console.Error.WriteLine($"[ERROR] {info} - {json}");
        }
        else
        {
            Console.WriteLine($"[{level}] {info} - {json}");
        }
    }

    protected override void WriteLogMessage(string source, FoxLogLevel level, string? message)
    {
        throw new InvalidOperationException();
    }
}
