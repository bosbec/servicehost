namespace Bosbec.ServiceHost.ServiceHostTests.Services
{
    using Bosbec.ServiceHost;

    public class ServiceD : IService, IDependOn<ServiceA>
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