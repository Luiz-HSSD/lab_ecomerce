using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;
using dominio;
using System.Data;

namespace core.DAO
{
    public class ClienteDAO : AbstractDAO
    {
        private Cliente cliente;
        private Endereco end;
        private EnderecoDAO enddao=new EnderecoDAO(); 
        public ClienteDAO() : base( "cliente", "id_cli")
        {

        }

        public override void alterar(EntidadeDominio entidade)
        {
            try
            {

                    connection.Open();
                    cliente = (Cliente)entidade;
                if (cliente.Endereco.Id==0)
                {
                    enddao.salvar(cliente.Endereco);
                    cliente.Endereco = (Endereco)enddao.consultar(cliente.Endereco).ElementAt(0);
                }
                    pst.Dispose();
                    pst.CommandText = "UPDATE clientes SET  sexo=:sexo,  nome_cli= :nome_cli, cpf=:cpf, rg=:rg , dt_nas=:dt_nas , email=:email , dimensoes=:dimensões,image=:imagem  , ext=:exte , fab=:fabr, id_end=:end_id  WHERE id_cli=:id_cli";
                    parameters = new OracleParameter[]
                    {
                    new OracleParameter("sexo",cliente.Sexo),
                    new OracleParameter("nome_cli",cliente.Nome),
                    new OracleParameter("cpf",cliente.Cpf),
                    new OracleParameter("rg",cliente.Rg),
                    new OracleParameter("dt_nas",cliente.Dt_Nas),
                    new OracleParameter("email",cliente.Email),
                    new OracleParameter("end_id",cliente.Endereco.Id),
                    new OracleParameter("id_cli",cliente.Id)
                    };
                    pst.Parameters.Clear();
                    pst.Parameters.AddRange(parameters);
                    pst.Connection = connection;
                    pst.CommandType = CommandType.Text;
                    OracleDataReader vai = pst.ExecuteReader();
                    vai.Read();
                    pst.CommandText = "commit work";
                    vai = pst.ExecuteReader();
                    vai.Read();
                    pst.ExecuteNonQuery();
                    connection.Close();

                    return;
                
            }
            catch (Exception e)
            {
                throw e;

            }

        }

        public override List<EntidadeDominio> consultar(EntidadeDominio entidade)
        {
            connection.Open();
            end = new Endereco();
            cliente = (Cliente)entidade;
            string sql = "";

            if (cliente.Nome == null)
            {
                cliente.Nome = "";
            }

            if (cliente.Sexo == '\0')
            {
                cliente.Sexo = 'M';
            }

            if (cliente.Id == 0)
            {
                sql = "SELECT * FROM clientes left join endereco using(id_end)";
            }
            else

            {
                sql = "SELECT * FROM clientes left join endereco using(id_end) WHERE id_cli=:cod";
                parameters = new OracleParameter[] { new OracleParameter("cod", cliente.Id.ToString()) };
            }
            pst.Parameters.Clear();
            pst.CommandText = sql;
            if (parameters != null) pst.Parameters.AddRange(parameters);
            pst.Connection = connection;
            vai = pst.ExecuteReader();
            List<EntidadeDominio> clientes = new List<EntidadeDominio>();
            Cliente p;
            while (vai.Read())
            {
                p = new Cliente();
                p.Id = Convert.ToInt32(vai["id_cli"]);
                p.Nome = vai["NOME_CLI"].ToString();
                p.Sexo =Convert.ToChar(vai["SEXO"]);
                p.Cpf = vai["CPF"].ToString();
                p.Rg = vai["RG"].ToString();
                p.Dt_Nas=Convert.ToDateTime( vai["DT_NAS"]);
                p.Email = vai["EMAIL"].ToString();
                p.Endereco.Id = Convert.ToInt32(vai["ID_END"]);
                p.Endereco.Numero = vai["NUMERO"].ToString();
                p.Endereco.Logradouro = vai["LOGRADOURO"].ToString();
                p.Endereco.Bairro = vai["BAIRRO"].ToString();
                p.Endereco.Cidade = vai["CIDADE"].ToString();
                p.Endereco.UF = vai["UF"].ToString();
                p.Endereco.Cep = vai["CEP"].ToString();
                clientes.Add(p);
            }

            connection.Close();
            return clientes;
        }

        public override void salvar(EntidadeDominio entidade)
        {

            connection.Open();
            cliente = (Cliente)entidade;
            if (cliente.Endereco.Id == 0)
            {
                enddao.salvar(cliente.Endereco);
                cliente.Endereco=(Endereco) enddao.consultar(cliente.Endereco).ElementAt(0);
            }
            pst.CommandText = "insert into produto (sexo, nome_cli, cpf, rg, dt_nas, email) values ( :sexo , :nome, :cpf, :rg, :dt_nas, :email )";
            if (cliente.Endereco.Id==0)
            parameters = new OracleParameter[]
            {
                    new OracleParameter("sexo",cliente.Sexo),
                    new OracleParameter("nome_cli",cliente.Nome),
                    new OracleParameter("cpf",cliente.Cpf),
                    new OracleParameter("rg",cliente.Rg),
                    new OracleParameter("dt_nas",cliente.Dt_Nas),
                    new OracleParameter("email",cliente.Email),
                    new OracleParameter("email",cliente.Endereco.Id)
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

    

