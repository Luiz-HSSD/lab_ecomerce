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
    public class LivroDAO : AbstractDAO
    {

        public LivroDAO() : base( "livro", "id_liv")
        {

        }

        public List<EntidadeDominio> consultar_formato(EntidadeDominio entidade)
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                pst.Dispose();
                Formato_Produto categoria = (Formato_Produto)entidade;
                string sql = null;

                if (categoria.Peso == null)
                {
                    categoria.Peso = "";
                }

                if (categoria.Dimensoes == null)
                {
                    categoria.Dimensoes = "";
                }

                if (categoria.Id == 0)
                {
                    sql = "SELECT * FROM formato_produto ";
                }
                else
                {
                    sql = "SELECT * FROM formato_produto WHERE id_for= :co";
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
                Formato_Produto p;
                while (vai.Read())
                {
                    p = new Formato_Produto();
                    p.Id = Convert.ToInt32(vai["id_for"]);
                    p.CodFormato = Convert.ToInt32(vai["cod_formato"]);
                    p.Peso = (vai["peso"].ToString());
                    p.Comprimento = Convert.ToDecimal(vai["comprimento"].ToString());
                    p.Altura = Convert.ToDecimal(vai["altura"].ToString());
                    p.Diametro = Convert.ToDecimal(vai["diametro"].ToString());
                    p.Dimensoes = (vai["dimensoes"].ToString());
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


    

    public override void salvar(EntidadeDominio entidade)
        {
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            Livro categoria = (Livro)entidade;
            pst.CommandText = "insert into livro ( ative, dim, id_g_pre, n_pags, isbn, edicao, cod_bar, edi, nome_liv, des_liv, image, ext) values ( 'A' , :eri, :g_preco, :n_pags , :isbnn, :ed, :cod_bar,:edit, :nome , :des , :img, :ext ) returning id_liv into :cod";
            parameters = new OracleParameter[]
            {
                new OracleParameter("eri",categoria.Formato.Dimensoes),
                new OracleParameter("g_preco",categoria.G_PRECO.Id),
                new OracleParameter("n_pags",categoria.N_pags),
                new OracleParameter("isbnn",categoria.ISBN),
                new OracleParameter("ed",categoria.Edicao),
                new OracleParameter("cod_bar",categoria.Codigo_barras),
                new OracleParameter("edit",categoria.Editora),
                new OracleParameter("nome",categoria.Nome),
                new OracleParameter("des",categoria.Descricao),
                new OracleParameter("img",categoria.Img),
                new OracleParameter("ext",categoria.Extension)
            };
            pst.Parameters.Clear();
            pst.Parameters.AddRange(parameters);
            OracleParameter Out = new OracleParameter("cod", categoria.Id);
            Out.Direction = ParameterDirection.ReturnValue;
            pst.Parameters.Add(Out);
            pst.Connection = connection;
            pst.CommandType = CommandType.Text;
            pst.ExecuteNonQuery();
            pst.CommandText = "commit work";
            pst.ExecuteNonQuery();
            connection.Close();
            categoria.Id = Convert.ToInt32(Out.Value);
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
                pst.CommandText = "UPDATE livro SET  ative=:ati , dim=:eri , id_g_pre=:g_preco , n_pags=:n_pags , isbn=:isbnn , edicao=:ed, cod_bar=:cod_bar , edi=:edit , nome_liv=:nome , des_liv=:des , image=:img , ext=:ext  WHERE id_liv=:cod";
                parameters = new OracleParameter[]
                    {
                    new OracleParameter("ati",categoria.Ative),
                    new OracleParameter("eri",categoria.Formato.Dimensoes),
                    new OracleParameter("g_preco",categoria.G_PRECO.Id),
                    new OracleParameter("n_pags",categoria.N_pags),
                    new OracleParameter("isbnn",categoria.ISBN),
                    new OracleParameter("ed",categoria.Edicao),
                    new OracleParameter("cod_bar",categoria.Codigo_barras),
                    new OracleParameter("edit",categoria.Editora),
                    new OracleParameter("nome",categoria.Nome),
                    new OracleParameter("des",categoria.Descricao),
                    new OracleParameter("img",categoria.Img),
                    new OracleParameter("ext",categoria.Extension),
                    new OracleParameter("cod",categoria.Id),
                    };
                pst.Parameters.Clear();
                pst.Parameters.AddRange(parameters);
                pst.Connection = connection;
                pst.CommandType = CommandType.Text;
                vai = pst.ExecuteReader();
                vai.Read();
                pst.ExecuteNonQuery();
                pst.CommandText = "DELETE FROM cat_liv WHERE id_liv=:cod ";
                pst.Parameters.Clear();
                parameters = new OracleParameter[]
                    {
                        new OracleParameter("cod",categoria.Id)
                    };
                pst.Parameters.AddRange(parameters);
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

                parameters = new OracleParameter[] { new OracleParameter("co", categoria.Id.ToString()) };
                if (categoria.Descricao == null)
                {
                    categoria.Descricao = "";
                }
                if (!string.IsNullOrEmpty(categoria.Nome))
                {
                    sql = "SELECT * FROM livro  WHERE nome_liv=:co";
                    parameters = new OracleParameter[] { new OracleParameter("co", categoria.Nome) };
                }
                else if (categoria.Id == 0)
                {
                    sql = "SELECT * FROM livro  left join formato_produto using(id_for) WHERE ative!='I'";            
                }
                else
                {
                    sql = "SELECT * FROM livro left join formato_produto using(id_for) WHERE ative!='I' AND id_liv= :co";
                    parameters = new OracleParameter[] { new OracleParameter("co", categoria.Id.ToString()) };
                }


                pst.CommandText = sql;
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
                    if(vai["image"].GetType().Name != "DBNull")
                        p.Img = (byte[])(vai["image"]);
                    p.G_PRECO.Id = Convert.ToInt32(vai["id_g_pre"].ToString());
                    if(vai["id_for"].GetType().Name != "DBNull") { 
                    p.Formato.Id = Convert.ToInt32(vai["id_for"]);
                    p.Formato.CodFormato = Convert.ToInt32(vai["cod_formato"]);
                    p.Formato.Peso = (vai["peso"].ToString());
                    p.Formato.Comprimento = Convert.ToDecimal(vai["comprimento"].ToString());
                    p.Formato.Altura = Convert.ToDecimal(vai["altura"].ToString());
                    p.Formato.Largura= Convert.ToDecimal(vai["largura"].ToString());
                    p.Formato.Diametro = Convert.ToDecimal(vai["diametro"].ToString());
                    p.Formato.Dimensoes = (vai["dimensoes"].ToString());
                    }
                    p.Extension = vai["ext"].ToString();
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
                        d.Id = Convert.ToInt32(vai2["id_cat"]);
                        d.Nome = (vai2["nome_cat"].ToString());
                        d.Descricao = (vai2["des_cat"].ToString());
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
