using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace core.DAO
{
    public class VendaDao : AbstractDAO
    {
        public VendaDao() : base("vendas", "id_ven")
        {

        }
        public override void alterar(EntidadeDominio entidade)
        {
            throw new NotImplementedException();
        }

        public override List<EntidadeDominio> consultar(EntidadeDominio entidade)
        {
            throw new NotImplementedException();
        }

        public override void salvar(EntidadeDominio entidade)
        {
            throw new NotImplementedException();
        }
    }
}
