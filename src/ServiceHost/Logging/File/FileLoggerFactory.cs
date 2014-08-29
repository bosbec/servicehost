// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileLoggerFactory.cs" company="Bosbec AB">
//   Copyright © Bosbec AB 2014
// </copyright>
// <summary>
//   Defines the FileLoggerFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Bosbec.ServiceHost.Logging.File
{
    using System;
    using System.Collections.Concurrent;

    /// <summary>
    /// Defines the FileLoggerFactory type.
    /// </summary>
    public class FileLoggerFactory : AbstractLoggerFactory
    {
        /// <summary>
        /// The loggers.
        /// </summary>
        private static readonly ConcurrentDictionary<Type, ILog> Loggers = new ConcurrentDictionary<Type, ILog>();

        /// <summary>
        /// Initializes a new instance of the <see cref="FileLoggerFactory"/> class.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        public FileLoggerFactory(string path)
        {
            Path = path;
        }

        /// <summary>
        /// Gets or sets a value indicating whether show timestamps.
        /// </summary>
        public bool ShowTimestamps { get; set; }

        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the min level.
        /// </summary>
        public LogLevel MinLevel { get; set; }

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
                logger = new FileLogger(type, this);

                Loggers.TryAdd(type, logger);
            }

            return logger;
        }
    }
}