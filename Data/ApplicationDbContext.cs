using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Round_OP.Models;

namespace Round_OP.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { 
        }

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

            builder.Entity<ReportAttachment>()
                .HasOne(a => a.InvestigationReport)
                .WithMany(r => r.Attachments)
                .HasForeignKey(a => a.InvestigationReportId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
