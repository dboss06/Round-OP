using Round_OP.Models.Enums;

namespace Round_OP.ViewModels;

public class ReportListItemViewModel
{
    public int Id { get; set; }
    public string ReportId { get; set; } = string.Empty;
    public string InvestigatorName { get; set; } = string.Empty;
    public string? CaseNumber { get; set; }
    public string? ReportNumber { get; set; }
    public CaseStatus CaseStatus { get; set; }
    public InvestigationPriority InvestigationPriority { get; set; }
    public ComplaintType ComplaintType { get; set; }
    public DateTime SubmittedAt { get; set; }
    public int AttachmentCount { get; set; }
}