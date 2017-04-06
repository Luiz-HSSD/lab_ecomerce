using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using core.core;
using dominio;
using core.br.com.correios.ws;
namespace core.negocio
{
    public class Calcular_Frete : IStrategy
    {
        private static string cep_origem = "08563010";
        private Venda acalcular;
        private cResultado resu;
        private CalcPrecoPrazoWS cal = new CalcPrecoPrazoWS();

        public string processar(EntidadeDominio entidade)
        {
            try
            {
                acalcular = (Venda)entidade;
                for (int i = 0; i < acalcular.Produtos.Count; i++)
                {
                    Produto produto = acalcular.Produtos.ElementAt(i).Pro;
                    resu = cal.CalcPrecoPrazo("", "", "", cep_origem, acalcular.Frete.Destino.Cep, produto.Formato.Peso, 1, produto.Formato.Comprimento, produto.Formato.Altura, produto.Formato.Largura, produto.Formato.Diametro, "N", 'N', "S");
                }
                return null;
            }
            catch
            {
                return resu.Servicos.ElementAt(0).MsgErro;
            } 
        }
    }
}
