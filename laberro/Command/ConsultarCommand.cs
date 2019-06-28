using dominio;
using core.aplicacao;

namespace lab.Command
{
    public class ConsultarCommand : AbstractCommand
    {
        public override Resultado execute(EntidadeDominio entidade)
        {
            return fachada.consultar(entidade);
        }
    }
}