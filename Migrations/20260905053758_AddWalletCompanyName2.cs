using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Round_OP.Migrations
{
    /// <inheritdoc />
    public partial class AddWalletCompanyName2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DO $$
                BEGIN
                    IF NOT EXISTS (
                        SELECT 1 FROM information_schema.columns
                        WHERE table_name = 'InvestigationReports'
                          AND column_name = 'WalletCompanyName'
                    ) THEN
                        ALTER TABLE ""InvestigationReports""
                        ADD COLUMN ""WalletCompanyName"" integer NULL;

                    ELSIF (
                        SELECT data_type FROM information_schema.columns
                        WHERE table_name = 'InvestigationReports'
                          AND column_name = 'WalletCompanyName'
                    ) = 'text' THEN
                        ALTER TABLE ""InvestigationReports""
                        ALTER COLUMN ""WalletCompanyName"" TYPE integer
                        USING NULLIF(""WalletCompanyName"", '')::integer;
                    END IF;
                END $$;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DO $$
                BEGIN
                    IF EXISTS (
                        SELECT 1 FROM information_schema.columns
                        WHERE table_name = 'InvestigationReports'
                          AND column_name = 'WalletCompanyName'
                          AND data_type = 'integer'
                    ) THEN
                        ALTER TABLE ""InvestigationReports""
                        ALTER COLUMN ""WalletCompanyName"" TYPE text
                        USING ""WalletCompanyName""::text;
                    END IF;
                END $$;
            ");
        }
    }
}