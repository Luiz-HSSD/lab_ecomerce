using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Ranking:EntidadeDominio
    {
        private double _Montante;

        private char _g_preco;

        public Ranking() : base(0, Convert.ToDateTime("01/01/1995 03:30"))
        {
            _Montante = 0;
            _g_preco = 'A';
        }

        public char g_preco
        {
            get { return _g_preco; }
            set { _g_preco = value; }
        }

        public double Montante
        {
            get { return _Montante; }
            set { _Montante = value; }
        }

    }
}
