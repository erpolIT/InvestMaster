using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class AddAccountBalanceTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountBalance_Portfolios_PortfolioId",
                schema: "identity",
                table: "AccountBalance");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccountBalance",
                schema: "identity",
                table: "AccountBalance");

            migrationBuilder.RenameTable(
                name: "AccountBalance",
                schema: "identity",
                newName: "AccountBalances",
                newSchema: "identity");

            migrationBuilder.RenameIndex(
                name: "IX_AccountBalance_PortfolioId",
                schema: "identity",
                table: "AccountBalances",
                newName: "IX_AccountBalances_PortfolioId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccountBalances",
                schema: "identity",
                table: "AccountBalances",
                column: "Id");

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "Assets",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastUpdated",
                value: new DateTime(2025, 2, 12, 14, 35, 19, 608, DateTimeKind.Utc).AddTicks(1935));

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "Assets",
                keyColumn: "Id",
                keyValue: 2,
                column: "LastUpdated",
                value: new DateTime(2025, 2, 12, 14, 35, 19, 608, DateTimeKind.Utc).AddTicks(1940));

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "Assets",
                keyColumn: "Id",
                keyValue: 3,
                column: "LastUpdated",
                value: new DateTime(2025, 2, 12, 14, 35, 19, 608, DateTimeKind.Utc).AddTicks(1941));

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "Assets",
                keyColumn: "Id",
                keyValue: 4,
                column: "LastUpdated",
                value: new DateTime(2025, 2, 12, 14, 35, 19, 608, DateTimeKind.Utc).AddTicks(1943));

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "Assets",
                keyColumn: "Id",
                keyValue: 5,
                column: "LastUpdated",
                value: new DateTime(2025, 2, 12, 14, 35, 19, 608, DateTimeKind.Utc).AddTicks(1944));

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "Assets",
                keyColumn: "Id",
                keyValue: 6,
                column: "LastUpdated",
                value: new DateTime(2025, 2, 12, 14, 35, 19, 608, DateTimeKind.Utc).AddTicks(1945));

            migrationBuilder.AddForeignKey(
                name: "FK_AccountBalances_Portfolios_PortfolioId",
                schema: "identity",
                table: "AccountBalances",
                column: "PortfolioId",
                principalSchema: "identity",
                principalTable: "Portfolios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountBalances_Portfolios_PortfolioId",
                schema: "identity",
                table: "AccountBalances");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccountBalances",
                schema: "identity",
                table: "AccountBalances");

            migrationBuilder.RenameTable(
                name: "AccountBalances",
                schema: "identity",
                newName: "AccountBalance",
                newSchema: "identity");

            migrationBuilder.RenameIndex(
                name: "IX_AccountBalances_PortfolioId",
                schema: "identity",
                table: "AccountBalance",
                newName: "IX_AccountBalance_PortfolioId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccountBalance",
                schema: "identity",
                table: "AccountBalance",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_AccountBalance_Portfolios_PortfolioId",
                schema: "identity",
                table: "AccountBalance",
                column: "PortfolioId",
                principalSchema: "identity",
                principalTable: "Portfolios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
