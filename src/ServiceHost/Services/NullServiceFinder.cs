// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NullServiceFinder.cs" company="Bosbec AB">
//   Copyright © Bosbec AB 2014
// </copyright>
// <summary>
//   Defines the NullServiceFinder type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Bosbec.ServiceHost.Services
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the NullServiceFinder type.
    /// </summary>
    public class NullServiceFinder : IServiceFinder
    {
        /// <summary>
        /// Find all services.
        /// </summary>
        /// <returns>
        /// The found services.
        /// </returns>
        public IEnumerable<Type> FindServices()
        {
            return new Type[0];
        }
    }
}