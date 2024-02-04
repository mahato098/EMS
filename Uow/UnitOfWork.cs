using EMS.Data;
using Microsoft.EntityFrameworkCore;

namespace EMS.Uow
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDBContext _context;
        private bool _disposed = false;

        public UnitOfWork(ApplicationDBContext context)
        {
            _context = context;
        }

        public DbContext Context => _context;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                _disposed = true;
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
