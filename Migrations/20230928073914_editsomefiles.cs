using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assignment2.Migrations
{
    /// <inheritdoc />
    public partial class editsomefiles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buses_Drivers_DriverId",
                table: "Buses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Drivers",
                table: "Drivers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Buses",
                table: "Buses");

            migrationBuilder.RenameTable(
                name: "Drivers",
                newName: "drivers");

            migrationBuilder.RenameTable(
                name: "Buses",
                newName: "buses");

            migrationBuilder.RenameColumn(
                name: "BusNumber",
                table: "buses",
                newName: "Busnumber");

            migrationBuilder.RenameIndex(
                name: "IX_Buses_DriverId",
                table: "buses",
                newName: "IX_buses_DriverId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_drivers",
                table: "drivers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_buses",
                table: "buses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_buses_drivers_DriverId",
                table: "buses",
                column: "DriverId",
                principalTable: "drivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_buses_drivers_DriverId",
                table: "buses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_drivers",
                table: "drivers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_buses",
                table: "buses");

            migrationBuilder.RenameTable(
                name: "drivers",
                newName: "Drivers");

            migrationBuilder.RenameTable(
                name: "buses",
                newName: "Buses");

            migrationBuilder.RenameColumn(
                name: "Busnumber",
                table: "Buses",
                newName: "BusNumber");

            migrationBuilder.RenameIndex(
                name: "IX_buses_DriverId",
                table: "Buses",
                newName: "IX_Buses_DriverId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Drivers",
                table: "Drivers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Buses",
                table: "Buses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Buses_Drivers_DriverId",
                table: "Buses",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
