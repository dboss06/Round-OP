using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Round_OP.Migrations
{
    /// <inheritdoc />
    public partial class RemoveSections2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourtDocumentsDescription",
                table: "InvestigationReports");

            migrationBuilder.DropColumn(
                name: "PartiesNamedInProceedings",
                table: "InvestigationReports");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CourtDocumentsDescription",
                table: "InvestigationReports",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PartiesNamedInProceedings",
                table: "InvestigationReports",
                type: "text",
                nullable: true);
        }
    }
}
