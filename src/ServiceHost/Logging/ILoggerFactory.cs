// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ILoggerFactory.cs" company="Bosbec AB">
//   Copyright © Bosbec AB 2014
// </copyright>
// <summary>
//   Defines the ILoggerFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Bosbec.ServiceHost.Logging
{
    /// <summary>
    /// Defines the ILoggerFactory type.
    /// </summary>
    public interface ILoggerFactory
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
        ILog GetCurrentClassLogger();
    }
}