using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Grupo_Precificacao : EntidadeDominio
    {
        private string _nome;

        private Double _porcentagem;


        public Grupo_Precificacao() : base(0, Convert.ToDateTime("01/01/1995 03:30"))
        {
            Nome = "";
            _porcentagem = 0;
        }

        public Double Porcentagem
        {
            get { return _porcentagem; }
            set { _porcentagem = value; }
        }


        public string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }

    }
}
