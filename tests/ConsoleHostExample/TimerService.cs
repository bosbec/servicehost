namespace Bosbec.ServiceHost.ConsoleHostExample
{
    using System;
    using System.Timers;

    public class TimerService : IService, IRequireInitialization
    {
        private Timer _timer;

        public event EventHandler Tick;

        public void Start()
        {
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }

        public void Initliaze()
        {
            _timer = new Timer(1000);
            _timer.Elapsed += (sender, args) => OnTick();
            _timer.AutoReset = true;
            _timer.Enabled = true;
        }

        protected virtual void OnTick()
        {
            var handler = Tick;

            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
    }
}