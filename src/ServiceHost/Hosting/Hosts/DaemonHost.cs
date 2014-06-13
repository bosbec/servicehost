// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DaemonHost.cs" company="Bosbec AB">
//   Copyright © Bosbec AB 2014
// </copyright>
// <summary>
//   Defines the DaemonHost type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Bosbec.ServiceHost.Hosting.Hosts
{
    using System;
    using System.Threading;

    using Mono.Unix;
    using Mono.Unix.Native;

    /// <summary>
    /// Defines the DaemonHost type.
    /// </summary>
    public class DaemonHost : IHost
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
        /// Run the service host.
        /// </summary>
        public void Run()
        {
            OnStarting();

            var signals = new[]
            {
                new UnixSignal(Signum.SIGINT),
                new UnixSignal(Signum.SIGTERM)
            };

            for (var exit = false; !exit;)
            {
                var id = UnixSignal.WaitAny(signals);

                if (id < 0 || id >= signals.Length)
                {
                    continue;
                }

                if (signals[id].IsSet)
                {
                    exit = true;
                }
            }

            OnStopped();
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