using Round_OP.Models.Enums;

namespace Round_OP.ViewModels;

public class ReportsViewModel
{
    public string? Search { get; set; }
    public CaseStatus? Status { get; set; }
    public InvestigationPriority? Priority { get; set; }
    public ComplaintType? ComplaintType { get; set; }
    public List<ReportListItemViewModel> Reports { get; set; } = new();
}