using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SQLSanitizorNator.Migrations
{
    /// <inheritdoc />
    public partial class Initial_Models : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NaughtyTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    Description = table.Column<string>(type: "varchar(511)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NaughtyTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NaughtyWords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "varchar(100)", nullable: false),
                    UsageCount = table.Column<int>(type: "int", nullable: false),
                    Severity = table.Column<byte>(type: "tinyint", nullable: false),
                    NaughtyTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NaughtyWords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NaughtyWords_NaughtyTypes_NaughtyTypeId",
                        column: x => x.NaughtyTypeId,
                        principalTable: "NaughtyTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NaughtyWords_NaughtyTypeId",
                table: "NaughtyWords",
                column: "NaughtyTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NaughtyWords");

            migrationBuilder.DropTable(
                name: "NaughtyTypes");
        }
    }
}
