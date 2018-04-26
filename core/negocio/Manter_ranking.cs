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
    public class Manter_ranking : IStrategy
    {
        public string processar(EntidadeDominio entidade)
        {
            Venda ven = (Venda)entidade;
            RankingDAO RANDAO = new RankingDAO();
            foreach (Item_venda pro in ven.Produtos)
            {
                Ranking ran = new Ranking()
                {
                    g_preco = ((Livro)pro.Pro).G_PRECO,
                    Montante= pro.Pro.Preco*pro.Qtd,
                    Id=ven.Cliente_prop.Id
                };
                RANDAO.alterar(ran);

            }
            return null;
        }
    }
}
