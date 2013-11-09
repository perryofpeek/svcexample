using System.Threading;
using System.Web.Http;
using System.Web.Http.SelfHost;

namespace Library
{
    public class Service
    {
        private readonly int _port;

        public Service(int port)
        {
            _port = port;
            running = false;
        }

        private bool running;


        public void Start()
        {
            var thread = new Thread(() => StartService());
            thread.Start();
        }

        private void StartService()
        {
            var config = new HttpSelfHostConfiguration(string.Format("http://localhost:{0}", _port));

            config.Routes.MapHttpRoute("API Default", "api/{controller}/{id}", new { id = RouteParameter.Optional });

            using (var server = new HttpSelfHostServer(config))
            {
                running = true;
                server.OpenAsync().Wait();
                while (running)
                {
                    Thread.Sleep(500);
                }
            }
        }

        public void Stop()
        {
            running = false;
        }
    }
}