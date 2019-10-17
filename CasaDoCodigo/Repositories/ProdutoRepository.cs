using CasaDoCodigo.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repositories
{
    public class ProdutoRepository : BaseRepository<Produto>, IProdutoRepository
    {
        private readonly ICategoriaRepository categoriaRepository;

        public ProdutoRepository(ApplicationContext contexto, ICategoriaRepository categoriaRepository) 
            : base(contexto)
        {
            this.categoriaRepository = categoriaRepository;
        }

        public async Task<IList<Produto>> GetProdutos()
        {
            return await dbSet.Include(p => p.Categoria).ToListAsync();
        }

        public async Task<IList<Produto>> GetProdutos(string termoPesquisa)
        {
            if (string.IsNullOrEmpty(termoPesquisa))
            {
                return await GetProdutos();
            }
            else
            {
                return await dbSet.Include(p => p.Categoria)
                    .Where(p => p.Nome.ToUpper().Contains(termoPesquisa.ToUpper()) 
                    || p.Categoria.Nome.ToUpper().Contains(termoPesquisa.ToUpper())).ToListAsync();
            }
        }

        public async Task SaveProdutos(List<Livro> livros)
        {
            foreach (var livro in livros)
            {
                if (!dbSet.Where(p => p.Codigo == livro.Codigo).Any())
                {
                    await categoriaRepository.SalvarCategoria(livro.Categoria);

                    dbSet.Add(new Produto(livro.Codigo, livro.Nome, livro.Preco,
                        categoriaRepository.BuscarCategoria(livro.Categoria)));
                }
            }

            await contexto.SaveChangesAsync();
        }
    }
}
