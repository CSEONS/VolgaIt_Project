using VolgaIt.Domain.Entities;

namespace VolgaIt.Domain.Repositories.Abstract
{
    public interface ITransportTypeRepository
    {
        Task<TransportType> GetById(string id);
        Task<TransportType> GetByNameAsync(string name);
        Task<List<TransportType>> GetAll();
        Task DeleteAsync(string id);
        Task AddAsync(TransportType transport);
        void Update(string id);
        Task SaveChangesAsyn();
    }
}
