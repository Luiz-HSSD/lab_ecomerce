using core.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using core.aplicacao;
using dominio;
using core.DAO;
using core.negocio;

namespace core.controle
{
    public sealed class Fachada : IFachada
    {


        /** 
         * Mapa de DAOS, será indexado pelo nome da entidade 
         * O valor é uma instância do DAO para uma dada entidade; 
         */
        private Dictionary<string, IDAO> daos;

        /**
         * Mapa para conter as regras de negócio de todas operações por entidade;
         * O valor é um mapa que de regras de negócio indexado pela operação
         */
        private Dictionary<string, Dictionary<string, List<IStrategy>>> rns;

        private Resultado resultado;
        private Fachada()
        {
            daos = new Dictionary<string, IDAO>();
            /* Intânciando o Map de Regras de Negócio */
            rns = new Dictionary<string, Dictionary<string, List<IStrategy>>>();

            parametro_excluir para_ex = new parametro_excluir();
            CategoriaDAO catDAO = new CategoriaDAO();
            daos.Add(typeof(Categoria).Name, catDAO);
            List<IStrategy> rnsSalvarCategoria = new List<IStrategy>();
            List<IStrategy> rnsAlterarCategoria = new List<IStrategy>();
            List<IStrategy> rnsExcluirCategoria = new List<IStrategy>();
            rnsExcluirCategoria.Add(para_ex);
            List<IStrategy> rnsConsultarCategoria = new List<IStrategy>();
            Dictionary<string, List<IStrategy>> rnsCategoria = new Dictionary<string, List<IStrategy>>();
            rnsCategoria.Add("SALVAR", rnsSalvarCategoria);
            rnsCategoria.Add("ALTERAR", rnsAlterarCategoria);
            rnsCategoria.Add("EXCLUIR", rnsExcluirCategoria);
            rnsCategoria.Add("CONSULTAR", rnsConsultarCategoria);
            rns.Add(typeof(Categoria).Name, rnsCategoria);



            ProdutoDAO proDAO = new ProdutoDAO();
            daos.Add(typeof(Produto).Name, proDAO);
            Valida_cod_barras val_cod = new Valida_cod_barras();
            transfomar_formato trans_for = new transfomar_formato();
            List<IStrategy> rnsSalvarProduto = new List<IStrategy>();
            rnsSalvarProduto.Add(val_cod);
            rnsSalvarProduto.Add(trans_for);
            List<IStrategy> rnsAlterarProduto = new List<IStrategy>();
            rnsAlterarProduto.Add(val_cod);
            rnsAlterarProduto.Add(trans_for);
            List<IStrategy> rnsExcluirProduto = new List<IStrategy>();
            rnsExcluirProduto.Add(para_ex);
            List<IStrategy> rnsConsultarProduto = new List<IStrategy>();
            Dictionary<string, List<IStrategy>> rnsProduto = new Dictionary<string, List<IStrategy>>();
            rnsProduto.Add("SALVAR", rnsSalvarProduto);
            rnsProduto.Add("ALTERAR", rnsAlterarProduto);
            rnsProduto.Add("EXCLUIR", rnsExcluirProduto);
            rnsProduto.Add("CONSULTAR", rnsConsultarProduto);
            rns.Add(typeof(Produto).Name, rnsProduto);

            EnderecoDAO endDAO = new EnderecoDAO();
            daos.Add(typeof(Endereco).Name, proDAO);
            List<IStrategy> rnsSalvarendereco = new List<IStrategy>();
            List<IStrategy> rnsAlterarendereco = new List<IStrategy>();
            List<IStrategy> rnsExcluirendereco = new List<IStrategy>();
            rnsExcluirendereco.Add(para_ex);
            List<IStrategy> rnsConsultarendereco = new List<IStrategy>();
            Dictionary<string, List<IStrategy>> rnsendereco = new Dictionary<string, List<IStrategy>>();
            rnsendereco.Add("SALVAR", rnsSalvarendereco);
            rnsendereco.Add("ALTERAR", rnsAlterarendereco);
            rnsendereco.Add("EXCLUIR", rnsExcluirendereco);
            rnsendereco.Add("CONSULTAR", rnsConsultarendereco);
            rns.Add(typeof(Endereco).Name, rnsendereco);


            ClienteDAO cliDAO = new ClienteDAO();
            daos.Add(typeof(Cliente).Name, cliDAO);
            ValidarCpf val_cpf = new ValidarCpf();
            List<IStrategy> rnsSalvarCliente = new List<IStrategy>();
            rnsSalvarCliente.Add(val_cpf);
            List<IStrategy> rnsAlterarCliente = new List<IStrategy>();
            rnsAlterarCliente.Add(val_cpf);
            List<IStrategy> rnsExcluirCliente = new List<IStrategy>();
            rnsExcluirCliente.Add(para_ex);
            List<IStrategy> rnsConsultarCliente = new List<IStrategy>();
            Dictionary<string, List<IStrategy>> rnsCliente = new Dictionary<string, List<IStrategy>>();
            rnsCliente.Add("SALVAR", rnsSalvarCliente);
            rnsCliente.Add("ALTERAR", rnsAlterarCliente);
            rnsCliente.Add("EXCLUIR", rnsExcluirCliente);
            rnsCliente.Add("CONSULTAR", rnsConsultarCliente);
            rns.Add(typeof(Cliente).Name, rnsCliente);

            Item_vendaDAO itemDAO = new Item_vendaDAO();
            daos.Add(typeof(Item_venda).Name, itemDAO);
            List<IStrategy> rnsSalvarItem_venda = new List<IStrategy>();
            List<IStrategy> rnsAlterarItem_venda = new List<IStrategy>();
            List<IStrategy> rnsExcluirItem_venda = new List<IStrategy>();
            rnsExcluirItem_venda.Add(para_ex);
            List<IStrategy> rnsConsultarItem_venda = new List<IStrategy>();
            Dictionary<string, List<IStrategy>> rnsItem_venda = new Dictionary<string, List<IStrategy>>();
            rnsItem_venda.Add("SALVAR", rnsSalvarItem_venda);
            rnsItem_venda.Add("ALTERAR", rnsAlterarItem_venda);
            rnsItem_venda.Add("EXCLUIR", rnsExcluirItem_venda);
            rnsItem_venda.Add("CONSULTAR", rnsConsultarItem_venda);
            rns.Add(typeof(Item_venda).Name, rnsItem_venda);

            VendaDao venDAO = new VendaDao();
            Calcular_Frete CalFrete = new Calcular_Frete();
            daos.Add(typeof(Venda).Name, venDAO);
            List<IStrategy> rnsSalvarVenda = new List<IStrategy>();
            rnsSalvarVenda.Add(CalFrete);
            List<IStrategy> rnsAlterarVenda = new List<IStrategy>();
            List<IStrategy> rnsExcluirVenda = new List<IStrategy>();
            rnsExcluirVenda.Add(para_ex);
            List<IStrategy> rnsConsultarVenda = new List<IStrategy>();
            Dictionary<string, List<IStrategy>> rnsVenda = new Dictionary<string, List<IStrategy>>();
            rnsVenda.Add("SALVAR", rnsSalvarVenda);
            rnsVenda.Add("ALTERAR", rnsAlterarVenda);
            rnsVenda.Add("EXCLUIR", rnsExcluirVenda);
            rnsVenda.Add("CONSULTAR", rnsConsultarVenda);
            rns.Add(typeof(Venda).Name, rnsVenda);

            PedidoDAO pedDAO = new PedidoDAO();
            daos.Add(typeof(Pedido).Name, pedDAO);
            List<IStrategy> rnsSalvarPedido = new List<IStrategy>();
            List<IStrategy> rnsAlterarPedido = new List<IStrategy>();
            List<IStrategy> rnsExcluirPedido = new List<IStrategy>();
            rnsExcluirPedido.Add(para_ex);
            List<IStrategy> rnsConsultarPedido = new List<IStrategy>();
            Dictionary<string, List<IStrategy>> rnsPedido = new Dictionary<string, List<IStrategy>>();
            rnsPedido.Add("SALVAR", rnsSalvarPedido);
            rnsPedido.Add("ALTERAR", rnsAlterarPedido);
            rnsPedido.Add("EXCLUIR", rnsExcluirPedido);
            rnsPedido.Add("CONSULTAR", rnsConsultarPedido);
            rns.Add(typeof(Pedido).Name, rnsPedido);
        }
        private static readonly Fachada Instance = new Fachada();

