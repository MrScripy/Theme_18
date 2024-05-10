using AvaloniaTemplate.Desktop.AppContext;
using AvaloniaTemplate.Models;
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
        private readonly DbSet<T> _dbSet;

        public virtual IQueryable<T> Items => _dbSet;

        public bool AutoSaveChanges { get; set; } = true;

        public T Add(T item)
        {
            CheckIfNull(item);
            using (var db = _dbContextFactory.CreateDbContext())
            {
                db.Entry(item).State = EntityState.Added;
                if (AutoSaveChanges)
                    db.SaveChanges();
            }
            return item;
        }

        public async Task<T> AddAsync(T item, CancellationToken cancellationToken = default)
        {
            CheckIfNull(item);
            var db = await _dbContextFactory.CreateDbContextAsync();
            using (db)
            {
                db.Entry(item).State = EntityState.Added;
                if (AutoSaveChanges)
                    await db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            }
            return item;
        }

        public T? Get(int id) =>
            Items.SingleOrDefault(item => item.Id == id);

        public async Task<T?> GetAsync(int id, CancellationToken cancellationToken = default) =>
            await Items.SingleOrDefaultAsync(item => item.Id == id, cancellationToken)
            .ConfigureAwait(false);

        public void Remove(int id)
        {
            using (var db = _dbContextFactory.CreateDbContext())
            {
                db.Remove(new T { Id = id });
                if (AutoSaveChanges)
                    db.SaveChanges();
            }
        }

        public async Task RemoveAsync(int id, CancellationToken cancellationToken = default)
        {
            var db = await _dbContextFactory.CreateDbContextAsync();
            using (db)
            {
                db.Remove(new T { Id = id });
                if (AutoSaveChanges)
                    await db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            }
        }

        public void Update(T item)
        {
            CheckIfNull(item);

            using (var db = _dbContextFactory.CreateDbContext())
            {
                db.Entry(item).State = EntityState.Modified;
                if (AutoSaveChanges)
                    db.SaveChanges();
            }
        }

        public async Task UpdateAsync(T item, CancellationToken cancellationToken = default)
        {
            CheckIfNull(item);
            var db = await _dbContextFactory.CreateDbContextAsync();
            using (db)
            {
                db.Entry(item).State = EntityState.Modified;
                if (AutoSaveChanges)
                    await db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            }
        }

        private void CheckIfNull(T item)
        {
            if (item == null) throw new ArgumentNullException("item cannot be null");
        }

        public DbRepository(IDbContextFactory<ApplicationContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
            _dbSet = _dbContextFactory.CreateDbContext().Set<T>();
        }
    }
}
