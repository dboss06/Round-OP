using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Round_OP.Migrations
{
    /// <inheritdoc />
    public partial class CreateInvestigationReports : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.CreateTable(
                name: "InvestigationReports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReportId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    InvestigatorName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BadgeIdNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PoliceStationUnit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfReport = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CaseNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReportNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CaseStatus = table.Column<int>(type: "int", nullable: false),
                    InvestigationPriority = table.Column<int>(type: "int", nullable: false),
                    ComplaintType = table.Column<int>(type: "int", nullable: false),
                    InitialComplaintReceived = table.Column<int>(type: "int", nullable: false),
                    EvidenceReceived = table.Column<int>(type: "int", nullable: false),
                    EvidenceVerified = table.Column<int>(type: "int", nullable: false),
                    WitnessInterviewDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BlockchainAnalysisRequested = table.Column<int>(type: "int", nullable: false),
                    BlockchainAnalysisApproved = table.Column<int>(type: "int", nullable: false),
                    TransactionHistoryObtained = table.Column<int>(type: "int", nullable: false),
                    WalletTracingCompleted = table.Column<int>(type: "int", nullable: false),
                    ExchangeInformationRequested = table.Column<int>(type: "int", nullable: false),
                    ChainOfCustodyUpdated = table.Column<int>(type: "int", nullable: false),
                    ReportSubmittedToHeadquarters = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BlockchainPlatform = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WalletAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IncomingTransactions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OutgoingTransactions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RelatedWalletsIdentified = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LinkedExchange = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FundFreezeStatus = table.Column<int>(type: "int", nullable: true),
                    EvidenceMatchesVictimStatement = table.Column<int>(type: "int", nullable: false),
                    EvidenceMatchLevel = table.Column<int>(type: "int", nullable: false),
                    ComplaintsStatement = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FurtherEvidenceRequested = table.Column<int>(type: "int", nullable: false),
                    FraudConfirmed = table.Column<int>(type: "int", nullable: false),
                    InvestigationContinuing = table.Column<int>(type: "int", nullable: false),
                    ProsecutionReferralRating = table.Column<int>(type: "int", nullable: true),
                    ContactCryptocurrencyExchange = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubmitToHeadquarters = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CloseCase = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReportReviewed = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupervisorApproval = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HeadquartersSubmission = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CaseClosedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SubmittedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvestigationReports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReportAttachments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvestigationReportId = table.Column<int>(type: "int", nullable: false),
                    OriginalFileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StoredFileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    UploadedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportAttachments_InvestigationReports_InvestigationReportId",
                        column: x => x.InvestigationReportId,
                        principalTable: "InvestigationReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvestigationReports_ReportId",
                table: "InvestigationReports",
                column: "ReportId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReportAttachments_InvestigationReportId",
                table: "ReportAttachments",
                column: "InvestigationReportId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReportAttachments");

            migrationBuilder.DropTable(
                name: "InvestigationReports");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
