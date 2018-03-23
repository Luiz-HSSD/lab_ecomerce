using System;
using System.Data;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;
using System.Drawing;
using System.Linq;
using core.Utils;

namespace lab.Manager
{

    public partial class livros : viewgenerico
    {
        private string fromRootToPhotos = @"C://inetpub/wwwroot/photos/";
        private string fromPhotosToExtension;
        private dominio.Livro pro=new dominio.Livro();
        private dominio.Categoria categoria = new dominio.Categoria();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (!Directory.Exists(fromRootToPhotos))
                        Directory.CreateDirectory(fromRootToPhotos);
                    categoria.Id=0;
                    res = commands["CONSULTAR"].execute(categoria);
                    ListBoxcat.DataSource = ResultadoToDataTable.cat_to_datatable(res);
                    ListBoxcat.DataBind();
                    Pesquisar();
                    if (!string.IsNullOrEmpty(Request.QueryString["cod"]))
                    {

                        pro.Id=Convert.ToInt32(Request.QueryString["cod"]);
                        res = commands["CONSULTAR"].execute(pro);
                        pro = (dominio.Livro)res.Entidades.ElementAt(0);
                        codigo.Text = pro.Id.ToString();
                        nome.Text = pro.Nome;
                        peso.Text = pro.Formato.Peso.ToString();
                        dimensoes.Text = pro.Formato.Dimensoes;
                        descricao.Text = pro.Descricao.ToString();
                        ListBoxcat.SelectedValue = pro.Categoria.Id.ToString();
                        codigo_de_barra.Text = pro.Codigo_barras;
                        fabricante.Text = pro.Fabricante;
                        preco.Text = pro.Preco.ToString();
                        Imagems.SaveByteArrayAsImage(fromRootToPhotos + "from_bd" + DateTime.Now.Ticks.ToString(), pro.Img, pro.Extension);

                }
                        

                    
                    else
                    {
                        //verificr edição
                        if (!string.IsNullOrEmpty(Request.QueryString["del"]))
                        {
                            pro.Id=Convert.ToInt32(Request.QueryString["del"]);
                            commands["EXCLUIR"].execute(pro);
                            Response.Redirect("produto.aspx", false);
                        }

                    }
                
