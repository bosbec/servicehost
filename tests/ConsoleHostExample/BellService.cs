namespace Bosbec.ServiceHost.ConsoleHostExample
{
    using System;

    public class BellService : IService, IDependOn<TimerService>
    {
        private readonly TimerService _timerService;

        public BellService(TimerService timerService)
        {
            _timerService = timerService;
        }

        public void Start()
        {
            _timerService.Tick += PrintMessage;
        }

        public void Stop()
        {
            _timerService.Tick -= PrintMessage;
        }

        private void PrintMessage(object sender, EventArgs arguments)
        {
            Console.WriteLine("Ding dong!");
        }
    }
}