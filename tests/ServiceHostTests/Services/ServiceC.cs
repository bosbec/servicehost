namespace Bosbec.ServiceHost.ServiceHostTests.Services
{
    using Bosbec.ServiceHost;

    public class ServiceC : IService, IDependOn<ServiceA>, IDependOn<ServiceB>
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