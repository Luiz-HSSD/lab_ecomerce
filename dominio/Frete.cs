using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Frete : EntidadeDominio
    {
        private Endereco destino;
        private double valor;
        private int prazo;

        private Produto _item;
                    
        public Produto Item
        {
            get { return _item; }
            set { _item = value; }
        }


        public int Prazo
        {
            get { return prazo; }
            set { prazo = value; }
        }

        public double Valor
        {
            get { return valor; }
            set { valor = value; }
        }

        public Endereco Destino
        {
            get { return destino; }
            set { destino = value; }
        }

        public Frete() : base(0, Convert.ToDateTime("01/01/1995 03:30"))
        {
            prazo = 0;
            valor = 0;
            destino = new Endereco();
            _item = new Produto();
        }
        
    }
}
