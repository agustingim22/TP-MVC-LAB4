using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TPFINALLAB4MVC.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "estados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_estados", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "jugadores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nickName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    edad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_jugadores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "partidos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fecha = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NickNameRival = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdJugador = table.Column<int>(type: "int", nullable: false),
                    jugadorId = table.Column<int>(type: "int", nullable: true),
                    IdEstado = table.Column<int>(type: "int", nullable: false),
                    estadoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_partidos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_partidos_estados_estadoId",
                        column: x => x.estadoId,
                        principalTable: "estados",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_partidos_jugadores_jugadorId",
                        column: x => x.jugadorId,
                        principalTable: "jugadores",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "partidosDetalles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    golesJugador = table.Column<int>(type: "int", nullable: false),
                    golesRival = table.Column<int>(type: "int", nullable: false),
                    cantRojas = table.Column<int>(type: "int", nullable: false),
                    cantAmarillas = table.Column<int>(type: "int", nullable: false),
                    IdPartido = table.Column<int>(type: "int", nullable: false),
                    PartidoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_partidosDetalles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_partidosDetalles_partidos_PartidoId",
                        column: x => x.PartidoId,
                        principalTable: "partidos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_partidos_estadoId",
                table: "partidos",
                column: "estadoId");

            migrationBuilder.CreateIndex(
                name: "IX_partidos_jugadorId",
                table: "partidos",
                column: "jugadorId");

            migrationBuilder.CreateIndex(
                name: "IX_partidosDetalles_PartidoId",
                table: "partidosDetalles",
                column: "PartidoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "partidosDetalles");

            migrationBuilder.DropTable(
                name: "partidos");

            migrationBuilder.DropTable(
                name: "estados");

            migrationBuilder.DropTable(
                name: "jugadores");
        }
    }
}
