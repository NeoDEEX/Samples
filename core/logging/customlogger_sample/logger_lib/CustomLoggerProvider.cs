using NeoDEEX.Diagnostics;

namespace LoggerLib;

internal class CustomLoggerProvider : FoxLoggerProviderBase
{
    public CustomLoggerProvider(string name, FoxLoggerPropertyCollection properties)
        : base(name, properties)
    {
    }

    protected override IFoxLog OnCreateLogger(string loggerName, FoxLoggerPropertyCollection properties)
    {
        CustomLogger logger = new(loggerName);
        foreach(string key in properties)
        {
            string value = properties[key];
            if (String.Compare(key, "console", StringComparison.OrdinalIgnoreCase) == 0)
            {
                logger.ConsoleLogerName = value;
            }
            else if (String.Compare(key, "file", StringComparison.OrdinalIgnoreCase) == 0)
            {
                logger.FileLoggerName = value;
            }
            else
            {
                FoxLoggerProviderBase.ThrowPropertyNameException(logger, key);
            }
        }
        return logger;
    }
}
