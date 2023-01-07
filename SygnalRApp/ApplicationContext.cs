using Microsoft.EntityFrameworkCore;
using SignalRApp.Entities;
using System.Linq;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace SignalRApp
{
    /// <summary>
    /// Контекст для работы с базой данных
    /// </summary>
    public class ApplicationContext : IdentityDbContext<UserIdentity, RoleIdentity, string>
    {
        public ApplicationContext()
        {

        }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
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

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            BuildUserIdentity(builder);
            BuildUser(builder);
        }

        /// <summary>
        /// Обеспечивает построение авторизации
        /// </summary>
        /// <param name="modelBuilder">Билдер для привязки модели к БД</param>
        private static void BuildUserIdentity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserIdentity>()
                .HasMany(x => x.Claims)
                .WithOne()
                .HasForeignKey(x => x.UserId)
                .IsRequired();
        }

        /// <summary>
        /// Обеспечивает построение пользователей
        /// </summary>
        /// <param name="modelBuilder">Билдер для привязки модели к БД</param>
        private static void BuildUser(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>()
                .HasOne(x => x.Identity)
                .WithOne()
                .HasForeignKey(typeof(UserEntity));

            modelBuilder.Entity<UserEntity>()
                .HasIndex(x => x.IdentityId)
                .IsUnique();
        }

    }
}
