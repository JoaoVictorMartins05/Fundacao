using Fundacao_API.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Fundacao_API.Repositories
{
    public class Repository : IRepository
    {
        public DataContext _context { get; }

        public Repository(DataContext context)
        {
            this._context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }
        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<Fundacao[]> GetAllFundacoesAsync()
        {
            IQueryable<Fundacao> query = _context.Fundacao;
            query = query.AsNoTracking().OrderBy(a => a.Id);

            return await query.ToArrayAsync();
        }

        public Task<Fundacao> GetFundacaoAsyncByCNPJ(int IDFundacao)
        {
            IQueryable<Fundacao> query = _context.Fundacao;
            query = query.AsNoTracking().Where(fundacao => fundacao.Id == IDFundacao);

            return (Task<Fundacao>) query.FirstOrDefaultAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
          return (await _context.SaveChangesAsync() > 0);
        }

       
    }
}

