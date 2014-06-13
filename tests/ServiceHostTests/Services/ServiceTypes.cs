namespace Bosbec.ServiceHost.ServiceHostTests.Services
{
    using System;

    public class ServiceTypes
    {
        public static readonly Type[] WithoutCyclicDependencies =
        {
            typeof(ServiceA),
            typeof(ServiceB),
            typeof(ServiceC),
            typeof(ServiceD),
            typeof(ServiceE)
        };

        public static readonly Type[] WithCyclicDependencies =
        {
            typeof(ServiceA),
            typeof(ServiceB),
            typeof(ServiceC),
            typeof(ServiceD),
            typeof(ServiceE),
            typeof(ServiceF),
            typeof(ServiceH)
        };
    }
}