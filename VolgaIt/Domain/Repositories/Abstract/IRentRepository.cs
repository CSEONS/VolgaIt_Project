using VolgaIt.Domain.Entities;

namespace VolgaIt.Domain.Repositories.Abstract
{
    public interface IRentRepository
    {
        IEnumerable<Rent> GetAll();
        Task<Rent?> GetByIdAsync(string id);
        Task<Rent?> GetByIdAsyncEager(string id);
        Task AddAsync(Rent rent);
        Task SaveChangesAsync();
        void Update(Rent rent);
        void Delte(Rent rent);
    }
}
