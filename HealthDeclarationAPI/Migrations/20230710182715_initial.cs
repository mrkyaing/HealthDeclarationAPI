using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthDeclarationAPI.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HealthDeclaration",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DeviceId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeviceType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeviceName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GroupId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthDeclaration", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HealthDeclarationItem",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FullPowerMode = table.Column<bool>(type: "bit", nullable: false),
                    ActivePowerControl = table.Column<bool>(type: "bit", nullable: false),
                    FirmwareVersion = table.Column<int>(type: "int", nullable: false),
                    Temperature = table.Column<int>(type: "int", nullable: false),
                    Humidity = table.Column<int>(type: "int", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false),
                    MessageType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Occupancy = table.Column<bool>(type: "bit", nullable: false),
                    StateChanged = table.Column<int>(type: "int", nullable: false),
                    HeaderId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthDeclarationItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HealthDeclarationItem_HealthDeclaration_HeaderId",
                        column: x => x.HeaderId,
                        principalTable: "HealthDeclaration",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HealthDeclarationItem_HeaderId",
                table: "HealthDeclarationItem",
                column: "HeaderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HealthDeclarationItem");

            migrationBuilder.DropTable(
                name: "HealthDeclaration");
        }
    }
}
