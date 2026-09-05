using System.ComponentModel.DataAnnotations;

namespace Round_OP.Models.Enums{
    public enum WalletCompanyName
    {
        [Display(Name = "Bybit")]
        Bybit,

        [Display(Name = "Trust Wallet")]
        TrustWallet,

        [Display(Name = "Blockchain")]
        Blockchain
    }
}
