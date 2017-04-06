using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoletoNet;

namespace core.Utils
{
    public class BoletoLuizin:BoletoBancario
    {
        private static DateTime dataVencimento;

        public static DateTime DataVencimento
        {
            get { return dataVencimento = DateTime.Now.AddDays(5); }
        }

        public BoletoLuizin(Decimal ValorBoleto)
        {
            Boleto.CodigoBarra.PreencheValores(104,9,dataVencimento.Ticks,ValorBoleto.ToString(),"00");
            Boleto.Aceite = "N";
            Boleto.DataVencimento = DataVencimento;
            Boleto.ValorBoleto = ValorBoleto;
            Boleto.ValorCobrado = ValorBoleto;
            Boleto.Carteira = "8";
            Boleto.ValorDesconto = 0;
            Banco.Codigo = 104;
            Banco.Cedente.ContaBancaria.OperacaConta = "013";
            Banco.Cedente.ContaBancaria.Agencia = "Poá - SP";
            Banco.Cedente.ContaBancaria.DigitoAgencia = "0908";
            Banco.Cedente.ContaBancaria.DigitoConta = "00101417-8";
            Banco.Cedente.ContaBancaria.Conta = "Poupança";
            Boleto.Valida();
        }
    }
}
