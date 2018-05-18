using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using core.core;
using dominio;
using core.DAO;

namespace core.negocio
{
    public class gerar_produtos_venda : IStrategy
    {
        public string processar(EntidadeDominio entidade)
        {
            Gerar_produtos gp = (Gerar_produtos)entidade;
            ProdutoDAO ld = new ProdutoDAO();
            gp.Colocar_preco= ld.consultar(gp.Colocar_preco.ElementAt(0)).Cast<Produto>().ToList();
            foreach(Livro a in gp.Colocar_preco)
            {
                a.Preco = a.estoque.Custo*(1+(a.G_PRECO.Porcentagem));
            }
            return "consultado con sucesso";
        }
    }
}
