using core.aplicacao;
using dominio;
namespace lab.ICommand
{
    public interface ICommand
    {
         Resultado execute(EntidadeDominio entidade) ;
    }
}
