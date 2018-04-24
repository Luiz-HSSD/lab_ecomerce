using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using core.core;
using core.DAO;
using System.Text;
using dominio;
using System.IO;

namespace core.negocio
{
    public class Gerar_log : IStrategy
    {

        StringBuilder sb = new StringBuilder();
        public string processar(EntidadeDominio entidade)
        {
            switch (entidade.GetType().Name)
            {
                case "Cliente":
                    l_Cliente(entidade);
                    break;
                case "Cartao_Credito":
                     l_Cartao_Credito(entidade);
                    break;
                case "Endereco":
                    l_Endereco(entidade);
                    break;
                default:
                    break;
            }
            return null;
        }
        public void l_Cliente(EntidadeDominio entidade)
        {
            Cliente cli = (Cliente)entidade;
            sb.Clear();
            sb.AppendFormat("{0},{1},{2},{3},{4},{5},{6},{7},{8}\n",
                cli.OPeracao.ToString(),
                DateTime.UtcNow.ToString(),
                cli.Id.ToString(),
                cli.Nome,
                cli.Sexo.ToString(),
                cli.Cpf,
                cli.Rg,
                cli.Dt_Nas.ToString(),
                cli.Email);
            foreach(Endereco end in cli.Enderecos)
            {
                sb.AppendFormat("{0},{1},{2},{3},{4}\n",
                    cli.OPeracao.ToString(),
                    DateTime.UtcNow.ToString(),
                    cli.Id.ToString(),
                    end.Id.ToString(),
                    end.Tipo.ToString()
                    );
            }
            foreach (Cartao_Credito end in cli.Cartoes)
            {
                sb.AppendFormat("{0},{1},{2},{3},{4}\n",
                    cli.OPeracao.ToString(),
                    DateTime.UtcNow.ToString(),
                    cli.Id.ToString(),
                    end.Id.ToString(),
                    end.Preferencial
                    );
            }
            if (File.Exists(@".\log_cliente.txt"))
                File.Create(@".\log_cliente.txt");
            File.AppendAllText(@".\log_cliente.txt", sb.ToString());
            

        }
        public void l_Cartao_Credito(EntidadeDominio entidade)
        {
            Cartao_Credito cli = (Cartao_Credito)entidade;
            sb.Clear();
            sb.AppendFormat("{0},{1},{2},{3},{4},{5},{6},{7}\n",
                cli.OPeracao.ToString(),
                DateTime.UtcNow.ToString(),
                cli.Id.ToString(),
                cli.Nome_Titular,
                cli.Numero,
                cli.Validade,
                cli.CCV,
                cli.Bandeira.Id
            );
            if (File.Exists(@".\log_cliente.txt"))
                File.Create(@".\log_cliente.txt");
            File.AppendAllText(@".\log_cliente.txt", sb.ToString());


        }
        public void l_Endereco(EntidadeDominio entidade)
        {
            Endereco cli = (Endereco)entidade;
            sb.Clear();
            sb.AppendFormat("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}\n",
                cli.OPeracao.ToString(),
                DateTime.UtcNow.ToString(),
                cli.Id.ToString(),
                cli.Complemento,
                cli.Numero,
                cli.Logradouro,
                cli.Bairro,
                cli.Cidade,
                cli.UF,
                cli.Cep
            );
            if (File.Exists(@".\log_cliente.txt"))
                File.Create(@".\log_cliente.txt");
            File.AppendAllText(@".\log_cliente.txt", sb.ToString());


        }
    }
}
