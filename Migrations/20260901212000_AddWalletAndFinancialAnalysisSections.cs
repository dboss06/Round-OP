using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Round_OP.Migrations
{
    /// <inheritdoc />
    public partial class AddWalletAndFinancialAnalysisSections : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AddressesLinkedToExchanges",
                table: "InvestigationReports",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CommonActivityPatterns",
                table: "InvestigationReports",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExpectedTransactionFrequency",
                table: "InvestigationReports",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FundsConsolidatedIntoAnotherWallet",
                table: "InvestigationReports",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FundsCorrespondWithComplainantAccount",
                table: "InvestigationReports",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FundsDividedOrMultipleAddresses",
                table: "InvestigationReports",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HasExistingWallet",
                table: "InvestigationReports",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IncomingOutgoingAnalyzedSeparately",
                table: "InvestigationReports",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InitialTransactionAmount",
                table: "InvestigationReports",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MostSignificantUnresolvedLead",
                table: "InvestigationReports",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MultipleAddressesInteracting",
                table: "InvestigationReports",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NewOrUnknownAddressesEncountered",
                table: "InvestigationReports",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PreferredWalletType",
                table: "InvestigationReports",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RepeatedTransactionAddresses",
                table: "InvestigationReports",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubsequentFundMovements",
                table: "InvestigationReports",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TransactionFeesOrConversions",
                table: "InvestigationReports",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnaccountedFinancialTrail",
                table: "InvestigationReports",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnderstandsRecoveryPhraseSecurity",
                table: "InvestigationReports",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnusualTransactionPatterns",
                table: "InvestigationReports",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WalletAddressIdentificationMethod",
                table: "InvestigationReports",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WalletNetwork",
                table: "InvestigationReports",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WalletPurpose",
                table: "InvestigationReports",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WalletRecoveryBackedUp",
                table: "InvestigationReports",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WalletRegistrationType",
                table: "InvestigationReports",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WalletRelationshipsRequiringTracing",
                table: "InvestigationReports",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddressesLinkedToExchanges",
                table: "InvestigationReports");

            migrationBuilder.DropColumn(
                name: "CommonActivityPatterns",
                table: "InvestigationReports");

            migrationBuilder.DropColumn(
                name: "ExpectedTransactionFrequency",
                table: "InvestigationReports");

            migrationBuilder.DropColumn(
                name: "FundsConsolidatedIntoAnotherWallet",
                table: "InvestigationReports");

            migrationBuilder.DropColumn(
                name: "FundsCorrespondWithComplainantAccount",
                table: "InvestigationReports");

            migrationBuilder.DropColumn(
                name: "FundsDividedOrMultipleAddresses",
                table: "InvestigationReports");

            migrationBuilder.DropColumn(
                name: "HasExistingWallet",
                table: "InvestigationReports");

            migrationBuilder.DropColumn(
                name: "IncomingOutgoingAnalyzedSeparately",
                table: "InvestigationReports");

            migrationBuilder.DropColumn(
                name: "InitialTransactionAmount",
                table: "InvestigationReports");

            migrationBuilder.DropColumn(
                name: "MostSignificantUnresolvedLead",
                table: "InvestigationReports");

            migrationBuilder.DropColumn(
                name: "MultipleAddressesInteracting",
                table: "InvestigationReports");

            migrationBuilder.DropColumn(
                name: "NewOrUnknownAddressesEncountered",
                table: "InvestigationReports");

            migrationBuilder.DropColumn(
                name: "PreferredWalletType",
                table: "InvestigationReports");

            migrationBuilder.DropColumn(
                name: "RepeatedTransactionAddresses",
                table: "InvestigationReports");

            migrationBuilder.DropColumn(
                name: "SubsequentFundMovements",
                table: "InvestigationReports");

            migrationBuilder.DropColumn(
                name: "TransactionFeesOrConversions",
                table: "InvestigationReports");

            migrationBuilder.DropColumn(
                name: "UnaccountedFinancialTrail",
                table: "InvestigationReports");

            migrationBuilder.DropColumn(
                name: "UnderstandsRecoveryPhraseSecurity",
                table: "InvestigationReports");

            migrationBuilder.DropColumn(
                name: "UnusualTransactionPatterns",
                table: "InvestigationReports");

            migrationBuilder.DropColumn(
                name: "WalletAddressIdentificationMethod",
                table: "InvestigationReports");

            migrationBuilder.DropColumn(
                name: "WalletNetwork",
                table: "InvestigationReports");

            migrationBuilder.DropColumn(
                name: "WalletPurpose",
                table: "InvestigationReports");

            migrationBuilder.DropColumn(
                name: "WalletRecoveryBackedUp",
                table: "InvestigationReports");

            migrationBuilder.DropColumn(
                name: "WalletRegistrationType",
                table: "InvestigationReports");

            migrationBuilder.DropColumn(
                name: "WalletRelationshipsRequiringTracing",
                table: "InvestigationReports");
        }
    }
}
