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
        private Frete acalcular;
        private cResultado resu;
        private CalcPrecoPrazoWS cal = new CalcPrecoPrazoWS();

        public string processar(EntidadeDominio entidade)
        {
            try
            {
                acalcular = (Frete)entidade;
                Produto produto = acalcular.Item;
                resu = cal.CalcPrecoPrazo("", "", "40010 , 40045 , 40215 , 40290 , 41106", cep_origem, acalcular.Destino.Cep, produto.Formato.Peso, 1, produto.Formato.Comprimento, produto.Formato.Altura, produto.Formato.Largura, produto.Formato.Diametro, "N", 'N', "S");
                acalcular.Valor = Convert.ToDouble(resu.Servicos.ElementAt(0).Valor);
                acalcular.Prazo = Convert.ToInt32(resu.Servicos.ElementAt(0).PrazoEntrega);
                return "Consultado com succeso";
            }
            catch
            {
                return resu.Servicos.ElementAt(0).MsgErro;
            } 
        }
    }
}
