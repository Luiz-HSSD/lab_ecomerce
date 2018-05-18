using System;
using System.Data;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;
using System.Drawing;
using System.Linq;
using core.Utils;
using dominio;
namespace lab.Manager
{

    public partial class livros : viewgenerico
    {
        private string fromRootToPhotos = System.Web.HttpContext.Current.Server.MapPath( "~/photos/");
        private string fromPhotosToExtension;
        private Livro pro=new dominio.Livro();
        private dominio.Categoria categoria = new dominio.Categoria();
        private Grupo_Precificacao g_preco = new Grupo_Precificacao();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (!Directory.Exists(fromRootToPhotos))
                        Directory.CreateDirectory(fromRootToPhotos);
                    categoria.Id=0;
                    categoria.Nome = null;
                    res = commands["CONSULTAR"].execute(categoria);
                    ListBoxcat.DataSource = ResultadoToDataTable.cat_to_datatable(res);
                    ListBoxcat.DataBind();
                    g_preco.Id = 0;
                    g_preco.Nome = null;
                    res = commands["CONSULTAR"].execute(g_preco);
                    preco.DataSource = ResultadoToDataTable.g_pre_to_datatable(res);
                    preco.DataBind();
                    Pesquisar();
                    if (!string.IsNullOrEmpty(Request.QueryString["cod"]))
                    {

                        pro.Id=Convert.ToInt32(Request.QueryString["cod"]);
                        pro.Nome = null;
                        res = commands["CONSULTAR"].execute(pro);
                        pro = (dominio.Livro)res.Entidades.ElementAt(0);
                        codigo.Text = pro.Id.ToString();
                        nome.Text = pro.Nome;
                        peso.Text = pro.Formato.Peso.ToString();
                        dimensoes.Text = pro.Formato.Dimensoes;
                        descricao.Text = pro.Descricao.ToString();
                        ///ListBoxcat.SelectedValue = pro.Categoria.Id.ToString();
                        for(int i = 0; i < pro.Generos.Count; i++)
                        {
                            for (int j = 0; j < ListBoxcat.Items.Count; j++)
                            {
                                if (pro.Generos[i].Id.ToString() == ListBoxcat.Items[j].Value)
                                {
                                    ListBoxcat.Items[j].Selected = true;
                                }
                            }
                        }
                        codigo_de_barra.Text = pro.Codigo_barras;
                        ISBN.Text = pro.ISBN;
                        Editora.Text = pro.Editora;
                        Num_pags.Text = pro.N_pags.ToString();
                        Edicao.Text = pro.Edicao;
                        for(int i = 0; i < preco.Items.Count; i++)
                        {
                            if(preco.Items[i].Value==pro.G_PRECO.Id.ToString())
                            preco.SelectedIndex = i;
                        }
                        try
                        {
                            string vai="";
                            switch (pro.Extension)
                            {
                                case "image/jpeg":
                                     vai= ".jpg";
                                    break;
                                case "image/png":
                                    vai = ".png";
                                    break;
                                case "image/bmp":
                                    vai = ".bmp";
                                    break;
                            }
                            File.WriteAllBytes(fromRootToPhotos + "from_bd" + DateTime.Now.Ticks.ToString()+vai, pro.Img);
                            //Imagems.SaveByteArrayAsImage(fromRootToPhotos + "from_bd" + DateTime.Now.Ticks.ToString(), pro.Img, vai);

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
                            pro.Id=Convert.ToInt32(Request.QueryString["del"]);
                            //Session["livro"] = pro;
                            commands["EXCLUIR"].execute(pro);
                            //Response.Redirect("Motivo.aspx?del="+pro.Id, false);
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
            string tituloColunas = "<tr><th></th><th>Código</th><th>Nome Resumido</th><th>Descrição</th><th>Categoria</th><th>Código de Barras</th><th>Editora</th><th>paginas</th><th>Edicao</th><th>Preço</th><th>categorias</th><th>imagem</th>";
            string linha = "<tr><td> <a href='livros.aspx?cod={0}'>editar</a> ";
            linha += "<a href='livros.aspx?del={0}'>apagar</a></td><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td>{6}</td><td>{7}</td><td>{8}</td><td>{9}</td><td><img src=\"{10}\" style=\"width: 100px; height: 100px;\" /><br /></td></tr>";

            ImageConverter ic = new ImageConverter();
            pro.Id=0;
            categoria.Id=0;
            pro.Categoria=categoria;
            pro.Nome = null;
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
                string limitsofdead,generos;
                generos = "";
                try
                {
                     limitsofdead = @"data:" + pro.Extension + ";charset=utf-8;base64, " + (Convert.ToBase64String(pro.Img));
                }
                catch
                {
                    limitsofdead = @"data:" + pro.Extension + ";charset=utf-8;base64, ";
                }
                for(int j = 0; j < pro.Generos.Count; j++)
                {
                    generos+=pro.Generos.ElementAt(j).Nome;
                    if (j < pro.Generos.Count - 1)
                        generos += ", ";
                }
                    
                 conteudo.AppendFormat(linha,
                        pro.Id.ToString(),
                        pro.Nome.ToString(),
                        pro.Descricao.ToString(),
                        pro.Categoria.Nome.ToString(),
                        pro.Codigo_barras.ToString(),
                        pro.Editora.ToString(),
                        pro.N_pags.ToString(),
                        pro.Edicao.ToString(),
                        pro.Preco.ToString(),
                        generos,
                        limitsofdead
                       );

                }
                string tabelafinal = string.Format(GRID, tituloColunas, conteudo.ToString());
                divTable.InnerHtml = tabelafinal;
            msg.Text = res.Msg;
        }

