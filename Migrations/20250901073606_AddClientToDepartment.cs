using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddClientToDepartment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "Departments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Departments_ClientId",
                table: "Departments",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Clients_ClientId",
                table: "Departments",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Clients_ClientId",
                table: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_Departments_ClientId",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Departments");
        }
    }
}
