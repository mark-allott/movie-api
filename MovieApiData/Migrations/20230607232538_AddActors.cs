using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieApi.Data.Migrations
{
	/// <inheritdoc />
	public partial class AddActors : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "Actors",
				columns: table => new
				{
					Id = table.Column<long>(type: "bigint", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Actors", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "MovieActor",
				columns: table => new
				{
					MovieId = table.Column<long>(type: "bigint", nullable: false),
					ActorId = table.Column<long>(type: "bigint", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_MovieActor", x => new { x.MovieId, x.ActorId });
					table.ForeignKey(
						name: "FK_MovieActor_Actors_ActorId",
						column: x => x.ActorId,
						principalTable: "Actors",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_MovieActor_Movies_MovieId",
						column: x => x.MovieId,
						principalTable: "Movies",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateIndex(
				name: "IX_MovieActor_ActorId",
				table: "MovieActor",
				column: "ActorId");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
					name: "MovieActor");

			migrationBuilder.DropTable(
					name: "Actors");
		}
	}
}