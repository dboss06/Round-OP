using System.ComponentModel.DataAnnotations;

namespace Round_OP.Models.Enums;

public enum FundFreezeStatus
{
    [Display(Name = "Requested")]
    Requested,

    [Display(Name = "Denied")]
    Denied,

    [Display(Name = "Approved")]
    Approved
}