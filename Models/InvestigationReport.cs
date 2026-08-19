using Round_OP.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Round_OP.Models;

public class InvestigationReport
{
    [Key]
    public int Id { get; set; }
    public string? UserId { get; set; }

    public ApplicationUser? User { get; set; }
    public string ReportId { get; set; } = string.Empty;

    // Investigator Information
    public string InvestigatorName { get; set; } = string.Empty;
    public string BadgeIdNumber { get; set; } = string.Empty;
    public string PoliceStationUnit { get; set; } = string.Empty;
    public DateTime? DateOfReport { get; set; }

    // Case Information
    public string? CaseNumber { get; set; }
    public string? ReportNumber { get; set; }
    public CaseStatus CaseStatus { get; set; }
    public InvestigationPriority InvestigationPriority { get; set; }
    public ComplaintType ComplaintType { get; set; }

    // Investigation Actions
    public YesNo InitialComplaintReceived { get; set; }
    public YesNo EvidenceReceived { get; set; }
    public YesNo EvidenceVerified { get; set; }
    public DateTime? WitnessInterviewDate { get; set; }
    public YesNo BlockchainAnalysisRequested { get; set; }
    public YesNo BlockchainAnalysisApproved { get; set; }
    public YesNo TransactionHistoryObtained { get; set; }
    public YesNo WalletTracingCompleted { get; set; }
    public YesNo ExchangeInformationRequested { get; set; }
    public YesNo ChainOfCustodyUpdated { get; set; }
    public string ReportSubmittedToHeadquarters { get; set; } = string.Empty;

    // Blockchain Investigation
    public BlockchainPlatform BlockchainPlatform { get; set; }
    public string? WalletAddress { get; set; }
    public string? IncomingTransactions { get; set; }
    public string? OutgoingTransactions { get; set; }
    public YesNo? RelatedWalletsIdentified { get; set; }
    public string? LinkedExchange { get; set; }
    public FundFreezeStatus? FundFreezeStatus { get; set; }

    // Evidence Corroboration and Findings
    public YesNo EvidenceMatchesVictimStatement { get; set; }
    public EvidenceMatch EvidenceMatchLevel { get; set; }
    public string? ComplaintsStatement { get; set; }
    public YesNo FurtherEvidenceRequested { get; set; }
    public YesNo FraudConfirmed { get; set; }
    public YesNo InvestigationContinuing { get; set; }

    // Recommendations and Final Submission
    public int? ProsecutionReferralRating { get; set; }
    public string? ContactCryptocurrencyExchange { get; set; }
    public string? SubmitToHeadquarters { get; set; }
    public string? CloseCase { get; set; }
    public YesNo? ReportReviewed { get; set; }
    public YesNo? SupervisorApproval { get; set; }
    public string? HeadquartersSubmission { get; set; }
    public DateTime? CaseClosedDate { get; set; }

    // System fields
    public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    public ICollection<ReportAttachment> Attachments { get; set; }
        = new List<ReportAttachment>();
}