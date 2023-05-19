using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElectronicMedia.Core.Migrations
{
    public partial class updatePostCategoryDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Dob",
                table: "user",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 19, 13, 20, 13, 257, DateTimeKind.Local).AddTicks(4744),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 19, 13, 5, 41, 276, DateTimeKind.Local).AddTicks(1393));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "replyComment",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 5, 19, 13, 20, 13, 258, DateTimeKind.Local).AddTicks(2511),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 5, 19, 13, 5, 41, 276, DateTimeKind.Local).AddTicks(9469));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "replyComment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 19, 13, 20, 13, 258, DateTimeKind.Local).AddTicks(2325),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 19, 13, 5, 41, 276, DateTimeKind.Local).AddTicks(9231));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "postCategory",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "post",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 19, 6, 20, 13, 258, DateTimeKind.Utc).AddTicks(6792),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 19, 6, 5, 41, 277, DateTimeKind.Utc).AddTicks(3530));

            migrationBuilder.AlterColumn<DateTime>(
                name: "PublishedDate",
                table: "post",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 19, 6, 20, 13, 258, DateTimeKind.Utc).AddTicks(6374),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 19, 6, 5, 41, 277, DateTimeKind.Utc).AddTicks(3178));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "post",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 19, 6, 20, 13, 258, DateTimeKind.Utc).AddTicks(6635),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 19, 6, 5, 41, 277, DateTimeKind.Utc).AddTicks(3376));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "comment",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 5, 19, 13, 20, 13, 258, DateTimeKind.Local).AddTicks(484),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 5, 19, 13, 5, 41, 276, DateTimeKind.Local).AddTicks(7177));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "comment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 19, 13, 20, 13, 258, DateTimeKind.Local).AddTicks(237),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 19, 13, 5, 41, 276, DateTimeKind.Local).AddTicks(6968));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "postCategory");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Dob",
                table: "user",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 19, 13, 5, 41, 276, DateTimeKind.Local).AddTicks(1393),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 19, 13, 20, 13, 257, DateTimeKind.Local).AddTicks(4744));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "replyComment",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 5, 19, 13, 5, 41, 276, DateTimeKind.Local).AddTicks(9469),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 5, 19, 13, 20, 13, 258, DateTimeKind.Local).AddTicks(2511));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "replyComment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 19, 13, 5, 41, 276, DateTimeKind.Local).AddTicks(9231),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 19, 13, 20, 13, 258, DateTimeKind.Local).AddTicks(2325));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "post",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 19, 6, 5, 41, 277, DateTimeKind.Utc).AddTicks(3530),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 19, 6, 20, 13, 258, DateTimeKind.Utc).AddTicks(6792));

            migrationBuilder.AlterColumn<DateTime>(
                name: "PublishedDate",
                table: "post",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 19, 6, 5, 41, 277, DateTimeKind.Utc).AddTicks(3178),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 19, 6, 20, 13, 258, DateTimeKind.Utc).AddTicks(6374));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "post",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 19, 6, 5, 41, 277, DateTimeKind.Utc).AddTicks(3376),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 19, 6, 20, 13, 258, DateTimeKind.Utc).AddTicks(6635));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "comment",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 5, 19, 13, 5, 41, 276, DateTimeKind.Local).AddTicks(7177),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 5, 19, 13, 20, 13, 258, DateTimeKind.Local).AddTicks(484));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "comment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 19, 13, 5, 41, 276, DateTimeKind.Local).AddTicks(6968),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 19, 13, 20, 13, 258, DateTimeKind.Local).AddTicks(237));
        }
    }
}
