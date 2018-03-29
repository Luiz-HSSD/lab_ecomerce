using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Produto:EntidadeDominio
    {

        private string nome;
        private string descricao;
        private string fabricante;
        private string codigo_barras;
        private Categoria categoria;
        private string extension;
        private int qtd;
        private byte[] img;
        private double preco;
        private Formato_Produto formato;
        private Motivo razao;
        
        public Motivo Razao
        {
            get { return razao; }
            set { razao = value; }
        }

        public Produto():base(0,Convert.ToDateTime("01/01/1995 03:30"))
        {
            Razao = new Motivo();
            Qtd = 0;
            Nome="";
            Descricao="";
            Fabricante="";
            Codigo_barras="";
            Categoria=new Categoria();
            Extension="";
            Img = null;
            Preco=0;
            formato = new Formato_Produto();

        }
        public int Qtd
        {
            get { return qtd; }
            set { qtd = value; }
        }
        public string Nome
        {
            get { return nome; }
            set { nome = value; }
        }
        public string Descricao
        {
            get { return descricao; }
            set { descricao = value; }
        }
        public string Fabricante
        {
            get { return fabricante; }
            set { fabricante = value; }
        }
        public string Codigo_barras
        {
            get { return codigo_barras; }
            set { codigo_barras = value; }
        }
        public Categoria Categoria
        {
            get { return categoria; }
            set { categoria = value; }
        }
        public string Extension
        {
            get { return extension; }
            set { extension = value; }
        }
        public byte[] Img
        {
            get { return img; }
            set { img = value; }
        }
        public double Preco
        {
            get { return preco; }
            set { preco = value; }
        }
        
        public Formato_Produto Formato
        {
            get { return formato; }
            set { formato = value; }
        }
    }
}
