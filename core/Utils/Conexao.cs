﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;

namespace core.Utils
{
    public class Conexao
    {
        static string conx = "Data Source=(DESCRIPTION =  (ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))(CONNECT_DATA =(SERVER = DEDICATED)(SERVICE_NAME = XE))); User Id=lesbd;Password=123;";
        private static OracleConnection connection;

        public static OracleConnection Connection
        {
            get
            {
                if (connection == null)
                {
                    connection = new OracleConnection(conx);
                    connection.Open();
                }
                return connection;
            }
            set { connection = value; }
        }

        

    }
}
