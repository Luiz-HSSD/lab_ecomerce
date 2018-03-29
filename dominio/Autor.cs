using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    class Autor : EntidadeDominio
    {
        private string nome;

        

        public Autor() : base(0, Convert.ToDateTime("01/01/1995 03:30"))
        {
            Nome = "";
        }
        public string Nome
        {
            get { return nome; }
            set { nome = value; }
        }
    }
}
