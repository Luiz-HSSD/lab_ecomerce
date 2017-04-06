using System;

namespace dominio
{
    public class Categoria : EntidadeDominio
    {
        private string nome;
        private string descricao;
         
        public Categoria() : base(0, Convert.ToDateTime("01/01/1995 03:30"))
        {
            Nome="";
            Descricao="";
            Id=0;
        }
        public string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        public string Descricao
        {
            get { return descricao; }
            set { descricao = value; }
        }


    }
}
