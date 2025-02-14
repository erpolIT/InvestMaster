using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class AddBalanceTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_AspNetUsers_UserId",
                schema: "identity",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Portfolios_PortfolioId",
                schema: "identity",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_PortfolioId",
                schema: "identity",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_UserId",
                schema: "identity",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "PortfolioId",
                schema: "identity",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "identity",
                table: "Transactions");

            migrationBuilder.CreateTable(
                name: "AccountBalance",
                schema: "identity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PortfolioId = table.Column<int>(type: "integer", nullable: false),
                    Balance = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountBalance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountBalance_Portfolios_PortfolioId",
                        column: x => x.PortfolioId,
                        principalSchema: "identity",
                        principalTable: "Portfolios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "Assets",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastUpdated",
                value: new DateTime(2025, 2, 12, 12, 19, 21, 168, DateTimeKind.Utc).AddTicks(3372));

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "Assets",
                keyColumn: "Id",
                keyValue: 2,
                column: "LastUpdated",
                value: new DateTime(2025, 2, 12, 12, 19, 21, 168, DateTimeKind.Utc).AddTicks(3377));

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "Assets",
                keyColumn: "Id",
                keyValue: 3,
                column: "LastUpdated",
                value: new DateTime(2025, 2, 12, 12, 19, 21, 168, DateTimeKind.Utc).AddTicks(3378));

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "Assets",
                keyColumn: "Id",
                keyValue: 4,
                column: "LastUpdated",
                value: new DateTime(2025, 2, 12, 12, 19, 21, 168, DateTimeKind.Utc).AddTicks(3380));

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "Assets",
                keyColumn: "Id",
                keyValue: 5,
                column: "LastUpdated",
                value: new DateTime(2025, 2, 12, 12, 19, 21, 168, DateTimeKind.Utc).AddTicks(3381));

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "Assets",
                keyColumn: "Id",
                keyValue: 6,
                column: "LastUpdated",
                value: new DateTime(2025, 2, 12, 12, 19, 21, 168, DateTimeKind.Utc).AddTicks(3383));

            migrationBuilder.CreateIndex(
                name: "IX_AccountBalance_PortfolioId",
                schema: "identity",
                table: "AccountBalance",
                column: "PortfolioId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountBalance",
                schema: "identity");

            migrationBuilder.AddColumn<int>(
                name: "PortfolioId",
                schema: "identity",
                table: "Transactions",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                schema: "identity",
                table: "Transactions",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "Assets",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastUpdated",
                value: new DateTime(2025, 2, 11, 12, 6, 52, 434, DateTimeKind.Utc).AddTicks(1926));

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "Assets",
                keyColumn: "Id",
                keyValue: 2,
                column: "LastUpdated",
                value: new DateTime(2025, 2, 11, 12, 6, 52, 434, DateTimeKind.Utc).AddTicks(1935));

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "Assets",
                keyColumn: "Id",
                keyValue: 3,
                column: "LastUpdated",
                value: new DateTime(2025, 2, 11, 12, 6, 52, 434, DateTimeKind.Utc).AddTicks(1936));

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "Assets",
                keyColumn: "Id",
                keyValue: 4,
                column: "LastUpdated",
                value: new DateTime(2025, 2, 11, 12, 6, 52, 434, DateTimeKind.Utc).AddTicks(1938));

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "Assets",
                keyColumn: "Id",
                keyValue: 5,
                column: "LastUpdated",
                value: new DateTime(2025, 2, 11, 12, 6, 52, 434, DateTimeKind.Utc).AddTicks(1939));

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "Assets",
                keyColumn: "Id",
                keyValue: 6,
                column: "LastUpdated",
                value: new DateTime(2025, 2, 11, 12, 6, 52, 434, DateTimeKind.Utc).AddTicks(1940));

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_PortfolioId",
                schema: "identity",
                table: "Transactions",
                column: "PortfolioId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_UserId",
                schema: "identity",
                table: "Transactions",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_AspNetUsers_UserId",
                schema: "identity",
                table: "Transactions",
                column: "UserId",
                principalSchema: "identity",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Portfolios_PortfolioId",
                schema: "identity",
                table: "Transactions",
                column: "PortfolioId",
                principalSchema: "identity",
                principalTable: "Portfolios",
                principalColumn: "Id");
        }
    }
}
