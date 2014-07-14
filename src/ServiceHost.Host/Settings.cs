// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Settings.cs" company="Bosbec AB">
//   Copyright © Bosbec AB 2014
// </copyright>
// <summary>
//   Defines the Settings type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Bosbec.ServiceHost.Host
{
    using Bosbec.ServiceHost.Host.Services.Packages;

    /// <summary>
    /// Defines the Settings type.
    /// </summary>
    public class Settings
    {
        /// <summary>
        /// Gets or sets the packages.
        /// </summary>
        public PackagesServiceSettings Packages { get; set; }
    }
}