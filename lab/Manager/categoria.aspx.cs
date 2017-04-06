using System;
using dominio;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using lab.Command;
using lab.ICommand;
using core.aplicacao;
using core.DAO;
using Oracle.DataAccess.Client;

namespace lab.Manager
{
    public partial class Categoria : viewgenerico
    {
        private dominio.Categoria categoria = new dominio.Categoria();

        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {

                if (!IsPostBack)
                {
                    Pesquisar();
                    if (!string.IsNullOrEmpty(Request.QueryString["cod"]))
                    {
                        categoria.Id=Convert.ToInt32(Request.QueryString["cod"]);
                        res=commands["CONSULTAR"].execute(categoria);
                        categoria =(dominio.Categoria) res.Entidades.ElementAt(0);
                        txtcod.Text = Convert.ToString(categoria.Id);
                        txtnome.Text = categoria.Nome;
                        txtdescricao.Text = categoria.Descricao;

                    }
                    else
                    {
                        //verificr edição
                        if (!string.IsNullOrEmpty(Request.QueryString["del"]))
                        {

                            categoria.Id=Convert.ToInt32(Request.QueryString["del"]);
                            commands["EXCLUIR"].execute(categoria);
                            Response.Redirect("categoria.aspx");
                        }

                    }

                    //carregando caixa listagem
                    
               }
            }
            catch(OracleException E)
            {
                // Response.Redirect("~/Default.aspx", false);
                throw E;
            }

        }
        private void Pesquisar()
        {
            int evade=0;
            string GRID = "<TABLE class='display' id='GridViewcat'><THEAD>{0}</THEAD><TBODY>{1}</TBODY></TABLE>";
            string tituloColunas = "<tr><th></th><th>Código</th><th>Nome</th><th>Descrição</th></tr>";
            string linha = "<tr><td> <a href='categoria.aspx?cod={0}'>editar</a> ";
            linha += "<a href='categoria.aspx?del={0}'>apagar</a></td><td>{0}</td><td>{1}</td><td>{2}</td></tr>";

            categoria.Id=0;
            res = commands["CONSULTAR"].execute(categoria);
            try
            {
                evade = res.Entidades.Count;
            }
            catch
            {
                evade = 0;
            }
            StringBuilder conteudo = new StringBuilder();
            for (int i=0;i<evade;i++)
            {
                categoria=(dominio.Categoria)res.Entidades.ElementAt(i);
                conteudo.AppendFormat(linha,
                    categoria.Id.ToString(),
                    categoria.Nome.ToString(),
                    categoria.Descricao.ToString());


            }
            string tabelafinal = string.Format(GRID, tituloColunas, conteudo.ToString());
            divTable.InnerHtml = tabelafinal;
            categoria.Id=0;
        }

        protected void novo_cat_Click(object sender, EventArgs e)
        {

                categoria.Nome = txtnome.Text;
                categoria.Descricao = txtdescricao.Text;
                commands["SALVAR"].execute(categoria);
                txtcod.Text = "";
                txtnome.Text = "";
                txtdescricao.Text = "";
            Pesquisar();
        }
        protected void alterar_cat_Click(object sender, EventArgs e)
        {
            if (txtcod.Text.Equals("")) return;
            categoria.Id=Convert.ToInt32(txtcod.Text);
            categoria.Nome=txtnome.Text;
            categoria.Descricao=txtdescricao.Text;
            commands["ALTERAR"].execute(categoria);
            txtcod.Text = "";
            txtnome.Text = "";
            txtdescricao.Text = "";
            Pesquisar();
            Response.Redirect("categoria.aspx");
        }
        protected void cancelar_cat_Click(object sender, EventArgs e)
        {
            txtcod.Text = "";
            txtnome.Text = "";
            txtdescricao.Text = "";
        }
        protected void excluir_cat_Click(object sender, EventArgs e)
        {
            if (txtcod.Text.Equals("")) return;
            categoria.Id=Convert.ToInt32(txtcod.Text);
            categoria.Nome=txtnome.Text;
            categoria.Descricao=txtdescricao.Text;
            commands["EXCLUIR"].execute(categoria);
            txtcod.Text = "";
            txtnome.Text = "";
            txtdescricao.Text = "";
            Response.Redirect("categoria.aspx");
            Pesquisar();
        }

                
    }
}