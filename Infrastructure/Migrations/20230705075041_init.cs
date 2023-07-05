using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    Login = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    PeopleCount = table.Column<int>(type: "INTEGER", nullable: false),
                    HasHvsMeter = table.Column<bool>(type: "INTEGER", nullable: false),
                    HasGvsMeter = table.Column<bool>(type: "INTEGER", nullable: false),
                    HasEnergyMeter = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Energy",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    DayValue = table.Column<double>(type: "REAL", nullable: false),
                    NightValue = table.Column<double>(type: "REAL", nullable: false),
                    TotalResult = table.Column<double>(type: "REAL", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Energy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Energy_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GVS",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    CurrentValue = table.Column<double>(type: "REAL", nullable: false),
                    TotalResultTN = table.Column<double>(type: "REAL", nullable: false),
                    TotalResultTE = table.Column<double>(type: "REAL", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GVS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GVS_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HVS",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    CurrentValue = table.Column<double>(type: "REAL", nullable: false),
                    TotalResult = table.Column<double>(type: "REAL", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HVS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HVS_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Energy_UserId",
                table: "Energy",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_GVS_UserId",
                table: "GVS",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_HVS_UserId",
                table: "HVS",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Energy");

            migrationBuilder.DropTable(
                name: "GVS");

            migrationBuilder.DropTable(
                name: "HVS");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
