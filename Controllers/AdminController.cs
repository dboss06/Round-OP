using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Round_OP.Data;
using Round_OP.Extensions;
using Round_OP.Models.Enums;
using Round_OP.ViewModels;

namespace Round_OP.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IWebHostEnvironment _environment;

    public AdminController(ApplicationDbContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var model = new AdminDashboardViewModel
        {
            TotalReports = await _context.InvestigationReports.CountAsync(),
            OpenReports = await _context.InvestigationReports.CountAsync(r => r.CaseStatus == CaseStatus.Open),
            PendingReports = await _context.InvestigationReports.CountAsync(r => r.CaseStatus == CaseStatus.Pending),
            UnderReviewReports = await _context.InvestigationReports.CountAsync(r => r.CaseStatus == CaseStatus.UnderReview),
            ClosedReports = await _context.InvestigationReports.CountAsync(r => r.CaseStatus == CaseStatus.Closed),
            RecentReports = await _context.InvestigationReports
                .AsNoTracking()
                .OrderByDescending(r => r.SubmittedAt)
                .Take(5)
                .Select(r => new ReportListItemViewModel
                {
                    Id = r.Id,
                    ReportId = r.ReportId,
                    InvestigatorName = r.InvestigatorName,
                    CaseNumber = r.CaseNumber,
                    ReportNumber = r.ReportNumber,
                    CaseStatus = r.CaseStatus,
                    InvestigationPriority = r.InvestigationPriority,
                    ComplaintType = r.ComplaintType,
                    SubmittedAt = r.SubmittedAt,
                    AttachmentCount = r.Attachments.Count
                })
                .ToListAsync()
        };

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Reports(string? search, CaseStatus? status, InvestigationPriority? priority, ComplaintType? complaintType)
    {
        var query = _context.InvestigationReports.AsNoTracking().AsQueryable();

        if (!string.IsNullOrEmpty(search))
        {
            search = search.Trim();
            query = query.Where(r => r.ReportId.Contains(search) || (r.CaseNumber != null &&  r.CaseNumber.Contains(search)) || (r.ReportNumber != null && r.ReportNumber.Contains(search)) || r.InvestigatorName.Contains(search));
        }

        if (status.HasValue)
        {
            query = query.Where(r => r.CaseStatus == status.Value);
        }
        if (priority.HasValue)
        {
            query = query.Where(r => r.InvestigationPriority == priority.Value);
        }
        if(complaintType.HasValue)
        {
            query = query.Where(r => r.ComplaintType == complaintType.Value);
        }
        var reports = await query.OrderByDescending(r => r.SubmittedAt).Select(r => new ReportListItemViewModel
        {
            Id = r.Id,
            ReportId = r.ReportId,
            InvestigatorName = r.InvestigatorName,
            CaseNumber = r.CaseNumber,
            ReportNumber = r.ReportNumber,
            CaseStatus = r.CaseStatus,
            InvestigationPriority = r.InvestigationPriority,
            ComplaintType = r.ComplaintType,
            SubmittedAt = r.SubmittedAt,
            AttachmentCount = r.Attachments.Count
        }).ToListAsync();

        var model = new ReportsViewModel
        {
            Search = search,
            Status = status,
            Priority = priority,
            ComplaintType = complaintType,
            Reports = reports
        };
        return View(model);
    }
    [HttpGet]
    public async Task<IActionResult> ReportDetails(int id)
    {
        var report = await _context.InvestigationReports.AsNoTracking().Include(r => r.Attachments).FirstOrDefaultAsync(r => r.Id == id);
        if(report == null)
        {
            return NotFound();
        }
        var model = new ReportDetailsViewModel
        {
            Id = report.Id,
            ReportId = report.ReportId,
            // Investigator
            InvestigatorName = report.InvestigatorName,
            BadgeIdNumber = report.BadgeIdNumber,
            PoliceStationUnit = report.PoliceStationUnit,
            DateOfReport = report.DateOfReport,
            // Case
            CaseNumber = report.CaseNumber,
            ReportNumber = report.ReportNumber,
            CaseStatus = report.CaseStatus,
            InvestigationPriority = report.InvestigationPriority,
            ComplaintType = report.ComplaintType,
            // Investigation actions
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
            // Blockchain
            BlockchainPlatform = report.BlockchainPlatform,
            WalletAddress = report.WalletAddress,
            IncomingTransactions = report.IncomingTransactions,
            OutgoingTransactions = report.OutgoingTransactions,
            RelatedWalletsIdentified = report.RelatedWalletsIdentified,
            LinkedExchange = report.LinkedExchange,
            FundFreezeStatus = report.FundFreezeStatus,
            // Findings
            EvidenceMatchesVictimStatement = report.EvidenceMatchesVictimStatement,
            EvidenceMatchLevel = report.EvidenceMatchLevel,
            ComplaintsStatement = report.ComplaintsStatement,
            FurtherEvidenceRequested = report.FurtherEvidenceRequested,
            FraudConfirmed = report.FraudConfirmed,
            InvestigationContinuing = report.InvestigationContinuing,
            // Final submission
            ProsecutionReferralRating = report.ProsecutionReferralRating,
            ContactCryptocurrencyExchange = report.ContactCryptocurrencyExchange,
            SubmitToHeadquarters = report.SubmitToHeadquarters,
            CloseCase = report.CloseCase,
            ReportReviewed = report.ReportReviewed,
            SupervisorApproval = report.SupervisorApproval,
            HeadquartersSubmission = report.HeadquartersSubmission,
            CaseClosedDate = report.CaseClosedDate,
            // System
            SubmittedAt = report.SubmittedAt,
            UpdatedAt = report.UpdatedAt,
            Attachments = report.Attachments.Select(a => new ReportAttachmentViewModel
            {
                Id = a.Id,
                OriginalFileName = a.OriginalFileName,
                ContentType = a.ContentType,
                FileSize = a.FileSize,
                UploadedAt = a.UploadedAt
            }).ToList()
        };

        return View(model);
    }
    public async Task<IActionResult> DownloadAttachment(int id)
    {
        var attachment = await _context.ReportAttachments.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
        if(attachment == null)
        {
            return NotFound();
        }

        if(string.IsNullOrWhiteSpace(attachment.FilePath))
        {
            return NotFound();
        }
        var uploadsRoot = Path.GetFullPath(Path.Combine(_environment.ContentRootPath, "Uploads", "Reports"));
        var requestedFilePath = Path.GetFullPath(attachment.FilePath);
        if (!requestedFilePath.StartsWith(uploadsRoot + Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase))
        {
            return Forbid();
        }
        if (!System.IO.File.Exists(requestedFilePath))
        {
            return NotFound();
        }
        
        var contentType = string.IsNullOrWhiteSpace(attachment.ContentType) ? "application/octet-stream" : attachment.ContentType;
        var downloadName = Path.GetFileName(attachment.OriginalFileName);
        if (string.IsNullOrWhiteSpace(downloadName))
        {
            downloadName = attachment.StoredFileName;
        }
        return PhysicalFile(requestedFilePath, contentType, downloadName);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateReportStatus(
    UpdateReportStatusViewModel model)
    {
        if (!ModelState.IsValid)
        {
            TempData["ToastType"] = "error";
            TempData["ToastMessage"] =
                "Please select a valid case status.";

            return RedirectToAction(
                nameof(ReportDetails),
                new { id = model.ReportId });
        }

        var report = await _context.InvestigationReports
            .FirstOrDefaultAsync(r => r.Id == model.ReportId);

        if (report == null)
        {
            return NotFound();
        }

        report.CaseStatus = model.Status;
        report.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        TempData["ToastType"] = "success";
        TempData["ToastMessage"] =
            $"Report {report.ReportId} status updated to {model.Status.GetDisplayName()}.";

        return RedirectToAction(
            nameof(ReportDetails),
            new { id = report.Id });
    }
}