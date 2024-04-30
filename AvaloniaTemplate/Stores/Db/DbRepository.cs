using AvaloniaTemplate.Desktop.AppContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AvaloniaTemplate.Stores.Db
{
    public class DbRepository<T> : IRepository<T>
        where T : class, IEntity, new()
    {
        private IDbContextFactory<ApplicationContext> _dbContextFactory;
        private ApplicationContext _db => _dbContextFactory.CreateDbContext();
        private readonly DbSet<T> _dbSet;

        public virtual IQueryable<T> Items => _dbSet;

        public bool AutoSaveChanges { get; set; } = true;

        public T Add(T item)
        {
            if (item == null) throw new ArgumentNullException("item cannot be null");
            _db.Entry(item).State = EntityState.Added;
            if (AutoSaveChanges)
                _db.SaveChanges();
            return item;
        }

        public async Task<T> AddAsync(T item, CancellationToken cancellationToken = default)
        {
            if (item == null) throw new ArgumentNullException("item cannot be null");
            _db.Entry(item).State = EntityState.Added;
            if (AutoSaveChanges)
                await _db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return item;
        }

        public T? Get(int id) =>
            Items.SingleOrDefault(item => item.Id == id);

        public async Task<T?> GetAsync(int id, CancellationToken cancellationToken = default) =>
            await Items.SingleOrDefaultAsync(item => item.Id == id, cancellationToken)
            .ConfigureAwait(false);

        public void Remove(int id)
        {
            _db.Remove(new T { Id = id });
            if (AutoSaveChanges)
                _db.SaveChanges();
        }

        public async Task RemoveAsync(int id, CancellationToken cancellationToken = default)
        {
            _db.Remove(new T { Id = id });
            if (AutoSaveChanges)
                await _db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public void Update(T item)
        {
            if (item == null) throw new ArgumentNullException("item cannot be null");
            _db.Entry(item).State = EntityState.Modified;
            if (AutoSaveChanges)
                _db.SaveChanges();
        }

        public async Task UpdateAsync(T item, CancellationToken cancellationToken = default)
        {
            if (item == null) throw new ArgumentNullException("item cannot be null");
            _db.Entry(item).State = EntityState.Modified;
            if (AutoSaveChanges)
                await _db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public DbRepository(IDbContextFactory<ApplicationContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
            _dbSet = _db.Set<T>();
        }
    }
}
