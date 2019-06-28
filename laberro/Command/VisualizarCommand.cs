using core.aplicacao;
using dominio;

namespace lab.Command
{
    public class VisualizarCommand : AbstractCommand
    {
        public override Resultado execute(EntidadeDominio entidade)
        {
            return fachada.visualizar(entidade);
        }
    }
}