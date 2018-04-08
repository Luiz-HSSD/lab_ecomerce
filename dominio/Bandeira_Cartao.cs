using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Bandeira_Cartao : EntidadeDominio
    {
        private string nome;
        private int tipo;

        public Bandeira_Cartao() : base(0, Convert.ToDateTime("01/01/1995 03:30"))
        {
            Nome = "";
            Tipo = 0;

        }
        
        public int Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }


        public string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

    }
}
