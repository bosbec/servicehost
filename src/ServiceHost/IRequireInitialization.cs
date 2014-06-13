// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRequireInitialization.cs" company="Bosbec AB">
//   Copyright © Bosbec AB 2014
// </copyright>
// <summary>
//   Defines the IRequireInitialization type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Bosbec.ServiceHost
{
    /// <summary>
    /// Defines the IRequireInitialization type.
    /// </summary>
    public interface IRequireInitialization
    {
        /// <summary>
        /// Perform initialization logic.
        /// </summary>
        void Initliaze();
    }
}