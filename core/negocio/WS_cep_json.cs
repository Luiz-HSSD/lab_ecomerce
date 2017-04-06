using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using core.core;
using dominio;

using Newtonsoft.Json.Linq;
using System.Net;

namespace core.negocio
{
    class WS_cep_json : IStrategy
    {
        public string processar(EntidadeDominio entidade)
        {
            try
            {
                Endereco end = (Endereco)entidade;
                string URL = "https://viacep.com.br/ws/" + end.Cep + "/json/unicode";
                WebClient client = new WebClient();
                string json = client.DownloadString(new Uri(URL));
                JObject jobject = JObject.Parse(json);
                // Recupera o objeto principal do json
               // end.Cep = (string)jobject["cep"];
                end.Logradouro = (string)jobject["logradouro"];
                end.Bairro = (string)jobject["bairro"];
                end.Cidade = (string)jobject["localidade"];
                end.Complemento = (string)jobject["complemento"];
                end.UF = (string)jobject["uf"];
                return null;
            }
            catch
            {
                return "Erro!";
            }

        }
    }
}