                    //carregando caixa listagem
                    DisplayUploadedPhotos(fromRootToPhotos);
                    msg.Text = res.Msg;


                }
            }
            catch(Exception ea)
            {
                throw ea;
           //     Response.Redirect("~/Default.aspx", false);
            }
        }

        private void Pesquisar()
        {
            int evade;
            string GRID = "<TABLE class='display' onload=\"bora()\" id='GridViewliv'><THEAD>{0}</THEAD><TBODY>{1}</TBODY></TABLE>";
            string tituloColunas = "<tr><th></th><th>Código</th><th>Nome Resumido</th><th>Descrição</th><th>Categoria</th><th>Código de Barras</th><th>Fabricante</th><th>Preço</th><th>imagem</th>";
            string linha = "<tr><td> <a href='produto.aspx?cod={0}'>editar</a> ";
            linha += "<a href='produto.aspx?del={0}'>apagar</a></td><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td>{6}</td><td><img src=\"{7}\" style=\"width: 100px; height: 100px;\" /><br /></td></tr>";

            ImageConverter ic = new ImageConverter();
            pro.Id=0;
            categoria.Id=0;
            pro.Categoria=categoria;
            pro.Nome = "";
            res = commands["CONSULTAR"].execute(pro);
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
                pro= (dominio.Livro)res.Entidades.ElementAt(i);
                string limitsofdead = @"data:" + pro.Extension + ";charset=utf-8;base64, " + (Convert.ToBase64String(pro.Img));

                 conteudo.AppendFormat(linha,
                        pro.Id.ToString(),
                        pro.Nome.ToString(),
                        pro.Descricao.ToString(),
                        pro.Categoria.Nome.ToString(),
                        pro.Codigo_barras.ToString(),
                        pro.Fabricante.ToString(),
                        pro.Preco.ToString(),
                        limitsofdead
                       );

                }
                string tabelafinal = string.Format(GRID, tituloColunas, conteudo.ToString());
                divTable.InnerHtml = tabelafinal;
            msg.Text = res.Msg;
        }
        


        protected void novo_pro_Click(object sender, EventArgs e)
        {
            string fileToBD = "";
            foreach (ListItem listItem in ListBoxcat.Items)
            {
                if (listItem.Selected)
                {
                    var cat = new dominio.Categoria();
                    cat.Id = Convert.ToInt32(listItem.Value);
                    cat.Nome = listItem.Text;
                    pro.Generos.Add(cat);
                }
            }
            pro.Categoria=categoria ;
            pro.Preco=Convert.ToDouble( preco.Text);
            pro.Codigo_barras=codigo_de_barra.Text;
            pro.Fabricante=fabricante.Text ;
            pro.Nome=nome.Text ;
            pro.Descricao=descricao.Text ;
            pro.Formato.Dimensoes = dimensoes.Text;
            pro.Formato.Peso=peso.Text;
            int cont;
            cont = 0;

            foreach (RepeaterItem ri in rptrUserPhotos.Items)
            {
                CheckBox cb = ri.FindControl("cbDelete") as CheckBox;

                if (cb.Checked)
                {
                    fromPhotosToExtension = cb.Attributes["special"];

                    fileToBD = fromRootToPhotos + fromPhotosToExtension.Substring(9);
                    cont++;

                }

            }
            if (cont != 1)
            {
                lblStatus.Text = "Selecione apenas uma imagem.";
                return;
            }
            switch (Path.GetExtension(fromPhotosToExtension) )
            {
                case ".jpg":
                    pro.Extension="image/jpeg";
                    break;
                case ".png":
                    pro.Extension="image/png";
                    break;
                case ".bmp":
                    pro.Extension="image/bmp";
                    break;
            }
            pro.Img=(Imagems.ReadFile(fileToBD));
            res=commands["SALVAR"].execute(pro);
            codigo.Text = "";
            nome.Text = "";
            descricao.Text = "";
            codigo_de_barra.Text = "";
            fabricante.Text = "";
            preco.Text = "";
            msg.Text = res.Msg;
            Pesquisar();
            return;
        }
        protected void alterar_pro_Click(object sender, EventArgs e)
        {
            string fileToBD = "";
            pro.Id=Convert.ToInt32(codigo.Text);
            pro.Generos.Clear();
            foreach (ListItem listItem in ListBoxcat.Items)
            {
                if (listItem.Selected)
                {
                    var cat = new dominio.Categoria();
                    cat.Id = Convert.ToInt32(listItem.Value);
                    cat.Nome = listItem.Text;   
                    pro.Generos.Add(cat);
                }
            }
            pro.Categoria = categoria;
            pro.Preco = Convert.ToDouble(preco.Text);
            pro.Codigo_barras = codigo_de_barra.Text;
            pro.Fabricante = fabricante.Text;
            pro.Nome = nome.Text;
            pro.Descricao = descricao.Text;
            pro.Formato.Peso = peso.Text;
            pro.Formato.Dimensoes = dimensoes.Text;
            int cont;
            cont = 0;

            foreach (RepeaterItem ri in rptrUserPhotos.Items)
            {
                CheckBox cb = ri.FindControl("cbDelete") as CheckBox;

                if (cb.Checked)
                {
                    fromPhotosToExtension = cb.Attributes["special"];

                    fileToBD = fromRootToPhotos + fromPhotosToExtension.Substring(9);
                    cont++;

                }

            }
            if (cont != 1)
            {
                lblStatus.Text = "Selecione apenas uma imagem.";
                return;
            }
            switch (Path.GetExtension(fromPhotosToExtension))
            {
                case ".jpg":
                    pro.Extension = "image/jpeg";
                    break;
                case ".png":
                    pro.Extension = "image/png";
                    break;
                case ".bmp":
                    pro.Extension = "image/bmp";
                    break;
            }
            pro.Img = (Imagems.ReadFile(fileToBD));
            res = commands["SALVAR"].execute(pro);
            codigo.Text = "";
            nome.Text = "";
            descricao.Text = "";
            codigo_de_barra.Text = "";
            fabricante.Text = "";
            preco.Text = "";
            msg.Text = res.Msg;
            Pesquisar();
            return;
            
        }
        protected void cancelar_pro_Click(object sender, EventArgs e)
        {
            codigo.Text = "";
            nome.Text = "";
            descricao.Text = "";
            codigo_de_barra.Text = "";
            fabricante.Text = "";
            preco.Text = "";
            Response.Redirect("produto.aspx");
        }

        protected void subir_Click(object sender, EventArgs e)
        {
            if (foto.HasFile)
            {
                if ((foto.PostedFile.ContentType == "image/jpeg") ||
                    (foto.PostedFile.ContentType == "image/png") ||
                    (foto.PostedFile.ContentType == "image/bmp") )
                {
                    if (Convert.ToInt64(foto.PostedFile.ContentLength) < 10000000)
                    {
                        string photoFolder = fromRootToPhotos;

                        if (!Directory.Exists(photoFolder))
                            Directory.CreateDirectory(photoFolder);

                        string extension = Path.GetExtension(foto.FileName);
                        string uniqueFileName = Path.ChangeExtension(foto.FileName, DateTime.Now.Ticks.ToString());

                        foto.SaveAs(Path.Combine(fromRootToPhotos, uniqueFileName + extension));

                        DisplayUploadedPhotos(photoFolder);

                        lblStatus.Text = "<font color='Green'>Successfully uploaded " + foto.FileName + "</font>";
                    }
                    else
                        lblStatus.Text = "File must be less than 10 MB.";
                }
                else
                    lblStatus.Text = "File must be of type jpeg, jpg, png, bmp, or gif.";
            }
            else
                lblStatus.Text = "No file selected to upload.";
        }

        public void DisplayUploadedPhotos(string folder)
        {
            string[] allPhotoFiles = Directory.GetFiles(folder);
            IList<string> allPhotoPaths = new List<string>();
            string fileName;

            foreach (string f in allPhotoFiles)
            {
                fileName = Path.GetFileName(f);
                allPhotoPaths.Add( "~/photos/"+ fileName);
            }

            rptrUserPhotos.DataSource = allPhotoPaths;
            rptrUserPhotos.DataBind();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {

                bool deletionOccurs = false;

                foreach (RepeaterItem ri in rptrUserPhotos.Items)
                {
                    CheckBox cb = ri.FindControl("cbDelete") as CheckBox;

                    if (cb.Checked)
                    {
                        string fromPhotosToExtension = cb.Attributes["special"];

                        string fileToDelete = fromRootToPhotos + fromPhotosToExtension.Substring(9);
                        File.Delete(fileToDelete);

                        lblStatus.Text = "<font color='Green'>Photo(s) successfully deleted.</font>";
                        deletionOccurs = true;
                    }
                }

                if (deletionOccurs)
                    DisplayUploadedPhotos(Path.Combine(fromRootToPhotos, User.Identity.Name));
                else
                    lblStatus.Text = "No file selected to delete.";

            }catch(Exception vad)
            {
                throw vad;
            }
            }
    }
}