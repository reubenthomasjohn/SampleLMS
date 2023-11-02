using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SampleLMS.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatingModels2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "PublishedDate",
                table: "Courses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 1,
                column: "PublishedDate",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 2,
                columns: new[] { "Content", "Heading", "PublishedDate", "Title" },
                values: new object[] { "aaaaaaaaaaaabbbbbbbccccccccc", "Kubernetes", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kubernetes101" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "PublishedDate",
                table: "Courses",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 1,
                column: "PublishedDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 2,
                columns: new[] { "Content", "Heading", "PublishedDate", "Title" },
                values: new object[] { "", "", null, "Kubernetes" });
        }
    }
}
