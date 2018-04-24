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
        //carregando caixa listagem
        List<Endereco> cache_end = new List<Endereco>();
        List<Cartao_Credito> cache_car = new List<Cartao_Credito>();
        Endereco end_bus = new Endereco();
        Cartao_Credito car_bus = new Cartao_Credito();
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
                    uf.Rows.Add(CreateRow(1, "AC", uf));
                    uf.Rows.Add(CreateRow(2, "AL", uf));
                    uf.Rows.Add(CreateRow(3, "AP", uf));
                    uf.Rows.Add(CreateRow(4, "AM", uf));
                    uf.Rows.Add(CreateRow(5, "BA", uf));
                    uf.Rows.Add(CreateRow(6, "CE", uf));
                    uf.Rows.Add(CreateRow(7, "DF", uf));
                    uf.Rows.Add(CreateRow(8, "ES", uf));
                    uf.Rows.Add(CreateRow(9, "GO", uf));
                    uf.Rows.Add(CreateRow(10, "MA", uf));
                    uf.Rows.Add(CreateRow(11, "MT", uf));
                    uf.Rows.Add(CreateRow(12, "MS", uf));
                    uf.Rows.Add(CreateRow(13, "MG", uf));
                    uf.Rows.Add(CreateRow(14, "PA", uf));
                    uf.Rows.Add(CreateRow(15, "PB", uf));
                    uf.Rows.Add(CreateRow(16, "PR", uf));
                    uf.Rows.Add(CreateRow(17, "PE", uf));
                    uf.Rows.Add(CreateRow(18, "PI", uf));
                    uf.Rows.Add(CreateRow(19, "RJ", uf));
                    uf.Rows.Add(CreateRow(20, "RN", uf));
                    uf.Rows.Add(CreateRow(21, "RS", uf));
                    uf.Rows.Add(CreateRow(22, "RO", uf));
                    uf.Rows.Add(CreateRow(23, "RR", uf));
                    uf.Rows.Add(CreateRow(24, "SC", uf));
                    uf.Rows.Add(CreateRow(25, "SP", uf));
                    uf.Rows.Add(CreateRow(26, "SE", uf));
                    uf.Rows.Add(CreateRow(27, "TO", uf));
                    
                    DropDownListcliuf.DataSource = uf;
                    DropDownListcliuf.DataBind();

                    DataTable tipo_end = new DataTable();
                    tipo_end.Columns.Add(new DataColumn("id", typeof(int)));
                    tipo_end.Columns.Add(new DataColumn("tipo", typeof(string)));

                    // Populate the table with sample values.
                    tipo_end.Rows.Add(CreateRow(1, "Cobrança", tipo_end));
                    tipo_end.Rows.Add(CreateRow(2, "Entrega", tipo_end));
                    
                    DropDownList_tipo_end.DataSource = tipo_end;
                    DropDownList_tipo_end.DataBind();
                    if (Session["car_cache"] == null)
                        Session["car_cache"] = new List<Cartao_Credito>();
                    if (Session["end_cache"] == null)
                        Session["end_cache"] = new List<Endereco>();
                    Pesquisar();
                        if (!string.IsNullOrEmpty(Request.QueryString["cod"]))
                        {
                                categoria.Id = Convert.ToInt32(Request.QueryString["cod"]);
                                res = commands["CONSULTAR"].execute(categoria);
                                categoria = (Cliente)res.Entidades.ElementAt(0);
                                Session["cli"] = categoria;
                                cache_car = categoria.Cartoes;
                                Session["car_cache"] = cache_car;
                                PesquisarCartao_Credito();
                                cache_end = categoria.Enderecos;
                                Session["end_cache"] = cache_end;
                                PesquisarEnderecos();
                                codigo.Text = categoria.Id.ToString();
                                senha.Text = categoria.Senha;
                                nome.Text  = categoria.Nome;
                                cpf.Text =categoria.Cpf;
                                rg.Text=categoria.Rg;
                                data.Text = categoria.Dt_Nas.ToString("yyyy-MM-dd");
                                email.Text = categoria.Email;
                                for (int j = 0; j < DropDownListcli.Items.Count; j++)
                                {
                                    if (categoria.Sexo.ToString() == DropDownListcli.Items[j].Value)
                                    {
                                        DropDownListcli.Items[j].Selected = true;
                                    }
                                }    
                            
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
                            else if (!string.IsNullOrEmpty(Request.QueryString["del_end"]))
                            {

                                    cache_end =(List<Endereco>) Session["end_cache"];
                                    int a= Convert.ToInt32(Request.QueryString["del_end"]);
                                    for (int i = 0; i < cache_end.Count; i++)
                                    {
                                            if (a == cache_end.ElementAt(i).Id)
                                            {
                                                cache_end.RemoveAt(i);
                                                Session["end_cache"] = cache_end;
                                                PesquisarEnderecos();
                                                categoria.Enderecos = cache_end;
                                                Session["cli"] = categoria;
                                                cache_car = categoria.Cartoes;
                                                Session["car_cache"] = cache_car;
                                                PesquisarCartao_Credito();
                                                codigo.Text = categoria.Id.ToString();
                                                senha.Text = categoria.Senha;
                                                nome.Text = categoria.Nome;
                                                cpf.Text = categoria.Cpf;
                                                rg.Text = categoria.Rg;
                                                data.Text = categoria.Dt_Nas.ToString("yyyy-MM-dd");
                                                email.Text = categoria.Email;
                                                for (int j = 0; j < DropDownListcli.Items.Count; j++)
                                                {
                                                    if (categoria.Sexo.ToString() == DropDownListcli.Items[j].Value)
                                                    {
                                                        DropDownListcli.Items[j].Selected = true;
                                                    }
                                                }
                                    }                    
                            }
                        }
                        else if (!string.IsNullOrEmpty(Request.QueryString["del_car"]))
                        {
                            cache_car = (List<Cartao_Credito>)Session["car_cache"];
                            int a = Convert.ToInt32(Request.QueryString["del_car"]);
                            for (int i = 0; i < cache_car.Count; i++)
                            {
                                if (a == cache_car.ElementAt(i).Id)
                                {
                                    cache_car.RemoveAt(i);
                                    Session["car_cache"] = cache_car;
                                }

                            }
                        }

                    }
                }
                        
                
            }
            catch (Exception eee)
            {
                Response.Redirect("~/Default.aspx", false);
            }
        }

        private void PesquisarEnderecos()
        {
            enderecos.InnerHtml = "";
            int evade = 0;
            string GRID = "<TABLE class='display' id='GridViewcli'><THEAD>{0}</THEAD><TBODY>{1}</TBODY></TABLE>";
            string tituloColunas = "<tr>  <th></th><th>Código</th><th>numero</th><th>logradouro</th><th>Bairro</th><th>Cidade</th><th>CEP</th><th>UF</th><th>complemento</th><th>Tipo</th></tr>";
            string linha = "<tr><td> ";
            linha += "<a href='clientes.aspx?del_end={0}'>apagar</a></td><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td>{6}</td><td>{7}</td></tr>";

            
            
            try
            {
                evade = cache_end.Count;
            }
            catch
            {
                evade = 0;
            }
            StringBuilder conteudo = new StringBuilder();
            for (int i = 0; i < evade; i++)
            {
                end_bus = cache_end.ElementAt(i);
                string asdf="";
                if (end_bus.Tipo == 0)
                    asdf = "Cobrança";
                else if (end_bus.Tipo == 1)
                    asdf = "Entrega";
                conteudo.AppendFormat(linha,
                        end_bus.Id.ToString(),
                        end_bus.Numero.ToString(),
                        end_bus.Logradouro,
                        end_bus.Bairro,
                        end_bus.Cidade,
                        end_bus.Cep,
                        end_bus.UF,
                        end_bus.Complemento,
                        asdf
                        );

            }
            string tabelafinal = string.Format(GRID, tituloColunas, conteudo.ToString());
            tabelafinal += "<br/>";
            enderecos.InnerHtml = tabelafinal;
           


        }

        private void PesquisarCartao_Credito()
        {
            Cartoes.InnerHtml = "";
            int evade = 0;
            string GRID = "<TABLE class='display' id='GridViewcli'><THEAD>{0}</THEAD><TBODY>{1}</TBODY></TABLE>";
            string tituloColunas = "<tr> <th></th><th>Código</th><th>numero</th><th>nome titular</th><th>Validade</th><th>CCV</th><th>preferencial</th><th>Bandeira</th>";
            string linha = "<tr><td>";
            linha += "<a href='clientes.aspx?del={0}'>apagar</a></td><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td colspan=4><img src=\"{6}\" style=\"width: 100px; height: 100px;\" /></td></tr>";



            try
            {
                evade = cache_car.Count;
            }
            catch
            {
                evade = 0;
            }
            string limitsofdead;
            if (evade > 0)
                Cartoes.Style.Value =" height:"+ (evade+1) * 100 + "px";
            StringBuilder conteudo = new StringBuilder();
            for (int i = 0; i < evade; i++)
            {
                car_bus = cache_car.ElementAt(i);
                try
                {
                    switch (car_bus.Bandeira.Id)
                    {
                        case 1:
                            limitsofdead = @"data:image/jpeg;charset=utf-8;base64, /9j/4AAQSkZJRgABAQEAYABgAAD/4QBuRXhpZgAATU0AKgAAAAgAAwESAAMAAAABAAEAAAExAAIAAAAHAAAAModpAAQAAAABAAAAOgAAAABHb29nbGUAAAADkAAABwAAAAQwMjIwoAIABAAAAAEAAAFToAMABAAAAAEAAAChAAAAAAAA/9sAQwACAQECAQECAgICAgICAgMFAwMDAwMGBAQDBQcGBwcHBgcHCAkLCQgICggHBwoNCgoLDAwMDAcJDg8NDA4LDAwM/9sAQwECAgIDAwMGAwMGDAgHCAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwM/8AAEQgAoQC0AwEiAAIRAQMRAf/EAB8AAAEFAQEBAQEBAAAAAAAAAAABAgMEBQYHCAkKC//EALUQAAIBAwMCBAMFBQQEAAABfQECAwAEEQUSITFBBhNRYQcicRQygZGhCCNCscEVUtHwJDNicoIJChYXGBkaJSYnKCkqNDU2Nzg5OkNERUZHSElKU1RVVldYWVpjZGVmZ2hpanN0dXZ3eHl6g4SFhoeIiYqSk5SVlpeYmZqio6Slpqeoqaqys7S1tre4ubrCw8TFxsfIycrS09TV1tfY2drh4uPk5ebn6Onq8fLz9PX29/j5+v/EAB8BAAMBAQEBAQEBAQEAAAAAAAABAgMEBQYHCAkKC//EALURAAIBAgQEAwQHBQQEAAECdwABAgMRBAUhMQYSQVEHYXETIjKBCBRCkaGxwQkjM1LwFWJy0QoWJDThJfEXGBkaJicoKSo1Njc4OTpDREVGR0hJSlNUVVZXWFlaY2RlZmdoaWpzdHV2d3h5eoKDhIWGh4iJipKTlJWWl5iZmqKjpKWmp6ipqrKztLW2t7i5usLDxMXGx8jJytLT1NXW19jZ2uLj5OXm5+jp6vLz9PX29/j5+v/aAAwDAQACEQMRAD8A/fyiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKRjimmTH4+1AXH0VGZxtz27ntUUuqQQH95NGvsTzS5kUot7FmiqqaxbzNhbiFsdcMKmS4V2/qKYSi1uiSimb8UokyKNibjqKYGzSF8n0/GlzJASUVC17GrAeYtKlyrnhw30rKOIpydlJfeh8rJaKjMuD3o8w1t0uIkopqNmnUAFFFFABRRRQAUUUUAFFFNkk2D0oAbcNhf5+1fMv7a3/BU34c/saxy6deXU3iLxYyAxaJppV50yMq0zH5YlPXLHdjkK1eG/wDBW/8A4KvS/BA3Pw1+HV0p8YToBqepRkOuiRsMhV7eewwR/cU7sElRX5EajfXGr6lcXt5NNeXl5K89xcTuZJbiRjlndmyzMTyWJ5PP0+ZzjPlhn7Kiry79Ef054RfR/r5/Sjm2d3p4d/DFaSn5+S/Fn1z8e/8Agtl8avjDdTR6HqFl4D0uQsEi0yITXWw5wGmlB5x3RV5GQe1fNviT49ePPGU27WPHnjbVWJLH7Xr13OAT6bpDjqenA6Vyec9uPb/PtR/n6V8TXzLE1W3ObP7OyPw94cyqmqeDwcI6b8qcvm3ds3NB+KPijwoytpPijxNpLITtay1a4t2XPUgo4Ofevbvgf/wVY+OnwKuofs3jS88SWMfWz8QD7crD/rs37/t1Mhx6V860BR0x1qaOOxFL3oTa+Z2ZpwXkOZUvZYzCU5R2+BX+/wDyP2V/Yy/4LoeCfjfeW+g/EC3TwDr0oVY7qabdpd2x4+WY4MRz/DKAACMMxyB9z3Or2trpzXUlxDHbxoZHlZwFVQMknngV/MTp+mTaxfw2lvC09xdSiKONRy7McAegySBk8DPJHUffX7KXi74pfC/wbZ+CfFniLR9V8D26hrRP7U3z6ftHyxjON0QxgIc7D04wB1Zl4gf2dhJzqx56iWiXX1P5I8T/AKPuX4OrHE5LWUIyetOV27d4v9H95+hvxQ/bWs9Jmks/DVr/AGlKvBupAVtwf9kdW/QHsTzjxbxR8fPGHi6VmuNevraPPEVlIbVF9gUw35k1xsLrIgaNlZWAKkeh5/L36cmnHt6iv5e4h8SM9zWo1VrOEb/DHS36nymV8G5bgYpRgpS6uSv/AMBFq71y+vTumvr6Zt2d0lxIxz65J61d0vx/r2hSo1nrmsWu05wl24U/8Bzj8CKyM0V8hDOMdCXPCtK6/vP/ADPdll+GlHllCLXoj17wN+2V4o8PyRx6tHb61brwzFfJmA9cqNp/EfiK+hfhZ8btB+LVj5mm3KrdRjM1rLhZovqMnj3GR718OdR/D+NWdK1m68P6nDqFjczWt5aneksbFWTj+vTnI9eMiv1DhHxizXLaqp46TrUuqe69GfGZ54f4LFwc8IlTn5bP1X+R+hcT7jx0xUleS/s2/tCx/FPT20/UGih1+zX94qn5bleP3ij8QCOxPvXqySFmx+lf15keeYTNsJHG4OXNGX4eT8z8Nx2BrYSs6FdWkv6uvIkooBor2DjCiiigAooooAa5wK+ff+Ckv7YEf7G/7MureILdoW8QXw/s/RYZRlZLuRW2MV6sqANIwGMqhGRmvoGYZSvxo/4L/fHWfxx+0/o3gmG4VtL8G6WLmaMNjN3ckk7u3yxJGQe3mN615ubYx4bDSqLfZerP0rwl4SjxFxNQwFVXpp80/wDDHW3zdkfC2s61eeJdXutS1K5mvtQ1CZ7m6uJ33yXErsWZ3z1Yk5PbnoKrAYqFdTtpZgq3MLyMcAbxlv6n0/zmnT3kNq+2SaONmHAY4Y/h19BX5ZKNRu7Tuf6iYerhKNKMKcoqMVbRqy/yJKKbFOs6bkaNlboVYEHsf/104j/PpUaLQ7KcozgpQ1X9dQpD1/n7D1pev8uvemPcwoyLJNDCrkYaQ8LyPm9wOeBknGByRSle1+xNatGlBzm0vuPUfgZ4CWWyuNR1r4Z+LfGljeKEs2sYJlt0wSHO5B8x6DrgYYV3y+CfCpz/AMY7eOSenK3QqvofjPwH4d0q2sbH4+eObG0t0CRW8Gn3UccQAHAUR4/LpxVz/hZ/g/PP7RHxAx/15Xef/QK/NsfiJ167m1p8z8JzbMK2MxM61m03ppVWnTRafcfR37MPxFm8Q6G+iSeCvFHg210WFFtF1aKXbLHlhtWSQclf7pOcEY6GvVgdw/Q49f8AP+TXx38Kfjb4L8L/ABD0u+k+OnjDXI1mEbWN9Y3Rt7repQK+UxwWByejKD2r7HggkvJtsMck0nXail2wPwyRX5LxFlc4YpOlH4tbJP5n5fnmHjhq3PP3U9dpJf8Ak2oyin3FvJa3HkzRyQTf885VKtj1weaYDn+nFfM1KcqcuSorPzPHhUjNXi7oM4NGeR6jp7UD5v4l9M570u056fn/AIdajS+hWyuXvDPia98Ga/aapp83l3lk++MnOw+oIHJB7j+oFfdHw28b2/xD8G2GsWv+rvIgxXujfxKfcHI/CvgdpFjYb2CbiFzuxjpn6d/yr6M/YU8YyTW+s6FI3y25S8gU/eUNw649AQD9XNfvPgfxNXwmZ/2bWb9nVWl9uZdj8y8Rsop1sIsZT+KG9uz7n0WjZp1NRs06v68PxAKKKKACigc0UAR3BxC30r8EP2x7GX42/wDBVrXNHk8xv7Y8Z2GiKB95VLwQ+h4GSfp9K/fGcZjI9RX49+CPg1Jrf/BffUrSeLdb6drM/iGRSM/ILJSh9gJJEOfUYrxM6ourGnBdZI/dPA/MqeAq5njJuzhh5tev9WPuf9vXwn4cuv2Jvi1pNnZ6bHqWl+EbueTEXliAm3lKNuA4wYyeOmB6iue/4Je/swaP+zr+yf4T0jxDY2P/AAl3imCXXr2CZFadN+1ipB5xCkkEbEcbyD/FWh+yv46s/jX+1F+0jo94ft2kWOtWenmGYhogi6dFFLH16eYkmR/tVB8OPjpa/FP9sn4vaktxnwv8I9Eh8PwSxSbo/tD7rq/OBxlfLtoyByGhcHtjrVOm5xqxSvqv83+B8vLFZpTwFXJ1OSjeNWbu95RSivvkfD3xR+B3wi/aa/ap+MHir4jfFSx+HOl6Z4jk0PTLGCa3juJls444ZpCsisdvmpIoCryQ3JyCIfj7/wAEcNKtPgfpfjz4O/EBfHOlaheW1mi3bQslybi5S3QxTQhV+WRwGDA8buQVAPuWufArTf2av2OfDvxC8I/BvTPjV8TviHdx6pqN1JYnUvs095HJdyzgBGbyFb92qptzvQs3JJ7r9qiT4naX/wAE7/BGi6L4P0vS/ilq2padfDQtAijhtrG4t5xftsQuUOw26ll3kM2cE5APkzy+g1KVWCva90nfV99vkfqGD8Rs9o1MLSynFyjTUlTUZuHKlFa3jbmSvf3pM+ffHP8AwSO+Bn7Neh6PbfFb41XmjeINagMscXmW9pHMVxuMcbI77ASBlmPPftXqn/BLn4VfD34U/sz/ABc8QXetW6eD28SX1pZeItSERD2kCLCtzvICEFtxHGMj3r0L4Iwz/wDBSL4Uatpvx++B0nhbUvDiLbWuoahCUE5dfne1Z1WaLBUEhSV5X52IIHnviX4bat8E/wDgjpofh3wf4Pm+Jg1a/FybCOLzhdWlxeyXSSSCMHKBDGMgdweADTqYGlSTq0qSaUX3u+jTueZmfGGa5lhXlWZ4ypKvKrFS96HskrtpwcdOmzdit4L/AGetH8L+AtX+InxM8YeE/DPw1lvnXw/OunQie/sTIUt7qWTlCZ1AkRI4x8ki55JC5nx+/Zl1rwb4++Ft54H1Dwr4q8A/EbWrXTJL3+zIjdWUUv7wzRyIfKdTCshBK8ME4cMceyftU6zfeJ/gR4N8X+C/hXo3x08J31jZmDw5cTRJDpGEkK3kUbQyB2ZZBEQNrRqvGQzgYP7Knxs+Ifxr+K/w78O+Jvglb/CHwv4f+1arpkRv1LyGG3aAIlr5MZjRftQJbjBwMc14suDcldoSo2vZ3957+d7WOChxLnapTx6q6R5lKLlBWSTStH4uZPW+z9Cj8X/2NtP+E3iBYZvGHh4yanDbWmiaXNp9qmp6lqM05ix0CiBS8JJVGYDzWJAUA/RHjTxvov7KPhTStI0vS4b3VrqLdLIcR7woAaVzySS3RRx1AwABXyn4r8U6xff8FTrPxFqfwz8P3Gj6Xq6WEPiKS9R763t1haJZVQjKgO7ErkYBY9Tmvpj9qX4H69458YW+uaHbrqVvLaJbvCjhXQqzEHkgFTu7HjHvXwmcyo0MuxNbh7DJYiEuRfalbrJJ7Hn5qsTPEYOjnldypzhzu9krv7Lt28+5y2sfEiD9pbV9O0/Wvs3hqzsfMubm5EwO7gKoBYfeyxGDW3ZfsseFPHenXh8L+LZL68teXO+OZEY/d3BQCAcEDGO/XFXPgl+z1D4Y8O6xqviDR7fWtYtY2MOnMVm8vCBwmMEGVsgZwcZGOprsvg14q1i08IeINX1/w9Y+GbO0j8y3gSDypSiKzMXGegGAMheQ3FePkPDKxsYVeIqMZVaqk72d4pLS7Xur8zwcyzh4WUqWU1HGEGkldWbb6J3bPLfBn7JsfiX4cNq15rB0u/WeVJA5X7LEkczIzZxkjapIyR2zjmnaT+yjY3s1/qdx4ijtfCluQLa8yha5AUbpN2NqqH3KOpO30IJ2v2kL6bQf2cPDmls22bUWiedd33iFMrZHfD4P1ArZ+Nnhe+8a/s8eHLfwzbS39qptnaCAjdJEEwp6gEBtp/DPGKxxHC+RQnUw9HCqpOhBSau7zb/Q2o55mslTrVMQ4wqScb2Vopbv/If8PPhrp3wk+G3inWLXUbXXLO6gaazuQo+6sZwpxkZ3lskHn0GK81/Yzv3tfjJFEG+W4spAeeuCp/PP9K7nX/DOofC39kI6TdLnUtRlWLyAdzB5pR8gx1YA846nOM1y37Kfg3UNF+OMQvLeS3aCwkkCkghlJRc5FcmKwro55lNDDUfZqNpOK+y5bpvc2w1SE8tx9SrU53K6TfVR62PrJODTqbGc06v6jV+p+ThRRRQA1TyadTc04UlcBs33Px7V+T//AAWPu/GX7HX7ZOifFvwLdrpM3jDRDpNxeLZx3GyaF9zKfMUgeZEY8YHSFq/WBxkfrXgv/BRX9kaD9sf9mPWfDMf2eLXLdPt+jXE33YLyMZj3HkhW5RsAna7cHpXBmWHlVw8lB2ktV6n3nhrxBhcoz6jXx8FOhL3KkXqnGWjuuqTs/kfib8MP27/ir8HPE3ifWPDnilrHUfGV39s1ib7JDL9tm55w6nb94/dx2xWf4A/bI+JHww8MeKdH0XxE1rZeNp5rvWgbSGSS+lmXbIxdlLLkFhgEY7Yrz3xD4dv/AAdr17pOqWlxY6npc72t5bSr+8t5EYqysM9sAcccgiqeNpx3U9M/dr8zljMRGXI5tWv1P9MqPB/D2Ih9YhhaclUUXey1Sty3726dj2z4Y/8ABRj40fB34bW/hHw744vLHQrKJbe1ia2hmktIwAAiO6FlCgDaM4GAOlWLz/gpX8cL+fQZpvHt9JN4dybKVrWAyAlDGS7FSZMqer5OST1wa8Lo/wA8Uf2linHl53ZeZm/D3hp1HWeDpuUrtvkW70f3o94+In/BTb46fFTwvdaNq/j7UP7OvozFOlpbQ2jyoeGUyRKrYIyDgisvwZ/wUF+Mnw7+FkfgzR/G+oWvh21tvslvamCJmggA2rGsrKWUBflHPHHYDHjdGf58gCplmGJl/wAvHd+ZUPD7hyFJYeGCpqN+b4Vo+j2Psb9nH9sG0/Z+8NQ2vgub4yWOjy5kW1i2XlkGP3zGsiOoy2c7cAnNdnH/AMFGNTXx4viT7R8YpdTjtJLCFpNOtmjghkdHdVj8nYpZoo8tjcQgGcACvmX9nX4weILS2k8Pn4lDwPpdnGZbZrixhnhYs53KHZSVIyCASe/SvUl+IWpZb/jJDRF7H/iU23P6Y+vcV+c47MMww9V0vbTt/iZ+OZ9wTltLG1HWwqk31953XnaD/Ml8B6P4J+N3xvjkfS/ioNU1e9l1K4udQnaK28wEyu0g4GC3yhQP4gBgdPuzw/8AHjxh4a05bOz1y5WCMfKjhZNvsCRnHt9R2rw/9nvwz4g0rw5Ne6740PjGPVBHNZTLZx20ccWCcgKATuyDk+g6ZNehH5h656k9/evyPOeJsZDFt4StKPdp7/8ABPgOIqOFxs40Z01KEFZJ+9b0ulb7jrNC+OXizw3ql7fWuszfaNRYNcGRBIrkAAHaQQCAAOAOKl1T4++L9a02+s7rWJJrfUFZZkeJACpGCAQPlBHp6+tcdnnNGOK8qHFWbwjyQrytrpd9T5qWQ5dKXM6Mb97dje8afEvW/iEtqNXvvtC2g/cARqnl5+g56dat+EPjZ4o8CaKdO0vVZLezBJSJo1kWLPXbuBI9cDjJJ61y1Brmjn2YQxDxcKslNqzld3t2Np5PgpUlh5U1yp3SsrI6bxH8YfEvjDSrOz1LVZ7qHT5UuIdyKGWRB8rbsZJHXknJ56817v8AsY6ffeIItW8SaiyyNPssbUhcbUQlnx7F2x9U+lfN3hzw/ceKNbg0+12faJmxuc4WIDlmY+igZ6frX298H7HSdG8CWdho8yz2tigiLYwxbAJJHYnO7/gVft3g3hsZmebPMsbUcowVld3u/L0Pznj+phcJg1gsLBRcn0Wy/wCCdZGMHNOpkfWn1/WCPxoKKKKAGqOKdQBiigBGbFQyL5q4GOvcVOaaYwRQHU/Pn/grX/wSjf8AaDM3xE+Hdnbx+NreL/iY2IxEuuRqMK2fuidVAALYDgKpIABH5B6rpV1oGrXWnX1rdWV9YytDcWs8TRS28i/eRkbBVh6HkfrX9QDRDZg8185ftn/8Exvhp+2dbteaxp7aP4mRAsWt6YFhvOOgckFZF56ODjPGK+Yzjh+OJftaOkuq6M/pfwi8fK3D9OOVZ0nUw60jJayh8uq8uh+AgbJ4+b0PYiivsz49f8ENfjN8Jp5pfDsemePtPUnY9iws7thx1gkO0HrwsjfnxXzb4o/Zd+J3gm5ki1b4c+OrExkgu+g3JiOPSRUKH8DXxlfLcVRdpwZ/Z+SeI/DebU1UweMg7rZyUX6WdmcLjNDdP8BXWaT8APH2vz+VY+AfHN9JnGy38PXcrA+4WM4/H+XNe3fBf/gkL8dvjNeR/wDFJr4XsZOt1rk6wbRj/nmpaT8Cimpo4HEVf4cG36HZmnHXD+XU3UxmMpxVv5lf7k7nzIzeWNzMqleQxUEYAzyDkEeoPUZHIPP33+y9+zTq3j/4YWnjPxh8HPA+i+GbiJPsdz/Yqfarw8ETmMpiKJs5DHO49OMMfqf9iz/gh74D+AF/b6541uI/iB4khYSRfabZY9OtGByDHAS25hx80hbkAgIa+35dJguLNrZ442tyuxkK5BHcYNdeY+H8sxwcqdWo4Tt7rXR+fc/kfxP+kFgsdWhhskpc0IvWburrtFfq/uPzySNYY1VQqqowoB+6PTFLX078Uf2K9P12aS68OzR6RcMS5t3XdbOfbun4ZA9Oa8a8U/s5eMvCsrrJos95GnSSzImU/RV+b/x2v5e4i8Nc+yuo1Voua/mirp/dqfL5XxpluNjdVOWXaWhw9A5NaE3hDWLaTbLo+rxt/dexlVvyK1e0b4W+JtemVbXw9rEm7oXtHjX/AL6YAfrXyFPJcfOXJGjO/wDhf+R7ksywkI88qkbeqMHpn1HvirWh6LeeJdYgsNOt5by9uGxFFGvzN/gPUnAxmvWvA37F3iLxE8cmrz2+j2/DMq/vpiPQYO0H3yfp3H0J8Mfgvofwr07y9NtE85hiS5k+aab/AHm69unSv1HhHwbzbMasauPi6NLrfdryR8ZnniBg8NBwwj9pPp2Xq+pyvwB/Zus/hv4bkk1JIrzV9RhMVy/VY426xIcZ2+p43EDpgAeh+GvClp4UtpobNPLWZ/Mck5ZztC8/goH4VpqmPrSlMiv60yjh/BZbQhQwsElFWTtr637s/EcdmWIxdWVWvJty3FjGFp1NjTZTq9xHEFFFFABRRRQAUUUUANc0hGaeRmkK80AMEQPy9vTFRyWkcx+ZEbtkip9tG3FSF2tisthFGfljjHsq1IIlVT7+1SKOadT1Kcm9yNVGc+tOK06gjNFtLEkbLz/WmmIZ55qQpk0oTFLlDUieCPOWVfril8tR/CAPanleaUrurONGEXpFfcPmYwIpP/1qdtwtOxigDFaaiGqcmnUUVQBRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUA5FFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFAH/9k=";
                            break;
                        case 2:
                            limitsofdead = @"data:image/jpeg;charset=utf-8;base64, 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";
                            break;
                        case 3:
                            limitsofdead = @"data: image/png ;charset=utf-8;base64, ";
                            break;
                        default:
                            limitsofdead = @"data:image/jpeg;charset=utf-8;base64, ";
                            break;


                    }
                    
                }
                catch
                {
                    limitsofdead = @"data: ;charset=utf-8;base64, ";
                }
                conteudo.AppendFormat(linha,
                        car_bus.Id.ToString(),
                        car_bus.Numero.ToString(),
                        car_bus.Nome_Titular,
                        car_bus.Validade,
                        car_bus.CCV,
                        car_bus.Preferencial.ToString(),
                        limitsofdead
                        );

            }
            string tabelafinal = string.Format(GRID, tituloColunas, conteudo.ToString());
            tabelafinal += "<br/>";
            Cartoes.InnerHtml = tabelafinal;
            

        }


        private void Pesquisar()
        {
            int evade = 0;
            string GRID = "<TABLE class='display' id='GridViewcli'><THEAD>{0}</THEAD><TBODY>{1}</TBODY></TABLE>";
            string tituloColunas = "<tr><th></th><th>Código</th><th  colspan='5'>Nome</th><th>Sexo</th><th>CPF</th><th>RG</th><th>Nascimento</th><th>EMAIL</th>";
            string linha = "<tr><td> <a href='clientes.aspx?cod={0}'>editar</a> ";
            linha += "<a href='clientes.aspx?del={0}'>apagar</a></td><td>{0}</td><td colspan='5'>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td>{6}</td></tr>";


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
                        categoria.Email
                        );

            }
            string tabelafinal = string.Format(GRID, tituloColunas, conteudo.ToString());
            divTable.InnerHtml = tabelafinal;
            categoria.Id = 0;
        }

    
        private void getcliente()
        {
            if(!string.IsNullOrEmpty( codigo.Text))
            categoria.Id = Convert.ToInt32(codigo.Text);
            categoria.Nome = nome.Text;
            categoria.Senha = senha.Text; 
            categoria.Sexo = Convert.ToChar(DropDownListcli.SelectedValue);
            categoria.Cpf = cpf.Text;
            categoria.Rg = rg.Text;
            categoria.Dt_Nas = Convert.ToDateTime(data.Text);
            categoria.Email = email.Text;
            cache_end =(List<Endereco>) Session["end_cache"];
            categoria.Enderecos = cache_end;
            cache_car = (List<Cartao_Credito>)Session["car_cache"];
            categoria.Cartoes = cache_car;
        }



        protected void novo_cli_Click(object sender, EventArgs e)
        {

            getcliente();
            categoria.Id = 0;
            categoria.OPeracao = 'C';
            res=commands["SALVAR"].execute(categoria);
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
            erro_cartao.Text = res.Msg;
            Response.Redirect("clientes.aspx");
            
        }
        protected void alterar_cli_Click(object sender, EventArgs e)
        {
            getcliente();
            categoria.OPeracao = 'A';
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
            categoria.OPeracao = 'E';
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

                end_bus.Cep= cep.Text;
                res=commands["WS"].execute(end_bus);
                end_bus = (Endereco)res.Entidades.ElementAt(0);
                logradouro.Text = end_bus.Logradouro;
                cep.Text = end_bus.Cep;
                cidade.Text = end_bus.Cidade;
                bairro.Text = end_bus.Bairro;
                for(int i=0;i< DropDownListcliuf.Items.Count; i++)
                {
                    if (DropDownListcliuf.Items[i].Text == end_bus.UF)
                        DropDownListcliuf.SelectedValue= DropDownListcliuf.Items[i].Value;
                }
                end_bus.Id = 0;

            }
        }

        protected void add_endereco_Click(object sender, EventArgs e)
        {
            erro_cartao.Text = "";
            Endereco end = new Endereco()
            {
                Numero = numero.Text,
                Logradouro = logradouro.Text,
                Bairro = bairro.Text,
                Cidade = cidade.Text,
                Cep = cep.Text,
                UF = DropDownListcliuf.SelectedItem.Text,
                Complemento = complemento.Text,
                Tipo=int.Parse(DropDownList_tipo_end.SelectedItem.Value)
            };
            res=commands["SALVAR"].execute(end);
            erro_cartao.Text = res.Msg;
            if (Session["end_cache"]!=null)
            cache_end = (List<Endereco>)Session["end_cache"];
            cache_end.Add(end);
            Session["end_cache"]=cache_end;
            PesquisarEnderecos();
            if(Session["cli"]!=null)
            categoria = (Cliente)Session["cli"];
            Session["cli"] = categoria;
            cache_car = categoria.Cartoes;
            Session["car_cache"] = cache_car;
            PesquisarCartao_Credito();
            codigo.Text = categoria.Id.ToString();
            senha.Text = categoria.Senha;
            nome.Text = categoria.Nome;
            cpf.Text = categoria.Cpf;
            rg.Text = categoria.Rg;
            data.Text = categoria.Dt_Nas.ToString("yyyy-MM-dd");
            email.Text = categoria.Email;
            for (int j = 0; j < DropDownListcli.Items.Count; j++)
            {
                if (categoria.Sexo.ToString() == DropDownListcli.Items[j].Value)
                {
                    DropDownListcli.Items[j].Selected = true;
                }
            }

        }

        protected void novo_cartao_Click(object sender, EventArgs e)
        {
            char vai;
            vai = 'N';
            if (CHK_preferencial.Checked)
            {
                vai = 'S';
                if (Session["car_cache"] != null)
                    cache_car = (List<Cartao_Credito>)Session["car_cache"];
                foreach(Cartao_Credito cat in cache_car)
                {
                    cat.Preferencial = 'N';
                }
                Session["car_cache"] = cache_car;
            }
            Cartao_Credito end = new Cartao_Credito()
            {
                Numero = num_car.Text,
                Nome_Titular = nome_titular.Text,
                Validade = validade.Text,
                CCV = int.Parse(ccv.Text),
                Preferencial=vai            
            };
            commands["SALVAR"].execute(end);
            if (Session["car_cache"] != null)
                cache_car = (List<Cartao_Credito>)Session["car_cache"];
            cache_car.Add(end);
            Session["car_cache"] = cache_car;
            PesquisarCartao_Credito();
        }

        protected void mudar_Click(object sender, EventArgs e)
        {
            Response.Redirect("mudar_senha.aspx");
        }
    }
}