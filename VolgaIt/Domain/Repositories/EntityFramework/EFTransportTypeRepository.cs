using Microsoft.EntityFrameworkCore;
using VolgaIt.Domain.Entities;
using VolgaIt.Domain.Repositories.Abstract;

namespace VolgaIt.Domain.Repositories.EntityFramework
{
    public class EFTransportTypeRepository : ITransportTypeRepository
    {
        private readonly ApplicationDbContext _context;

        public EFTransportTypeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(TransportType type)
        {
            await _context.TransportTypes.AddAsync(type);
        }

        public async Task DeleteAsync(string id)
        {
            var type = await _context.TransportTypes.FirstOrDefaultAsync(t => t.Id == id);
            
            _context.TransportTypes.Remove(type);
        }

        public async Task<List<TransportType>> GetAll() 
        {
            return await _context.TransportTypes.ToListAsync();
        }

        public Task<TransportType> GetById(string id)
        {
            var type = _context.TransportTypes.FirstOrDefaultAsync(t => t.Id == id);

            return type;
        }

        public async Task<TransportType> GetByNameAsync(string name)
        {
            var type = await _context.TransportTypes.FirstOrDefaultAsync(t => t.NormalizedName.ToUpper() == name.ToUpper());

            return type;
        }

        public async Task SaveChangesAsyn()
        {
            await _context.SaveChangesAsync();
        }

        public async void Update(string id)
        {
            var type = await _context.TransportTypes.FirstOrDefaultAsync(t => t.Id == id);

            _context.TransportTypes.Update(type);
        }
    }
}
