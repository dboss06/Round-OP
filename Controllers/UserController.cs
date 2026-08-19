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
            DateOfReport = report.DateOfReport,

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
            CaseClosedDate = report.CaseClosedDate
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
        var report = await _context.InvestigationReports.FirstOrDefaultAsync(r => r.UserId == userId);
        if (report == null)
        {
            return RedirectToAction("Create", "Reports");
        }

        if (!ModelState.IsValid)
        {
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
        report.InitialComplaintReceived = model.InitialComplaintReceived;
        report.EvidenceReceived = model.EvidenceReceived;
        report.EvidenceVerified = model.EvidenceVerified;
        report.WitnessInterviewDate = model.WitnessInterviewDate;
        report.BlockchainAnalysisRequested = model.BlockchainAnalysisRequested;
        report.BlockchainAnalysisApproved = model.BlockchainAnalysisApproved;
        report.TransactionHistoryObtained = model.TransactionHistoryObtained;
        report.WalletTracingCompleted = model.WalletTracingCompleted;
        report.ExchangeInformationRequested = model.ExchangeInformationRequested;
        report.ChainOfCustodyUpdated = model.ChainOfCustodyUpdated;
        report.ReportSubmittedToHeadquarters = model.ReportSubmittedToHeadquarters;
        report.BlockchainPlatformBlockchainPlatform = model.BlockchainPlatformBlockchainPlatform;
        report.WalletAddress = model.WalletAddress;
        report.IncomingTransactions = model.IncomingTransactions;
        report.OutgoingTransactions = model.OutgoingTransactions;
        report.RelatedWalletsIdentified = model.RelatedWalletsIdentified;
        report.LinkedExchange = model.LinkedExchange;
        report.FundFreezeStatus = model.FundFreezeStatus;

        report.EvidenceMatchesVictimStatement = model.EvidenceMatchesVictimStatement;
        report.EvidenceMatchLevel = model.EvidenceMatchLevel;
        report.ComplaintsStatement = model.ComplaintsStatement;
        report.FurtherEvidenceRequested = model.FurtherEvidenceRequested;
        report.FraudConfirmed = model.FraudConfirmed;
        report.InvestigationContinuing = model.InvestigationContinuing;
        report.ProsecutionReferralRating = model.ProsecutionReferralRating;
        report.ContactCryptocurrencyExchange = model.ContactCryptocurrencyExchange;
        report.SubmitToHeadquarters = model.SubmitToHeadquarters;
        report.CloseCase = model.CloseCase;
        report.ReportReviewed = model.ReportReviewed;
        report.SupervisorApproval = model.SupervisorApproval;
        report.HeadquartersSubmission = model.HeadquartersSubmission;
        report.CaseClosedDate = model.CaseClosedDate;

        report.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
}