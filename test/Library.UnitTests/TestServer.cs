using System.Threading;
using NUnit.Framework;

namespace Library.UnitTests
{
    [TestFixture]
    public class TestServer
    {
        [Test]
        public void StartServer()
        {
            var hosting = new Service(9090);
            hosting.Start();
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(200);
            }
            hosting.Stop();
        }
    }
}