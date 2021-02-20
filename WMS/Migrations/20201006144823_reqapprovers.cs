using Microsoft.EntityFrameworkCore.Migrations;

namespace WMS.Migrations
{
    public partial class reqapprovers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Approvers",
                table: "RequestTypes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Approvers",
                table: "RequestTypes");
        }
    }
}
