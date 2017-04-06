using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using System.Drawing;
using System.Text;

namespace lab
{
    public partial class _Default : viewgenerico
    {
        private Categoria categoria=new Categoria();
        private List<Categoria> cat =new List<Categoria>();
        private Produto prod=new Produto();
        private List<Produto> pro =new List<Produto>();

        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                if (!IsPostBack)
                {
                    categoria.Id=0;
                    Pesquisar();
                    if (!string.IsNullOrEmpty(Request.QueryString["cod"]))
                    {

                        prod.Id=Convert.ToInt32(Request.QueryString["cod"]);
                        res = commands["CONSULTAR"].execute(prod);
                        prod = (dominio.Produto)res.Entidades.ElementAt(0);

                    }

                }
            }
            catch (Exception ea)
            {
                throw ea;
                //     Response.Redirect("~/Default.aspx", false);
            }

        }

        private string pesquisar_cat()
        {
            int evade;
            string comeco_cat = "<ul>",
                li = "<li> <a   href=\"Default.aspx?cod={0}\" >{1}</a></li>",
            fim_cat = "</ul><br/>";
            categoria.Id = 0;
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
            for (int i = 0; i < evade; i++)
            {
                categoria = (Categoria)res.Entidades.ElementAt(i);
                conteudo.AppendFormat(li,
                       categoria.Id.ToString(),
                       categoria.Nome.ToString()
                    );

            }
            return string.Format(comeco_cat, conteudo,fim_cat);


        }
        private string pesquisar_pro(string cabecalho)
        {
            int evade;
            string GRID = "<TABLE class='display' id='GridViewpro'><TBODY>{0}</TBODY></TABLE>";
            string linha = "<tr><a href='Default.aspx?cod={0}'><td> ";
            linha += "</td><td><img src=\"{1}\" style=\"width: 100px; height: 100px;\" /><br /></td><td>{2}</td><br /><td>{3}</td><br /></a></tr> ";

            ImageConverter ic = new ImageConverter();
            prod.Id = 0;
            prod.Categoria.Id = 0;
            prod.Nome = "";
            categoria.Id = 0;
            prod.Categoria=new Categoria();
            
            res = commands["CONSULTAR"].execute(prod);
            try
            {
                evade = res.Entidades.Count;
            }
            catch
            {
                evade = 0;
            }
            StringBuilder conteudo = new StringBuilder();
            for (int i = 0; i < evade; i++)
            {
                prod = (dominio.Produto)res.Entidades.ElementAt(i);
                string limitsofdead = @"data:" + prod.Extension + ";charset=utf-8;base64, " + (Convert.ToBase64String(prod.Img));

                conteudo.AppendFormat(linha,
                       prod.Id.ToString(),
                       limitsofdead,
                       prod.Nome.ToString(),
                       prod.Preco.ToString()
                      );

            }
            return cabecalho+ string.Format(GRID, conteudo.ToString());

        }
        private void Pesquisar()
        {
          detalhes.Text=  pesquisar_cat();
  //          detalhes.Text = pesquisar_pro(pesquisar_cat());
           
        }
    }
}

