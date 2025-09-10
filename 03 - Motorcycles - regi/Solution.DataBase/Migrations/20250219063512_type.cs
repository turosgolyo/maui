﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Solution.Database.Migrations
{
    /// <inheritdoc />
    public partial class type : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "TypeId",
                table: "Motorcycle",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "Type",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Type", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Motorcycle_TypeId",
                table: "Motorcycle",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Type_Name",
                table: "Type",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Motorcycle_Type_TypeId",
                table: "Motorcycle",
                column: "TypeId",
                principalTable: "Type",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Motorcycle_Type_TypeId",
                table: "Motorcycle");

            migrationBuilder.DropTable(
                name: "Type");

            migrationBuilder.DropIndex(
                name: "IX_Motorcycle_TypeId",
                table: "Motorcycle");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "Motorcycle");
        }
    }
}
