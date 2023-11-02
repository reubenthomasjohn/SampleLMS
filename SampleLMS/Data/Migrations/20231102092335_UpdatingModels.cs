using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SampleLMS.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatingModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FeaturedImageUrl",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Heading",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PublishedDate",
                table: "Courses",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UrlHandle",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 1,
                columns: new[] { "Author", "Content", "Description", "FeaturedImageUrl", "Heading", "PublishedDate", "Title", "UrlHandle" },
                values: new object[] { "", "aaaaaaaaaaaabbbbbbbccccccccc", "Docker is a containerization tool, used by all kinds of engineers.", "", "Docker", null, "Docker101", "" });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 2,
                columns: new[] { "Author", "Content", "FeaturedImageUrl", "Heading", "PublishedDate", "UrlHandle" },
                values: new object[] { "", "", "", "", null, "" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Author",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Content",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "FeaturedImageUrl",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Heading",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "PublishedDate",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "UrlHandle",
                table: "Courses");

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 1,
                columns: new[] { "Description", "Title" },
                values: new object[] { "Docker is a containerization tool", "Docker" });
        }
    }
}
