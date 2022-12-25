using Microsoft.EntityFrameworkCore;
using SignalRApp.Entities;
using System.Linq;

namespace SignalRApp
{
    /// <summary>
    /// Контекст для работы с базой данных
    /// </summary>
    public class ApplicationContext : DbContext
    {
        /// <summary>
        /// Сущность пользователя
        /// </summary>
        public DbSet<UserEntity> UserEntities { get; set; }

        /// <summary>
        /// Сущность сообщения
        /// </summary>
        public DbSet<MessageEntity> MessageEntities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=salt.db.elephantsql.com;Port=5432;Database=viyrcyvr;Username=viyrcyvr;Password=JrFygnqjp5GWffyWue9i0xf_PeBlml19");
        }

        /// <inheritdoc/>
        public override int SaveChanges()
        {
            var entries = ChangeTracker.Entries<BaseEntity>()
                .Where(e => e.State == EntityState.Modified);

            foreach (var entry in entries)
            {
                entry.Entity.UpdateModifiedTimestamp();
            }

            return base.SaveChanges();
        }
    }
}
