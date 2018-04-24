using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;
using Oracle.DataAccess.Client;
using System.Data;

namespace core.DAO
{
    public class RankingDAO : AbstractDAO
    {
        public RankingDAO() : base("ranking", "id_cli")
        {
        }

        public override void salvar(EntidadeDominio entidade)
        {
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            Categoria categoria = (Categoria)entidade;
            pst.CommandText = "insert into ranking (des_cat, nome_cat, ative ) values ( :des , :nome,'A' )";
            parameters = new OracleParameter[]
                    {
                        new OracleParameter("des",categoria.Descricao),
                        new OracleParameter("nome",categoria.Nome)
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

        public override void alterar(EntidadeDominio entidade)
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                Categoria categoria = (Categoria)entidade;
                pst.CommandText = "UPDATE ranking SET des_cat=:des, nome_cat=:nome WHERE id_cat=:co";
                parameters = new OracleParameter[]
                    {
                        new OracleParameter("des",categoria.Descricao),
                        new OracleParameter("nome",categoria.Nome),
                        new OracleParameter("co",categoria.Id)

                    };
                pst.Parameters.Clear();
                pst.Parameters.AddRange(parameters);
                pst.Connection = connection;
                pst.CommandType = CommandType.Text;
                vai = pst.ExecuteReader();
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
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                pst.Dispose();
                Categoria categoria = (Categoria)entidade;
                string sql = null;

                if (categoria.Nome == null)
                {
                    categoria.Nome = "";
                }

                if (categoria.Descricao == null)
                {
                    categoria.Descricao = "";
                }

                if (categoria.Id == 0)
                {
                    sql = "SELECT * FROM ranking WHERE ative!='I'";
                }
                else
                {
                    sql = "SELECT * FROM ranking WHERE id_cli= :co";
                }


                pst.CommandText = sql;
                parameters = new OracleParameter[] { new OracleParameter("co", categoria.Id.ToString()) };
                pst.Parameters.Clear();
                pst.Parameters.AddRange(parameters);
                pst.Connection = connection;
                pst.CommandType = CommandType.Text;
                //pst.ExecuteNonQuery();
                vai = pst.ExecuteReader();
                List<EntidadeDominio> categorias = new List<EntidadeDominio>();
                Categoria p;
                while (vai.Read())
                {
                    p = new Categoria();
                    p.Id = Convert.ToInt32(vai["id_cat"]);
                    p.Nome = (vai["nome_cat"].ToString());
                    p.Descricao = (vai["des_cat"].ToString());
                    categorias.Add(p);
                }
                connection.Close();
                return categorias;
            }
            catch (OracleException ora)
            {
                throw ora;
            }


        }
    }
}
