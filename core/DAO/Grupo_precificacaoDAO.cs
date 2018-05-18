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
    public class Grupo_precificacaoDAO : AbstractDAO
    {
        public Grupo_precificacaoDAO():base("g_preco", "id_g_pre")
        {

        }
             
        public override void alterar(EntidadeDominio entidade)
        {
            throw new NotImplementedException();
        }

        public override List<EntidadeDominio> consultar(EntidadeDominio entidade)
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                //pst.Dispose();
                Grupo_Precificacao categoria = (Grupo_Precificacao)entidade;
                string sql = null;

                if (categoria.Nome == null)
                {
                    categoria.Nome = "";
                }   

                if (categoria.Id == 0)
                {
                    sql = "SELECT * FROM g_preco ";
                }
                else
                {
                    sql = "SELECT * FROM g_preco WHERE id_g_pre= :co";
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
                Grupo_Precificacao p;
                while (vai.Read())
                {
                    p = new Grupo_Precificacao();
                    p.Id = Convert.ToInt32(vai["id_g_pre"]);
                    p.Nome = (vai["nome_g_preco"].ToString());
                    p.Porcentagem = Convert.ToDouble(vai["porcentagem"].ToString());
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
            throw new NotImplementedException();
        }
    }
}
