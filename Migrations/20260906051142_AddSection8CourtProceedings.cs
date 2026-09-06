using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Round_OP.Migrations
{
    /// <inheritdoc />
    public partial class AddSection8CourtProceedings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClaimsOrAllegationsBeforeCourt",
                table: "InvestigationReports",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CourtCaseReferenceNumber",
                table: "InvestigationReports",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CourtDocumentsDescription",
                table: "InvestigationReports",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CourtNameAndLocation",
                table: "InvestigationReports",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CourtOrderDetails",
                table: "InvestigationReports",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CourtOrdersIssued",
                table: "InvestigationReports",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CourtRequestedIndependentInvestigation",
                table: "InvestigationReports",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateFiledWithCourt",
                table: "InvestigationReports",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DisputedAssetsIdentifiedInProceedings",
                table: "InvestigationReports",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EvidenceSubmittedDetails",
                table: "InvestigationReports",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EvidenceSubmittedToCourt",
                table: "InvestigationReports",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LegalCounselAppointed",
                table: "InvestigationReports",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MatterReportedToCourt",
                table: "InvestigationReports",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NonComplianceDetails",
                table: "InvestigationReports",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NonComplianceWithCourtOrder",
                table: "InvestigationReports",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PartiesNamedInProceedings",
                table: "InvestigationReports",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PendingAppealsOrMotions",
                table: "InvestigationReports",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpcomingHearingDetails",
                table: "InvestigationReports",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpcomingHearingsOrDeadlines",
                table: "InvestigationReports",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClaimsOrAllegationsBeforeCourt",
                table: "InvestigationReports");

            migrationBuilder.DropColumn(
                name: "CourtCaseReferenceNumber",
                table: "InvestigationReports");

            migrationBuilder.DropColumn(
                name: "CourtDocumentsDescription",
                table: "InvestigationReports");

            migrationBuilder.DropColumn(
                name: "CourtNameAndLocation",
                table: "InvestigationReports");

            migrationBuilder.DropColumn(
                name: "CourtOrderDetails",
                table: "InvestigationReports");

            migrationBuilder.DropColumn(
                name: "CourtOrdersIssued",
                table: "InvestigationReports");

            migrationBuilder.DropColumn(
                name: "CourtRequestedIndependentInvestigation",
                table: "InvestigationReports");

            migrationBuilder.DropColumn(
                name: "DateFiledWithCourt",
                table: "InvestigationReports");

            migrationBuilder.DropColumn(
                name: "DisputedAssetsIdentifiedInProceedings",
                table: "InvestigationReports");

            migrationBuilder.DropColumn(
                name: "EvidenceSubmittedDetails",
                table: "InvestigationReports");

            migrationBuilder.DropColumn(
                name: "EvidenceSubmittedToCourt",
                table: "InvestigationReports");

            migrationBuilder.DropColumn(
                name: "LegalCounselAppointed",
                table: "InvestigationReports");

            migrationBuilder.DropColumn(
                name: "MatterReportedToCourt",
                table: "InvestigationReports");

            migrationBuilder.DropColumn(
                name: "NonComplianceDetails",
                table: "InvestigationReports");

            migrationBuilder.DropColumn(
                name: "NonComplianceWithCourtOrder",
                table: "InvestigationReports");

            migrationBuilder.DropColumn(
                name: "PartiesNamedInProceedings",
                table: "InvestigationReports");

            migrationBuilder.DropColumn(
                name: "PendingAppealsOrMotions",
                table: "InvestigationReports");

            migrationBuilder.DropColumn(
                name: "UpcomingHearingDetails",
                table: "InvestigationReports");

            migrationBuilder.DropColumn(
                name: "UpcomingHearingsOrDeadlines",
                table: "InvestigationReports");
        }
    }
}
