using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContactMS.Data.Migrations
{
    /// <inheritdoc />
    public partial class contactDetails_added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactDetail_Contacts_ContactId",
                table: "ContactDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContactDetail",
                table: "ContactDetail");

            migrationBuilder.RenameTable(
                name: "ContactDetail",
                newName: "ContactDetails");

            migrationBuilder.RenameIndex(
                name: "IX_ContactDetail_ContactId",
                table: "ContactDetails",
                newName: "IX_ContactDetails_ContactId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContactDetails",
                table: "ContactDetails",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ContactDetails_Contacts_ContactId",
                table: "ContactDetails",
                column: "ContactId",
                principalTable: "Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactDetails_Contacts_ContactId",
                table: "ContactDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContactDetails",
                table: "ContactDetails");

            migrationBuilder.RenameTable(
                name: "ContactDetails",
                newName: "ContactDetail");

            migrationBuilder.RenameIndex(
                name: "IX_ContactDetails_ContactId",
                table: "ContactDetail",
                newName: "IX_ContactDetail_ContactId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContactDetail",
                table: "ContactDetail",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ContactDetail_Contacts_ContactId",
                table: "ContactDetail",
                column: "ContactId",
                principalTable: "Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
