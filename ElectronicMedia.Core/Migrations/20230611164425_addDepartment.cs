using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElectronicMedia.Core.Migrations
{
    public partial class addDepartment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Dob",
                table: "user",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 11, 23, 44, 25, 199, DateTimeKind.Local).AddTicks(6475),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 6, 11, 22, 3, 15, 26, DateTimeKind.Local).AddTicks(4113));

            migrationBuilder.AddColumn<Guid>(
                name: "DepartmentId",
                table: "user",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "replyComment",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 11, 23, 44, 25, 200, DateTimeKind.Local).AddTicks(6092),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 11, 22, 3, 15, 27, DateTimeKind.Local).AddTicks(4283));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "replyComment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 11, 23, 44, 25, 200, DateTimeKind.Local).AddTicks(5857),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 6, 11, 22, 3, 15, 27, DateTimeKind.Local).AddTicks(4048));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "post",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 11, 16, 44, 25, 201, DateTimeKind.Utc).AddTicks(869),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 6, 11, 15, 3, 15, 27, DateTimeKind.Utc).AddTicks(8307));

            migrationBuilder.AlterColumn<DateTime>(
                name: "PublishedDate",
                table: "post",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 11, 16, 44, 25, 201, DateTimeKind.Utc).AddTicks(219),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 6, 11, 15, 3, 15, 27, DateTimeKind.Utc).AddTicks(7914));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "post",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 11, 16, 44, 25, 201, DateTimeKind.Utc).AddTicks(566),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 6, 11, 15, 3, 15, 27, DateTimeKind.Utc).AddTicks(8153));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "comment",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 11, 23, 44, 25, 200, DateTimeKind.Local).AddTicks(3919),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 11, 22, 3, 15, 27, DateTimeKind.Local).AddTicks(1759));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "comment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 11, 23, 44, 25, 200, DateTimeKind.Local).AddTicks(3674),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 6, 11, 22, 3, 15, 27, DateTimeKind.Local).AddTicks(1455));

            migrationBuilder.CreateTable(
                name: "department",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_department", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_user_DepartmentId",
                table: "user",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_user_department_DepartmentId",
                table: "user",
                column: "DepartmentId",
                principalTable: "department",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_department_DepartmentId",
                table: "user");

            migrationBuilder.DropTable(
                name: "department");

            migrationBuilder.DropIndex(
                name: "IX_user_DepartmentId",
                table: "user");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "user");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Dob",
                table: "user",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 11, 22, 3, 15, 26, DateTimeKind.Local).AddTicks(4113),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 6, 11, 23, 44, 25, 199, DateTimeKind.Local).AddTicks(6475));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "replyComment",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 11, 22, 3, 15, 27, DateTimeKind.Local).AddTicks(4283),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 11, 23, 44, 25, 200, DateTimeKind.Local).AddTicks(6092));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "replyComment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 11, 22, 3, 15, 27, DateTimeKind.Local).AddTicks(4048),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 6, 11, 23, 44, 25, 200, DateTimeKind.Local).AddTicks(5857));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "post",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 11, 15, 3, 15, 27, DateTimeKind.Utc).AddTicks(8307),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 6, 11, 16, 44, 25, 201, DateTimeKind.Utc).AddTicks(869));

            migrationBuilder.AlterColumn<DateTime>(
                name: "PublishedDate",
                table: "post",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 11, 15, 3, 15, 27, DateTimeKind.Utc).AddTicks(7914),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 6, 11, 16, 44, 25, 201, DateTimeKind.Utc).AddTicks(219));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "post",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 11, 15, 3, 15, 27, DateTimeKind.Utc).AddTicks(8153),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 6, 11, 16, 44, 25, 201, DateTimeKind.Utc).AddTicks(566));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "comment",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 11, 22, 3, 15, 27, DateTimeKind.Local).AddTicks(1759),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 11, 23, 44, 25, 200, DateTimeKind.Local).AddTicks(3919));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "comment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 11, 22, 3, 15, 27, DateTimeKind.Local).AddTicks(1455),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 6, 11, 23, 44, 25, 200, DateTimeKind.Local).AddTicks(3674));
        }
    }
}
