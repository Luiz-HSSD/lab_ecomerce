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
    public class ValidarCpf : IStrategy
    {
        public string processar(EntidadeDominio entidade)
        {
            Cliente cli= (Cliente)entidade;
            string cpf = cli.Cpf;
            int i = 0, j = 0, somatorio = 0;
            try
            {
                for (j = 9; j < 11; j++)
                {
                    for (i = j; i > 0; i--)
                    {
                        somatorio += (i + 1) * (Convert.ToInt32(cpf.Substring(j - i, 1)));
                        if (i == 1)
                        {
                            somatorio = (somatorio * 10) % (11);
                            if (somatorio == 10 || somatorio == 10) somatorio = 0;
                            if (Convert.ToInt32(cpf.Substring(j, 1)) != somatorio)
                                return "cpf invalido";
                        }


                    }
                    somatorio = 0;
                }
                ClienteDAO daao = new ClienteDAO();
                List<EntidadeDominio> cliente = daao.consultar(entidade);
                for (i = 0; i < cliente.Count; i++)
                {
                    if (((Cliente)cliente.ElementAt(i)).Cpf == cpf) return "esse cpf já existe no banco";
                }
                return null;
                
            }
            catch(Exception e)
            {
                return "cpf não está no formato certo";
            }
        }
    }
}

