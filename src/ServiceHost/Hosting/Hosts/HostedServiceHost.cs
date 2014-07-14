// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HostedServiceHost.cs" company="Bosbec AB">
//   Copyright © Bosbec AB 2014
// </copyright>
// <summary>
//   Defines the HostedServiceHost type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Bosbec.ServiceHost.Hosting.Hosts
{
    using System;

    using Bosbec.ServiceHost.Logging;

    /// <summary>
    /// Defines the HostedServiceHost type.
    /// </summary>
    public class HostedServiceHost : IHost
    {
        /// <summary>
        /// The log.
        /// </summary>
        private static ILog _log;

        /// <summary>
        /// Initializes static members of the <see cref="HostedServiceHost"/> class.
        /// </summary>
        static HostedServiceHost()
        {
            LogManager.Changed += f => _log = f.GetCurrentClassLogger();
        }

        /// <summary>
        /// Raised when the service is starting.
        /// </summary>
        public event EventHandler Starting;

        /// <summary>
        /// Raised when the service is stopped.
        /// </summary>
        public event EventHandler Stopped;

        /// <summary>
        /// Run the service host.
        /// </summary>
        public void Run()
        {
            HostedServiceHostCommunicator.Instance.Stopped += (sender, arguments) => OnStopped();
            HostedServiceHostCommunicator.Instance.Starting += (sender, arguments) => OnStarting();
        }

        /// <summary>
        /// Raise the Starting event.
        /// </summary>
        protected virtual void OnStarting()
        {
            _log.Info("Starting");

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
            _log.Info("Stopping");

            var handler = Stopped;

            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
    }
}