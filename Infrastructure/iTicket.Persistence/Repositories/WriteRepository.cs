using iTicket.Application.Interfaces.Repositories;
using iTicket.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace iTicket.Persistence.Repositories
{
    public class WriteRepository<T>(DbContext dbContext) : IWriteRepository<T> where T : class, IEntityBase, new()
    {
        private readonly DbContext dbContext = dbContext;
        private DbSet<T> Table { get => dbContext.Set<T>(); }


        public async Task AddAsync(T entity) => await Table.AddAsync(entity);

        public async Task AddRangeAsync(IList<T> entities) => await Table.AddRangeAsync(entities);


        public async Task<T> UpdateAsync(T entity)
        {
            await Task.Run(() => Table.Update(entity));
            return entity;
        }
        public async Task UpdateRangeAsync(IList<T> entities)
        {
            await Task.Run(() => Table.UpdateRange(entities));
        }

        public async Task HardDeleteAsync(T entity) => await Task.Run(() => Table.Remove(entity));

        public async Task HardDeleteRangeAsync(IList<T> entities) => await Task.Run(() => Table.RemoveRange(entities));


    }
}
