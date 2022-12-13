using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeoPET.Migrations
{
    /// <inheritdoc />
    public partial class inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PETS_CARER",
                columns: table => new
                {
                    PetCarerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ZipCode = table.Column<int>(type: "int", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PETS_CARER", x => x.PetCarerId);
                });

            migrationBuilder.CreateTable(
                name: "PETS",
                columns: table => new
                {
                    PetId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Breed = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HashLocalization = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PetCarerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PETS", x => x.PetId);
                    table.ForeignKey(
                        name: "FK_PETS_PETS_CARER_PetCarerId",
                        column: x => x.PetCarerId,
                        principalTable: "PETS_CARER",
                        principalColumn: "PetCarerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PETS_PetCarerId",
                table: "PETS",
                column: "PetCarerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PETS");

            migrationBuilder.DropTable(
                name: "PETS_CARER");
        }
    }
}
