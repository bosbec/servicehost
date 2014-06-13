// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AbstractLoggerFactory.cs" company="Bosbec AB">
//   Copyright © Bosbec AB 2014
// </copyright>
// <summary>
//   Defines the AbstractLoggerFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Bosbec.ServiceHost.Logging
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// Defines the AbstractLoggerFactory type.
    /// </summary>
    public abstract class AbstractLoggerFactory : ILoggerFactory
    {
        /// <summary>
        /// Gets a logger that is initialized to somehow carry information on
        /// the class that is using it.
        /// Be warned that this method will most likely be pretty slow,
        /// because it will probably rely on some clunky stackframe inspection.
        /// </summary>
        /// <returns>
        /// The <see cref="ILog"/> instance for the current class.
        /// </returns>
        public ILog GetCurrentClassLogger()
        {
            var stackFrame = new StackFrame(1);

            return GetLogger(stackFrame.GetMethod().DeclaringType);
        }

        /// <summary>
        /// Should get a logger for the specified type 
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <returns>
        /// The logger.
        /// </returns>
        protected abstract ILog GetLogger(Type type);
    }
}