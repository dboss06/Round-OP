using System.ComponentModel.DataAnnotations;

namespace Round_OP.Models.Enums;

public enum EvidenceMatch
{
    [Display(Name = "Fully")]
    Fully,

    [Display(Name = "Partially")]
    Partially
}