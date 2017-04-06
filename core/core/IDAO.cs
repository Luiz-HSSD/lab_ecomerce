using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;
namespace core.core
{
    public interface IDAO
    {
         void salvar(EntidadeDominio entidade);
         void alterar(EntidadeDominio entidade);
         void excluir(EntidadeDominio entidade);
         List<EntidadeDominio> consultar(EntidadeDominio entidade);

    }
}
