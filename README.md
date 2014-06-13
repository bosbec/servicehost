The ServiceHost framework
=========================

Getting started

1. Create a new Console project in Visual Studio.
2. Download the `Bosbec.ServiceHost` package in the NuGet package manager.
3. Change the content of your Main() method like this:

        ```csharp
        public static void Main()
        {
        	ServiceHost.Create(new StructureMapContainerAdapter())
        		.Logging(l => l.ColoredConsole())
        		.ServiceFinder(f => f.ServicesInAssemblyOfType<Program>())
        		.Run();
        }
        ```
4. Create a new C# file named `TimerService.cs` and paste this content:

        ```csharp
        public class TimerService : IService, IRequireInitialization
        {
        	private System.Timers.Timer _timer;

        	public void Initialize()
        	{
        		_timer = new System.Timers.Timer(1000)
        		{
        			AutoReset = true,
        			Enabled = true
        		};

        		_timer.Elapsed += (sender, args) => Console.WriteLine("Ding dong!");
	        }

        	public void Start()
        	{
        		_timer.Start();
        	}

        	public void Stop()
        	{
        		_timer.Stop();
        	}
        }
        ```
5. That's it, you're ready to Debug the application and let the magic happen.
You should see "Ding dong!" all over your screen along with some debugging
information from the framework itself.
