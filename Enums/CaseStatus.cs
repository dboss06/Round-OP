using System.ComponentModel.DataAnnotations;

namespace Round_OP.Models.Enums;

public enum CaseStatus
{
    [Display(Name = "Open")]
    Open,

    [Display(Name = "Pending")]
    Pending,

    [Display(Name = "Under Review")]
    UnderReview,

    [Display(Name = "Suspended")]
    Suspended,

    [Display(Name = "Closed")]
    Closed
}