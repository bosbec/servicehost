// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServiceFinderConfiguration.cs" company="Bosbec AB">
//   Copyright © Bosbec AB 2014
// </copyright>
// <summary>
//   Defines the ServiceFinderConfiguration type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Bosbec.ServiceHost.Services
{
    /// <summary>
    /// Defines the ServiceFinderConfiguration type.
    /// </summary>
    public class ServiceFinderConfiguration
    {
        /// <summary>
        /// Gets the service finder.
        /// </summary>
        public IServiceFinder ServiceFinder { get; private set; }

        /// <summary>
        /// Find services in assembly of type <typeparamref name="T" />.
        /// </summary>
        /// <typeparam name="T">
        /// The type whos assembly to find services in.
        /// </typeparam>
        public void ServicesInAssemblyOfType<T>()
        {
            Use(new AssemblyServiceFinder(typeof(T).Assembly));
        }

        /// <summary>
        /// Use the specified service finder.
        /// </summary>
        /// <param name="serviceFinder">
        /// The service finder.
        /// </param>
        public void Use(IServiceFinder serviceFinder)
        {
            ServiceFinder = serviceFinder;
        }
    }
}