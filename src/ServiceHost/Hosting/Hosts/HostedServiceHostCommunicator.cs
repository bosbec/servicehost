// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HostedServiceHostCommunicator.cs" company="Bosbec AB">
//   Copyright © Bosbec AB 2014
// </copyright>
// <summary>
//   Defines the HostedServiceHostCommunicator type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Bosbec.ServiceHost.Hosting.Hosts
{
    using System;

    /// <summary>
    /// Defines the HostedServiceHostCommunicator type.
    /// </summary>
    internal class HostedServiceHostCommunicator : MarshalByRefObject
    {
        /// <summary>
        /// Raised when the service is starting.
        /// </summary>
        public event EventHandler Starting;

        /// <summary>
        /// Raised when the service is stopped.
        /// </summary>
        public event EventHandler Stopped;

        /// <summary>
        /// Gets the instance.
        /// </summary>
        public static HostedServiceHostCommunicator Instance { get; private set; }

        /// <summary>
        /// Start the services.
        /// </summary>
        public void Start()
        {
            OnStarting();
        }

        /// <summary>
        /// Stop the services.
        /// </summary>
        public void Stop()
        {
            OnStopped();
        }

        /// <summary>
        /// Set this instance as the singleton instance.
        /// </summary>
        public void SetAsSingletonInstance()
        {
            Instance = this;
        }

        /// <summary>
        /// Raise the Starting event.
        /// </summary>
        protected virtual void OnStarting()
        {
            var handler = Starting;

            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Raise the Stopped event.
        /// </summary>
        protected virtual void OnStopped()
        {
            var handler = Stopped;

            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
    }
}