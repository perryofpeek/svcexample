using log4net;

namespace Library
{
    public class TaskManager
    {
        private static ILog log;
  
        private readonly IStop stop;
        
        private readonly Service service;

        private readonly int sleepTime;

        private readonly ISleep sleep;        

        public TaskManager(ILog log, int sleepTime,ISleep sleep ,IStop stop, Service service)
        {
            TaskManager.log = log;
            this.stop = stop;
            this.service = service;
            this.sleepTime = sleepTime;
            this.sleep = sleep;            
        }

        public void Start()
        {
            log.Info("Starting service");
            service.Start();
            log.Info("Started service");
            while (!stop.ShouldStop())
            {                                
                sleep.Sleep(sleepTime);
            }
        }      

        public void Stop()
        {
            log.Info("Stopping service");
            service.Stop();
            log.Info("Stopped service");
        }
    }
}