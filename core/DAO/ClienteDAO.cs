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

        public override void salvar(EntidadeDominio entidade)
        {
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            cliente = (Cliente)entidade;
 
            pst.CommandText = "insert into produto (nome_cli, senha, sexo ,cpf ,rg ,dt_nas ,email) values ( :senha ,:nome_cli ,:sexo , :cpf, :rg, :dt_nas, :email ) returning id_liv into :cod";
            parameters = new OracleParameter[]
            {
                new OracleParameter("senha",cliente.Senha),
                new OracleParameter("nome_cli",cliente.Nome),
                new OracleParameter("sexo",cliente.Sexo),
                new OracleParameter("cpf",cliente.Cpf),
                new OracleParameter("rg",cliente.Rg),
                new OracleParameter("dt_nas",cliente.Dt_Nas),
                new OracleParameter("email",cliente.Email)
            };
            pst.Parameters.Clear();
            pst.Parameters.AddRange(parameters);
            OracleParameter Out = new OracleParameter("cod", cliente.Id);
            Out.Direction = ParameterDirection.ReturnValue;
            pst.Parameters.Add(Out);
            pst.Connection = connection;
            pst.CommandType = CommandType.Text;
            pst.ExecuteNonQuery();
            pst.CommandText = "commit work";
            pst.ExecuteNonQuery();
            connection.Close();
            cliente.Id = Convert.ToInt32(Out.Value);
            foreach (Endereco cat in cliente.Enderecos)
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                pst.CommandText = "insert into end_cli (id_end, id_cli ) values ( :des,:bora )";
                parameters = new OracleParameter[]
                        {
                        new OracleParameter("des",cat.Id),
                        new OracleParameter("bora",cliente.Id)
                        };
                pst.Parameters.Clear();
                pst.Parameters.AddRange(parameters);
                pst.Connection = connection;
                pst.CommandType = CommandType.Text;
                pst.ExecuteNonQuery();
                pst.CommandText = "commit work";
                pst.ExecuteNonQuery();
                connection.Close();

            }
            foreach (Cartao_Credito cat in cliente.Cartoes)
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                pst.CommandText = "insert into car_cli (id_car, id_cli ) values ( :des,:bora )";
                parameters = new OracleParameter[]
                        {
                        new OracleParameter("des",cat.Id),
                        new OracleParameter("bora",cliente.Id)
                        };
                pst.Parameters.Clear();
                pst.Parameters.AddRange(parameters);
                pst.Connection = connection;
                pst.CommandType = CommandType.Text;
                pst.ExecuteNonQuery();
                pst.CommandText = "commit work";
                pst.ExecuteNonQuery();
                connection.Close();

            }
            return;
        }

        public override void alterar(EntidadeDominio entidade)
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                cliente = (Cliente)entidade;
                
                    pst.Dispose();
                    pst.CommandText = "UPDATE clientes SET  senha=:senha, nome_cli= :nome_cli,   sexo=:sexo,  cpf=:cpf, rg=:rg , dt_nas=:dt_nas , email=:email , dimensoes=:dimensões,image=:imagem  , ext=:exte , fab=:fabr, id_end=:end_id  WHERE id_cli=:id_cli";
                    parameters = new OracleParameter[]
                    {
                        new OracleParameter("senha",cliente.Senha),
                        new OracleParameter("nome_cli",cliente.Nome),
                        new OracleParameter("sexo",cliente.Sexo),
                        new OracleParameter("cpf",cliente.Cpf),
                        new OracleParameter("rg",cliente.Rg),
                        new OracleParameter("dt_nas",cliente.Dt_Nas),
                        new OracleParameter("email",cliente.Email),
                        new OracleParameter("cod",cliente.Id),
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
                pst.CommandText = "DELETE FROM end_cli WHERE id_cli=:cod ";
                pst.Parameters.Clear();
                parameters = new OracleParameter[]
                    {
                        new OracleParameter("cod",cliente.Id)
                    };
                pst.Parameters.AddRange(parameters);
                pst.ExecuteNonQuery();
                pst.CommandText = "commit work";
                vai = pst.ExecuteReader();
                vai.Read();
                pst.ExecuteNonQuery();
                connection.Close();
                foreach (Endereco cat in cliente.Enderecos)
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    pst.CommandText = "insert into end_cli (id_end, id_cli ) values ( :des,:bora )";
                    parameters = new OracleParameter[]
                            {
                        new OracleParameter("des",cat.Id),
                        new OracleParameter("bora",cliente.Id)
                            };
                    pst.Parameters.Clear();
                    pst.Parameters.AddRange(parameters);
                    pst.Connection = connection;
                    pst.CommandType = CommandType.Text;
                    pst.ExecuteNonQuery();
                    pst.CommandText = "commit work";
                    pst.ExecuteNonQuery();
                    connection.Close();

                }
                pst.CommandText = "DELETE FROM car_cli WHERE id_cli=:cod ";
                pst.Parameters.Clear();
                parameters = new OracleParameter[]
                    {
                        new OracleParameter("cod",cliente.Id)
                    };
                pst.Parameters.AddRange(parameters);
                pst.ExecuteNonQuery();
                pst.CommandText = "commit work";
                vai = pst.ExecuteReader();
                vai.Read();
                pst.ExecuteNonQuery();
                connection.Close();
                foreach (Cartao_Credito cat in cliente.Cartoes)
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    pst.CommandText = "insert into car_cli (id_car, id_cli ) values ( :des,:bora )";
                    parameters = new OracleParameter[]
                            {
                            new OracleParameter("des",cat.Id),
                            new OracleParameter("bora",cliente.Id)
                            };
                    pst.Parameters.Clear();
                    pst.Parameters.AddRange(parameters);
                    pst.Connection = connection;
                    pst.CommandType = CommandType.Text;
                    pst.ExecuteNonQuery();
                    pst.CommandText = "commit work";
                    pst.ExecuteNonQuery();
                    connection.Close();

                }
                return;
            }
            catch (Exception e)
            {
                throw e;

            }

        }
        protected OracleParameter[] parameters2;
        protected OracleDataReader vai2;
        private OracleCommand pst2 = new OracleCommand();
        public override List<EntidadeDominio> consultar(EntidadeDominio entidade)
        {
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
                sql = "SELECT * FROM clientes left join end_cli using(id_cli) left join endereco using(id_end)";
            }
            else

            {
                sql = "SELECT * FROM clientes left join end_cli using(id_cli) left join endereco using(id_end) WHERE id_cli=:cod";
                parameters = new OracleParameter[] { new OracleParameter("cod", cliente.Id.ToString()) };
            }
            pst.Parameters.Clear();
            pst.CommandText = sql;
            if (parameters != null) pst.Parameters.AddRange(parameters);
            pst.Connection = connection;
            vai = pst.ExecuteReader();
            List<EntidadeDominio> clientes = new List<EntidadeDominio>();
            Cliente p;
            Endereco d;
            Cartao_Credito q;
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
                pst2.CommandText = "select * from end_cli join endereco using (id_end) where id_cli=:co";
                parameters2 = new OracleParameter[] { new OracleParameter("co", p.Id.ToString()) };
                pst2.Parameters.Clear();
                pst2.Parameters.AddRange(parameters2);
                pst2.Connection = connection;
                pst2.CommandType = CommandType.Text;
                vai2 = pst2.ExecuteReader();
                List<Endereco> cats = new List<Endereco>();
                while (vai2.Read())
                {

                    d = new Endereco();
                    d.Id = Convert.ToInt32(vai2["id_end"]);
                    d.Numero = vai2["numero"].ToString();
                    d.Logradouro = vai2["logradouro"].ToString();
                    d.Bairro = vai2["bairro"].ToString();
                    d.Cidade = vai2["cidade"].ToString();
                    d.UF = vai2["uf"].ToString();
                    d.Cep = vai2["cep"].ToString();
                    cats.Add(d);
                }
                p.Enderecos = cats;
                pst2.CommandText = "select * from car_cli join cartao_credito using (id_car) where id_cli=:co";
                parameters2 = new OracleParameter[] { new OracleParameter("co", p.Id.ToString()) };
                pst2.Parameters.Clear();
                pst2.Parameters.AddRange(parameters2);
                pst2.Connection = connection;
                pst2.CommandType = CommandType.Text;
                vai2 = pst2.ExecuteReader();
                List<Cartao_Credito> carts = new List<Cartao_Credito>();
                while (vai2.Read())
                {

                    q = new Cartao_Credito();
                    q.Id = Convert.ToInt32(vai2["id_car"]);
                    q.Numero = vai2["numero"].ToString();
                    q.Nome_Titular = vai2["nome_car"].ToString();
                    q.Validade = vai2["validade"].ToString();
                    q.CCV = Convert.ToInt32(vai2["ccv"]);
                    q.Bandeira.Id = Convert.ToInt32(vai2["id_band"]);
                    q.Bandeira.Nome = vai2["nome_band"].ToString();
                    carts.Add(q);
                }
                p.Cartoes = carts;
                clientes.Add(p);
            }

            connection.Close();
            return clientes;
        }


    }
}

    

