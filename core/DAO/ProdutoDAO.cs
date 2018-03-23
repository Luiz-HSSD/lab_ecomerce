using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using core.Utils;
using System.Data;

namespace core.DAO
{
    public class ProdutoDAO : AbstractDAO
    {
        private Produto produto=new Produto();
        private Categoria cat= new Categoria();
        public ProdutoDAO() : base("produto", "id_pro")
        {

        }



        public override List<EntidadeDominio> consultar(EntidadeDominio entidade)
        {
            cat = new Categoria();
            produto = (Produto)entidade;
            string sql = "";
           
            if (produto.Nome == null)
            {
                produto.Nome="";
            }

            if (produto.Descricao == null)
            {
                produto.Descricao="";
            }

            if (produto.Id == 0 && produto.Categoria.Id == 0 && produto.Nome=="")
            {
                sql = "SELECT * FROM produto";
            }
            else if(produto.Categoria.Id!=0)
            {
                sql = "SELECT * FROM produto WHERE cat=:cat" ;
                parameters=new OracleParameter[] { new OracleParameter("cat", produto.Categoria.Id.ToString()) };
            }
            else if (produto.Nome != "")
            {
                sql = "SELECT * FROM produto WHERE nome_pro=:nome";
                parameters = new OracleParameter[] { new OracleParameter("nome", produto.Nome.ToString()) };
            }
            else

            {
                sql = "SELECT * FROM produtoview WHERE id_pro=:cod";
                parameters = new OracleParameter[] { new OracleParameter("cod", produto.Id.ToString()) };
            }
            pst.Parameters.Clear();
            pst.CommandText = sql;
            if( parameters !=null)  pst.Parameters.AddRange(parameters);
            pst.Connection = connection;
            vai = pst.ExecuteReader();
            List<EntidadeDominio> produtos = new List<EntidadeDominio>();
            Produto p;
            while (vai.Read())
            {
                p = new Produto();
                p.Id=Convert.ToInt32(vai["id_pro"]);
                cat.Id=Convert.ToInt32(vai["CAT"]);
                cat.Nome= vai["NOME_CAT"].ToString();
                cat.Descricao= vai["DES_CAT"].ToString();
                p.Categoria=cat;
                if (vai["PRECO"].ToString() != "")
                    p.Preco = Convert.ToDouble(vai["PRECO"]);
                else p.Preco = 0;
                p.Codigo_barras= vai["COD_BARRAS"].ToString();
                if (vai["PESO"].ToString() != "")
                    p.Formato.Peso = Convert.ToString(vai["PESO"]);
                else p.Formato.Peso = Convert.ToString(0);
                p.Formato.Dimensoes= vai["DIMENSOES"].ToString();
                p.Fabricante= vai["FAB"].ToString();
                p.Nome= vai["NOME_PRO"].ToString();
                p.Descricao= vai["DES_PRO"].ToString();
                if (vai["IMAGE"].ToString() != "")
                    p.Img=(byte[])vai["IMAGE"];
                p.Extension= vai["EXT"].ToString();
                produtos.Add(p);
            }

            connection.Close();
            return produtos;

        }

        public override void salvar(EntidadeDominio entidade)
        {
            produto = (Produto)entidade;

            pst.CommandText = "insert into produto (des_pro, nome_pro, cod_barras, cat, preco, peso, image, ext, fab ) values ( :des , :nome, :cod, :cat, :preco, :peso, :dimensões,:imagem, :ext, :fab )";
            parameters = new OracleParameter[]
            {
                new OracleParameter("des",produto.Descricao),
                new OracleParameter("nome",produto.Nome),
                new OracleParameter("cod",produto.Codigo_barras),
                new OracleParameter("cat",produto.Categoria.Id),
                new OracleParameter("preco",produto.Preco),
                new OracleParameter("peso",produto.Formato.Peso),
                new OracleParameter("imagem",produto.Img),
                new OracleParameter("ext",produto.Extension),
                new OracleParameter("fab",produto.Fabricante)
            };
            pst.Parameters.Clear();
            pst.Parameters.AddRange(parameters);
            pst.Connection = connection;
            pst.CommandType = CommandType.Text;
            pst.ExecuteNonQuery();
            pst.CommandText = "insert into formato_produto (des_pro, nome_pro, cod_barras, cat, preco, peso, image, ext, fab ) values ( :des , :nome, :cod, :cat, :preco, :peso, :dimensões,:imagem, :ext, :fab )";
            parameters = new OracleParameter[]
            {
                new OracleParameter("des",produto.Formato.Altura),
                new OracleParameter("nome",produto.Formato.Comprimento),
                new OracleParameter("cat",produto.Formato.Diametro),
                new OracleParameter("preco",produto.Formato.Largura),
                new OracleParameter("peso",produto.Formato.Peso),
                new OracleParameter("cod",produto.Formato.CodFormato),
                new OracleParameter("imagem",produto.Formato.Dimensoes),
                new OracleParameter("ext",produto.Formato.Id),
                new OracleParameter("fab",produto.Fabricante)
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

                produto = (Produto)entidade;
                pst.Dispose();
                pst.CommandText = "UPDATE produto SET des_pro= :des , nome_pro=:nome, cod_barras=:cod_b, cat=:cate , preco=:prec , peso=:pes , dimensoes=:dimensões,image=:imagem  , ext=:exte , fab=:fabr  WHERE id_pro=:cod";
                parameters = new OracleParameter[]
                {
                    new OracleParameter("des",produto.Descricao),
                    new OracleParameter("nome",produto.Nome),
                    new OracleParameter("cod",produto.Codigo_barras),
                    new OracleParameter("cat",produto.Categoria.Id),
                    new OracleParameter("preco",produto.Preco),
                    new OracleParameter("peso",produto.Formato.Peso),
                    new OracleParameter("dimensões",produto.Formato.Dimensoes),
                    new OracleParameter("imagem",produto.Img),
                    new OracleParameter("ext",produto.Extension),
                    new OracleParameter("fab",produto.Fabricante),
                    new OracleParameter("cod",produto.Id)
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


    }
}
