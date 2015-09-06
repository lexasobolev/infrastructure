using Infrastructure.Logging.Data.Migrations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Logging.Data
{
    public class LogContext : DbContext, ILogContext
    {
        static LogContext()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<LogContext, Configuration>());
        }

        public LogContext()
            : base("Name=Log")
        {
            Entries = new Repository<LogEntryData>(this);
        }

        public IRepository<LogEntryData> Entries { get; }

        public Task SaveAsync()
        {
            return SaveChangesAsync();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new LogEntryConfiguration());
        }
    }
}
