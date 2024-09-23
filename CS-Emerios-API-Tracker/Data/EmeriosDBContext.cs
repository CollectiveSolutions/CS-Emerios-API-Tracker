using CS_Emerios_API_Tracker.Entities;
using Microsoft.EntityFrameworkCore;

namespace CS_Emerios_API_Tracker.Data
{
    public partial class EmeriosDBContext : DbContext
    {
        public EmeriosDBContext()
        {

        }

        public EmeriosDBContext(DbContextOptions<EmeriosDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CallLogs> CallLogs { get; set; } = null!;
        public virtual DbSet<CallActivityLogs> CallActivityLogs { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CallLogs>(entity =>
            {
                entity.HasKey(e => e.C_Id).HasName("c_id");
                entity.ToTable("emr_record_logs");
                entity.Property(e => e.C_AgentId).HasColumnName("agent_id");
                entity.Property(e => e.DestinationPhone).HasColumnName("destination_phone");
                entity.Property(e => e.Callback_Url).HasColumnName("callback_url");
                entity.Property(e => e.Ip).HasColumnName("ip");
                entity.Property(e => e.Status).HasColumnName("status");
                entity.Property(e => e.Audio_file_sent).HasColumnName("audio_file_sent");
                entity.Property(e => e.Call_Id).HasColumnName("em_gen_callid");
                entity.Property(e => e.Date_added).HasColumnName("date_added");
                entity.Property(e => e.Date_updated).HasColumnName("date_updated");
                entity.Property(e => e.Audio_length).HasColumnName("audio_length");

            });

            modelBuilder.Entity<CallActivityLogs>(entity =>
            {
                entity.HasKey(e => e.LogId).HasName("log_id");
                entity.ToTable("emr_record_activity_logs");
                entity.Property(e => e.LogId).HasColumnName("log_id");
                entity.Property(e => e.ActivityLog).HasColumnName("activity");
                entity.Property(e => e.DateAdded).HasColumnName("date_added");
                entity.Property(e => e.DateAddedEst).HasColumnName("date_added_est");
            });
        }

    }
}
