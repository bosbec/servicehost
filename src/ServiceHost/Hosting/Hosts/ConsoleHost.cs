// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleHost.cs" company="Bosbec AB">
//   Copyright © Bosbec AB 2014
// </copyright>
// <summary>
//   Defines the ConsoleHost type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Bosbec.ServiceHost.Hosting.Hosts
{
    using System;
    using System.Threading;

    using Bosbec.ServiceHost.Logging;

    /// <summary>
    /// Defines the ConsoleHost type.
    /// </summary>
    public class ConsoleHost : IHost
    {
        /// <summary>
        /// The log.
        /// </summary>
        private static ILog _log;

        /// <summary>
        /// The exit handle.
        /// </summary>
        private readonly AutoResetEvent _exit = new AutoResetEvent(false);

        /// <summary>
        /// Initializes static members of the <see cref="ConsoleHost"/> class.
        /// </summary>
        static ConsoleHost()
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
            Console.CancelKeyPress += HandleCancelKeyPress;

            OnStarting();

            _exit.WaitOne();

            OnStopped();
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

        /// <summary>
        /// Handle cancel keypress.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="arguments">
        /// The event arguments.
        /// </param>
        private void HandleCancelKeyPress(object sender, ConsoleCancelEventArgs arguments)
        {
            _log.Debug("Cancel key was pressed");

            arguments.Cancel = true;

            _exit.Set();
        }
    }
}