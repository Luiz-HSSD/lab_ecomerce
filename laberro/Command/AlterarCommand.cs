using core.aplicacao;
using dominio;
namespace lab.Command
{
    public class AlterarCommand : AbstractCommand
    {
        public override Resultado execute(EntidadeDominio entidade)
        {
            return fachada.alterar(entidade);
        }
    }
}