// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LoggingConfiguration.cs" company="Bosbec AB">
//   Copyright © Bosbec AB 2014
// </copyright>
// <summary>
//   Defines the LoggingConfiguration type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Bosbec.ServiceHost.Logging
{
    using Bosbec.ServiceHost.Logging.Console;

    /// <summary>
    /// Defines the LoggingConfiguration type.
    /// </summary>
    public class LoggingConfiguration
    {
        public void Console()
        {
            Use(new ConsoleLoggerFactory(false));
        }

        public void Console(LogLevel minLevel)
        {
            Use(new ConsoleLoggerFactory(false) { MinLevel = minLevel });
        }

        public void ColoredConsole()
        {
            Use(new ConsoleLoggerFactory(true));
        }

        public void ColoredConsole(LogLevel minLevel)
        {
            Use(new ConsoleLoggerFactory(true) { MinLevel = minLevel });
        }

        public void None()
        {
            Use(new NullLoggerFactory());
        }

        public void Use(ILoggerFactory loggerFactory)
        {
            LogManager.Current = loggerFactory;
        }
    }
}