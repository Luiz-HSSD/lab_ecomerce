using core.aplicacao;
using dominio;
using core.core;
using core.controle;

namespace lab.Command
{
    public abstract class AbstractCommand : ICommand.ICommand
    {
        protected IFachada fachada = Fachada.UniqueInstance;

        public abstract Resultado execute(EntidadeDominio entidade);
       
    }
}