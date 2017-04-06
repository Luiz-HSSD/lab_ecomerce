using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class ocorencia
    {
        private string tipo;
        private string descricao;

        public string Descricao
        {
            get { return descricao; }
            set { descricao = value; }
        }

        public  string Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }

    }
}
