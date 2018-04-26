using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using core.core;
using dominio;
using core.Utils;

namespace core.negocio
{
    class Criptografar_senha : IStrategy
    {
        public string processar(EntidadeDominio entidade)
        {
            try
            {
                Cliente cli = (Cliente)entidade;
                string texto = cli.usuario.Password;
                string key = "vgfrecopkl´wdxçddkjith3hu4jrko"; 
                Criptografia crip = new Criptografia(CryptProvider.Rijndael);
                crip.Key = key;
                cli.usuario.Password = crip.Encrypt(texto);
                return null;
            }
            catch
            {
                return "erro ao criptografar";
            }

        }
    }
}
