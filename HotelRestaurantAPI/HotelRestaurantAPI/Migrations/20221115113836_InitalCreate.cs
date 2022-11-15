using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelRestaurantAPI.Migrations
{
    public partial class InitalCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DailyBreakfasts",
                columns: table => new
                {
                    Day = table.Column<int>(type: "int", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyBreakfasts", x => new { x.Day, x.Month });
                });

            migrationBuilder.CreateTable(
                name: "CheckedIn",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomNumber = table.Column<int>(type: "int", nullable: false),
                    Adults = table.Column<int>(type: "int", nullable: false),
                    Children = table.Column<int>(type: "int", nullable: false),
                    DailyBreakfastDay = table.Column<int>(type: "int", nullable: false),
                    DailyBreakfastMonth = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckedIn", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CheckedIn_DailyBreakfasts_DailyBreakfastDay_DailyBreakfastMonth",
                        columns: x => new { x.DailyBreakfastDay, x.DailyBreakfastMonth },
                        principalTable: "DailyBreakfasts",
                        principalColumns: new[] { "Day", "Month" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Expected",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adults = table.Column<int>(type: "int", nullable: false),
                    Children = table.Column<int>(type: "int", nullable: false),
                    Day = table.Column<int>(type: "int", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expected", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Expected_DailyBreakfasts_Day_Month",
                        columns: x => new { x.Day, x.Month },
                        principalTable: "DailyBreakfasts",
                        principalColumns: new[] { "Day", "Month" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CheckedIn_DailyBreakfastDay_DailyBreakfastMonth",
                table: "CheckedIn",
                columns: new[] { "DailyBreakfastDay", "DailyBreakfastMonth" });

            migrationBuilder.CreateIndex(
                name: "IX_Expected_Day_Month",
                table: "Expected",
                columns: new[] { "Day", "Month" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CheckedIn");

            migrationBuilder.DropTable(
                name: "Expected");

            migrationBuilder.DropTable(
                name: "DailyBreakfasts");
        }
    }
}
