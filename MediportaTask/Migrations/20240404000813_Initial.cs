using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MediportaTask.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StackOverflowResponses",
                columns: table => new
                {
                    ResponseId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    HasMore = table.Column<bool>(type: "boolean", nullable: false),
                    QuotaMax = table.Column<int>(type: "integer", nullable: false),
                    QuotaRemaining = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StackOverflowResponses", x => x.ResponseId);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    TagId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    HasSynonyms = table.Column<bool>(type: "boolean", nullable: false),
                    IsModeratorOnly = table.Column<bool>(type: "boolean", nullable: false),
                    IsRequired = table.Column<bool>(type: "boolean", nullable: false),
                    Count = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Percentage = table.Column<double>(type: "double precision", nullable: false),
                    StackOverflowResponseResponseId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.TagId);
                    table.ForeignKey(
                        name: "FK_Tags_StackOverflowResponses_StackOverflowResponseResponseId",
                        column: x => x.StackOverflowResponseResponseId,
                        principalTable: "StackOverflowResponses",
                        principalColumn: "ResponseId");
                });

            migrationBuilder.CreateTable(
                name: "Collectives",
                columns: table => new
                {
                    CollectiveId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Tags = table.Column<List<string>>(type: "text[]", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Link = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Slug = table.Column<string>(type: "text", nullable: false),
                    TagId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collectives", x => x.CollectiveId);
                    table.ForeignKey(
                        name: "FK_Collectives_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "TagId");
                });

            migrationBuilder.CreateTable(
                name: "ExternalLinks",
                columns: table => new
                {
                    LinkId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Link = table.Column<string>(type: "text", nullable: false),
                    CollectiveId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExternalLinks", x => x.LinkId);
                    table.ForeignKey(
                        name: "FK_ExternalLinks_Collectives_CollectiveId",
                        column: x => x.CollectiveId,
                        principalTable: "Collectives",
                        principalColumn: "CollectiveId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Collectives_TagId",
                table: "Collectives",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_ExternalLinks_CollectiveId",
                table: "ExternalLinks",
                column: "CollectiveId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_StackOverflowResponseResponseId",
                table: "Tags",
                column: "StackOverflowResponseResponseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExternalLinks");

            migrationBuilder.DropTable(
                name: "Collectives");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "StackOverflowResponses");
        }
    }
}
