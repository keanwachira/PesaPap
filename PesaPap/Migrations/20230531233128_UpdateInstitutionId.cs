using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PesaPap.Migrations
{
    /// <inheritdoc />
    public partial class UpdateInstitutionId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IPNSettings_Institutions_InstitutionInsititutionId",
                table: "IPNSettings");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentNotifications_Institutions_InstitutionInsititutionId",
                table: "PaymentNotifications");

            migrationBuilder.RenameColumn(
                name: "InstitutionInsititutionId",
                table: "PaymentNotifications",
                newName: "InstitutionId");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentNotifications_InstitutionInsititutionId",
                table: "PaymentNotifications",
                newName: "IX_PaymentNotifications_InstitutionId");

            migrationBuilder.RenameColumn(
                name: "InstitutionInsititutionId",
                table: "IPNSettings",
                newName: "InstitutionId");

            migrationBuilder.RenameIndex(
                name: "IX_IPNSettings_InstitutionInsititutionId",
                table: "IPNSettings",
                newName: "IX_IPNSettings_InstitutionId");

            migrationBuilder.RenameColumn(
                name: "InsititutionId",
                table: "Institutions",
                newName: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentNotifications_TransactionRef",
                table: "PaymentNotifications",
                column: "TransactionRef",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_IPNSettings_Institutions_InstitutionId",
                table: "IPNSettings",
                column: "InstitutionId",
                principalTable: "Institutions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentNotifications_Institutions_InstitutionId",
                table: "PaymentNotifications",
                column: "InstitutionId",
                principalTable: "Institutions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IPNSettings_Institutions_InstitutionId",
                table: "IPNSettings");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentNotifications_Institutions_InstitutionId",
                table: "PaymentNotifications");

            migrationBuilder.DropIndex(
                name: "IX_PaymentNotifications_TransactionRef",
                table: "PaymentNotifications");

            migrationBuilder.RenameColumn(
                name: "InstitutionId",
                table: "PaymentNotifications",
                newName: "InstitutionInsititutionId");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentNotifications_InstitutionId",
                table: "PaymentNotifications",
                newName: "IX_PaymentNotifications_InstitutionInsititutionId");

            migrationBuilder.RenameColumn(
                name: "InstitutionId",
                table: "IPNSettings",
                newName: "InstitutionInsititutionId");

            migrationBuilder.RenameIndex(
                name: "IX_IPNSettings_InstitutionId",
                table: "IPNSettings",
                newName: "IX_IPNSettings_InstitutionInsititutionId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Institutions",
                newName: "InsititutionId");

            migrationBuilder.AddForeignKey(
                name: "FK_IPNSettings_Institutions_InstitutionInsititutionId",
                table: "IPNSettings",
                column: "InstitutionInsititutionId",
                principalTable: "Institutions",
                principalColumn: "InsititutionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentNotifications_Institutions_InstitutionInsititutionId",
                table: "PaymentNotifications",
                column: "InstitutionInsititutionId",
                principalTable: "Institutions",
                principalColumn: "InsititutionId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
