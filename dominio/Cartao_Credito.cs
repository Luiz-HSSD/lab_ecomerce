using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Cartao_Credito : EntidadeDominio
    {
        private string numero;
        private string nome_titular;
        private Endereco endereco_cartao;
        private int ccv;
        private string validade;
        private Bandeira_Cartao bandeira;

        public Bandeira_Cartao Bandeira
        {
            get { return bandeira; }
            set { bandeira = value; }
        }


        public Cartao_Credito() : base(0, Convert.ToDateTime("01/01/1995 03:30"))
        {
            Numero = "0";
            Nome_Titular = "";
            Endereco_Cartao = new Endereco();
            CCV = 0;
            Validade = "";

        }

        public string Validade
        {
            get { return validade; }
            set { validade = value; }
        }


        public int CCV
        {
            get { return ccv; }
            set { ccv = value; }
        }

        public string Numero
        {
            get { return numero; }
            set { numero = value; }
        }


        public string Nome_Titular
        {
            get { return nome_titular; }
            set { nome_titular = value; }
        }

        
        public Endereco Endereco_Cartao
        {
            get { return endereco_cartao; }
            set { endereco_cartao = value; }
        }


    }
}
