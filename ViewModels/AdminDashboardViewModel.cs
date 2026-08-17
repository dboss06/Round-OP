using Round_OP.Models.Enums;

namespace Round_OP.ViewModels;

public class AdminDashboardViewModel
{
    public int TotalReports { get; set; }
    public int OpenReports { get; set; }
    public int PendingReports { get; set; }
    public int UnderReviewReports { get; set; }
    public int ClosedReports { get; set; }
    public List<ReportListItemViewModel> RecentReports { get; set; } = new();
}