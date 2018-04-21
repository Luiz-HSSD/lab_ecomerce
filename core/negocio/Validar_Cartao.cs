using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using core.core;
using dominio;

namespace core.negocio
{
    public class Validar_Cartao : IStrategy
    {
        public string processar(EntidadeDominio entidade)
        {
            Cartao_Credito cc = (Cartao_Credito)entidade;
            if(!string.IsNullOrEmpty(cc.Numero))
            {
                if (cc.Numero.Length != 16)
                    return "numero do cartão não pode ter tamanho diferente de 16 digitos";
                if (!string.IsNullOrEmpty(cc.Nome_Titular))
                {
                    if (cc.Nome_Titular.Length < 6)
                        return "nome do titular não pode ter tamanho menor de 6 caracteteres";
                    else if (cc.Nome_Titular.All(char.IsDigit))
                        return "nome do titular não pode conter numeros";

                }
                else
                {
                    return "nome do titular não pode ser vazio";
                }
            }
            else
            {
                return "numero do cartão não pode ser vazio";
            }
            return null;

        }
    }
}
