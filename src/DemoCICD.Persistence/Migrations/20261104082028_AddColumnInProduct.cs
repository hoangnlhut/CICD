using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoCICD.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnInProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop the primary key constraint
            migrationBuilder.Sql("ALTER TABLE [Product] DROP CONSTRAINT [PK_Product]");

            // Alter the column type (example: from int to bigint or Guid to int)
            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Product",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            // Recreate the primary key constraint
            migrationBuilder.Sql("ALTER TABLE [Product] ADD CONSTRAINT [PK_Product] PRIMARY KEY ([Id])");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Product",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Product");

            // Drop the primary key constraint
            migrationBuilder.Sql("ALTER TABLE [Product] DROP CONSTRAINT [PK_Product]");

            // Revert the column type
            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Product",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            // Recreate the primary key constraint
            migrationBuilder.Sql("ALTER TABLE [Product] ADD CONSTRAINT [PK_Product] PRIMARY KEY ([Id])");
        }
    }
}
