using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly Cloudinary _cloudinary;

    public ReportsController(ApplicationDbContext context, IWebHostEnvironment environment,
    UserManager<ApplicationUser> userManager, Cloudinary cloudinary)
    {
        _context = context;
        _environment = environment;
        _userManager = userManager;
        _cloudinary = cloudinary;
    }
    private const int MaxAttachments = 10;
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
        var userId = _userManager.GetUserId(User);
        var report = new InvestigationReport
        {
            ReportId = await GenerateReportIdAsync(),
            UserId = User.Identity?.IsAuthenticated == true ? userId : null,
            InvestigatorName = model.InvestigatorName,
            BadgeIdNumber = model.BadgeIdNumber,
            PoliceStationUnit = model.PoliceStationUnit,
            DateOfReport = model.DateOfReport,

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

            // =================================================
            // SECTION 6 - WALLET REGISTRATION QUESTIONS
            // =================================================

            WalletRegistrationType = model.WalletRegistrationType,
            WalletPurpose = model.WalletPurpose,
            WalletNetwork = model.WalletNetwork,
            HasExistingWallet = model.HasExistingWallet,
            PreferredWalletType = model.PreferredWalletType,
            WalletRecoveryBackedUp = model.WalletRecoveryBackedUp,
            ExpectedTransactionFrequency = model.ExpectedTransactionFrequency,
            UnderstandsRecoveryPhraseSecurity = model.UnderstandsRecoveryPhraseSecurity,


            // =================================================
            // SECTION 7 - FINANCIAL & ASSET FLOW ANALYSIS
            // =================================================

            InitialTransactionAmount = model.InitialTransactionAmount,
            SubsequentFundMovements = model.SubsequentFundMovements,
            FundsDividedOrMultipleAddresses = model.FundsDividedOrMultipleAddresses,
            FundsConsolidatedIntoAnotherWallet = model.FundsConsolidatedIntoAnotherWallet,
            TransactionFeesOrConversions = model.TransactionFeesOrConversions,
            FundsCorrespondWithComplainantAccount = model.FundsCorrespondWithComplainantAccount,
            UnusualTransactionPatterns = model.UnusualTransactionPatterns,
            UnaccountedFinancialTrail = model.UnaccountedFinancialTrail,
            MostSignificantUnresolvedLead = model.MostSignificantUnresolvedLead,


            // =================================================
            // SECTION 8 - WALLET RELATIONSHIP ANALYSIS
            // =================================================

            WalletAddressIdentificationMethod = model.WalletAddressIdentificationMethod,
            MultipleAddressesInteracting = model.MultipleAddressesInteracting,
            RepeatedTransactionAddresses = model.RepeatedTransactionAddresses,
            IncomingOutgoingAnalyzedSeparately = model.IncomingOutgoingAnalyzedSeparately,
            CommonActivityPatterns = model.CommonActivityPatterns,
            AddressesLinkedToExchanges = model.AddressesLinkedToExchanges,
            NewOrUnknownAddressesEncountered = model.NewOrUnknownAddressesEncountered,
            WalletRelationshipsRequiringTracing = model.WalletRelationshipsRequiringTracing,
            SubmittedAt = DateTime.UtcNow
        };

        var uploadDirectory = Path.Combine(
            _environment.ContentRootPath,
            "Uploads",
            "Reports",
            report.ReportId);

        try
        {
            await using var transaction =
                await _context.Database.BeginTransactionAsync();

            try
            {
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
                await transaction.RollbackAsync();
                throw;
            }
        }
        catch (Exception ex)
        {
            // Clean up upload directory if it was created
            if (Directory.Exists(uploadDirectory))
            {
                try
                {
                    Directory.Delete(
                        uploadDirectory,
                        recursive: true);
                }
                catch
                {
                    // Ignore cleanup errors
                }
            }

            ViewData["ToastType"] = "error";

            // Provide more specific error messages based on the exception
            if (ex.Message.Contains("timestamp with time zone", StringComparison.OrdinalIgnoreCase) ||
                ex.Message.Contains("DateTime", StringComparison.OrdinalIgnoreCase))
            {
                ViewData["ToastMessage"] = "Invalid date format. Please check the date fields.";
            }
            else if (ex.Message.Contains("duplicate", StringComparison.OrdinalIgnoreCase) ||
                     ex.Message.Contains("unique", StringComparison.OrdinalIgnoreCase))
            {
                ViewData["ToastMessage"] = "A report with this reference already exists. Please try again.";
            }
            else if (ex.Message.Contains("23505", StringComparison.OrdinalIgnoreCase)) // PostgreSQL unique violation
            {
                ViewData["ToastMessage"] = "A report with this reference already exists. Please try again.";
            }
            else if (ex is InvalidOperationException)
            {
                ViewData["ToastMessage"] = ex.Message;
            }
            else
            {
                ViewData["ToastMessage"] = "The report could not be submitted. Please try again.";
            }

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
        int attempts = 0;
        const int maxAttempts = 10;

        do
        {
            if (attempts >= maxAttempts)
            {
                throw new InvalidOperationException("Failed to generate unique ReportId after multiple attempts");
            }

            reportId =
                $"RPT-{DateTime.UtcNow:yyyyMMdd}-{Random.Shared.Next(100000, 999999)}";
            attempts++;

        } while (await _context.InvestigationReports
            .AnyAsync(r => r.ReportId == reportId));

        return reportId;
    }

    private async Task SaveAttachmentsAsync(InvestigationReport report, IEnumerable<IFormFile> files)
    {
        var validFiles = files.Where(file => file != null && file.Length > 0).ToList();
        if (!validFiles.Any()) return;

        foreach (var file in validFiles)
        {
            var extension = Path.GetExtension(file.FileName);
            var originalFileName = Path.GetFileName(file.FileName);
            var isImage = new[] { ".jpg", ".jpeg", ".png" }.Contains(extension.ToLowerInvariant());

            await using var stream = file.OpenReadStream();

            var uploadParams = isImage
                ? (RawUploadParams)new ImageUploadParams
                {
                    File = new FileDescription(originalFileName, stream),
                    Folder = $"round-op/{report.ReportId}",
                    PublicId = Guid.NewGuid().ToString("N")
                }
                : new RawUploadParams
                {
                    File = new FileDescription(originalFileName, stream),
                    Folder = $"round-op/{report.ReportId}",
                    PublicId = Guid.NewGuid().ToString("N")
                };

            var result = await _cloudinary.UploadAsync(uploadParams);

            if (result.Error != null)
                throw new InvalidOperationException($"Failed to upload {originalFileName}: {result.Error.Message}");

            report.Attachments.Add(new ReportAttachment
            {
                OriginalFileName = originalFileName,
                StoredFileName = result.PublicId,
                FilePath = result.SecureUrl.ToString(),   // now a URL, not a local path
                PublicId = result.PublicId,
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
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> ViewAttachment(int attachmentId)
    {
        var attachment = await _context.ReportAttachments
            .Include(a => a.InvestigationReport)
            .FirstOrDefaultAsync(a => a.Id == attachmentId);

        if (attachment == null)
            return NotFound();

        var userId = _userManager.GetUserId(User);
        var isAdmin = User.IsInRole("Admin");
        if (!isAdmin && attachment.InvestigationReport.UserId != userId)
            return Forbid();

        return Redirect(attachment.FilePath); // FilePath now holds the Cloudinary secure URL
    }

}
