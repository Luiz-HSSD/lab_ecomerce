using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using core.core;
using dominio;

namespace core.negocio
{
    class transfomar_formato : IStrategy
    {
        public string processar(EntidadeDominio entidade)
        {
            int  i=0, cont = 1;
            Formato_Produto formato = ((Produto)entidade).Formato;
            string cc = formato.Dimensoes;
            do {
                if (cc.Substring(i + 1, 2) == "cm")
                {

                    switch (cont)
                    {
                        case 1:
                            formato.Comprimento = Convert.ToDecimal(cc.Substring(0, i + 1));
                            cont++;
                            break;
                        case 2:
                            formato.Altura = Convert.ToDecimal(cc.Substring(0, i + 1));
                            cont++;
                            break;
                        case 3:
                            formato.Largura = Convert.ToDecimal(cc.Substring(0, i + 1));
                            cont++;
                            break;
                        default:
                            formato.Diametro = Convert.ToDecimal(cc.Substring(0, i + 1));
                            cont++;
                            break;
                    }
                    i++;
                    cc = cc.Substring(i + 2, cc.Length - (i + 2));
                    i = 0;
                }
                else
                {
                    i++;
                }
            } while (cont < 3);
            ((Produto)entidade).Formato = formato;
            return null;
        }
    }
}
