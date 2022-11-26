using Microsoft.EntityFrameworkCore;
using SignalRApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRApp
{
    public class ApplicationContext : DbContext
    {
        public DbSet<UserEntity> UserEntities { get; set; }
        public DbSet<MessageEntity> MessageEntities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=salt.db.elephantsql.com;Port=5432;Database=viyrcyvr;Username=viyrcyvr;Password=JrFygnqjp5GWffyWue9i0xf_PeBlml19");
        }
    }
}
