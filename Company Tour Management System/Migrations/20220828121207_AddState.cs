using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Company_Tour_Management_System.Migrations
{
    public partial class AddState : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "Participants",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "Participants");
        }
    }
}