        public static Fachada UniqueInstance
        {
            get { return Instance; }
        }
        public Resultado salvar(EntidadeDominio entidade)
        {
            resultado = new Resultado();
            string nmClasse = entidade.GetType().Name;

            string msg = executarRegras(entidade, "SALVAR");


            if (msg == null)
            {
                IDAO dao = daos[nmClasse];

                dao.salvar(entidade);
                List<EntidadeDominio> entidades = new List<EntidadeDominio>();
                entidades.Add(entidade);
                resultado.Entidades=entidades;

            }
            else
            {
                resultado.Msg=msg;


            }

            return resultado;
        }

        public Resultado alterar(EntidadeDominio entidade)
        {
            resultado = new Resultado();
            string nmClasse = entidade.GetType().Name;
            string msg = executarRegras(entidade, "ALTERAR");

            if (msg == null)
            {
                IDAO dao = daos[nmClasse];
                dao.alterar(entidade);
                List<EntidadeDominio> entidades = new List<EntidadeDominio>();
                entidades.Add(entidade);
                resultado.Entidades=entidades;
            }
            else
            {
                resultado.Msg=msg;


            }

            return resultado;

        }


        public Resultado excluir(EntidadeDominio entidade)
        {
            resultado = new Resultado();
            string nmClasse = entidade.GetType().Name;
            string msg = executarRegras(entidade, "EXCLUIR");


            if (msg == null)
            {
                IDAO dao = daos[nmClasse];
                dao.excluir(entidade);
                List<EntidadeDominio> entidades = new List<EntidadeDominio>();
                entidades.Add(entidade);
                resultado.Entidades=entidades;
            }
            else
            {
                resultado.Msg=msg;
            }

            return resultado;

        }

