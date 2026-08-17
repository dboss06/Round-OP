using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Round_OP.Models;

namespace Round_OP.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IDataProtectionKeyContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<DataProtectionKey> DataProtectionKeys { get; set; } = null!;
        public DbSet<InvestigationReport> InvestigationReports { get; set; }
        public DbSet<ReportAttachment> ReportAttachments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<InvestigationReport>()
                .HasKey(r => r.Id);

            builder.Entity<InvestigationReport>()
                .HasIndex(r => r.ReportId)
                .IsUnique();

            // Configure calendar date properties as timestamp without time zone
            // These represent calendar dates (report date, interview date, case close date)
            // and do not require time zone information
            builder.Entity<InvestigationReport>()
                .Property(r => r.DateOfReport)
                .HasColumnType("timestamp without time zone");

            builder.Entity<InvestigationReport>()
                .Property(r => r.WitnessInterviewDate)
                .HasColumnType("timestamp without time zone");

            builder.Entity<InvestigationReport>()
                .Property(r => r.CaseClosedDate)
                .HasColumnType("timestamp without time zone");

            builder.Entity<ReportAttachment>()
                .HasOne(a => a.InvestigationReport)
                .WithMany(r => r.Attachments)
                .HasForeignKey(a => a.InvestigationReportId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
