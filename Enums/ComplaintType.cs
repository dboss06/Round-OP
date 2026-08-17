using System.ComponentModel.DataAnnotations;

namespace Round_OP.Models.Enums;

public enum ComplaintType
{
    [Display(Name = "Cryptocurrency Fraud")]
    CryptocurrencyFraud,

    [Display(Name = "Investment Scam")]
    InvestmentScam,

    [Display(Name = "Identity Theft")]
    IdentityTheft,

    [Display(Name = "Money Laundering")]
    MoneyLaundering
}