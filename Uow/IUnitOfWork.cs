using Microsoft.EntityFrameworkCore;

namespace EMS.Uow
{
    public interface IUnitOfWork : IDisposable
    {
        DbContext Context { get; }
        public Task SaveChangesAsync();
    }
}
