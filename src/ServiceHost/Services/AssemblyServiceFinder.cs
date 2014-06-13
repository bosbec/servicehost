// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AssemblyServiceFinder.cs" company="Bosbec AB">
//   Copyright © Bosbec AB 2014
// </copyright>
// <summary>
//   Defines the AssemblyServiceFinder type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Bosbec.ServiceHost.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Defines the AssemblyServiceFinder type.
    /// </summary>
    public class AssemblyServiceFinder : IServiceFinder
    {
        /// <summary>
        /// The assemblies.
        /// </summary>
        private readonly Assembly[] _assemblies;

        /// <summary>
        /// Initializes a new instance of the <see cref="AssemblyServiceFinder"/> class.
        /// </summary>
        /// <param name="assemblies">
        /// The assemblies.
        /// </param>
        public AssemblyServiceFinder(params Assembly[] assemblies)
        {
            _assemblies = assemblies;
        }

        /// <summary>
        /// Find all services.
        /// </summary>
        /// <returns>
        /// The found services.
        /// </returns>
        public IEnumerable<Type> FindServices()
        {
            var services = _assemblies.SelectMany(x => x.GetTypes())
                .Where(x => typeof(IService).IsAssignableFrom(x) && !x.IsAbstract && x.IsClass)
                .ToList();

            return services;
        }
    }
}