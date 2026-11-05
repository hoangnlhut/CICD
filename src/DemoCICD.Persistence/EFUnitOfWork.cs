using DemoCICD.Domain.Abstractions;

namespace DemoCICD.Persistence
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public EFUnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
           await _context.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
