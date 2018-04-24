using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class EntidadeDominio : IEntidade
    {
        private int id;
        private DateTime dataHora;
        private char ative;

        private char _Operacao;

        



        public EntidadeDominio(int id, DateTime DataHora)
        {
            Ative = 'A';
            _Operacao = 'C';
            Id = 0;
            DataHora = Convert.ToDateTime("01/01/1995 03:30");
        }
        public char OPeracao
        {
            get { return _Operacao; }
            set { _Operacao = value; }
        }
        public char Ative
        {
            get { return ative; }
            set { ative = value; }
        }
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public DateTime DataHora {
            get { return dataHora; }
            set { dataHora = value; }
        }
    }
}
