using Microsoft.EntityFrameworkCore;
using VolgaIt.Domain.Entities;
using VolgaIt.Domain.Repositories.Abstract;

namespace VolgaIt.Domain.Repositories.EntityFramework
{
    public class EFRentRepository : IRentRepository
    {
        private readonly ApplicationDbContext _context;

        public EFRentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Rent rent)
        {
            await _context.Rents.AddAsync(rent);
        }

        public void Delte(Rent rent)
        {
            _context.Rents.Remove(rent);
        }

        public IEnumerable<Rent> GetAll()
        {
            return _context.Rents.AsEnumerable();
        }

        public async Task<Rent?> GetByIdAsync(string id)
        {
            return await _context.Rents.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Rent?> GetByIdAsyncEager(string id)
        {
            return await _context.Rents.Include(r => r.User).Include(r => r.Transport).FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(Rent rent)
        {
            _context.Update(rent);
        }
    }
}
