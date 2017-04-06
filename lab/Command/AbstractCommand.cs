using core.aplicacao;
using dominio;
using core.core;
using core.controle;

namespace lab.Command
{
    public abstract class AbstractCommand : ICommand.ICommand
    {
        protected IFachada fachada = new Fachada();

        public abstract Resultado execute(EntidadeDominio entidade);
       
    }
}