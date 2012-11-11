using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace MeAnota.Specs.Config
{
    [Binding]
    public class SeleniumConfig
    {
        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks
        private static Process _seleniumServer;

        [BeforeTestRun]
        public static void BeforeScenario()
        {
            var pathToSelenium = Path.GetFullPath(@"..\..\..\Tools\selenium-server-standalone-2.25.0.jar");
            var argumentos = string.Format(@"-jar ""{0}""", pathToSelenium);
            var processInfo = new ProcessStartInfo
            {
                FileName = pathToSelenium,
                Arguments = argumentos,
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden
            };
            _seleniumServer = new Process { StartInfo = processInfo };
            var iniciado = _seleniumServer.Start();
        }

        [AfterTestRun]
        public static void AfterScenario()
        {
            _seleniumServer.Close();
        }
    }
}
