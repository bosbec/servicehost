// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StructureMapContainerAdapter.cs" company="Bosbec AB">
//   Copyright © Bosbec AB 2014
// </copyright>
// <summary>
//   Defines the StructureMapContainerAdapter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Bosbec.ServiceHost.StructureMap
{
    using System;

    /// <summary>
    /// Defines the StructureMapContainerAdapter type.
    /// </summary>
    public class StructureMapContainerAdapter : IContainerAdapter
    {
        /// <summary>
        /// The container.
        /// </summary>
        private readonly global::StructureMap.IContainer _container;

        /// <summary>
        /// Initializes a new instance of the <see cref="StructureMapContainerAdapter"/> class.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        public StructureMapContainerAdapter(global::StructureMap.IContainer container)
        {
            _container = container;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StructureMapContainerAdapter"/> class.
        /// </summary>
        public StructureMapContainerAdapter()
            : this(new global::StructureMap.Container())
        {
        }

        /// <summary>
        /// Get a service instance of a given service type.
        /// </summary>
        /// <param name="serviceType">
        /// The service type.
        /// </param>
        /// <returns>
        /// The service instance.
        /// </returns>
        public IService GetServiceInstance(Type serviceType)
        {
            return (IService)_container.GetInstance(serviceType);
        }

        /// <summary>
        /// Register a service instance by the given service type.
        /// </summary>
        /// <param name="serviceType">
        /// The service type.
        /// </param>
        /// <param name="serviceInstance">
        /// The service instance.
        /// </param>
        public void RegisterServiceInstance(Type serviceType, object serviceInstance)
        {
            _container.Configure(c => c.For(serviceType).Singleton().Use(serviceInstance));
        }
    }
}