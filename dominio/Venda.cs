using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Venda:EntidadeDominio
    {
        private List<Item_venda> produtos;
        private Cliente cliente;
        private Frete frete;
        private decimal total;

        public Venda(int id, DateTime DataHora) : base(id, DataHora)
        {
        }

        public decimal Total
        {
            get { return total; }
            set { total = value; }
        }

        public Frete Frete
        {
            get { return frete; }
            set { frete = value; }
        }

        public Cliente Cliente_prop
        {
            get { return cliente; }
            set { cliente = value; }
        }


        public List<Item_venda> Produtos
        {
            get { return produtos; }
            set { produtos = value; }
        }

    }
}
