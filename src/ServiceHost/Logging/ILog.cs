// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ILog.cs" company="Bosbec AB">
//   Copyright © Bosbec AB 2014
// </copyright>
// <summary>
//   Defines the ILog type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Bosbec.ServiceHost.Logging
{
    using System;

    /// <summary>
    /// Defines the ILog type.
    /// </summary>
    public interface ILog
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
        void Debug(string message, params object[] parameters);

        /// <summary>
        /// Writes the specified message with the INFO level.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="parameters">
        /// The parameters.
        /// </param>
        void Info(string message, params object[] parameters);

        /// <summary>
        /// Writes the specified message with the WARN level.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="parameters">
        /// The parameters.
        /// </param>
        void Warn(string message, params object[] parameters);

        /// <summary>
        /// Writes the specified message with the ERROR level.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="parameters">
        /// The parameters.
        /// </param>
        void Error(string message, params object[] parameters);

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
        void Error(Exception exception, string message, params object[] parameters);
    }
}