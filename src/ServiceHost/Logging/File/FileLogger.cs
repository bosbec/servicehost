// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileLogger.cs" company="Bosbec AB">
//   Copyright © Bosbec AB 2014
// </copyright>
// <summary>
//   Defines the FileLogger type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Bosbec.ServiceHost.Logging.File
{
    using System;
    using System.IO;
    using System.Threading;

    /// <summary>
    /// Defines the FileLogger type.
    /// </summary>
    public class FileLogger : ILog
    {
        /// <summary>
        /// The type.
        /// </summary>
        private readonly Type _type;

        /// <summary>
        /// The factory.
        /// </summary>
        private readonly FileLoggerFactory _factory;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileLogger"/> class.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <param name="factory">
        /// The factory.
        /// </param>
        public FileLogger(Type type, FileLoggerFactory factory)
        {
            _type = type;
            _factory = factory;
        }

        /// <summary>
        /// Writes the specified message with the DEBUG level.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="parameters">
        /// The parameters.
        /// </param>
        public void Debug(string message, params object[] parameters)
        {
            Log(LogLevel.Debug, message, parameters);
        }

        /// <summary>
        /// Writes the specified message with the INFO level.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="parameters">
        /// The parameters.
        /// </param>
        public void Info(string message, params object[] parameters)
        {
            Log(LogLevel.Info, message, parameters);
        }

        /// <summary>
        /// Writes the specified message with the WARN level.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="parameters">
        /// The parameters.
        /// </param>
        public void Warn(string message, params object[] parameters)
        {
            Log(LogLevel.Warn, message, parameters);
        }

        /// <summary>
        /// Writes the specified message with the ERROR level.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="parameters">
        /// The parameters.
        /// </param>
        public void Error(string message, params object[] parameters)
        {
            Log(LogLevel.Error, message, parameters);
        }

        /// <summary>
        /// Writes the specified message with the ERROR level and includes the
        /// full details of the specified exception.
        /// </summary>
        /// <param name="exception">
        /// The exception.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="parameters">
        /// The parameters.
        /// </param>
        public void Error(Exception exception, string message, params object[] parameters)
        {
            Log(LogLevel.Error, string.Format(message, parameters) + Environment.NewLine + exception);
        }

        /// <summary>
        /// Log the specified message.
        /// </summary>
        /// <param name="level">
        /// The level.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="parameters">
        /// The parameters.
        /// </param>
        private void Log(LogLevel level, string message, params object[] parameters)
        {
            Write(level, message, parameters);
        }

        /// <summary>
        /// Write the specified message to the console.
        /// </summary>
        /// <param name="level">
        /// The level.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="parameters">
        /// The parameters.
        /// </param>
        private void Write(LogLevel level, string message, object[] parameters)
        {
            if ((int)level < (int)_factory.MinLevel)
            {
                return;
            }

            var levelString = (Enum.GetName(typeof(LogLevel), level) ?? string.Empty).ToUpper();

            var threadName = Thread.CurrentThread.Name;
            var typeName = _type.FullName;

            try
            {
                var renderedMessage = string.Format(message, parameters);
                var timeFormat = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");

                var format = _factory.ShowTimestamps
                    ? "{0} {1} {2} ({3}): {4}"
                    : "{1} {2} ({3}): {4}";

                var text = string.Format(format, timeFormat, typeName, levelString, threadName, renderedMessage);

                File.AppendAllLines(_factory.Path, new[] { text });
            }
            catch
            {
                Warn("Could not render output string: '{0}' with args: {1}", message, string.Join(", ", parameters));
            }
        }
    }
}