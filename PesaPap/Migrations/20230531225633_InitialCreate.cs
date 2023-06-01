using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PesaPap.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Institutions",
                columns: table => new
                {
                    InsititutionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstitutionName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Institutions", x => x.InsititutionId);
                });

            migrationBuilder.CreateTable(
                name: "PaymentChannels",
                columns: table => new
                {
                    ChannelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChannelName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentChannels", x => x.ChannelId);
                });

            migrationBuilder.CreateTable(
                name: "IPNSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NotificationUrl = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ValidationUrl = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    NotificationChannel = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    InstitutionInsititutionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IPNSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IPNSettings_Institutions_InstitutionInsititutionId",
                        column: x => x.InstitutionInsititutionId,
                        principalTable: "Institutions",
                        principalColumn: "InsititutionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaymentNotifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionRef = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InstitutionInsititutionId = table.Column<int>(type: "int", nullable: false),
                    ChannelId = table.Column<int>(type: "int", nullable: false),
                    StudentName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StudentNo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PaymentNarration = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PayerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IPNSent = table.Column<bool>(type: "bit", nullable: false),
                    DateTimeSent = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NoOfTries = table.Column<int>(type: "int", nullable: false),
                    LastIPNRetryDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentNotifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentNotifications_Institutions_InstitutionInsititutionId",
                        column: x => x.InstitutionInsititutionId,
                        principalTable: "Institutions",
                        principalColumn: "InsititutionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PaymentNotifications_PaymentChannels_ChannelId",
                        column: x => x.ChannelId,
                        principalTable: "PaymentChannels",
                        principalColumn: "ChannelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IPNSettings_InstitutionInsititutionId",
                table: "IPNSettings",
                column: "InstitutionInsititutionId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentNotifications_ChannelId",
                table: "PaymentNotifications",
                column: "ChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentNotifications_InstitutionInsititutionId",
                table: "PaymentNotifications",
                column: "InstitutionInsititutionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IPNSettings");

            migrationBuilder.DropTable(
                name: "PaymentNotifications");

            migrationBuilder.DropTable(
                name: "Institutions");

            migrationBuilder.DropTable(
                name: "PaymentChannels");
        }
    }
}
