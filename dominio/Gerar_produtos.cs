using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Gerar_produtos : EntidadeDominio
    {

        private List<Produto> _colocar_preco;

        public List<Produto> Colocar_preco
        {
            get { return _colocar_preco; }
            set { _colocar_preco = value; }
        }

        public Gerar_produtos() : base(0, Convert.ToDateTime("01/01/1995 03:30"))
        {
            _colocar_preco = new List<Produto>();
        }
    }
}
