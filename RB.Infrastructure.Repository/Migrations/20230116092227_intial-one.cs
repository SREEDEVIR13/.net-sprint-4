using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RB.Infrastructure.Migrations
{
    public partial class intialone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HostedRideId = table.Column<int>(type: "int", nullable: false),
                    JoineeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Requests_HostedRides_HostedRideId",
                        column: x => x.HostedRideId,
                        principalTable: "HostedRides",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Requests_Users_JoineeId",
                        column: x => x.JoineeId,
                        principalTable: "Users",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Requests_HostedRideId",
                table: "Requests",
                column: "HostedRideId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_JoineeId",
                table: "Requests",
                column: "JoineeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Requests");
        }
    }
}
