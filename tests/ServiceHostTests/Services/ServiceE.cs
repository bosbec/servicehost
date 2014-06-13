namespace Bosbec.ServiceHost.ServiceHostTests.Services
{
    using Bosbec.ServiceHost;

    public class ServiceE : IService, IDependOn<ServiceD>
    {
        public void Start()
        {
            throw new System.NotImplementedException();
        }

        public void Stop()
        {
            throw new System.NotImplementedException();
        }
    }
}