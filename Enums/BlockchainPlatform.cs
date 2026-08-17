using System.ComponentModel.DataAnnotations;

namespace Round_OP.Models.Enums;

public enum BlockchainPlatform
{
    [Display(Name = "Bitcoin (BTC)")]
    Bitcoin,

    [Display(Name = "Ethereum (ETH)")]
    Ethereum,

    [Display(Name = "Other")]
    Other
}