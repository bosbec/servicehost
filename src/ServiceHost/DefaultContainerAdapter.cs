// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultContainerAdapter.cs" company="Bosbec AB">
//   Copyright © Bosbec AB 2014
// </copyright>
// <summary>
//   Defines the DefaultContainerAdapter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Bosbec.ServiceHost
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Defines the DefaultContainerAdapter type.
    /// </summary>
    public class DefaultContainerAdapter : IContainerAdapter
    {
        /// <summary>
        /// The registered service instances.
        /// </summary>
        private readonly IDictionary<Type, object> _registeredServiceInstances = new Dictionary<Type, object>();

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
            var constructors = serviceType.GetConstructors(BindingFlags.Public | BindingFlags.Instance);

            var noArgumentsConstructor = constructors.FirstOrDefault(x => x.GetParameters().Length == 0);

            var greediestConstructor = (from constructor in constructors
                                        let parameters = constructor.GetParameters()
                                        where parameters.All(x => _registeredServiceInstances.ContainsKey(x.ParameterType))
                                        orderby parameters.Length descending
                                        select constructor).FirstOrDefault();

            if (noArgumentsConstructor == null && greediestConstructor == null)
            {
                // Throw exception
            }

            if (greediestConstructor == null)
            {
                return (IService)Activator.CreateInstance(serviceType);
            }

            {
                var parameters = greediestConstructor.GetParameters().Select(x => _registeredServiceInstances[x.ParameterType]).ToArray();

                return (IService)Activator.CreateInstance(serviceType, parameters);
            }
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
            if (_registeredServiceInstances.ContainsKey(serviceType))
            {
                return;
            }

            _registeredServiceInstances.Add(serviceType, serviceInstance);
        }
    }
}