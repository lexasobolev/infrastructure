using System.Data.Entity.ModelConfiguration;

namespace Infrastructure.Logging.Data
{
    class LogEntryConfiguration : EntityTypeConfiguration<LogEntryData>
    {
        public LogEntryConfiguration()
        {
            ToTable("Entries", "Log");
        }
    }
}