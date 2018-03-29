using core.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;
using core.DAO;

namespace core.negocio
{
    public class Ativacao_livro : IStrategy
    {
        public string processar(EntidadeDominio entidade)
        {
            LivroDAO liv = new LivroDAO();
            Livro livr = (Livro)entidade;
            if (!string.IsNullOrEmpty(livr.Nome))
            {
                List<EntidadeDominio> bora =liv.consultar(entidade);
                if (bora.Count > 0)
                {
                    livr =(Livro) bora.ElementAt(0);
                    livr.Ative = 'A';
                    liv.alterar(livr);
                    return "vai para ativação";
                }
            }
            return null;
            throw new NotImplementedException();
        }
    }
}
