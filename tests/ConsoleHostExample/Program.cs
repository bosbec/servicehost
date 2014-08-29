namespace Bosbec.ServiceHost.ConsoleHostExample
{
    using Bosbec.ServiceHost;
    using Bosbec.ServiceHost.StructureMap;

    public class Program
    {
        public static void Main(string[] args)
        {
            ServiceHost.Create(new StructureMapContainerAdapter())
                .Logging(x => x.File("log.txt"))
                .ServiceFinder(x => x.ServicesInAssemblyOfType<Program>())
                .Run();
        }
    }
}