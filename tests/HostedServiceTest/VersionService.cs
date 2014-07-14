namespace Bosbec.ServiceHost.HostedServiceTest
{
    using System;

    public class VersionService : IService
    {
        public void Start()
        {
            Console.WriteLine("Starting the Version service running version {0}", GetType().Assembly.GetName().Version);
        }

        public void Stop()
        {
            Console.WriteLine("Stopping the Version service running version {0}", GetType().Assembly.GetName().Version);
        }
    }
}