        public Resultado consultar(EntidadeDominio entidade)
        {
            resultado = new Resultado();
            string nmClasse = entidade.GetType().Name;
            string msg = executarRegras(entidade, "CONSULTAR");


            if (msg == null)
            {
                IDAO dao = daos[nmClasse];
                try
                {

                    resultado.Entidades=dao.consultar(entidade);
                }
                catch
                {

                }
            }
            else
            {
                resultado.Msg=msg;

            }

            return resultado;

        }

        public Resultado visualizar(EntidadeDominio entidade)
        {
            resultado = new Resultado();
            resultado.Entidades=new List<EntidadeDominio>(1);
            resultado.Entidades.Add(entidade);
            return resultado;

        }
        public Resultado WebService(EntidadeDominio entidade)
        {
            resultado = new Resultado();
            resultado.Msg = "";
            string nmClasse = entidade.GetType().Name;
            IStrategy Ws;
            switch (nmClasse)
            {
                case "Endereco":
                            Ws = new WS_cep_json();
                            resultado.Msg = Ws.processar(entidade); 
                            break;
                case "Frete":
                            Ws = new Calcular_Frete();
                            resultado.Msg = Ws.processar(entidade);
                            break;
                default:
                    resultado.Msg = "erro de solitação de serviço!";
                    return resultado;
            }

            List<EntidadeDominio> entidades = new List<EntidadeDominio>();
            entidades.Add(entidade);
            resultado.Entidades = entidades;
            resultado.Msg = "";
            return resultado;
        }

        private string executarRegras(EntidadeDominio entidade, string operacao)
        {
            string nmClasse = entidade.GetType().Name;
            StringBuilder msg = new StringBuilder();

            Dictionary<string, List<IStrategy>> regrasOperacao = rns[nmClasse];


            if (regrasOperacao != null)
            {
                List<IStrategy> regras = regrasOperacao[operacao];

                if (regras != null)
                {
                    foreach (IStrategy s in regras)
                    {
                        string m = s.processar(entidade);

                        if (m != null)
                        {
                            msg.Append(m);
                            msg.Append("\n");
                        }
                    }
                }

            }

            if (msg.Length > 0)
                return msg.ToString();
            else
                return null;


        }
    }
    
}

