using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TheGoodStuff.Church.Data.Migrations
{
    public partial class AddedPostsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "QueuedSitesToRetreive",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateEntered",
                table: "QueuedSitesToRetreive",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 10, 26, 5, 42, 10, 611, DateTimeKind.Utc).AddTicks(5763),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 10, 25, 6, 56, 13, 891, DateTimeKind.Utc).AddTicks(2686));

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubjectLine = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateEntered = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2020, 10, 26, 5, 42, 10, 616, DateTimeKind.Utc).AddTicks(407)),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Posts_OwnerId",
                table: "Posts",
                column: "OwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "QueuedSitesToRetreive",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateEntered",
                table: "QueuedSitesToRetreive",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 10, 25, 6, 56, 13, 891, DateTimeKind.Utc).AddTicks(2686),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 10, 26, 5, 42, 10, 611, DateTimeKind.Utc).AddTicks(5763));
        }
    }
}
