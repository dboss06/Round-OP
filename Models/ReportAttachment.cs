using System.ComponentModel.DataAnnotations;

namespace Round_OP.Models;

public class ReportAttachment
{
    [Key]
    public int Id { get; set; }
    public int InvestigationReportId { get; set; }
    public string OriginalFileName { get; set; } = string.Empty;
    public string StoredFileName { get; set; } = string.Empty;
    public string FilePath { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
    public long FileSize { get; set; }
    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
    public InvestigationReport InvestigationReport { get; set; } = null!;
}