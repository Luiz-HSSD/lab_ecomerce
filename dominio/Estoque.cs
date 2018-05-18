using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Estoque : EntidadeDominio
    {
        private double _custo;

        public double Custo
        {
            get { return _custo; }
            set { _custo = value; }
        }

        private int _qtd;

        public int qtd
        {
            get { return _qtd; }
            set { _qtd = value; }
        }
        private Livro _item;

        public Livro item
        {
            get { return _item; }
            set { _item = value; }
        }

        public Estoque(Produto item) : base(0, Convert.ToDateTime("01/01/1995 03:30"))
        {
            _qtd = 0;
            _custo = 0;
            if(item.GetType().Name=="Livro")
            this.item = (Livro)item;
        }
    }
}
