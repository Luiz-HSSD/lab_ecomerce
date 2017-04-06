using System;

using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace lab.Manager
{
    public partial class vendas : viewgenerico
    {

        string[] values = new string[5];
        protected void Page_Load(object sender, EventArgs e)
        {
            try { 
            if (!IsPostBack)
            {
                if (!IsPostBack)
                {
                    DataTable cat = geren_bd.getdados("select cod,nome_cli from clientes");
                    DropDownListcli.DataSource = cat;
                    DropDownListcli.DataBind();
                    cat = geren_bd.getdados("select cod,nome_pro from produto");
                    DropDownListpro.DataSource = cat;
                    DropDownListpro.DataBind();

                    DataTable pro = geren_bd.getdados("select * from vendasview");
                    GridViewven.DataSource = pro;
                    /*GridViewcat.DataBind();*/
                    Pesquisar(pro, "vendasview");
                    if (!string.IsNullOrEmpty(Request.QueryString["cod"]))
                    {

                        string comando = "select * from vendas where cod=" + Request.QueryString["cod"];
                        DataTable tabela = geren_bd.getdados(comando);
                        if (tabela.Rows.Count > 0)
                        {
                            codigo.Text                   = tabela.Rows[0].ItemArray[0].ToString();
                            DropDownListcli.SelectedValue = tabela.Rows[0].ItemArray[1].ToString();
                            DropDownListpro.SelectedValue = tabela.Rows[0].ItemArray[2].ToString();
                            qtd.Text                      = tabela.Rows[0].ItemArray[3].ToString();
                            preco.Text                    = tabela.Rows[0].ItemArray[4].ToString();

                        }

                    }
                    else
                    {
                        //verificr edição
                        if (!string.IsNullOrEmpty(Request.QueryString["del"]))
                        {

                            geren_bd.excluir("vendas", Request.QueryString["del"]);
                            Response.Redirect("vendas.aspx", false);
                        }

                    }

                    //carregando caixa listagem


                }
            }
            }
            catch
            {
                Response.Redirect("~/Default.aspx", false);
            }
        }
                
            
        private void Pesquisar(DataTable cat, string tab)
        {
            string comando = "select * from " + tab;
            DataTable tabela = cat;

            string GRID = "<TABLE class='display' id='GridViewcat'><THEAD>{0}</THEAD><TBODY>{1}</TBODY></TABLE>";
            string tituloColunas= "<tr><th></th><th>Código</th><th>Cliente</th><th>Produto</th><th>quantidade</th><th>Preço</th>";
            string linha = "<tr><td> <a href='vendas.aspx?cod={0}'>editar</a> ";
            linha += "<a href='vendas.aspx?del={0}'>apagar</a></td><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td></tr>";



            StringBuilder conteudo = new StringBuilder();
            foreach (DataRow item in tabela.Rows)
            {
                conteudo.AppendFormat(linha,
                    item.ItemArray[0].ToString(),
                    item.ItemArray[1].ToString(),
                    item.ItemArray[2].ToString(),
                    item.ItemArray[3].ToString(),
                    item.ItemArray[4].ToString());


            }
            string tabelafinal = String.Format(GRID, tituloColunas, conteudo.ToString());
            divTable.InnerHtml = tabelafinal;

        }
       

        
        protected void novo_ven_Click(object sender, EventArgs e)
        {
            values[0] = "" + DropDownListcli.SelectedValue + " , ";
            values[1] = "" + DropDownListpro.SelectedValue + ", ";
            values[2] =""+qtd.Text+" , ";
            values[3] = ""+preco.Text;
            geren_bd.inserir("vendas", values);
            codigo.Text = "";
            qtd.Text = "";
            preco.Text = "";
            Response.Redirect("./vendas.aspx");
        }
        protected void alterar_ven_Click(object sender, EventArgs e)
        {
            values[0] = codigo.Text + " , ";
            values[1] = "" + DropDownListcli.SelectedValue + " , ";
            values[2] = "" + DropDownListpro.SelectedValue + ", ";
            values[3] = "" + qtd.Text + " , ";
            values[4] = "" + preco.Text;
            geren_bd.alterar("vendas", values);
            codigo.Text = "";
            qtd.Text = "";
            preco.Text = "";
            Response.Redirect("./vendas.aspx");
        }
        protected void cancelar_ven_Click(object sender, EventArgs e)
        {
            codigo.Text = "";
            qtd.Text = "";
            preco.Text = "";
        }
    }
}