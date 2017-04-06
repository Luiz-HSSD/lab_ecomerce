using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using core.core;
using dominio;
using BoletoNet;
using core.Utils;

namespace core.negocio
{
    class gerar_boleto : IStrategy
    {
        public string processar(EntidadeDominio entidade)
        {
            Venda gerarboleto = (Venda)entidade;
            BoletoLuizin LuizinBoleto = new BoletoLuizin(gerarboleto.Total);


            return null;
        }
    }
}
