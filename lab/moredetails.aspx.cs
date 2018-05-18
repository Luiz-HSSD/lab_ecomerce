using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;

namespace lab
{
    public partial class moredetails : viewgenerico
    {
        private Livro liv=new Livro();
        private static Gerar_produtos gp = new Gerar_produtos();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    liv.Id = int.Parse(Request.QueryString["cod"]);
                    gp.Colocar_preco.Add(liv);
                    res = commands["CONSULTAR"].execute(gp);
                    liv =(Livro) gp.Colocar_preco.ElementAt(0);
                    Image_liv.ImageUrl = @"data:" + liv.Extension + ";charset=utf-8;base64, " + (Convert.ToBase64String(liv.Img));
                    detalhes.InnerHtml = "titulo: "+liv.Nome+"</br> sinopse: "+liv.Descricao;
                    Session["liv"] = liv;
                }
                catch
                {
                    Response.Redirect("~/Default.aspx");
                }

            }
        }

        protected void comprar_Click(object sender, EventArgs e)
        {
            Response.Redirect("cart.aspx");
        }
        dominio.Frete frete_r = new dominio.Frete();
        protected void calcular_Click(object sender, EventArgs e)
        {
            if (frete.Text.Length == 8)
            {
                frete_r.Destino.Cep = frete.Text;
                liv=(Livro) Session["liv"];
                frete_r.Item = liv;
                res = commands["CONSULTAR"].execute(frete_r);
                frete_res.InnerHtml ="Custo: R$" + frete_r.Valor.ToString()+" <br/> Dias: "+frete_r.Prazo ;
            }
        }
    }
}