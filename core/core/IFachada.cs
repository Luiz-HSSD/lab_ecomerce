using dominio;
using core.aplicacao;

namespace core.core
{
    public interface IFachada
    {
         Resultado salvar(EntidadeDominio entidade);
         Resultado alterar(EntidadeDominio entidade);
         Resultado excluir(EntidadeDominio entidade);
         Resultado consultar(EntidadeDominio entidade);
         Resultado visualizar(EntidadeDominio entidade);
         Resultado WebService(EntidadeDominio entidade);


    }
}
