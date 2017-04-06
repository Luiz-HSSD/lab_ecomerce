using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Frete : EntidadeDominio
    {
        private Endereco destino;
        private double valor;
        private int prazo;

        public int Prazo
        {
            get { return prazo; }
            set { prazo = value; }
        }

        public double Valor
        {
            get { return valor; }
            set { valor = value; }
        }

        public Endereco Destino
        {
            get { return destino; }
            set { destino = value; }
        }

        public Frete(int id, DateTime DataHora) : base(id, DataHora)
        {
        }
        
    }
}
