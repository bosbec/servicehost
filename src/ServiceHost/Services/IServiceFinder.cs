// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IServiceFinder.cs" company="Bosbec AB">
//   Copyright © Bosbec AB 2014
// </copyright>
// <summary>
//   Defines the IServiceFinder type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Bosbec.ServiceHost.Services
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the IServiceFinder type.
    /// </summary>
    public interface IServiceFinder
    {
        /// <summary>
        /// Find all services.
        /// </summary>
        /// <returns>
        /// The found services.
        /// </returns>
        IEnumerable<Type> FindServices();
    }
}