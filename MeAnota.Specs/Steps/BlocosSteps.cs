using MeAnota.Models;
using MeAnota.Specs.Config;
using System;
using TechTalk.SpecFlow;
using WebMatrix.WebData;
using System.Linq;
using TechTalk.SpecFlow.Assist;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MeAnota.Specs.Steps
{
    [Binding]
    public class BlocosSteps : StepsBase
    {
        private string password = "passw0rd";
        [Given(@"que existe um usuário ""(.*)""")]
        public void DadoQueExisteUmUsuario(string email)
        {
            if (!WebSecurity.UserExists(email))
                WebSecurity.CreateUserAndAccount(email, password);
        }
        
        [Given(@"que o usuário ""(.*)"" está logado no site")]
        public void DadoQueOUsuarioEstaLogadoNoSite(string email)
        {
            QuandoEuVisito("/Account/Login");
            Browser.FillIn("UserName").With(email);
            Browser.FillIn("Password").With(password);
            Browser.ClickButton("Entrar");
        }
        
        [Given(@"que eu tenho os seguintes blocos cadastrados")]
        public void DadoQueEuTenhoOsSeguintesBlocosCadastrados(Table table)
        {
            using (var db = new MeAnotaContext())
            {
                foreach (var row in table.Rows)
                {
                    var email = row["Email"];
                    var bloco = new Bloco
                    {
                        Nome = row["Nome"],
                        Proprietario = db.Usuarios.FirstOrDefault(u => u.Email == email)
                    };
                    db.Blocos.Add(bloco);
                    db.SaveChanges();
                }
            }
        }
        
        [When(@"eu visito ""(.*)""")]
        public void QuandoEuVisito(string url)
        {
            Browser.Visit(url);
        }
        
        [Then(@"eu vejo")]
        public void EntaoEuVejo(Table table)
        {
            var blocos = table.CreateSet<Bloco>();
            foreach (var bloco in blocos)
            {
                Assert.IsTrue(Browser.HasContent(bloco.Nome), 
                    "Texto não encontrado na página: {0}", bloco.Nome);
            }
        }

        [Then(@"eu não vejo")]
        public void EntaoEuNaoVejo(Table table)
        {
            var bloco = table.CreateInstance<Bloco>();
            Assert.IsTrue(Browser.HasNoContent(bloco.Nome), "Texto encontrado na página {0}", bloco.Nome);
        }

        [When(@"eu clico em ""(.*)""")]
        public void QuandoEuClicoEm(string link)
        {
            Browser.ClickLink(link);
        }

        [When(@"eu preencho")]
        public void QuandoEuPreencho(Table table)
        {
            var bloco = table.CreateInstance<Bloco>();
            Browser.FillIn("Nome").With(bloco.Nome);
        }

        [When(@"eu clico no botão ""(.*)""")]
        public void QuandoEuClicoNoBotao(string textoBotao)
        {
            Browser.ClickButton(textoBotao);
        }

        [Then(@"eu vejo ""(.*)""")]
        public void EntaoEuVejo(string texto)
        {
            Assert.IsTrue(Browser.HasContent(texto), 
                "Texto não encontrado: {0}", texto);
        }

        [When(@"eu confirmo a mensagem de cancelamento ""(.*)""")]
        public void QuandoEuConfirmoAMensagemDeCancelamento(string texto)
        {
            var temMensagem = Browser.HasContent(texto);
            Assert.IsTrue(temMensagem, "Não encontrou a mensagem {0}", texto);
            if(temMensagem)
                Browser.ClickButton("Ok");
        }

        [Then(@"eu não vejo ""(.*)""")]
        public void EntaoEuNaoVejo(string texto)
        {
            Assert.IsTrue(Browser.HasNoContent(texto), "Texto encontrado {0}", texto);
        }

    }
}
