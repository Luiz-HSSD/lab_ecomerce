using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using core.aplicacao;
using core.Utils;
using dominio;
namespace lab.Manager
{
    public partial class clientes : viewgenerico
    {

        private Cliente categoria = new Cliente();
        
        DataRow CreateRow(char Value, string Text,  DataTable dt)
        {

            // Create a DataRow using the DataTable defined in the 
            // CreateDataSource method.
            DataRow dr = dt.NewRow();

            // This DataRow contains the ColorTextField and ColorValueField 
            // fields, as defined in the CreateDataSource method. Set the 
            // fields with the appropriate value. Remember that column 0 
            // is defined as ColorTextField, and column 1 is defined as 
            // ColorValueField.
            dr[1] = Text;
            dr[0] = Value;

            return dr;

        }
        DataRow CreateRow(int Value, string Text, DataTable dt)
        {

            // Create a DataRow using the DataTable defined in the 
            // CreateDataSource method.
            DataRow dr = dt.NewRow();

            // This DataRow contains the ColorTextField and ColorValueField 
            // fields, as defined in the CreateDataSource method. Set the 
            // fields with the appropriate value. Remember that column 0 
            // is defined as ColorTextField, and column 1 is defined as 
            // ColorValueField.
            dr[1] = Text;
            dr[0] = Value;

            return dr;

        }


        protected void Page_Load(object sender, EventArgs e)
        {
            try {



                if (!IsPostBack)
                    {
                    DataTable vai = new DataTable();
                    vai.Columns.Add(new DataColumn("bd", typeof(char)));
                    vai.Columns.Add(new DataColumn("sexoid", typeof(string)));

                    // Populate the table with sample values.
                    vai.Rows.Add(CreateRow('m', "Masculino", vai));
                    vai.Rows.Add(CreateRow('f', "Feminino", vai));

                    DropDownListcli.DataSource = vai;
                    DropDownListcli.DataBind();
                    DataTable uf = new DataTable();
                    uf.Columns.Add(new DataColumn("id", typeof(int)));
                    uf.Columns.Add(new DataColumn("uf", typeof(string)));

                    // Populate the table with sample values.
                    uf.Rows.Add(CreateRow('1', "SP", uf));
                    uf.Rows.Add(CreateRow('2', "RJ", uf));
                    uf.Rows.Add(CreateRow('3', "AM", uf));
                    uf.Rows.Add(CreateRow('4', "AC", uf));
                    uf.Rows.Add(CreateRow('5', "MG", uf));
                    DropDownListcliuf.DataSource = uf;
                    DropDownListcliuf.DataBind();
                    
                        Pesquisar();
                        if (!string.IsNullOrEmpty(Request.QueryString["cod"]))
                        {

                            categoria.Id = Convert.ToInt32(Request.QueryString["cod"]);
                            res = commands["CONSULTAR"].execute(categoria);

                            categoria.Id = Convert.ToInt32(codigo.Text);
                            categoria.Nome = nome.Text;
                            categoria.Sexo = Convert.ToChar(DropDownListcli.SelectedValue);
                            categoria.Cpf = cpf.Text;
                            categoria.Rg = rg.Text;
                            categoria.Dt_Nas = Convert.ToDateTime(data.Text);
                            categoria.Email = email.Text;
                            categoria.Endereco.Numero = numero.Text;
                            categoria.Endereco.Logradouro = logradouro.Text;
                            categoria.Endereco.Bairro = bairro.Text;
                            categoria.Endereco.Cidade = cidade.Text;
                            categoria.Endereco.Cep = cep.Text;
                            categoria.Endereco.UF = DropDownListcliuf.SelectedItem.Text;
                       
                        }

                    
                         


                else
                {
                    //verificr edição
                    if (!string.IsNullOrEmpty(Request.QueryString["del"]))
                    {
                        categoria.Id = Convert.ToInt32(Request.QueryString["del"]);
                        commands["EXCLUIR"].execute(categoria);
                        Response.Redirect("clientes.aspx", false);
                    }

                }
                            }
                        
                
            }
            catch 
            {
                Response.Redirect("~/Default.aspx", false);
            }
        }
                    //carregando caixa listagem



                
            
        private void Pesquisar()
        {
            int evade = 0;
            string GRID = "<TABLE class='display' id='GridViewcli'><THEAD>{0}</THEAD><TBODY>{1}</TBODY></TABLE>";
            string tituloColunas = "<tr>  <th></th><th>Código</th><th  colspan='5'>Nome</th><th>Sexo</th><th>CPF</th><th>RG</th><th>Nascimento</th><th>EMAIL</th><th>logradouro</th><th>Email</th><th>Bairro</th><th>Cidade</th><th>CEP</th><th>UF</th></tr>";
            string linha = "<tr><td> <a href='clientes.aspx?cod={0}'>editar</a> ";
            linha += "<a href='clientes.aspx?del={0}'>apagar</a></td><td>{0}</td><td colspan='5'>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td>{6}</td><td>{7}</td><td>{8}</td><td>{9}</td><td>{10}</td><td>{11}</td><td>{12}</td></tr>";


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
                categoria = (Cliente)res.Entidades.ElementAt(i);
                conteudo.AppendFormat(linha,
                        categoria.Id.ToString(),
                        categoria.Nome,
                        categoria.Sexo.ToString(),
                        categoria.Cpf,
                        categoria.Rg,
                        categoria.Dt_Nas.ToString(),
                        categoria.Email,
                        categoria.Endereco.Numero.ToString(),
                        categoria.Endereco.Logradouro,
                        categoria.Endereco.Bairro,
                        categoria.Endereco.Cidade,
                        categoria.Endereco.Cep,
                        categoria.Endereco.UF,
                        categoria.Endereco.Complemento
                        );

            }
            string tabelafinal = string.Format(GRID, tituloColunas, conteudo.ToString());
            divTable.InnerHtml = tabelafinal;
            categoria.Id = 0;
        }

    
        



