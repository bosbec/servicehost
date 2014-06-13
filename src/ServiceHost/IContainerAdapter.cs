// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IContainerAdapter.cs" company="Bosbec AB">
//   Copyright © Bosbec AB 2014
// </copyright>
// <summary>
//   Defines the IContainerAdapter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Bosbec.ServiceHost
{
    using System;

    /// <summary>
    /// Defines the IContainerAdapter type.
    /// </summary>
    public interface IContainerAdapter
    {
        /// <summary>
        /// Get a service instance of a given service type.
        /// </summary>
        /// <param name="serviceType">
        /// The service type.
        /// </param>
        /// <returns>
        /// The service instance.
        /// </returns>
        IService GetServiceInstance(Type serviceType);

        /// <summary>
        /// Register a service instance by the given service type.
        /// </summary>
        /// <param name="serviceType">
        /// The service type.
        /// </param>
        /// <param name="serviceInstance">
        /// The service instance.
        /// </param>
        void RegisterServiceInstance(Type serviceType, object serviceInstance);
    }
}