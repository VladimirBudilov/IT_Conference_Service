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
                name: "AuthorInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ActivityName = table.Column<string>(type: "text", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "text", maxLength: 300, nullable: true),
                    Plan = table.Column<string>(type: "text", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Applications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ActivityType = table.Column<int>(type: "int", nullable: false),
                    AuthorInfoId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsSent = table.Column<bool>(type: "bool", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Applications_AuthorInfo_AuthorInfoId",
                        column: x => x.AuthorInfoId,
                        principalTable: "AuthorInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Applications_AuthorInfoId",
                table: "Applications",
                column: "AuthorInfoId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Applications");

            migrationBuilder.DropTable(
                name: "AuthorInfo");
        }
    }
}
