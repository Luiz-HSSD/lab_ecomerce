using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using core.core;
using dominio;

namespace core.negocio
{
    class Validar_senha : IStrategy
    {
        public string processar(EntidadeDominio entidade)
        {
            string cli = ((Cliente)entidade).Senha;
            if (cli.Length < 8)
            {
                return "senha muito pequena";
            }
            else
            {
                if (cli.Any(char.IsLower) && cli.Any(char.IsUpper))
                    return null;
                else
                    return "senha deve ter caracteres maiusculos e minusculos";
            }
        }
    }
}
