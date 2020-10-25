using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TheGoodStuff.Church.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateEntered",
                table: "QueuedSitesToRetreive",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 10, 25, 6, 56, 13, 891, DateTimeKind.Utc).AddTicks(2686),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 10, 25, 6, 55, 38, 561, DateTimeKind.Utc).AddTicks(5848));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateEntered",
                table: "QueuedSitesToRetreive",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 10, 25, 6, 55, 38, 561, DateTimeKind.Utc).AddTicks(5848),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 10, 25, 6, 56, 13, 891, DateTimeKind.Utc).AddTicks(2686));
        }
    }
}
