namespace Bosbec.ServiceHost.ServiceHostTests.Services
{
    public class ServiceF : IService, IDependOn<ServiceH>
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