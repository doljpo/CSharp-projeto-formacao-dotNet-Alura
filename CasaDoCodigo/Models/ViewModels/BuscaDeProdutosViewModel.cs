using System.Collections.Generic;

namespace CasaDoCodigo.Models.ViewModels
{
    public class BuscaDeProdutosViewModel
    {
        public BuscaDeProdutosViewModel(IList<Produto> produtos)
        {
            Produtos = produtos;
        }

        public IList<Produto> Produtos { get; }

        public string Pesquisa { get; set; }
    }
}
