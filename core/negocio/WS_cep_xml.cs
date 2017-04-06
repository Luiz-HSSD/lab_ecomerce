using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using core.core;
using dominio;
using System.Net;
using System.Xml;
using System.Xml.Linq;
//using System.Xml.Linq;


namespace core.negocio
{
    class WS_cep_xml : IStrategy
    {

        public string processar(EntidadeDominio entidade)
        {
            Endereco end = (Endereco)entidade;
            string URL = "https://viacep.com.br/ws/" + end.Cep + "/xml/";
            WebClient client = new WebClient();
            string xml = client.DownloadString(new Uri(URL));
            XDocument xdoc = XDocument.Parse(xml);
            // Le a tag <carros>
            XElement tagCep = xdoc.Element("xmlcep");
            end.Cep = tagCep.Element("cep").Value;
            end.Logradouro = tagCep.Element("logradouro").Value;
            end.Bairro = tagCep.Element("bairro").Value;
            end.Cidade = tagCep.Element("localidade").Value;
            end.Complemento = tagCep.Element("complemento").Value;
            end.UF = tagCep.Element("uf").Value;
            return null;
        }
    }
}
