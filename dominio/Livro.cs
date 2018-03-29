using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Livro:Produto
    {
        private string isbn;

        public string ISBN
        {
            get { return isbn; }
            set { isbn = value; }
        }
        private int n_pags;

        public int N_pags
        {
            get { return n_pags; }
            set { n_pags = value; }
        }
        private char g_preco;

        public char G_PRECO
        {
            get { return g_preco; }
            set { g_preco = value; }
        }
        private string editora;

        public string Editora
        {
            get { return editora; }
            set { editora = value; }
        }
        private string edicao;

        public string Edicao
        {
            get { return edicao; }
            set { edicao = value; }
        }

        private List<Categoria> generos;

        public List<Categoria> Generos
        {
            get { return generos; }
            set { generos = value; }
        }

        public Livro()
        {
            N_pags = 10;
            G_PRECO = 'A';
            Edicao = "";
            Editora = "";
            ISBN = "";
            Generos = new List<Categoria>();
        }
    }
}
