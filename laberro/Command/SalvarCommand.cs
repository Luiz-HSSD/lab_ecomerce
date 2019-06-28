using core.aplicacao;
using dominio;
namespace lab.Command
{
    public class SalvarCommand : AbstractCommand
    {
        public override Resultado execute(EntidadeDominio entidade)
        {
            return fachada.salvar(entidade);
        }
    }
}