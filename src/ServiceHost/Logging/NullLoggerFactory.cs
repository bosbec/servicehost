// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NullLoggerFactory.cs" company="Bosbec AB">
//   Copyright © Bosbec AB 2014
// </copyright>
// <summary>
//   Defines the NullLoggerFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Bosbec.ServiceHost.Logging
{
    using System;

    /// <summary>
    /// Defines the NullLoggerFactory type.
    /// </summary>
    public class NullLoggerFactory : AbstractLoggerFactory
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private static readonly NullLogger Logger = new NullLogger();

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
            return Logger;
        }

        /// <summary>
        /// Defines the NullLogger type.
        /// </summary>
        private class NullLogger : ILog
        {
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
            }
        }
    }
}