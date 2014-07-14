// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServiceHost.cs" company="Bosbec AB">
//   Copyright © Bosbec AB 2014
// </copyright>
// <summary>
//   Defines the ServiceHostFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Bosbec.ServiceHost
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Bosbec.ServiceHost.Hosting;
    using Bosbec.ServiceHost.Hosting.Hosts;
    using Bosbec.ServiceHost.Internal;
    using Bosbec.ServiceHost.Logging;
    using Bosbec.ServiceHost.Services;

    /// <summary>
    /// Defines the ServiceHostFactory type.
    /// </summary>
    public class ServiceHost
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private static ILog _log;

        /// <summary>
        /// The container adapter.
        /// </summary>
        private readonly IContainerAdapter _containerAdapter;

        /// <summary>
        /// The service finder.
        /// </summary>
        private IServiceFinder _serviceFinder;

        /// <summary>
        /// Initializes static members of the <see cref="ServiceHost"/> class.
        /// </summary>
        static ServiceHost()
        {
            LogManager.Changed += factory => _log = factory.GetCurrentClassLogger();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceHost"/> class.
        /// </summary>
        /// <param name="containerAdapter">
        /// The container adapter.
        /// </param>
        public ServiceHost(IContainerAdapter containerAdapter)
        {
            _containerAdapter = containerAdapter;
        }

        /// <summary>
        /// Create a new service host with the specified container adapter.
        /// </summary>
        /// <param name="containerAdapter">
        /// The container adapter.
        /// </param>
        /// <returns>
        /// The created service host.
        /// </returns>
        public static ServiceHost Create(IContainerAdapter containerAdapter)
        {
            var serviceHost = new ServiceHost(containerAdapter);

            return serviceHost;
        }

        /// <summary>
        /// Configure logging of the service host.
        /// </summary>
        /// <param name="configure">
        /// The configure action.
        /// </param>
        /// <returns>
        /// The configured service host.
        /// </returns>
        public ServiceHost Logging(Action<LoggingConfiguration> configure)
        {
            configure(new LoggingConfiguration());

            return this;
        }

        /// <summary>
        /// Configure service finder of the service host.
        /// </summary>
        /// <param name="configure">
        /// The configure action.
        /// </param>
        /// <returns>
        /// The configured service host.
        /// </returns>
        public ServiceHost ServiceFinder(Action<ServiceFinderConfiguration> configure)
        {
            var configuration = new ServiceFinderConfiguration();

            configure(configuration);

            _serviceFinder = configuration.ServiceFinder;

            return this;
        }

        /// <summary>
        /// Run the service host.
        /// </summary>
        /// <remarks>
        /// Blocks until all services are stopped.
        /// </remarks>
        public void Run()
        {
            /**
             * Command line parameters
             * /host:daemon - Run the Daemon host
             * /host:service - Run the Windows Service host
             * /host:console - Run the Console host
             * 
             * /daemon - Alias for /host:daemon
             * /service - Alias for /host:service
             * /console - Alias for /host:console
             * 
             * ServiceHost.Run(container);
             */

            SetDefaultValues();

            var services = FindServices();

            InitializeServices(services);

            var host = CreateHost();

            _log.Info("Using the {0} host", host.GetType());

            host.Starting += (sender, args) =>
            {
                _log.Info("Starting all services");

                foreach (var service in services)
                {
                    _log.Debug(@"Starting the ""{0}"" service", service.GetType());

                    service.Start();

                    _log.Debug(@"Started the ""{0}"" service", service.GetType());
                }
            };

            host.Stopped += (sender, args) =>
            {
                _log.Info("Stopping all services");

                for (var i = services.Count - 1; i >= 0; i--)
                {
                    var service = services[i];

                    _log.Debug(@"Stopping the ""{0}"" service", service.GetType());

                    service.Stop();

                    _log.Debug(@"Stopped the ""{0}"" service", service.GetType());
                }
            };

            host.Run();
        }

        /// <summary>
        /// Create the correct service host according to the running environment.
        /// </summary>
        /// <returns>
        /// The created service host.
        /// </returns>
        private static IHost CreateHost()
        {
            IHost host = null;

            if (IsHosted())
            {
                host = new HostedServiceHost();
            }
            else if (ShouldRunAsDaemonOrService())
            {
                if (IsWindows())
                {
                    host = new WindowsServiceHost();
                }

                if (IsUnixOrMacOsx())
                {
                    host = new DaemonHost();
                }
            }
            else
            {
                host = new ConsoleHost();
            }

            return host;
        }

        /// <summary>
        /// Determine if the service host should run as a daemon or service.
        /// </summary>
        /// <returns>
        /// <c>true</c> if the service should run as a daemon or service; otherwise, <c>false</c>.
        /// </returns>
        private static bool ShouldRunAsDaemonOrService()
        {
            var daemonArguments = new[] { "-d", "--daemon", "/d", "/daemon", "-s", "--service", "/s", "/service" };

            var arguments = Environment.GetCommandLineArgs();

            return daemonArguments.Any(arguments.Contains);
        }

        /// <summary>
        /// Determine if the service is hosted on the host server.
        /// </summary>
        /// <returns>
        /// <c>true</c> if the service is hosted on the host server; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsHosted()
        {
            return HostedServiceHostCommunicator.Instance != null;
        }

        /// <summary>
        /// Determine if the running OS is of the Windows NT family (Windows NT, Windows 2000, Windows XP, etc.).
        /// </summary>
        /// <returns>
        /// <c>true</c> if the running OS is of the Windows NT family; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsWindows()
        {
            return Environment.OSVersion.Platform == PlatformID.Win32NT;
        }

        /// <summary>
        /// Determine if the running OS is Unix or Mac OS X.
        /// </summary>
        /// <returns>
        /// <c>true</c> if the running OS is Unix or Mac OS X; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsUnixOrMacOsx()
        {
            return Environment.OSVersion.Platform == PlatformID.MacOSX || Environment.OSVersion.Platform == PlatformID.Unix;
        }

        /// <summary>
        /// Initialize all services.
        /// </summary>
        /// <param name="services">
        /// The services.
        /// </param>
        private static void InitializeServices(IEnumerable<IService> services)
        {
            foreach (var service in services.OfType<IRequireInitialization>())
            {
                service.Initialize();
            }
        }

        /// <summary>
        /// Set the default values unless they are already set.
        /// </summary>
        private void SetDefaultValues()
        {
            if (_containerAdapter == null)
            {
                throw new InvalidOperationException(@"Cannot start with a null container adapter!");
            }

            if (_serviceFinder == null)
            {
                _serviceFinder = new NullServiceFinder();
            }
        }

        /// <summary>
        /// Find and create all services.
        /// </summary>
        /// <returns>
        /// The found and created services.
        /// </returns>
        private List<IService> FindServices()
        {
            var serviceTypes = _serviceFinder.FindServices();

            var serviceSorter = new ServiceSorter();

            var sortedServiceTypes = serviceSorter.Sort(serviceTypes);

            var services = sortedServiceTypes.Select(
                serviceType =>
                {
                    var service = _containerAdapter.GetServiceInstance(serviceType);

                    _containerAdapter.RegisterServiceInstance(serviceType, service);

                    return service;
                }).ToList();

            return services;
        }
    }
}