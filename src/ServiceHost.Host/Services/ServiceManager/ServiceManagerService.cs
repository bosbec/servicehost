// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServiceManagerService.cs" company="Bosbec AB">
//   Copyright © Bosbec AB 2014
// </copyright>
// <summary>
//   Defines the ServiceManagerService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Bosbec.ServiceHost.Host.Services
{
    /// <summary>
    /// Defines the ServiceManagerService type.
    /// </summary>
    public class ServiceManagerService : IService, IRequireInitialization, IDependOn<HttpCommunicationService>, IDependOn<TcpCommunicationService>
    {
        /// <summary>
        /// Start the service.
        /// </summary>
        public void Start()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Stop the service.
        /// </summary>
        public void Stop()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Perform initialization logic.
        /// </summary>
        public void Initialize()
        {
            throw new System.NotImplementedException();
        }
    }
}