using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KingMeetup.Repository.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAtendeeListTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsOnSite",
                table: "AttendeeLists",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOnSite",
                table: "AttendeeLists");
        }
    }
}
