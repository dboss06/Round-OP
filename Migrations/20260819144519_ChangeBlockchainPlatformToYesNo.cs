using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Round_OP.Migrations
{
    /// <inheritdoc />
    public partial class ChangeBlockchainPlatformToYesNo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "BlockchainPlatform",
                table: "InvestigationReports",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "BlockchainPlatform",
                table: "InvestigationReports",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }
    }
}
