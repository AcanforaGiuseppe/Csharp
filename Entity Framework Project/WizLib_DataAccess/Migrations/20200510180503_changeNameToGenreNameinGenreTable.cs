using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class changeNameToGenreNameinGenreTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // When updating a database changing a table's name, we have to be careful, cause if we have records on it we'll go in loss of data problems:
            // so, the solution is to add a column, setting the new column to the old one, and then we can delete the column with the wrong name
            migrationBuilder.AddColumn<string>(
                name: "GenreName",
                table: "Genres",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.Sql("UPDATE dbo.genres SET GenreName=Name");

            migrationBuilder.DropColumn(
               name: "Name",
               table: "Genres");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Genres",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.Sql("UPDATE dbo.genres SET Name=GenreName");

            migrationBuilder.DropColumn(
                name: "GenreName",
                table: "Genres");

        }
    }
}