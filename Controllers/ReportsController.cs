using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Round_OP.Data;
using Round_OP.Models;
using Round_OP.ViewModels;

namespace Round_OP.Controllers;
public class ReportsController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IWebHostEnvironment _environment;

    public ReportsController(ApplicationDbContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }
    private const int MaxAttachments = 5;
    private const long MaxFileSize = 60 * 1024 * 1024;
    private const long MaxTotalUploadSize = 300 * 1024 * 1024;
    private static readonly HashSet<string> AllowedExtensions =
    new(StringComparer.OrdinalIgnoreCase)
    {
        ".pdf",
        ".jpg",
        ".jpeg",
        ".png",
        ".doc",
        ".docx",
        ".xls",
        ".xlsx",
        ".txt"
    };
    [HttpGet]
    public IActionResult Create()
    {
        var model = new InvestigationReportViewModel();

        var uploadError = Request.Query["uploadError"].ToString();

        if (!string.IsNullOrWhiteSpace(uploadError))
        {
            ViewData["ToastType"] = "error";
            ViewData["ToastMessage"] = uploadError;
        }

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [RequestSizeLimit(MaxTotalUploadSize)]
    public async Task<IActionResult> Create(InvestigationReportViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ViewData["ToastType"] = "error";
            ViewData["ToastMessage"] = "Please review the form and correct the highlighted fields.";

            ViewData["ScrollTo"] = GetFirstInvalidField();
            return View(model);
        }
        ValidateAttachments(model.Attachments);
        if (!ModelState.IsValid)
        {
            ViewData["ToastType"] = "error";
            ViewData["ToastMessage"] =
                "One or more attachments are invalid.";

            ViewData["ScrollTo"] = GetFirstInvalidField();

            return View(model);
        }
        var report = new InvestigationReport
        {
            ReportId = await GenerateReportIdAsync(),

            InvestigatorName = model.InvestigatorName,
            BadgeIdNumber = model.BadgeIdNumber,
            PoliceStationUnit = model.PoliceStationUnit,
            DateOfReport = DateTime.SpecifyKind(model.DateOfReport!.Value, DateTimeKind.Utc),

            CaseNumber = model.CaseNumber,
            ReportNumber = model.ReportNumber,

            CaseStatus = model.CaseStatus!.Value,
            InvestigationPriority = model.InvestigationPriority!.Value,
            ComplaintType = model.ComplaintType!.Value,

            InitialComplaintReceived = model.InitialComplaintReceived!.Value,
            EvidenceReceived = model.EvidenceReceived!.Value,
            EvidenceVerified = model.EvidenceVerified!.Value,
            WitnessInterviewDate = model.WitnessInterviewDate,

            BlockchainAnalysisRequested = model.BlockchainAnalysisRequested!.Value,
            BlockchainAnalysisApproved = model.BlockchainAnalysisApproved!.Value,
            TransactionHistoryObtained = model.TransactionHistoryObtained!.Value,
            WalletTracingCompleted = model.WalletTracingCompleted!.Value,
            ExchangeInformationRequested = model.ExchangeInformationRequested!.Value,
            ChainOfCustodyUpdated = model.ChainOfCustodyUpdated!.Value,
            ReportSubmittedToHeadquarters = model.ReportSubmittedToHeadquarters,

            BlockchainPlatform = model.BlockchainPlatform!.Value,
            WalletAddress = model.WalletAddress,
            IncomingTransactions = model.IncomingTransactions,
            OutgoingTransactions = model.OutgoingTransactions,
            RelatedWalletsIdentified = model.RelatedWalletsIdentified!.Value,
            LinkedExchange = model.LinkedExchange,
            FundFreezeStatus = model.FundFreezeStatus,

            EvidenceMatchesVictimStatement = model.EvidenceMatchesVictimStatement!.Value,

            EvidenceMatchLevel = model.EvidenceMatchLevel!.Value,

            ComplaintsStatement = model.ComplaintsStatement,

            FurtherEvidenceRequested = model.FurtherEvidenceRequested!.Value,

            FraudConfirmed = model.FraudConfirmed!.Value,

            InvestigationContinuing = model.InvestigationContinuing!.Value,

            ProsecutionReferralRating = model.ProsecutionReferralRating,

            ContactCryptocurrencyExchange = model.ContactCryptocurrencyExchange,

            SubmitToHeadquarters = model.SubmitToHeadquarters,
            CloseCase = model.CloseCase,
            ReportReviewed = model.ReportReviewed!.Value,
            SupervisorApproval = model.SupervisorApproval!.Value,
            HeadquartersSubmission = model.HeadquartersSubmission,
            CaseClosedDate = model.CaseClosedDate,

            SubmittedAt = DateTime.UtcNow
        };
        _context.InvestigationReports.Add(report);
        var uploadDirectory = Path.Combine(
        _environment.ContentRootPath,
        "Uploads",
        "Reports",
        report.ReportId);

        try
        {
            await using var transaction =
                await _context.Database.BeginTransactionAsync();

            _context.InvestigationReports.Add(report);

            await _context.SaveChangesAsync();

            await SaveAttachmentsAsync(
                report,
                model.Attachments);

            await transaction.CommitAsync();

            return RedirectToAction(
                nameof(Success),
                new { reportId = report.ReportId });
        }
        catch
        {
            if (Directory.Exists(uploadDirectory))
            {
                Directory.Delete(
                    uploadDirectory,
                    recursive: true);
            }

            ViewData["ToastType"] = "error";
            ViewData["ToastMessage"] =
                "The report could not be submitted. Please try again.";

            return View(model);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Success(string reportId)
    {
        if (string.IsNullOrWhiteSpace(reportId))
        {
            return RedirectToAction(nameof(Create));
        }
        var exists = await _context.InvestigationReports.AsNoTracking().AnyAsync(r => r.ReportId == reportId);
        if(!exists)
        {
            return RedirectToAction(nameof(Create));
        }
        ViewData["ReportId"] = reportId;

        return View();
    }
    private string? GetFirstInvalidField()
    {
        return ModelState
            .Where(x => x.Value?.Errors.Count > 0)
            .Select(x => x.Key)
            .FirstOrDefault();
    }
    private async Task<string> GenerateReportIdAsync()
    {
        string reportId;

        do
        {
            reportId =
                $"RPT-{DateTime.UtcNow:yyyyMMdd}-{Random.Shared.Next(100000, 999999)}";

        } while (await _context.InvestigationReports
            .AnyAsync(r => r.ReportId == reportId));

        return reportId;
    }

    private async Task SaveAttachmentsAsync(InvestigationReport report, IEnumerable<IFormFile> files)
    {
        var validFiles = files.Where(file => file != null && file.Length > 0).ToList();
        
        if (!validFiles.Any())
        {
            return;
        }

        var uploadDirectory = Path.Combine(_environment.ContentRootPath, "Uploads", "Reports", report.ReportId);

        Directory.CreateDirectory(uploadDirectory);

        foreach (var file in validFiles)
        {
            var extension = Path.GetExtension(file.FileName);
            var storedFileName =
                $"{Guid.NewGuid():N}{extension}";

            var filePath = Path.Combine(
                uploadDirectory,
                storedFileName);
            var originalFileName = Path.GetFileName(file.FileName);
            await using var stream =
                new FileStream(filePath, FileMode.CreateNew);

            await file.CopyToAsync(stream);

            report.Attachments.Add(new ReportAttachment
            {
                OriginalFileName = Path.GetFileName(file.FileName),
                StoredFileName = storedFileName,
                FilePath = filePath,
                ContentType = file.ContentType ?? "application/octet-stream",
                FileSize = file.Length,
                UploadedAt = DateTime.UtcNow
            });
        }

        await _context.SaveChangesAsync();
    }
    private void ValidateAttachments(IEnumerable<IFormFile> files)
    {
        
        var validFiles = files
            .Where(file => file is not null && file.Length > 0)
            .ToList();

        if (validFiles.Count > MaxAttachments)
        {
            ModelState.AddModelError(
                nameof(InvestigationReportViewModel.Attachments),
                $"You can upload a maximum of {MaxAttachments} files.");
        }

        var totalSize = validFiles.Sum(file => file.Length);
        
        if (totalSize > MaxTotalUploadSize)
        {
            ModelState.AddModelError(
                nameof(InvestigationReportViewModel.Attachments),
                "The total size of all attachments cannot exceed 300 MB.");
        }
        foreach (var file in validFiles)
        {
            var originalFileName = Path.GetFileName(file.FileName);
            var extension = Path.GetExtension(originalFileName);

            if (string.IsNullOrWhiteSpace(extension) ||
                !AllowedExtensions.Contains(extension))
            {
                ModelState.AddModelError(
                    nameof(InvestigationReportViewModel.Attachments),
                    $"{Path.GetFileName(file.FileName)} is not an allowed file type.");
            }

            if (file.Length > MaxFileSize)
            {
                ModelState.AddModelError(
                    nameof(InvestigationReportViewModel.Attachments),
                    $"{Path.GetFileName(file.FileName)} exceeds the 60 MB file size limit.");
            }
            if (file.Length == 0)
            {
                ModelState.AddModelError(
                    nameof(InvestigationReportViewModel.Attachments),
                    $"{originalFileName} is empty.");
            }
        }
    }
}
