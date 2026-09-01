using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Round_OP.Models.Enums;

namespace Round_OP.ViewModels;

public class InvestigationReportViewModel
{
    // =========================================================
    // INVESTIGATOR INFORMATION
    // =========================================================

    [Required]
    [Display(Name = "Investigator Name")]
    public string InvestigatorName { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Badge/ID Number")]
    public string BadgeIdNumber { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Police Station/Unit")]
    public string PoliceStationUnit { get; set; } = string.Empty;
    [Required]
    [DataType(DataType.Date)]
    [Display(Name = "Date of Report")]
    public DateTime DateOfReport { get; set; }


    // =========================================================
    // CASE INFORMATION
    // =========================================================

    [Display(Name = "Case Number")]
    public string? CaseNumber { get; set; }

    [Display(Name = "Report Number")]
    public string? ReportNumber { get; set; }

    [Required]
    [Display(Name = "Case Status")]
    public CaseStatus? CaseStatus { get; set; }

    [Required]
    [Display(Name = "Investigation Priority")]
    public InvestigationPriority? InvestigationPriority { get; set; }

    [Required]
    [Display(Name = "Complaint Type")]
    public ComplaintType? ComplaintType { get; set; }


    // =========================================================
    // INVESTIGATION ACTIONS
    // =========================================================

    [Required]
    [Display(Name = "Initial Complaint Received")]
    public YesNo? InitialComplaintReceived { get; set; }

    [Required]
    [Display(Name = "Evidence Received")]
    public YesNo? EvidenceReceived { get; set; }

    [Required]
    [Display(Name = "Evidence Verified")]
    public YesNo? EvidenceVerified { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "Witness Interview Conducted")]
    public DateTime? WitnessInterviewDate { get; set; }

    [Required]
    [Display(Name = "Blockchain Analysis Requested")]
    public YesNo? BlockchainAnalysisRequested { get; set; }

    [Required]
    [Display(Name = "Blockchain Analysis Approved")]
    public YesNo? BlockchainAnalysisApproved { get; set; }

    [Required]
    [Display(Name = "Transaction History Obtained")]
    public YesNo? TransactionHistoryObtained { get; set; }

    [Required]
    [Display(Name = "Wallet Tracing Completed")]
    public YesNo? WalletTracingCompleted { get; set; }

    [Required]
    [Display(Name = "Exchange Information Requested")]
    public YesNo? ExchangeInformationRequested { get; set; }

    [Required]
    [Display(Name = "Chain of Custody Updated")]
    public YesNo? ChainOfCustodyUpdated { get; set; }

    [Required]
    [Display(Name = "Report Submitted to Headquarters")]
    public string ReportSubmittedToHeadquarters { get; set; } = string.Empty;


    // =========================================================
    // BLOCKCHAIN INVESTIGATION
    // =========================================================

    [Required]
    [Display(Name = "Blockchain Platform")]
    public YesNo? BlockchainPlatform { get; set; }

    [Display(Name = "Wallet Address")]
    public string? WalletAddress { get; set; }

    [Display(Name = "Incoming Transactions")]
    public string? IncomingTransactions { get; set; }

    [Display(Name = "Outgoing Transactions")]
    public string? OutgoingTransactions { get; set; }

    [Display(Name = "Related Wallets Identified")]
    public YesNo? RelatedWalletsIdentified { get; set; }

    [Display(Name = "Linked Exchange")]
    public string? LinkedExchange { get; set; }

    [Display(Name = "Fund Frozen")]
    public FundFreezeStatus? FundFreezeStatus { get; set; }


    // =========================================================
    // EVIDENCE CORROBORATION AND FINDINGS
    // =========================================================

    [Required]
    [Display(Name = "Evidence Matches Victim Statement")]
    public YesNo? EvidenceMatchesVictimStatement { get; set; }

    [Required]
    [Display(Name = "Evidence Partially or Fully Matches")]
    public EvidenceMatch? EvidenceMatchLevel { get; set; }

    [Display(Name = "Complaints Statement")]
    public string? ComplaintsStatement { get; set; }

    [Required]
    [Display(Name = "Further Evidence Requested")]
    public YesNo? FurtherEvidenceRequested { get; set; }

    [Required]
    [Display(Name = "Fraud Confirmed")]
    public YesNo? FraudConfirmed { get; set; }

    [Required]
    [Display(Name = "Investigation Continuing")]
    public YesNo? InvestigationContinuing { get; set; }


    // =========================================================
    // RECOMMENDATIONS AND FINAL SUBMISSION
    // =========================================================

    [Range(1, 5)]
    [Display(Name = "Refer for Prosecution")]
    public int? ProsecutionReferralRating { get; set; }

    [Display(Name = "Contact Cryptocurrency Exchange")]
    public string? ContactCryptocurrencyExchange { get; set; }

    [Display(Name = "Submit to Headquarters")]
    public string? SubmitToHeadquarters { get; set; }

    [Display(Name = "Close Case")]
    public string? CloseCase { get; set; }

    [Required]
    [Display(Name = "Report Reviewed")]
    public YesNo? ReportReviewed { get; set; }

    [Required]
    [Display(Name = "Supervisor Approval")]
    public YesNo? SupervisorApproval { get; set; }

    [Display(Name = "Headquarters Submission")]
    public string? HeadquartersSubmission { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "Case Closed")]
    public DateTime? CaseClosedDate { get; set; }


    // =========================================================
    // ATTACHMENTS
    // =========================================================

    [Display(Name = "Attachments")]
    public List<IFormFile> Attachments { get; set; } = new();

    // Existing attachments already saved to this report (populated when editing)
    public List<ExistingAttachmentViewModel> ExistingAttachments { get; set; } = new();

    // =================================================
    // WALLET REGISTRATION QUESTIONS
    // =================================================

    public string? WalletRegistrationType { get; set; }
    public string? WalletPurpose { get; set; }
    public string? WalletNetwork { get; set; }
    public string? HasExistingWallet { get; set; }
    public string? PreferredWalletType { get; set; }
    public string? WalletRecoveryBackedUp { get; set; }
    public string? ExpectedTransactionFrequency { get; set; }
    public string? UnderstandsRecoveryPhraseSecurity { get; set; }


    // =================================================
    // FINANCIAL & ASSET FLOW ANALYSIS
    // =================================================

    public string? InitialTransactionAmount { get; set; }
    public string? SubsequentFundMovements { get; set; }
    public string? FundsDividedOrMultipleAddresses { get; set; }
    public string? FundsConsolidatedIntoAnotherWallet { get; set; }
    public string? TransactionFeesOrConversions { get; set; }
    public string? FundsCorrespondWithComplainantAccount { get; set; }
    public string? UnusualTransactionPatterns { get; set; }
    public string? UnaccountedFinancialTrail { get; set; }
    public string? MostSignificantUnresolvedLead { get; set; }


    // =================================================
    // WALLET RELATIONSHIP ANALYSIS
    // =================================================

    public string? WalletAddressIdentificationMethod { get; set; }
    public string? MultipleAddressesInteracting { get; set; }
    public string? RepeatedTransactionAddresses { get; set; }
    public string? IncomingOutgoingAnalyzedSeparately { get; set; }
    public string? CommonActivityPatterns { get; set; }
    public string? AddressesLinkedToExchanges { get; set; }
    public string? NewOrUnknownAddressesEncountered { get; set; }
    public string? WalletRelationshipsRequiringTracing { get; set; }
}