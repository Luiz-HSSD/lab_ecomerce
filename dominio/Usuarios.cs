﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Usuarios : EntidadeDominio
    {
        private string login;

        public string Login
        {
            get { return login; }
            set { login = value; }
        }
        private string password;

        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        private string pass_rec;

        public string passrec
        {
            get { return pass_rec; }
            set { pass_rec = value; }
        }
        private string domain;

        public string Domain
        {
            get { return domain; }
            set { domain = value; }
        }

        public Usuarios() : base(0, Convert.ToDateTime("01/01/1995 03:30"))
        {
            login = "";
            password = "";
            passrec = "";
            domain = "";
        }
        
    }
}
