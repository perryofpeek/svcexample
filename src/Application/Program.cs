using System;
using Library;
using Topshelf;
using log4net;

namespace Application
{
    class Program
    {
        private static ILog log;

        public static void Main(string[] args)
        {
            try
            {
                log = LogManager.GetLogger("log");
                log4net.Config.XmlConfigurator.Configure();
                var applicationConfiguration = ReadApplicationConfigurationFromAppConfig();
                var serviceConfiguration = ReadServiceConfigurationFromAppConfig();                
                RunApplication(CreateTaskManager(applicationConfiguration), serviceConfiguration);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                Console.WriteLine(ex.Message);
            }
        }

        private static TaskManager CreateTaskManager(ApplicationSection configuration)
        {
            const int sleeptimeLoopTime = 100;
            ISleep sleep = new Sleeper();
            IStop stop = new Stop();
            Service service = new Service(configuration.Port);
            return new TaskManager(log, sleeptimeLoopTime, sleep, stop, service);
        }

        private static ApplicationSection ReadApplicationConfigurationFromAppConfig()
        {
            return (ApplicationSection)System.Configuration.ConfigurationManager.GetSection(ApplicationSection.SectionName);
        }

        private static ServiceSection ReadServiceConfigurationFromAppConfig()
        {
            return (ServiceSection)System.Configuration.ConfigurationManager.GetSection(ServiceSection.SectionName);
        }

        private static void RunApplication(TaskManager taskManager, ServiceSection serviceConfiguration)
        {
            HostFactory.Run(
                x =>
                {
                    x.Service<ApplicationController>(
                        s =>
                        {
                            s.ConstructUsing(name => new ApplicationController(taskManager, log));
                            s.WhenStarted(tc => tc.Start());
                            s.WhenStopped(tc => tc.Stop());
                        });
                    x.RunAsLocalSystem();
                    x.SetDescription(serviceConfiguration.Description);
                    x.SetDisplayName(serviceConfiguration.Name);
                    x.SetServiceName(serviceConfiguration.Name);
                });
        }
    }
}