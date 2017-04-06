using System;
using core.core;
using dominio;

namespace core.negocio
{
    public class Valida_cod_barras : IStrategy
    {
        public string processar(EntidadeDominio entidade)
        {
            try
            {
                string cod_barras= ((Produto)entidade).Codigo_barras;
                ulong test = Convert.ToUInt64(cod_barras);
                if (cod_barras.Length != 13) return "codigo de barras em formato incorreto";
                return null;
            }
            catch
            {
                return "codigo contén caractere invalido";
            }
        }
    }
}
