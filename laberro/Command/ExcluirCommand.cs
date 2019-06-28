using core.aplicacao;
using dominio;

namespace lab.Command
{
    public class ExcluirCommand : AbstractCommand
    {
        public override Resultado execute(EntidadeDominio entidade)
        {
            return fachada.excluir(entidade);
        }
    }
}