using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Pedido
    {
        private string status;
        private ocorencia ocorencia;
        private Venda venda;

        public Venda Venda
        {
            get { return venda; }
            set { venda = value; }
        }

        public ocorencia Ocorencia
        {
            get { return ocorencia; }
            set { ocorencia = value; }
        }

        public string Status
        {
            get { return status; }
            set { status = value; }
        }

    }
}
