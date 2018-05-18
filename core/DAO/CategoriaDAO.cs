using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;
using core.Utils;
using System.Data;

namespace core.DAO
{
    public class CategoriaDAO : AbstractDAO
    {
        
        public  CategoriaDAO() : base( "categoria", "id_cat")
        {

        }
        


        public override void salvar(EntidadeDominio entidade)
        {
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            Categoria categoria = (Categoria)entidade;
            pst.CommandText = "insert into categoria (des_cat, nome_cat, ative ) values ( :des , :nome,'A' )";
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
                pst.CommandText = "UPDATE categoria SET des_cat=:des, nome_cat=:nome WHERE id_cat=:co";
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
                pst.ExecuteNonQuery();
                pst.CommandText = "commit work";
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
                    categoria.Nome="";
                }

                if (categoria.Descricao == null)
                {
                    categoria.Descricao="";
                }

                if (categoria.Id == 0)
                {
                    sql = "SELECT * FROM categoria WHERE ative!='I'";
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
                Categoria p;
                while (vai.Read())
                {
                    p = new Categoria();
                    p.Id = Convert.ToInt32(vai["id_cat"]);
                    p.Nome=(vai["nome_cat"].ToString());
                    p.Descricao=(vai["des_cat"].ToString());
                    categorias.Add(p);
                }
                connection.Close();
                return categorias;
            }
            catch(OracleException ora)
            {
                throw ora;
            }
            

        }


    }
}