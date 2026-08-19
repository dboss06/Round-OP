using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Round_OP.Migrations
{
    /// <inheritdoc />
    public partial class AddReportUserOwnership : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfReport",
                table: "InvestigationReports",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "InvestigationReports",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvestigationReports_UserId",
                table: "InvestigationReports",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvestigationReports_AspNetUsers_UserId",
                table: "InvestigationReports",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvestigationReports_AspNetUsers_UserId",
                table: "InvestigationReports");

            migrationBuilder.DropIndex(
                name: "IX_InvestigationReports_UserId",
                table: "InvestigationReports");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "InvestigationReports");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfReport",
                table: "InvestigationReports",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);
        }
    }
}
