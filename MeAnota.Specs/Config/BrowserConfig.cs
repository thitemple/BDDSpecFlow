using Coypu;
using MeAnota.Specs.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace MeAnota.Specs.Config
{
    [Binding]
    public class StepsBase
    {
        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks
        protected BrowserSession Browser;
   
        [BeforeScenario]
        public void BeforeScenario()
        {
            BrowserSession browserAtual;
            if (ScenarioContext.Current.TryGetValue<BrowserSession>(out browserAtual))
            {
                Browser = browserAtual;
                return;
            }
            var config = new SessionConfiguration { Port = CassiniDevConfig.PortaServidor, 
                AppHost = "localhost",
                Browser = Coypu.Drivers.Browser.HtmlUnitWithJavaScript,
                Timeout = TimeSpan.FromSeconds(5),
                RetryInterval = TimeSpan.FromSeconds(0.1)};
            Browser = new BrowserSession(config);
            ScenarioContext.Current.Set<BrowserSession>(Browser);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            Browser.Dispose();
        }
    }
}
