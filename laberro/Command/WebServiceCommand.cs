using core.aplicacao;
using dominio;

namespace lab.Command
{
    public class WebServiceCommand : AbstractCommand
    {
        public override Resultado execute(EntidadeDominio entidade)
        {
            return fachada.WebService(entidade);
        }
    }
}