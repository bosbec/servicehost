// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Bosbec AB">
//   Copyright © Bosbec AB 2014
// </copyright>
// <summary>
//   Defines the Program type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Bosbec.ServiceHost.Host
{
    /// <summary>
    /// Defines the Program type.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Start hosting services.
        /// </summary>
        /// <param name="args">
        /// The arguments.
        /// </param>
        public static void Main(string[] args)
        {
            /**
             * --packages-source=file:///opt/service-host-packages
             * --packages-source=s3://internal/bucket-name
             * 
             * --packages-keep-versions=10
             * --packages-directory=/opt/service-host/packages
             * --services-directory=/opt/service-host/services
             * --disable-http
             * --http-listen-url=http://localhost:8080/
             */
        }
    }
}