        private void getlivro()
        {
            if (!string.IsNullOrEmpty(codigo.Text))
                pro.Id = Convert.ToInt32(codigo.Text);
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
            pro.Categoria = categoria;
            try
            {
                pro.G_PRECO.Id = int.Parse(preco.Items[preco.SelectedIndex].Value);
            }
            catch
            {

            }
            pro.Codigo_barras = codigo_de_barra.Text;
            pro.ISBN = ISBN.Text;
            pro.Editora = Editora.Text;
            try
            {
                pro.N_pags = int.Parse(Num_pags.Text);
            }
            catch
            {

            }
            pro.Edicao = Edicao.Text;
            pro.Nome = nome.Text;
            pro.Descricao = descricao.Text;
            pro.Formato.Dimensoes = dimensoes.Text;
            pro.Formato.Peso = peso.Text;
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
        }

        protected void novo_pro_Click(object sender, EventArgs e)
        {
            getlivro();
            /*if (commands["CONSULTAR"].execute(pro).Entidades.Count>0)
            {
                Session["livro"] = pro;
                Response.Redirect("Motivo.aspx?cad=0", false);
            }
            else*/
            res =commands["SALVAR"].execute(pro);
            codigo.Text = "";
            nome.Text = "";
            descricao.Text = "";
            codigo_de_barra.Text = "";
            Editora.Text = "";
            Num_pags.Text = "";
            Edicao.Text = "";
            preco.SelectedIndex = 0;
            msg.Text = res.Msg;
            Pesquisar();
            return;
        }
        protected void alterar_pro_Click(object sender, EventArgs e)
        {
            getlivro();
            res = commands["ALTERAR"].execute(pro);
            codigo.Text = "";
            nome.Text = "";
            descricao.Text = "";
            codigo_de_barra.Text = "";
            Editora.Text = "";
            Num_pags.Text = "";
            Edicao.Text = "";
            preco.SelectedIndex = 0;
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
            Editora.Text = "";
            Num_pags.Text = "";
            Edicao.Text = "";
            preco.SelectedIndex = 0;
            Response.Redirect("livros.aspx");
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