using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CIoTD.Migrations
{
    /// <inheritdoc />
    public partial class alterCommandIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    Identifier = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Manufacturer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.Identifier);
                });

            migrationBuilder.CreateTable(
                name: "CommandDescriptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Operation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Result = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Format = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeviceId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DeviceIdentifier = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommandDescriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommandDescriptions_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Identifier",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommandDescriptions_Devices_DeviceIdentifier",
                        column: x => x.DeviceIdentifier,
                        principalTable: "Devices",
                        principalColumn: "Identifier");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommandDescriptions_DeviceId",
                table: "CommandDescriptions",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_CommandDescriptions_DeviceIdentifier",
                table: "CommandDescriptions",
                column: "DeviceIdentifier");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommandDescriptions");

            migrationBuilder.DropTable(
                name: "Devices");
        }
    }
}
