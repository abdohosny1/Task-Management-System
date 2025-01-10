
namespace MyTask_Management_System.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly ApplicationDBContext _dBContext;

        public BaseRepository(ApplicationDBContext dBContext)
        {
            _dBContext = dBContext;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dBContext.Set<T>().AddAsync(entity);
            await _dBContext.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            // var data = await _dBContext.Set<T>().FindAsync(id);

            EntityEntry entityEntry = _dBContext.Entry<T>(entity);
            entityEntry.State = EntityState.Deleted;
            await _dBContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GellAllAsync(params Expression<Func<T, object>>[] includeProperty)
        {

            IQueryable<T> quary = _dBContext.Set<T>();
            quary = includeProperty.Aggregate(quary, (current, includeProperty)
                                   => current.Include(includeProperty));

            return await quary.ToListAsync();
        }

        public async Task<IEnumerable<T>> GellAllAsync()
        {
            return await _dBContext.Set<T>().ToListAsync();

        }

        public async Task<T> GetByIDAsync(int id)
        {
            return await _dBContext.Set<T>().FindAsync(id);
        }



        public async Task<T> UpdateAsync(T entity)
        {
            _dBContext.Set<T>().Update(entity);
            await _dBContext.SaveChangesAsync();
            return entity;
        }
    }
}
