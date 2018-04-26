using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using core.core;
using dominio;

namespace core.negocio
{
    public class Validar_end_cli : IStrategy
    {
        public string processar(EntidadeDominio entidade)
        {
            Cliente cli = (Cliente)entidade;
            int i = 0, j = 0;
            foreach(Endereco end in cli.Enderecos)
            {
                if (end.Tipo == 1)
                {
                    i++;
                }
                else if (end.Tipo == 2)
                {
                    j++;
                }
                else
                {
                    return "exite tipo invalido";
                }
            }
            if(i==0)
            {
                return "necessario ao menos um endereco de cobrança";
            }
            if(j == 0)
            {
                return "necessario ao menos um endereco de entrega";
            }
            return null;
        }
    }
}
