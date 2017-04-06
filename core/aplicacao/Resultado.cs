using System;
using System.Collections.Generic;
using dominio;


namespace core.aplicacao
{
    public class Resultado:Entidadeaplicacao
    {

        private string msg;
        private List<EntidadeDominio> entidades;

        public List<EntidadeDominio> Entidades
        {
            get { return entidades; }
            set { entidades = value; }
        }

        public string Msg
        {
            get { return msg; }
            set { msg = value; }
        }



        public Resultado()
        {
            Msg="";
            Entidades = new List<EntidadeDominio>();
        }
    

    }
}
