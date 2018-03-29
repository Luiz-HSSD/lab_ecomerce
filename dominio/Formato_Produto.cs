using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Formato_Produto : EntidadeDominio
    {
        private int codFormato;
        private decimal altura;
        private decimal largura;
        private decimal diametro;
        private decimal comprimento;
        private string peso;
        private string dimensoes;

        public string Dimensoes
        {
            get { return dimensoes; }
            set { dimensoes = value;}
        }

        public string Peso
        {
            get { return peso; }
            set { peso = value; }
        }

        public decimal Comprimento
        {
            get { return comprimento; }
            set { comprimento = value; }
        }

        public decimal Diametro
        {
            get { return diametro; }
            set { diametro = value; }
        }

        public decimal Largura
        {
            get { return largura; }
            set { largura = value; }
        }

        public decimal Altura
        {
            get { return altura; }
            set { altura = value; }
        }

        public int CodFormato
        {
            get { return codFormato; }
            set { codFormato = value; }
        }

        public Formato_Produto():base(0,Convert.ToDateTime("01/01/1995 03:30"))
        {
            Dimensoes = "";
            Peso = "";
            Comprimento = 0;
            Diametro = 0;
            largura = 0;
            altura = 0;
            codFormato = 0;
        }

        
    }
}
