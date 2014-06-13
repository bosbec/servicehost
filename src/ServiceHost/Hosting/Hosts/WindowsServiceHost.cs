// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WindowsServiceHost.cs" company="Bosbec AB">
//   Copyright © Bosbec AB 2014
// </copyright>
// <summary>
//   Defines the WindowsServiceHost type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Bosbec.ServiceHost.Hosting.Hosts
{
    using System;
    using System.ServiceProcess;

    /// <summary>
    /// Defines the WindowsServiceHost type.
    /// </summary>
    public class WindowsServiceHost : IHost
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
            var wrapper = new WindowsServiceWrapper();

            wrapper.Started += (sender, arguments) => OnStarting();
            wrapper.Stopped += (sender, arguments) => OnStopped();

            ServiceBase.Run(wrapper);
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

        /// <summary>
        /// Defines the WindowsServiceWrapper type.
        /// </summary>
        private class WindowsServiceWrapper : ServiceBase
        {
            /// <summary>
            /// Raised when the service is started.
            /// </summary>
            public event EventHandler Started;

            /// <summary>
            /// Raised when the services is stopped.
            /// </summary>
            public event EventHandler Stopped;

            /// <summary>
            /// When implemented in a derived class, executes when a Start
            /// command is sent to the service by the Service Control
            /// Manager (SCM) or when the operating system starts
            /// (for a service that starts automatically). Specifies actions to
            /// take when the service starts.
            /// </summary>
            /// <param name="args">
            /// Data passed by the start command.
            /// </param>
            protected override void OnStart(string[] args)
            {
                var handler = Started;

                if (handler != null)
                {
                    handler(this, EventArgs.Empty);
                }
            }

            /// <summary>
            /// When implemented in a derived class, executes when a Stop
            /// command is sent to the service by the Service Control Manager
            /// (SCM). Specifies actions to take when a service stops running.
            /// </summary>
            protected override void OnStop()
            {
                var handler = Stopped;

                if (handler != null)
                {
                    handler(this, EventArgs.Empty);
                }
            }
        }
    }
}