        protected void novo_cli_Click(object sender, EventArgs e)
        {

            categoria.Id = Convert.ToInt32(codigo.Text);
            categoria.Nome = nome.Text;
            categoria.Sexo = Convert.ToChar(DropDownListcli.SelectedValue);
            categoria.Cpf = cpf.Text;
            categoria.Rg = rg.Text;
            categoria.Dt_Nas = Convert.ToDateTime(data.Text);
            categoria.Email = email.Text;
            categoria.Endereco.Numero = numero.Text;
            categoria.Endereco.Logradouro = logradouro.Text;
            categoria.Endereco.Bairro = bairro.Text;
            categoria.Endereco.Cidade = cidade.Text;
            categoria.Endereco.Cep = cep.Text;
            categoria.Endereco.UF = DropDownListcliuf.SelectedItem.Text;
            categoria.Endereco.Complemento = complemento.Text;
            commands["SALVAR"].execute(categoria);
            codigo.Text = "";
            nome.Text = "";
            cpf.Text = "";
            rg.Text = "";
            data.Text = "";
            logradouro.Text = "";
            email.Text = "";
            bairro.Text = "";
            cidade.Text = "";
            cep.Text = "";
            complemento.Text = "";
            Response.Redirect("clientes.aspx");
            
        }
        protected void alterar_cli_Click(object sender, EventArgs e)
        {

            categoria.Id=Convert.ToInt32(codigo.Text);
            categoria.Nome = nome.Text ;
            categoria.Sexo = Convert.ToChar( DropDownListcli.SelectedValue);
            categoria.Cpf =  cpf.Text ;
            categoria.Rg = rg.Text ;
            categoria.Dt_Nas = Convert.ToDateTime(data.Text);
            categoria.Email = email.Text ;
            categoria.Endereco.Numero = numero.Text;
            categoria.Endereco.Logradouro = logradouro.Text ;
            categoria.Endereco.Bairro = bairro.Text;
            categoria.Endereco.Cidade =  cidade.Text ;
            categoria.Endereco.Cep = cep.Text ;
            categoria.Endereco.UF =DropDownListcliuf.SelectedItem.Text;
            categoria.Endereco.Complemento = complemento.Text;
            commands["ALTERAR"].execute(categoria);
            codigo.Text = "";
            nome.Text = "";
            cpf.Text = "";
            rg.Text = "";
            data.Text = "";
            logradouro.Text = "";
            email.Text = "";
            bairro.Text = "";
            cidade.Text = "";
            cep.Text = "";
            complemento.Text = "";
            Response.Redirect("clientes.aspx");

        }
        protected void cancelar_cli_Click(object sender, EventArgs e)
        {
            codigo.Text ="";
            nome.Text = "";
            cpf.Text ="";
            rg.Text ="";
            data.Text ="";
            logradouro.Text = "";
            email.Text = "";
            bairro.Text = "";
            cidade.Text ="";
            cep.Text = "";
            complemento.Text = "";
            Response.Redirect("clientes.aspx");

        }
        protected void excluir_cli_Click(object sender, EventArgs e)
        {
            if (codigo.Text == "") return;
            categoria.Id =Convert.ToInt32(codigo.Text);
            commands["EXCLUIR"].execute(categoria);
            codigo.Text = "";
            nome.Text = "";
            cpf.Text = "";
            rg.Text = "";
            data.Text = "";
            logradouro.Text = "";
            email.Text = "";
            bairro.Text = "";
            cidade.Text = "";
            cep.Text = "";
            complemento.Text="";
            Response.Redirect("clientes.aspx");
        }

        protected void cep_TextChanged(object sender, EventArgs e)
        {
            if (cep.Text.Length == 8)
            {
                categoria.Endereco.Cep= cep.Text;
                res=commands["WS"].execute(categoria.Endereco);
                categoria.Endereco = (Endereco)res.Entidades.ElementAt(0);
                logradouro.Text = categoria.Endereco.Logradouro;
                cep.Text = categoria.Endereco.Cep;
                cidade.Text = categoria.Endereco.Cidade;
                bairro.Text = categoria.Endereco.Bairro;
                for(int i=0;i< DropDownListcliuf.Items.Count; i++)
                {
                    if (DropDownListcliuf.Items[i].Text == categoria.Endereco.UF)
                        DropDownListcliuf.SelectedValue= DropDownListcliuf.Items[i].Value;
                }


            }
        }

        protected void add_endereco_Click(object sender, EventArgs e)
        {

        }
    }
}