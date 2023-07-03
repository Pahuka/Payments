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
                name: "Energy",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CurrentValue = table.Column<double>(type: "REAL", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Energy", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EnergyMeter",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CurrentValueNight = table.Column<double>(type: "REAL", nullable: false),
                    CurrentValueDay = table.Column<double>(type: "REAL", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnergyMeter", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GVS",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CurrentValue = table.Column<double>(type: "REAL", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GVS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HVS",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CurrentValue = table.Column<double>(type: "REAL", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HVS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    Login = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
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
                name: "Statistics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    HVSId = table.Column<Guid>(type: "TEXT", nullable: false),
                    GVSId = table.Column<Guid>(type: "TEXT", nullable: false),
                    EnergyId = table.Column<Guid>(type: "TEXT", nullable: false),
                    EnergyMeterId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statistics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Statistics_EnergyMeter_EnergyMeterId",
                        column: x => x.EnergyMeterId,
                        principalTable: "EnergyMeter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Statistics_Energy_EnergyId",
                        column: x => x.EnergyId,
                        principalTable: "Energy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Statistics_GVS_GVSId",
                        column: x => x.GVSId,
                        principalTable: "GVS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Statistics_HVS_HVSId",
                        column: x => x.HVSId,
                        principalTable: "HVS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Statistics_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Statistics_EnergyId",
                table: "Statistics",
                column: "EnergyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Statistics_EnergyMeterId",
                table: "Statistics",
                column: "EnergyMeterId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Statistics_GVSId",
                table: "Statistics",
                column: "GVSId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Statistics_HVSId",
                table: "Statistics",
                column: "HVSId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Statistics_UserId",
                table: "Statistics",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Statistics");

            migrationBuilder.DropTable(
                name: "EnergyMeter");

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
