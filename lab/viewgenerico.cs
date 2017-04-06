using core.aplicacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using lab.Command;

namespace lab
{
    public class viewgenerico : System.Web.UI.Page
    {
        protected  Resultado res { get; set; } = new Resultado();
        protected  Dictionary<string, ICommand.ICommand> commands { get; set; } = new Dictionary<string, ICommand.ICommand>();

        public viewgenerico()
        {
            commands.Add("SALVAR", new SalvarCommand());
            commands.Add("ALTERAR", new AlterarCommand());
            commands.Add("EXCLUIR", new ExcluirCommand());
            commands.Add("CONSULTAR", new ConsultarCommand());
            commands.Add("VISUALIZAR", new VisualizarCommand());
            commands.Add("WS", new WebServiceCommand());
        }
               
    }
}