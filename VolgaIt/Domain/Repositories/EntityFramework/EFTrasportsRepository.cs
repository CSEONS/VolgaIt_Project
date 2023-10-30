using Microsoft.EntityFrameworkCore;
using VolgaIt.Domain.Entities;
using VolgaIt.Domain.Repositories.Abstract;

namespace VolgaIt.Domain.Repositories.EntityFramework
{
    public class EFTrasportsRepository : ITransportRepository
    {
        private readonly ApplicationDbContext _context;
        public EFTrasportsRepository(ApplicationDbContext _context)
        {
            this._context = _context;
        }
        public async Task AddAsync(Transport transport)
        {
            if (string.IsNullOrEmpty(transport.Id))
                transport.Id = Guid.NewGuid().ToString();

            await _context.Transports.AddAsync(transport);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var transport = await _context.Transports.FirstOrDefaultAsync(t => t.Id == id);

            _context.Transports.Remove(transport);
        }

        public List<Transport> GetAll()
        {
            return _context.Transports.ToList();
        }

        public Transport GetById(string id)
        {
            return _context.Transports.FirstOrDefault(t => t.Id == id);
        }

        public void Update(Transport transport)
        {
            _context.Transports.Update(transport);
        }

        public async Task SaveChangesAsyn()
        {
            await _context.SaveChangesAsync();
        }

        public Transport GetByIdEager(string id)
        {
            return _context.Transports.Include(t => t.Owner).FirstOrDefault(t => t.Id == id);
        }
            
        public List<Transport> GetAllEager()
        {
            return _context.Transports.Include(t => t.Owner).ToList();
        }
    }
}
