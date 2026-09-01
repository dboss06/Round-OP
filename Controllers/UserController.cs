using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Round_OP.Data;
using Round_OP.Models;
using Round_OP.Models.Enums;
using Round_OP.ViewModels;

namespace Round_OP.Controllers;

[Authorize]
public class UserController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public UserController(
        ApplicationDbContext context,
        UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var userId = _userManager.GetUserId(User);

        var report = await _context.InvestigationReports
            .AsNoTracking()
            .Include(r => r.Attachments)
            .FirstOrDefaultAsync(r => r.UserId == userId);

        if (report == null)
        {
            return RedirectToAction("Create", "Reports");
        }

        var model = new InvestigationReportViewModel
        {
            InvestigatorName = report.InvestigatorName,
            BadgeIdNumber = report.BadgeIdNumber,
            PoliceStationUnit = report.PoliceStationUnit,
            DateOfReport = report.DateOfReport!.Value,

            CaseNumber = report.CaseNumber,
            ReportNumber = report.ReportNumber,
            CaseStatus = report.CaseStatus,
            InvestigationPriority = report.InvestigationPriority,
            ComplaintType = report.ComplaintType,

            InitialComplaintReceived = report.InitialComplaintReceived,
            EvidenceReceived = report.EvidenceReceived,
            EvidenceVerified = report.EvidenceVerified,
            WitnessInterviewDate = report.WitnessInterviewDate,

            BlockchainAnalysisRequested = report.BlockchainAnalysisRequested,
            BlockchainAnalysisApproved = report.BlockchainAnalysisApproved,
            TransactionHistoryObtained = report.TransactionHistoryObtained,
            WalletTracingCompleted = report.WalletTracingCompleted,
            ExchangeInformationRequested = report.ExchangeInformationRequested,
            ChainOfCustodyUpdated = report.ChainOfCustodyUpdated,
            ReportSubmittedToHeadquarters = report.ReportSubmittedToHeadquarters,

            BlockchainPlatform = report.BlockchainPlatform,
            WalletAddress = report.WalletAddress,
            IncomingTransactions = report.IncomingTransactions,
            OutgoingTransactions = report.OutgoingTransactions,
            RelatedWalletsIdentified = report.RelatedWalletsIdentified,
            LinkedExchange = report.LinkedExchange,
            FundFreezeStatus = report.FundFreezeStatus,

            EvidenceMatchesVictimStatement = report.EvidenceMatchesVictimStatement,
            EvidenceMatchLevel = report.EvidenceMatchLevel,
            ComplaintsStatement = report.ComplaintsStatement,
            FurtherEvidenceRequested = report.FurtherEvidenceRequested,
            FraudConfirmed = report.FraudConfirmed,
            InvestigationContinuing = report.InvestigationContinuing,
            ProsecutionReferralRating = report.ProsecutionReferralRating,
            ContactCryptocurrencyExchange = report.ContactCryptocurrencyExchange,
            SubmitToHeadquarters = report.SubmitToHeadquarters,
            CloseCase = report.CloseCase,
            ReportReviewed = report.ReportReviewed,
            SupervisorApproval = report.SupervisorApproval,
            HeadquartersSubmission = report.HeadquartersSubmission,
            CaseClosedDate = report.CaseClosedDate,
            // =================================================
            // SECTION 6 - WALLET REGISTRATION QUESTIONS
            // =================================================

            WalletRegistrationType = report.WalletRegistrationType,
            WalletPurpose = report.WalletPurpose,
            WalletNetwork = report.WalletNetwork,
            HasExistingWallet = report.HasExistingWallet,
            PreferredWalletType = report.PreferredWalletType,
            WalletRecoveryBackedUp = report.WalletRecoveryBackedUp,
            ExpectedTransactionFrequency = report.ExpectedTransactionFrequency,
            UnderstandsRecoveryPhraseSecurity = report.UnderstandsRecoveryPhraseSecurity,


            // =================================================
            // SECTION 7 - FINANCIAL & ASSET FLOW ANALYSIS
            // =================================================

            InitialTransactionAmount = report.InitialTransactionAmount,
            SubsequentFundMovements = report.SubsequentFundMovements,
            FundsDividedOrMultipleAddresses = report.FundsDividedOrMultipleAddresses,
            FundsConsolidatedIntoAnotherWallet = report.FundsConsolidatedIntoAnotherWallet,
            TransactionFeesOrConversions = report.TransactionFeesOrConversions,
            FundsCorrespondWithComplainantAccount = report.FundsCorrespondWithComplainantAccount,
            UnusualTransactionPatterns = report.UnusualTransactionPatterns,
            UnaccountedFinancialTrail = report.UnaccountedFinancialTrail,
            MostSignificantUnresolvedLead = report.MostSignificantUnresolvedLead,


            // =================================================
            // SECTION 8 - WALLET RELATIONSHIP ANALYSIS
            // =================================================

            WalletAddressIdentificationMethod = report.WalletAddressIdentificationMethod,
            MultipleAddressesInteracting = report.MultipleAddressesInteracting,
            RepeatedTransactionAddresses = report.RepeatedTransactionAddresses,
            IncomingOutgoingAnalyzedSeparately = report.IncomingOutgoingAnalyzedSeparately,
            CommonActivityPatterns = report.CommonActivityPatterns,
            AddressesLinkedToExchanges = report.AddressesLinkedToExchanges,
            NewOrUnknownAddressesEncountered = report.NewOrUnknownAddressesEncountered,
            WalletRelationshipsRequiringTracing = report.WalletRelationshipsRequiringTracing,
            ExistingAttachments = report.Attachments.Select(a => new ExistingAttachmentViewModel
            {
                Id = a.Id,
                OriginalFileName = a.OriginalFileName,
                ContentType = a.ContentType
            }).ToList()
        };

        ViewData["IsUserEdit"] = true;

        return View("~/Views/Reports/Create.cshtml", model);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(InvestigationReportViewModel model)
    {
        var userId = _userManager.GetUserId(User);

        if (string.IsNullOrWhiteSpace(userId))
        {
            return Challenge();
        }
        var report = await _context.InvestigationReports.Include(r => r.Attachments).FirstOrDefaultAsync(r => r.UserId == userId);
        if (report == null)
        {
            return RedirectToAction("Create", "Reports");
        }

        if (!ModelState.IsValid)
        { 
            model.ExistingAttachments = report?.Attachments.Select(a => new ExistingAttachmentViewModel
            {
                Id = a.Id,
                OriginalFileName = a.OriginalFileName,
                ContentType = a.ContentType
            }).ToList() ?? new();

            ViewData["IsUserEdit"] = true;
            return View("~/Views/Reports/Create.cshtml", model);
        }

        report.InvestigatorName = model.InvestigatorName;
        report.BadgeIdNumber = model.BadgeIdNumber;
        report.PoliceStationUnit = model.PoliceStationUnit;
        report.DateOfReport = model.DateOfReport;

        report.CaseNumber = model.CaseNumber;
        report.ReportNumber = model.ReportNumber;
        report.CaseStatus = model.CaseStatus!.Value;
        report.InvestigationPriority = model.InvestigationPriority!.Value;
        report.ComplaintType = model.ComplaintType!.Value;
        report.InitialComplaintReceived = model.InitialComplaintReceived!.Value;
        report.EvidenceReceived = model.EvidenceReceived!.Value;
        report.EvidenceVerified = model.EvidenceVerified!.Value;
        report.WitnessInterviewDate = model.WitnessInterviewDate;
        report.BlockchainAnalysisRequested = model.BlockchainAnalysisRequested!.Value;
        report.BlockchainAnalysisApproved = model.BlockchainAnalysisApproved!.Value;
        report.TransactionHistoryObtained = model.TransactionHistoryObtained!.Value;
        report.WalletTracingCompleted = model.WalletTracingCompleted!.Value;
        report.ExchangeInformationRequested = model.ExchangeInformationRequested!.Value;
        report.ChainOfCustodyUpdated = model.ChainOfCustodyUpdated!.Value;
        report.ReportSubmittedToHeadquarters = model.ReportSubmittedToHeadquarters;
        report.BlockchainPlatform = model.BlockchainPlatform!.Value;
        report.WalletAddress = model.WalletAddress;
        report.IncomingTransactions = model.IncomingTransactions;
        report.OutgoingTransactions = model.OutgoingTransactions;
        report.RelatedWalletsIdentified = model.RelatedWalletsIdentified;
        report.LinkedExchange = model.LinkedExchange;
        report.FundFreezeStatus = model.FundFreezeStatus;

        report.EvidenceMatchesVictimStatement = model.EvidenceMatchesVictimStatement!.Value;
        report.EvidenceMatchLevel = model.EvidenceMatchLevel!.Value;
        report.ComplaintsStatement = model.ComplaintsStatement;
        report.FurtherEvidenceRequested = model.FurtherEvidenceRequested!.Value;
        report.FraudConfirmed = model.FraudConfirmed!.Value;
        report.InvestigationContinuing = model.InvestigationContinuing!.Value;
        report.ProsecutionReferralRating = model.ProsecutionReferralRating;
        report.ContactCryptocurrencyExchange = model.ContactCryptocurrencyExchange;
        report.SubmitToHeadquarters = model.SubmitToHeadquarters;
        report.CloseCase = model.CloseCase;
        report.ReportReviewed = model.ReportReviewed;
        report.SupervisorApproval = model.SupervisorApproval;
        report.HeadquartersSubmission = model.HeadquartersSubmission;
        report.CaseClosedDate = model.CaseClosedDate;
        // =================================================
        // SECTION 6 - WALLET REGISTRATION QUESTIONS
        // =================================================

        report.WalletRegistrationType = model.WalletRegistrationType;
        report.WalletPurpose = model.WalletPurpose;
        report.WalletNetwork = model.WalletNetwork;
        report.HasExistingWallet = model.HasExistingWallet;
        report.PreferredWalletType = model.PreferredWalletType;
        report.WalletRecoveryBackedUp = model.WalletRecoveryBackedUp;
        report.ExpectedTransactionFrequency = model.ExpectedTransactionFrequency;
        report.UnderstandsRecoveryPhraseSecurity =
            model.UnderstandsRecoveryPhraseSecurity;


        // =================================================
        // SECTION 7 - FINANCIAL & ASSET FLOW ANALYSIS
        // =================================================

        report.InitialTransactionAmount = model.InitialTransactionAmount;
        report.SubsequentFundMovements = model.SubsequentFundMovements;
        report.FundsDividedOrMultipleAddresses =
            model.FundsDividedOrMultipleAddresses;
        report.FundsConsolidatedIntoAnotherWallet =
            model.FundsConsolidatedIntoAnotherWallet;
        report.TransactionFeesOrConversions =
            model.TransactionFeesOrConversions;
        report.FundsCorrespondWithComplainantAccount =
            model.FundsCorrespondWithComplainantAccount;
        report.UnusualTransactionPatterns =
            model.UnusualTransactionPatterns;
        report.UnaccountedFinancialTrail =
            model.UnaccountedFinancialTrail;
        report.MostSignificantUnresolvedLead =
            model.MostSignificantUnresolvedLead;


        // =================================================
        // SECTION 8 - WALLET RELATIONSHIP ANALYSIS
        // =================================================

        report.WalletAddressIdentificationMethod =
            model.WalletAddressIdentificationMethod;
        report.MultipleAddressesInteracting =
            model.MultipleAddressesInteracting;
        report.RepeatedTransactionAddresses =
            model.RepeatedTransactionAddresses;
        report.IncomingOutgoingAnalyzedSeparately =
            model.IncomingOutgoingAnalyzedSeparately;
        report.CommonActivityPatterns =
            model.CommonActivityPatterns;
        report.AddressesLinkedToExchanges =
            model.AddressesLinkedToExchanges;
        report.NewOrUnknownAddressesEncountered =
            model.NewOrUnknownAddressesEncountered;
        report.WalletRelationshipsRequiringTracing =
            model.WalletRelationshipsRequiringTracing;
        report.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
    [HttpGet]
    public async Task<IActionResult> ViewAttachment(int attachmentId)
    {
        var userId = _userManager.GetUserId(User);

        var attachment = await _context.ReportAttachments
            .Include(a => a.InvestigationReport)
            .FirstOrDefaultAsync(a => a.Id == attachmentId);

        if (attachment == null || !System.IO.File.Exists(attachment.FilePath))
            return NotFound();

        if (attachment.InvestigationReport.UserId != userId)
            return Forbid();

        var stream = System.IO.File.OpenRead(attachment.FilePath);
        return File(stream, attachment.ContentType, attachment.OriginalFileName);
    }
}