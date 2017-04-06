using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Cliente:EntidadeDominio
    { 


        private string nome;
        private string rg;
        private string cpf;
        private char sexo;
        private string email;
        private DateTime dt_nas;
        private Endereco endereco;

        public Endereco Endereco
        {
            get { return endereco; }
            set { endereco = value; }
        }

        public DateTime Dt_Nas
        {
            get { return dt_nas; }
            set { dt_nas = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public char Sexo
        {
            get { return sexo; }
            set { sexo = value; }
        }

        public string Cpf
        {
            get { return cpf; }
            set { cpf = value; }
        }

        public string Rg
        {
            get { return rg; }
            set { rg = value; }
        }

        public string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        public Cliente() : base(0, Convert.ToDateTime("01/01/1995 03:30"))
        {
            Nome = "";
            Rg = "";
            Cpf = "";
            Sexo = 'M';
            Email = "";
            Dt_Nas = Convert.ToDateTime("01/01/1995 03:30");
            Endereco = new Endereco();
        }


    }
}
