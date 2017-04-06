using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Item_venda
    {
        private int qtd;
        private double valor;
        private Produto pro;
        private double valor_unidade;

        public double Valor_Unidade
        {
            get { return valor_unidade; }
            set { valor_unidade = value; }
        }


        public Produto Pro
        {
            get { return pro; }
            set { pro = value; }
        }

        public double Valor
        {
            get { return valor; }
            set { valor = value; }
        }

        public int Qtd
        {
            get { return qtd; }
            set { qtd = value; }
        }

    }
}
