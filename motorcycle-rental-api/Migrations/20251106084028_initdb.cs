using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace motorcycle_rental_api.Migrations
{
    /// <inheritdoc />
    public partial class initdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "mr_client",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Name = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false),
                    CPF = table.Column<string>(type: "NVARCHAR2(11)", maxLength: 11, nullable: false),
                    Street = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false),
                    HouseNumber = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Address2 = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false),
                    District = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false),
                    City = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false),
                    State = table.Column<string>(type: "NVARCHAR2(2)", maxLength: 2, nullable: false),
                    CEP = table.Column<string>(type: "NVARCHAR2(8)", maxLength: 8, nullable: false),
                    Fone = table.Column<string>(type: "NVARCHAR2(11)", maxLength: 11, nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mr_client", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "mr_motorcycle",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Brand = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Model = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Plate = table.Column<string>(type: "NVARCHAR2(7)", maxLength: 7, nullable: false),
                    ManufacturingYear = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    DailyValue = table.Column<decimal>(type: "DECIMAL(18,2)", precision: 18, scale: 2, nullable: false),
                    Availability = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mr_motorcycle", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "mr_rental",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ClientId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    MotorcycleId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    EndDate = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    TotalValue = table.Column<decimal>(type: "DECIMAL(18,2)", precision: 18, scale: 2, nullable: false),
                    Completed = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mr_rental", x => x.Id);
                    table.ForeignKey(
                        name: "FK_mr_rental_mr_client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "mr_client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_mr_rental_mr_motorcycle_MotorcycleId",
                        column: x => x.MotorcycleId,
                        principalTable: "mr_motorcycle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_mr_rental_ClientId",
                table: "mr_rental",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_mr_rental_MotorcycleId",
                table: "mr_rental",
                column: "MotorcycleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "mr_rental");

            migrationBuilder.DropTable(
                name: "mr_client");

            migrationBuilder.DropTable(
                name: "mr_motorcycle");
        }
    }
}
