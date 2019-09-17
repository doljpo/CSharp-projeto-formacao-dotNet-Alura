﻿using CasaDoCodigo.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repositories
{
    public interface ICategoriaRepository
    {
        Task SalvarCategoria(string nome);
        Categoria BuscarCategoria(string nome);
    }

    public class CategoriaRepository : BaseRepository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(ApplicationContext contexto) : base(contexto)
        {
        }

        public async Task SalvarCategoria(string nome)
        {
            if (!VerificarSeCategoriaExiste(nome))
            {
                dbSet.Add(new Categoria() { Nome = nome });
                await contexto.SaveChangesAsync();
            }
        }

        private bool VerificarSeCategoriaExiste(string nome)
        {
            return dbSet.Where(c => c.Nome == nome).Any();
        }

        public Categoria BuscarCategoria(string nome)
        {   
            return dbSet.Where(c => c.Nome == nome).SingleOrDefault();
        }
    }
}