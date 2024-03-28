using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IT_Conference_Service.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "application_info",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ActivityName = table.Column<string>(type: "text", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "text", maxLength: 300, nullable: true),
                    Plan = table.Column<string>(type: "text", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_application_info", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "applications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ActivityType = table.Column<int>(type: "int", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsSent = table.Column<bool>(type: "bool", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ApplicationInfoId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_applications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_applications_application_info_ApplicationInfoId",
                        column: x => x.ApplicationInfoId,
                        principalTable: "application_info",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_applications_ApplicationInfoId",
                table: "applications",
                column: "ApplicationInfoId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "applications");

            migrationBuilder.DropTable(
                name: "application_info");
        }
    }
}
