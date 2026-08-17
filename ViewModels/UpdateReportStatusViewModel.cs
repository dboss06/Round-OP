using System.ComponentModel.DataAnnotations;
using Round_OP.Models.Enums;

namespace Round_OP.ViewModels;

public class UpdateReportStatusViewModel
{
    public int ReportId { get; set; }

    public string ReportReference { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Case Status")]
    public CaseStatus Status { get; set; }

    [Display(Name = "Admin Note")]
    [StringLength(1000)]
    public string? Note { get; set; }
}