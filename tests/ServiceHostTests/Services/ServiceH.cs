namespace Bosbec.ServiceHost.ServiceHostTests.Services
{
    public class ServiceH : IService, IDependOn<ServiceF>
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