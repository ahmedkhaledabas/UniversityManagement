using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace B_UniversityManagement.Migrations
{
    /// <inheritdoc />
    public partial class addcourseIdtoquestion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CourseId",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Questions");
        }
    }
}
