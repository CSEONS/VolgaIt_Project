using VolgaIt.Domain.Entities;

namespace VolgaIt.Domain.Repositories.Abstract
{
    public interface ITransportRepository
    {
        Transport GetById(string id);
        Transport GetByIdEager(string id);
        List<Transport> GetAll();
        List<Transport> GetAllEager();
        Task DeleteAsync(string id);
        Task AddAsync(Transport transport);
        void Update(Transport transport);
        Task SaveChangesAsyn();
    }
}
