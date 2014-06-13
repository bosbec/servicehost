// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleLoggerFactory.cs" company="Bosbec AB">
//   Copyright © Bosbec AB 2014
// </copyright>
// <summary>
//   Defines the ConsoleLoggerFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Bosbec.ServiceHost.Logging.Console
{
    using System;
    using System.Collections.Concurrent;

    /// <summary>
    /// Defines the ConsoleLoggerFactory type.
    /// </summary>
    public class ConsoleLoggerFactory : AbstractLoggerFactory
    {
        /// <summary>
        /// The loggers.
        /// </summary>
        private static readonly ConcurrentDictionary<Type, ILog> Loggers = new ConcurrentDictionary<Type, ILog>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleLoggerFactory"/> class.
        /// </summary>
        /// <param name="colored">
        /// The colored.
        /// </param>
        public ConsoleLoggerFactory(bool colored)
        {
            Colors = new ConsoleLoggingColors();
            Colored = colored;
            MinLevel = LogLevel.Debug;
        }

        /// <summary>
        /// Gets or sets a value indicating whether show timestamps.
        /// </summary>
        public bool ShowTimestamps { get; set; }

        /// <summary>
        /// Gets or sets the colors.
        /// </summary>
        public ConsoleLoggingColors Colors { get; set; }

        /// <summary>
        /// Gets or sets the min level.
        /// </summary>
        public LogLevel MinLevel { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether colored.
        /// </summary>
        public bool Colored { get; set; }

        /// <summary>
        /// Should get a logger for the specified type 
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <returns>
        /// The logger.
        /// </returns>
        protected override ILog GetLogger(Type type)
        {
            ILog logger;

            if (!Loggers.TryGetValue(type, out logger))
            {
                logger = new ConsoleLogger(type, this);

                Loggers.TryAdd(type, logger);
            }

            return logger;
        }
    }
}