using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MLM.Migrations
{
    /// <inheritdoc />
    public partial class AddSettingsAndLogs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrimaryColor = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SecondaryColor = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TertiaryColor = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DarkThemeEnabled = table.Column<bool>(type: "bit", nullable: false),
                    SmtpServer = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SmtpPort = table.Column<int>(type: "int", nullable: false),
                    SmtpEmail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SmtpPassword = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    SmtpFromName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UseSsl = table.Column<bool>(type: "bit", nullable: false),
                    SmtpTimeout = table.Column<int>(type: "int", nullable: false),
                    OtpLoginRequired = table.Column<bool>(type: "bit", nullable: false),
                    CaptchaEnabled = table.Column<bool>(type: "bit", nullable: false),
                    ReferralEnabled = table.Column<bool>(type: "bit", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    TransactionType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    DefaultPaymentMode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NoOfClientRequiredForIb = table.Column<int>(type: "int", nullable: false),
                    WithdrawFee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RequiredTradeSeconds = table.Column<int>(type: "int", nullable: false),
                    Mt5Server = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    AppLogoPath = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    AppIconPath = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    FaviconPath = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ErrorLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Level = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    StackTrace = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErrorLogs", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppSettings");

            migrationBuilder.DropTable(
                name: "ErrorLogs");
        }
    }
}
