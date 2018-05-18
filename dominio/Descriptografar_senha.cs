using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    class Descriptografar_senha : EntidadeDominio
    {
        private string _senha;

        public string Senha
        {
            get { return _senha; }
            set { _senha = value; }
        }

        public Descriptografar_senha() : base(0, Convert.ToDateTime("01/01/1995 03:30"))
        {
        }
    }
}
