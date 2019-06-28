using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
namespace lab.Manager
{
    public partial class mudar_senha : viewgenerico
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["cli"] == null)
                Response.Redirect("clientes.aspx");

        }

        protected void salvar_Click(object sender, EventArgs e)
        {
            if (senha.Text == confirmar.Text)
            {
                Cliente a = (Cliente)Session["cli"];
                a.usuario.Password = senha.Text;
                res= commands["ALTERAR"].execute(a);
                Label1.Text = res.Msg;
                Session["cli"] = a;
            }
            else
            {
                Label1.Text="senhas não correspondem";
            }

        }
    }
}