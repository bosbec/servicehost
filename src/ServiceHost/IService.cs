// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IService.cs" company="Bosbec AB">
//   Copyright © Bosbec AB 2014
// </copyright>
// <summary>
//   Defines the IService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Bosbec.ServiceHost
{
    /// <summary>
    /// Defines the IService type.
    /// </summary>
    public interface IService
    {
        /// <summary>
        /// Start the service.
        /// </summary>
        void Start();

        /// <summary>
        /// Stop the service.
        /// </summary>
        void Stop();
    }
}