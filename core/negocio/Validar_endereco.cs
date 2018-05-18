using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using core.core;
using dominio;
using core.DAO;

namespace core.negocio
{
    public class Validar_endereco : IStrategy
    {
        public string processar(EntidadeDominio entidade)
        {
            Endereco end = (Endereco)entidade;
            EnderecoDAO end_dao = new EnderecoDAO();
            List<EntidadeDominio> res= end_dao.consultar(end);
            if (res.Count > 0)
            {
                end.Id = ((Endereco)res.ElementAt(0)).Id;
                return "já existe o endereco";
            }
            return null;
        }
    }
}