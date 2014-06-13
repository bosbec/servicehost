// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IHost.cs" company="Bosbec AB">
//   Copyright © Bosbec AB 2014
// </copyright>
// <summary>
//   Defines the IHost type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Bosbec.ServiceHost.Hosting
{
    using System;

    /// <summary>
    /// Defines the IHost type.
    /// </summary>
    public interface IHost
    {
        /// <summary>
        /// Raised when the service is starting.
        /// </summary>
        event EventHandler Starting;

        /// <summary>
        /// Raised when the service is stopped.
        /// </summary>
        event EventHandler Stopped;

        /// <summary>
        /// Run the service host.
        /// </summary>
        void Run();
    }
}