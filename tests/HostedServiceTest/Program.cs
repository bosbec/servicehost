namespace Bosbec.ServiceHost.HostedServiceTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ServiceHost.Create(new DefaultContainerAdapter())
                .Logging(x => x.ColoredConsole())
                .ServiceFinder(x => x.ServicesInAssemblyOfType<Program>())
                .Run();
        }
    }
}