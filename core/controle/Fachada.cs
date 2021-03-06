﻿using core.core;
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


        private Gerar_log _gerar_log;

        public Gerar_log gerar_log
        {
            get { return _gerar_log; }
            set { _gerar_log = value; }
        }

        private Resultado resultado;
        private Fachada()
        {
            daos = new Dictionary<string, IDAO>();
            /* Intânciando o Map de Regras de Negócio */
            rns = new Dictionary<string, Dictionary<string, List<IStrategy>>>();
            _gerar_log = new Gerar_log();
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

            Grupo_precificacaoDAO g_preDAO = new Grupo_precificacaoDAO();
            daos.Add(typeof(Grupo_Precificacao).Name, g_preDAO);
            List<IStrategy> rnsSalvarGrupo_precificacao = new List<IStrategy>();
            List<IStrategy> rnsAlterarGrupo_precificacao = new List<IStrategy>();
            List<IStrategy> rnsExcluirGrupo_precificacao = new List<IStrategy>();
            rnsExcluirGrupo_precificacao.Add(para_ex);
            List<IStrategy> rnsConsultarGrupo_precificacao = new List<IStrategy>();
            Dictionary<string, List<IStrategy>> rnsGrupo_precificacao = new Dictionary<string, List<IStrategy>>();
            rnsGrupo_precificacao.Add("SALVAR", rnsSalvarGrupo_precificacao);
            rnsGrupo_precificacao.Add("ALTERAR", rnsAlterarGrupo_precificacao);
            rnsGrupo_precificacao.Add("EXCLUIR", rnsExcluirGrupo_precificacao);
            rnsGrupo_precificacao.Add("CONSULTAR", rnsConsultarGrupo_precificacao);
            rns.Add(typeof(Grupo_Precificacao).Name, rnsGrupo_precificacao);

            LivroDAO proDAO = new LivroDAO();
            daos.Add(typeof(Livro).Name, proDAO);
            Ativacao_livro at = new Ativacao_livro();
            //transfomar_formato trans_for = new transfomar_formato();
            List<IStrategy> rnsSalvarProduto = new List<IStrategy>();
            rnsSalvarProduto.Add(at);
            //rnsSalvarProduto.Add(val_cod);
            //rnsSalvarProduto.Add(trans_for);
            List<IStrategy> rnsAlterarProduto = new List<IStrategy>();
            //rnsAlterarProduto.Add(val_cod);
            //rnsAlterarProduto.Add(trans_for);
            List<IStrategy> rnsExcluirProduto = new List<IStrategy>();
            rnsExcluirProduto.Add(para_ex);
            List<IStrategy> rnsConsultarProduto = new List<IStrategy>();
            //rnsConsultarProduto.Add(at);
            Dictionary<string, List<IStrategy>> rnsProduto = new Dictionary<string, List<IStrategy>>();
            rnsProduto.Add("SALVAR", rnsSalvarProduto);
            rnsProduto.Add("ALTERAR", rnsAlterarProduto);
            rnsProduto.Add("EXCLUIR", rnsExcluirProduto);
            rnsProduto.Add("CONSULTAR", rnsConsultarProduto);
            rns.Add(typeof(Livro).Name, rnsProduto);

            EnderecoDAO endDAO = new EnderecoDAO();
            daos.Add(typeof(Endereco).Name, endDAO);
            Validar_endereco val_end = new Validar_endereco();
            List<IStrategy> rnsSalvarendereco = new List<IStrategy>() { val_end};
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

            Cartao_CreditoDAO carDAO = new Cartao_CreditoDAO();
            GerarBandeira gb = new GerarBandeira();
            Validar_Cartao vc = new Validar_Cartao();
            daos.Add(typeof(Cartao_Credito).Name, carDAO);
            List<IStrategy> rnsSalvarCartao_Credito = new List<IStrategy>();
            rnsSalvarCartao_Credito.Add(gb);
            rnsSalvarCartao_Credito.Add(vc);
            List<IStrategy> rnsAlterarCartao_Credito = new List<IStrategy>();
            rnsAlterarCartao_Credito.Add(gb);
            rnsAlterarCartao_Credito.Add(vc);
            List<IStrategy> rnsExcluirCartao_Credito = new List<IStrategy>();
            rnsExcluirendereco.Add(para_ex);
            List<IStrategy> rnsConsultarCartao_Credito = new List<IStrategy>();
            Dictionary<string, List<IStrategy>> rnsCartao_Credito = new Dictionary<string, List<IStrategy>>();
            rnsCartao_Credito.Add("SALVAR", rnsSalvarCartao_Credito);
            rnsCartao_Credito.Add("ALTERAR", rnsAlterarCartao_Credito);
            rnsCartao_Credito.Add("EXCLUIR", rnsExcluirCartao_Credito);
            rnsCartao_Credito.Add("CONSULTAR", rnsConsultarCartao_Credito);
            rns.Add(typeof(Cartao_Credito).Name, rnsCartao_Credito);

            ClienteDAO cliDAO = new ClienteDAO();
            daos.Add(typeof(Cliente).Name, cliDAO);
            ValidarCpf val_cpf = new ValidarCpf();
            Validar_senha val_senha = new Validar_senha();
            Criptografar_senha cri_senha = new Criptografar_senha();
            Validar_end_cli val_end_cli = new Validar_end_cli();
            List<IStrategy> rnsSalvarCliente = new List<IStrategy>() {val_cpf,val_senha,cri_senha,val_end_cli };
            List<IStrategy> rnsAlterarCliente = new List<IStrategy>() { val_cpf, val_senha,cri_senha,val_end_cli };
            List<IStrategy> rnsExcluirCliente = new List<IStrategy>() {para_ex,cri_senha };
            List<IStrategy> rnsConsultarCliente = new List<IStrategy>() { cri_senha};
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
            Manter_ranking manRan = new Manter_ranking();
            daos.Add(typeof(Venda).Name, venDAO);
            List<IStrategy> rnsSalvarVenda = new List<IStrategy>();
            rnsSalvarVenda.Add(manRan);
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

            gerar_produtos_venda gpv = new gerar_produtos_venda();
            List<IStrategy> rnsSalvarGerar_produtos = new List<IStrategy>();
            List<IStrategy> rnsAlterarGerar_produtos = new List<IStrategy>();
            List<IStrategy> rnsExcluirGerar_produtos = new List<IStrategy>();
            rnsExcluirGerar_produtos.Add(para_ex);
            List<IStrategy> rnsConsultarGerar_produtos = new List<IStrategy>() { gpv };
            Dictionary<string, List<IStrategy>> rnsGerar_produtos = new Dictionary<string, List<IStrategy>>();
            rnsGerar_produtos.Add("SALVAR", rnsSalvarGerar_produtos);
            rnsGerar_produtos.Add("ALTERAR", rnsAlterarGerar_produtos);
            rnsGerar_produtos.Add("EXCLUIR", rnsExcluirGerar_produtos);
            rnsGerar_produtos.Add("CONSULTAR", rnsConsultarGerar_produtos);
            rns.Add(typeof(Gerar_produtos).Name, rnsGerar_produtos);

            List<IStrategy> rnsConsultarFrete = new List<IStrategy>();
            rnsConsultarFrete.Add(CalFrete);
            Dictionary<string, List<IStrategy>> rnsFrete = new Dictionary<string, List<IStrategy>>();
            rnsFrete.Add("CONSULTAR", rnsConsultarFrete);
            rns.Add(typeof(Frete).Name, rnsFrete);

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


            if (string.IsNullOrEmpty(msg))
            {
                IDAO dao = daos[nmClasse];
                dao.salvar(entidade);
                gerar_log.processar(entidade);
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

            if (string.IsNullOrEmpty(msg))
            {
                IDAO dao = daos[nmClasse];
                dao.alterar(entidade);
                gerar_log.processar(entidade);
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


            if (string.IsNullOrEmpty(msg))
            {
                
                IDAO dao = daos[nmClasse];
                dao.excluir(entidade);
                gerar_log.processar(entidade);
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


            if (string.IsNullOrEmpty( msg))
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

