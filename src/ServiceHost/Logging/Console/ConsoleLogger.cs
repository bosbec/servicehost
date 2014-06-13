// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleLogger.cs" company="Bosbec AB">
//   Copyright © Bosbec AB 2014
// </copyright>
// <summary>
//   Defines the ConsoleLogger type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Bosbec.ServiceHost.Logging.Console
{
    using System;
    using System.Threading;

    /// <summary>
    /// Defines the ConsoleLogger type.
    /// </summary>
    public class ConsoleLogger : ILog
    {
        /// <summary>
        /// The type.
        /// </summary>
        private readonly Type _type;

        /// <summary>
        /// The factory.
        /// </summary>
        private readonly ConsoleLoggerFactory _factory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleLogger"/> class.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <param name="factory">
        /// The factory.
        /// </param>
        public ConsoleLogger(Type type, ConsoleLoggerFactory factory)
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
            Log(LogLevel.Debug, message, _factory.Colors.Debug, parameters);
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
            Log(LogLevel.Info, message, _factory.Colors.Info, parameters);
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
            Log(LogLevel.Warn, message, _factory.Colors.Warn, parameters);
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
            Log(LogLevel.Error, message, _factory.Colors.Error, parameters);
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
            Log(LogLevel.Error, string.Format(message, parameters) + Environment.NewLine + exception, _factory.Colors.Error);
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
        /// <param name="colorSetting">
        /// The color setting.
        /// </param>
        /// <param name="parameters">
        /// The parameters.
        /// </param>
        private void Log(LogLevel level, string message, ConsoleColorSetting colorSetting, params object[] parameters)
        {
            if (_factory.Colored)
            {
                using (colorSetting.Enter())
                {
                    Write(level, message, parameters);
                }
            }
            else
            {
                Write(level, message, parameters);
            }
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

                Console.WriteLine(format, timeFormat, typeName, levelString, threadName, renderedMessage);
            }
            catch
            {
                Warn("Could not render output string: '{0}' with args: {1}", message, string.Join(", ", parameters));
            }
        }
    }
}