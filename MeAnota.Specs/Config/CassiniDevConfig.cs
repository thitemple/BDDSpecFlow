using CassiniDev;
using System.Text.RegularExpressions;
using TechTalk.SpecFlow;

namespace MeAnota.Specs.Config
{
    [Binding]
    public class CassiniDevConfig
    {
        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks

        private static CassiniDev.CassiniDevServer _server;
        public static int PortaServidor { get; private set; }

        [BeforeTestRun]
        public static void BeforeScenario()
        {
            _server = new CassiniDev.CassiniDevServer();
            _server.StartServer(@"..\..\..\MeAnota");
            PortaServidor = _server.Porta();
        }

        [AfterTestRun]
        public static void AfterScenario()
        {
            _server.StopServer();
        }
    }

    internal static class CassiniDevExtensions
    {
        public static int Porta(this CassiniDevServer server)
        {
            var match = Regex.Match(server.RootUrl, @":(?<porta>\d+)\/?");
            if (!match.Success) return 0;

            return int.Parse(match.Groups["porta"].Value);
        }
    }
}
