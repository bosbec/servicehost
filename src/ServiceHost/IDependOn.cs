// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDependOn.cs" company="Bosbec AB">
//   Copyright © Bosbec AB 2014
// </copyright>
// <summary>
//   Defines the IDependOn type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Bosbec.ServiceHost
{
    /// <summary>
    /// Defines the IDependOn type.
    /// </summary>
    /// <typeparam name="TService">
    /// The service type this type depends on.
    /// </typeparam>
    public interface IDependOn<TService> where TService : IService
    {
    }
}