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
    class LivroDAO : AbstractDAO
    {

        public LivroDAO() : base( "livro", "id_liv")
        {

        }



        public override void salvar(EntidadeDominio entidade)
        {
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            Livro categoria = (Livro)entidade;
            pst.CommandText = "insert into livro (ative,des_liv, g_preco,nome, n_pags, isbn, edi, nome_liv , des_liv,image , ext, isbn, dim ) values ( :des,'A' , :g_preco, :nome, :isbnn, :eri, 'A' )";
            parameters = new OracleParameter[]
                        {
                        new OracleParameter("des",categoria.Descricao),
                        new OracleParameter("g_preco",categoria.G_PRECO),
                        new OracleParameter("nome",categoria.Nome),
                        new OracleParameter("ed",categoria.Edicao),
                        new OracleParameter("edit",categoria.Editora),
                        new OracleParameter("des",categoria.Descricao),
                        new OracleParameter("isbnn",categoria.ISBN),
                        new OracleParameter("eri",categoria.Formato.Dimensoes)
                        };
            pst.Parameters.Clear();
            pst.Parameters.AddRange(parameters);
            pst.Connection = connection;
            pst.CommandType = CommandType.Text;
            pst.ExecuteNonQuery();
            pst.CommandText = "commit work";
            pst.ExecuteNonQuery();
            connection.Close();
            foreach(Categoria cat in categoria.Generos)
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                pst.CommandText = "insert into cat_liv (id_cat, id_liv ) values ( :des,:bora )";
                parameters = new OracleParameter[]
                        {
                        new OracleParameter("des",cat.Id),
                        new OracleParameter("bora",categoria.Id)
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
                Livro categoria = (Livro)entidade;
                pst.CommandText = "UPDATE categoria SET des_cat=:des, nome_cat=:nome, dim=:eri WHERE id_cat=:co";
                parameters = new OracleParameter[]
                        {
                        new OracleParameter("des",categoria.Descricao),
                        new OracleParameter("g_preco",categoria.G_PRECO),
                        new OracleParameter("nome",categoria.Nome),
                        new OracleParameter("ed",categoria.Edicao),
                        new OracleParameter("edit",categoria.Editora),
                        new OracleParameter("des",categoria.Descricao),
                        new OracleParameter("nome",categoria.Nome),
                        new OracleParameter("eri",categoria.Formato.Dimensoes)
                        };
                pst.Parameters.Clear();
                pst.Parameters.AddRange(parameters);
                pst.Connection = connection;
                pst.CommandType = CommandType.Text;
                vai = pst.ExecuteReader();
                vai.Read();
                pst.CommandText = "DELETE FROM cat_liv WHERE id_liv=:des";
                parameters = new OracleParameter[]
                    {
                        new OracleParameter("des",categoria.Id),
                    };
                vai = pst.ExecuteReader();
                vai.Read();
                pst.ExecuteNonQuery();
                pst.CommandText = "commit work";
                vai = pst.ExecuteReader();
                vai.Read();
                pst.ExecuteNonQuery();
                connection.Close();
                foreach (Categoria cat in categoria.Generos)
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    pst.CommandText = "insert into cat_liv (id_cat, id_liv ) values ( :des,:bora )";
                    parameters = new OracleParameter[]
                            {
                        new OracleParameter("des",cat.Id),
                        new OracleParameter("bora",categoria.Id)
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
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                pst.Dispose();
                pst2.Dispose();
                Livro categoria = (Livro)entidade;
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
                    sql = "SELECT * FROM livro WHERE ative!='I'";
                }
                else
                {
                    sql = "SELECT * FROM categoria WHERE id_cat= :co";
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
                Livro p;
                Categoria d;
                while (vai.Read())
                {
                    p = new Livro();
                    p.Id = Convert.ToInt32(vai["id_liv"]);
                    p.Nome = (vai["nome_liv"].ToString());
                    p.Descricao = (vai["des_liv"].ToString());
                    p.ISBN = (vai["isbn"].ToString());
                    p.Edicao = (vai["edicao"].ToString());
                    p.Editora = (vai["edi"].ToString());
                    p.Codigo_barras = (vai["cod_bar"].ToString());
                    p.N_pags = Convert.ToInt32(vai["n_pags"]);
                    p.G_PRECO = Convert.ToChar(vai["g_preco"]);
                    pst2.CommandText = "select * from cat_liv join categoria using (id_cat) where id_liv=:co";
                    parameters2 = new OracleParameter[] { new OracleParameter("co", p.Id.ToString()) };
                    pst2.Parameters.Clear();
                    pst2.Parameters.AddRange(parameters2);
                    pst2.Connection = connection;
                    pst2.CommandType = CommandType.Text;
                    vai2 = pst2.ExecuteReader();
                    List<Categoria> cats = new List<Categoria>();
                    while (vai2.Read())
                    {

                        d = new Categoria();
                        d.Id = Convert.ToInt32(vai["id_cat"]);
                        d.Nome = (vai["nome_cat"].ToString());
                        d.Descricao = (vai["des_cat"].ToString());
                        cats.Add(d);
                    }
                    p.Generos = cats;
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
