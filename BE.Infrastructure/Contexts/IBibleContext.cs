namespace BE.Infrastructure.Contexts
{
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;

    interface IBibleContext
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        Task<int> SaveChangesAsync();
    }
}
