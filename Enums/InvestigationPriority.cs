using System.ComponentModel.DataAnnotations;

namespace Round_OP.Models.Enums;

public enum InvestigationPriority
{
    [Display(Name = "Low")]
    Low,

    [Display(Name = "Medium")]
    Medium,

    [Display(Name = "High")]
    High,

    [Display(Name = "Critical")]
    Critical
}