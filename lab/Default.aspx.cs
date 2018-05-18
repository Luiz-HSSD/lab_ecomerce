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
        private Categoria categoria = new Categoria();
        private List<Categoria> cat = new List<Categoria>();
        private static Livro prod = new Livro();
        private static Gerar_produtos gp = new Gerar_produtos();
        private List<Produto> pro = new List<Produto>();

        private void Pesquisar()
        {
            ViewLiv.InnerHtml = "";
            int evade = 0;
            string linha = "<section onclick=\"window.location.href='./moredetails.aspx?cod={2}'\"><div><img src=\"{0}\" style=\"width: 200px; height: 200px;\" /></div><div>{1} R$ {3}</div></section>";
            prod.Id = 0;
            prod.Nome = "";
            prod.Categoria.Id = 0;
            prod.Categoria.Nome = "";
            gp.Colocar_preco.Add(prod);
            res=commands["CONSULTAR"].execute(gp);

            
            string limitsofdead;
            try
            {
                evade = gp.Colocar_preco.Count;
            }
            catch
            {
                evade = 0;
            }
            
            StringBuilder conteudo = new StringBuilder();
            for (int i = 0; i < evade; i++)
            {
                prod =(Livro) gp.Colocar_preco.ElementAt(i);
                try
                {
                    limitsofdead = @"data:" + prod.Extension + ";charset=utf-8;base64, " + (Convert.ToBase64String(prod.Img));
                }
                catch
                {
                    limitsofdead = @"data:" + prod.Extension + ";charset=utf-8;base64, ";
                }

                conteudo.AppendFormat(linha,
                        limitsofdead,
                        prod.Nome,
                        prod.Id,
                        prod.Preco.ToString("0.00")  
                        );

            }
            string tabelafinal = string.Format(conteudo.ToString());
            tabelafinal += "<br/>";
            ViewLiv.InnerHtml = tabelafinal;


        }


        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                if (!IsPostBack)
                {
                    categoria.Id = 0;
                    Pesquisar();
                    if (!string.IsNullOrEmpty(Request.QueryString["cod"]))
                    {

                        prod.Id = Convert.ToInt32(Request.QueryString["cod"]);
                        res = commands["CONSULTAR"].execute(prod);
                        prod = (Livro)res.Entidades.ElementAt(0);

                    }

                }
            }
            catch (Exception ea)
            {
                throw ea;
                //     Response.Redirect("~/Default.aspx", false);
            }

        }

    }
}

