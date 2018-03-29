using core.Utils;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace lab.Manager
{
    public partial class Motivo : viewgenerico
    {
        
            private dominio.Categoria pro = new dominio.Categoria();
            private dominio.Motivo mot = new dominio.Motivo();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    mot.Id = 0;
                    res = commands["CONSULTAR"].execute(mot);
                    txtnome.DataSource = ResultadoToDataTable.cat_to_datatable(res);
                    txtnome.DataBind();
                    Pesquisar();
                    if (!string.IsNullOrEmpty(Request.QueryString["cad"]))
                    {

                        pro.Id = Convert.ToInt32(Request.QueryString["cod"]);
                        pro.Nome = null;
                        res = commands[""].execute(pro);
                        pro = (dominio.Livro)res.Entidades.ElementAt(0);
                        codigo.Text = pro.Id.ToString();
                        nome.Text = pro.Nome;
                        peso.Text = pro.Formato.Peso.ToString();
                        dimensoes.Text = pro.Formato.Dimensoes;
                        descricao.Text = pro.Descricao.ToString();
                        ListBoxcat.SelectedValue = pro.Categoria.Id.ToString();
                        codigo_de_barra.Text = pro.Codigo_barras;
                        Editora.Text = pro.Editora;
                        Num_pags.Text = pro.N_pags.ToString();
                        Edicao.Text = pro.Edicao;
                        preco.Text = pro.Preco.ToString();
                        try
                        {
                            Imagems.SaveByteArrayAsImage(fromRootToPhotos + "from_bd" + DateTime.Now.Ticks.ToString(), pro.Img, pro.Extension);

                        }
                        catch
                        {

                        }


                    }



                    else
                    {
                        //verificr edição
                        if (!string.IsNullOrEmpty(Request.QueryString["del"]))
                        {
                            pro.Id = Convert.ToInt32(Request.QueryString["del"]);
                            Session["livro"] = pro;
                            commands["EXCLUIR"].execute(pro);
                            Response.Redirect("Motivo.aspx", false);
                        }

                    }

                    //carregando caixa listagem
                    DisplayUploadedPhotos(fromRootToPhotos);
                    msg.Text = res.Msg;


                }
            }
            catch (Exception ea)
            {
                throw ea;
                //     Response.Redirect("~/Default.aspx", false);
            }
        }
        private void Pesquisar()
            {
                int evade = 0;
                string GRID = "<TABLE class='display' onload=\"bora()\" id='GridViewcat'><THEAD>{0}</THEAD><TBODY>{1}</TBODY></TABLE>";
                string tituloColunas = "<tr><th></th><th>Código</th><th>Nome</th><th>Descrição</th></tr>";
                string linha = "<tr><td> <a href='categoria.aspx?cod={0}'>editar</a> ";
                linha += "<a href='categoria.aspx?del={0}'>apagar</a></td><td>{0}</td><td>{1}</td><td>{2}</td></tr>";

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
                    categoria = (dominio.Categoria)res.Entidades.ElementAt(i);
                    conteudo.AppendFormat(linha,
                        categoria.Id.ToString(),
                        categoria.Nome.ToString(),
                        categoria.Descricao.ToString());


                }
                string tabelafinal = string.Format(GRID, tituloColunas, conteudo.ToString());
                divTable.InnerHtml = tabelafinal;
                categoria.Id = 0;
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
            protected void excluir_cat_Click(object sender, EventArgs e)
            {
                if (txtcod.Text.Equals("")) return;
                categoria.Id = Convert.ToInt32(txtcod.Text);
                categoria.Nome = txtnome.Text;
                categoria.Descricao = txtdescricao.Text;
                commands["EXCLUIR"].execute(categoria);
                txtcod.Text = "";
                txtnome.Text = "";
                txtdescricao.Text = "";
                Response.Redirect("categoria.aspx");
                Pesquisar();
            }


    }
}
 