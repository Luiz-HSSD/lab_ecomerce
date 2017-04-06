using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;
using Oracle.DataAccess.Client;
using core.Utils;
using System.Data;

namespace core.DAO
{
    public class EnderecoDAO : AbstractDAO
    {
        private Endereco endereco = new Endereco();
        public EnderecoDAO() : base( "enderco", "id_end")
        {

        }

        public override void alterar(EntidadeDominio entidade)
        {
            throw new NotImplementedException();
        }

        public override List<EntidadeDominio> consultar(EntidadeDominio entidade)
        {
            connection.Open();
            endereco = (Endereco)entidade;
            string sql = null;

            
            if (endereco.Id == 0)
            {
                sql = "SELECT * FROM endereco";
            }
            else
            {
                sql = "SELECT * FROM endereco WHERE cep=:cep and bairro=:bairro and complemento=:comp and logradouro=:log and numero= :num and uf=:uf and  cidade=:cidade";
                
            }
            pst.Parameters.Clear();
            OracleParameter[] parameters = new OracleParameter[]
                    {
                            new OracleParameter("cep", endereco.Cep),
                            new OracleParameter("bairro", endereco.Bairro),
                            new OracleParameter("comp", endereco.Complemento),
                            new OracleParameter("log", endereco.Logradouro),
                            new OracleParameter("num", endereco.Numero),
                            new OracleParameter("uf", endereco.UF),
                            new OracleParameter("cidade", endereco.Cidade)
                    };
            pst.Parameters.Clear();
            pst.CommandText = sql;
            if (parameters != null) pst.Parameters.AddRange(parameters);
            pst.Connection = connection;
            vai = pst.ExecuteReader();
            List<EntidadeDominio> enderecos = new List<EntidadeDominio>();
            Endereco p;
            while (vai.Read())
            {
                p = new Endereco();
                p.Id=Convert.ToInt32(vai["id_end"]);
                p.Cep =(vai["cep"].ToString());
                p.Bairro=(vai["bairro"].ToString());
                p.Complemento=(vai["complemento"].ToString());
                p.Logradouro=(vai["logradouro"].ToString());
                p.Numero=(vai["numero"].ToString());
                p.UF=(vai["uf"].ToString());
                p.Cidade=(vai["cidade"].ToString());
                enderecos.Add(p);
            }

            connection.Close();
            return enderecos;

        }

        public override void salvar(EntidadeDominio entidade)
        {
            connection.Open();
            endereco = (Endereco)entidade;
            pst.Dispose();
            pst.CommandText = "insert into endereco(cep, bairro, complemento, logradouro ,numero ,uf , cidade ) values ( :cep , :bairro, :comp , :log , :num, :uf, :cidade )";
            OracleParameter[] parameters = new OracleParameter[]
                    {
                            new OracleParameter("cep", endereco.Cep),
                            new OracleParameter("bairro", endereco.Bairro),
                            new OracleParameter("comp", endereco.Complemento),
                            new OracleParameter("log", endereco.Logradouro),
                            new OracleParameter("num", endereco.Numero),
                            new OracleParameter("uf", endereco.UF),
                            new OracleParameter("cidade", endereco.Cidade)
                    };


            pst.Parameters.Clear();
            pst.Parameters.AddRange(parameters);
            pst.Connection = connection;
            pst.CommandType = CommandType.Text;
            pst.ExecuteNonQuery();
            pst.CommandText = "commit work";
            pst.ExecuteNonQuery();
            connection.Close();
            return;
        }
    }
}
