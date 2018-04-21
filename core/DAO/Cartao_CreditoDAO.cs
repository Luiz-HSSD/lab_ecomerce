using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;
using System.Data;
using Oracle.DataAccess.Client;

namespace core.DAO
{
    public class Cartao_CreditoDAO : AbstractDAO
    {
        public Cartao_CreditoDAO():base("cartao_credito", "id_car")
        {
        }

        Cartao_Credito cartao = new Cartao_Credito();
        public override void alterar(EntidadeDominio entidade)
        {
            throw new NotImplementedException();
        }

        public override List<EntidadeDominio> consultar(EntidadeDominio entidade)
        {
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            cartao = (Cartao_Credito)entidade;
            string sql = null;


            if (cartao.Id == 0)
            {
                sql = "SELECT * FROM cartao_credito join bandeira using(id_band)";
            }
            else
            {
                sql = "SELECT * FROM cartao_credito join bandeira using(id_band) WHERE nome_car=:nc and numero=:nu and validade=:va and ccv=:ccv and id_band=:band";

            }
            pst.Parameters.Clear();
            OracleParameter[] parameters = new OracleParameter[]
                    {
                            new OracleParameter("nc", cartao.Nome_Titular),
                            new OracleParameter("nu", cartao.Numero),
                            new OracleParameter("va", cartao.Validade),
                            new OracleParameter("ccv", cartao.CCV),
                            new OracleParameter("band", cartao.Bandeira.Id)
                    };
            pst.Parameters.Clear();
            pst.CommandText = sql;
            if (parameters != null) pst.Parameters.AddRange(parameters);
            pst.Connection = connection;
            vai = pst.ExecuteReader();
            List<EntidadeDominio> cartaos = new List<EntidadeDominio>();
            Cartao_Credito p;
            while (vai.Read())
            {
                p = new Cartao_Credito();
                p.Id = Convert.ToInt32(vai["id_car"]);
                p.Nome_Titular = (vai["nome_car"].ToString());
                p.Numero = (vai["numero"].ToString());
                p.Validade = (vai["validade"].ToString());
                p.CCV = Convert.ToInt32(vai["ccv"].ToString());
                p.Bandeira.Id = Convert.ToInt32(vai["id_band"].ToString());
                p.Bandeira.Nome = (vai["nome_band"].ToString());
                cartaos.Add(p);
            }

            connection.Close();
            return cartaos;

        }

        public override void salvar(EntidadeDominio entidade)
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                cartao = (Cartao_Credito)entidade;
                pst.Dispose();
                pst.CommandText = "insert into cartao_credito(nome_car , numero, validade, ccv ,id_band) values ( :noc , :num, :val , :ccv , :band)  returning id_car into :cod";
                OracleParameter[] parameters = new OracleParameter[]
                        {
                            new OracleParameter("noc", cartao.Nome_Titular),
                            new OracleParameter("num", cartao.Numero),
                            new OracleParameter("val", cartao.Validade),
                            new OracleParameter("ccv", cartao.CCV),
                            new OracleParameter("band", cartao.Bandeira.Id)
                        };


                pst.Parameters.Clear();
                pst.Parameters.AddRange(parameters);
                OracleParameter Out = new OracleParameter("cod", cartao.Id);
                Out.Direction = ParameterDirection.ReturnValue;
                pst.Parameters.Add(Out);
                pst.Connection = connection;
                pst.CommandType = CommandType.Text;
                pst.ExecuteNonQuery();
                cartao.Id = Convert.ToInt32(Out.Value);
                pst.CommandText = "commit work";
                pst.ExecuteNonQuery();
                connection.Close();
            }
            catch(OracleException e)
            {
                throw e;
            }
            return;
        }
    }

}
