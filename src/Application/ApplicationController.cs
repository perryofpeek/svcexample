using System;
using System.Threading;
using Library;
using log4net;

namespace Application
{
    public class ApplicationController
    {
        private readonly TaskManager taskManager;

        private static ILog log;

        private Thread workerThread;

        public ApplicationController(TaskManager taskManager, ILog log)
        {
            this.taskManager = taskManager;
            ApplicationController.log = log;
        }

        public void Start()
        {
            try
            {
                workerThread = new Thread(taskManager.Start);
                workerThread.Start();
            }
            catch (Exception ex)
            {
                log.Error(ex);
                Console.WriteLine(ex.Message);
            }
        }

        public void Stop()
        {
            try
            {
                taskManager.Stop();
                workerThread.Abort();
            }
            catch (Exception ex)
            {
                log.Error(ex);
                Console.WriteLine(ex.Message);
            }
        }
    }
}