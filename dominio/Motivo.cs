using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Motivo : EntidadeDominio
    {
        private string tipo;
        private string descricao;
        private int espec;

        public Motivo() : base(0, Convert.ToDateTime("01/01/1995 03:30"))
        {
            Especificação = 0;
            Tipo = "";
            Descricao = "";
        }
        public int Especificação
        {
            get { return espec; }
            set { espec = value; }
        }

        public string Descricao
        {
            get { return descricao; }
            set { descricao = value; }
        }
        public string Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }
    }
}
