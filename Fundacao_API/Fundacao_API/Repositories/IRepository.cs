using System;
using System.Threading.Tasks;

namespace Fundacao_API.Repositories
{
    public interface IRepository
    {
        //GERAL
        void Add<T>(T entity) where T : class;

        void Update<T>(T entity) where T : class;

        void Delete<T>(T entity) where T : class;

        Task<bool> SaveChangesAsync();

        //Fundacao
        Task<Fundacao[]> GetAllFundacoesAsync();

        Task<Fundacao> GetFundacaoAsyncByCNPJ(int IDFundacao);


    }
}
