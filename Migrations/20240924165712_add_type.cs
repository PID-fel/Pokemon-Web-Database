using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BulbaClone.Migrations
{
    /// <inheritdoc />
    public partial class add_type : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
        migrationBuilder.InsertData(
           table: "PkmnType",
           columns: new[] { "Id", "Name" , "BaseHexColour", "ComplementaryHexColour"},
           values: new object[,]
           {
				{1, "Normal", "9fa19f", "cbcccb"},
				{2, "Fire", "e62829", "ee7777"},
				{3, "Water", "2980ef", "84b6f3"},
				{4, "Electric", "fac000", "ffd75a"},
				{5, "Grass", "42a129", "9be08c"},
				{6, "Ice", "3fd8ff", "b5eeff"},
				{7, "Fighting", "ff8000", "ffc07e"},
				{8, "Poison", "9040cc", "c59ce3"},
				{9, "Ground", "915121", "dd985b"},
				{10, "Flying", "81b9ef", "cae1f6"},
				{11, "Psychic", "f14179", "f6a3be"},
				{12, "Bug", "91a119", "dcea79"},
				{13, "Rock", "afa981", "d9d6c2"},
				{14, "Ghost", "704170", "b582b5"},
				{15, "Dragon", "5061e1", "afb6ef"},
				{16, "Dark", "50413f", "b7a6a1"},
				{17, "Steel", "60a1b8", "b8d5e0"},
				{18, "Fairy", "f170f1", "f8ccf8"}
           }
           );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
