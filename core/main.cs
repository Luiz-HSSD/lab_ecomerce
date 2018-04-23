using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;
using core.DAO;
using core.aplicacao;
using core.negocio;
using core.Utils;
using core.controle;
using System.Net.Http;
using System.Net;

namespace core
{
    public    class main
    {
        public static Produto pro =new Produto();
        public static Cliente cat = new Cliente();
        public static Endereco end = new Endereco();
        public static Resultado res = new Resultado();
        public static void Main()
        {
            Criptografar_senha cs = new Criptografar_senha();
            Cliente cli = new Cliente();
            cli.Senha = "Ae454545";
            cs.processar(cli);
            //cs.processar(cli);
            Console.WriteLine(cli.Senha);
            Console.ReadLine();
            /*
            pro.Nome = "asdf";
            pro.Descricao="asdf";
            pro.Categoria.Id = 1;
            pro.Codigo_barras = "0101010101011";
            pro.Fabricante = "asdf";
            pro.Extension = "image/png";
            pro.Img = Imagems.ReadFile("C:\\open.png");
            pro.Formato.Dimensoes = "2cm2cm2cm4cm";
            pro.Preco = 100;
            pro.Qtd = 1000;
            Fachada facade =  Fachada.UniqueInstance;
            //facade.salvar(pro);
            Console.ReadLine();
            Console.WriteLine("sucesso!");
            end.Cep = "08563010";
            //res=facade.consultar(cat);
            ClienteDAO clid = new ClienteDAO();
            res.Entidades = clid.consultar(cat);
            for (int i = 0; i < res.Entidades.Count; i++)
            {
                cat = (Cliente)res.Entidades.ElementAt(i);
                Console.WriteLine(cat.Id);
                Console.WriteLine(cat.Nome);
            }
            /*
            cat.Nome = "Alterado!";
            cat.Descricao = "Alterado mesmo!";
            res = facade.alterar(cat);
            res = facade.consultar(cat);
            for (int i = 0; i < res.Entidades.Count; i++)
            {
                cat = (Categoria)res.Entidades.ElementAt(i);
                Console.WriteLine(cat.Id);
                Console.WriteLine(cat.Nome);
                Console.WriteLine(cat.Descricao);
            }
           
            res = facade.excluir(cat);
            cat.Id = 0;
            res = facade.consultar(cat);
            for (int i = 0; i < res.Entidades.Count; i++)
            {
                cat = (Categoria)res.Entidades.ElementAt(i);
                Console.WriteLine(cat.Id);
                Console.WriteLine(cat.Nome);
                Console.WriteLine(cat.Descricao);
            }


                        Console.WriteLine("sucesso!");
                        Console.ReadLine();
    */
        }
    }
}
