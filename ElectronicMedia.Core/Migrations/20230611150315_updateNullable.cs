using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElectronicMedia.Core.Migrations
{
    public partial class updateNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "Image",
                table: "user",
                type: "image",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "image");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Dob",
                table: "user",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 11, 22, 3, 15, 26, DateTimeKind.Local).AddTicks(4113),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 6, 10, 12, 55, 57, 236, DateTimeKind.Local).AddTicks(2204));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "replyComment",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 11, 22, 3, 15, 27, DateTimeKind.Local).AddTicks(4283),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 10, 12, 55, 57, 237, DateTimeKind.Local).AddTicks(852));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "replyComment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 11, 22, 3, 15, 27, DateTimeKind.Local).AddTicks(4048),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 6, 10, 12, 55, 57, 237, DateTimeKind.Local).AddTicks(648));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "post",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 11, 15, 3, 15, 27, DateTimeKind.Utc).AddTicks(8307),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 6, 10, 5, 55, 57, 237, DateTimeKind.Utc).AddTicks(5430));

            migrationBuilder.AlterColumn<DateTime>(
                name: "PublishedDate",
                table: "post",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 11, 15, 3, 15, 27, DateTimeKind.Utc).AddTicks(7914),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 6, 10, 5, 55, 57, 237, DateTimeKind.Utc).AddTicks(4981));

            migrationBuilder.AlterColumn<byte[]>(
                name: "Image",
                table: "post",
                type: "image",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "image");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "post",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 11, 15, 3, 15, 27, DateTimeKind.Utc).AddTicks(8153),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 6, 10, 5, 55, 57, 237, DateTimeKind.Utc).AddTicks(5258));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "comment",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 11, 22, 3, 15, 27, DateTimeKind.Local).AddTicks(1759),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 10, 12, 55, 57, 236, DateTimeKind.Local).AddTicks(8544));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "comment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 11, 22, 3, 15, 27, DateTimeKind.Local).AddTicks(1455),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 6, 10, 12, 55, 57, 236, DateTimeKind.Local).AddTicks(8276));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "Image",
                table: "user",
                type: "image",
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(byte[]),
                oldType: "image",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Dob",
                table: "user",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 10, 12, 55, 57, 236, DateTimeKind.Local).AddTicks(2204),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 6, 11, 22, 3, 15, 26, DateTimeKind.Local).AddTicks(4113));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "replyComment",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 10, 12, 55, 57, 237, DateTimeKind.Local).AddTicks(852),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 11, 22, 3, 15, 27, DateTimeKind.Local).AddTicks(4283));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "replyComment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 10, 12, 55, 57, 237, DateTimeKind.Local).AddTicks(648),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 6, 11, 22, 3, 15, 27, DateTimeKind.Local).AddTicks(4048));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "post",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 10, 5, 55, 57, 237, DateTimeKind.Utc).AddTicks(5430),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 6, 11, 15, 3, 15, 27, DateTimeKind.Utc).AddTicks(8307));

            migrationBuilder.AlterColumn<DateTime>(
                name: "PublishedDate",
                table: "post",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 10, 5, 55, 57, 237, DateTimeKind.Utc).AddTicks(4981),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 6, 11, 15, 3, 15, 27, DateTimeKind.Utc).AddTicks(7914));

            migrationBuilder.AlterColumn<byte[]>(
                name: "Image",
                table: "post",
                type: "image",
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(byte[]),
                oldType: "image",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "post",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 10, 5, 55, 57, 237, DateTimeKind.Utc).AddTicks(5258),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 6, 11, 15, 3, 15, 27, DateTimeKind.Utc).AddTicks(8153));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "comment",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 10, 12, 55, 57, 236, DateTimeKind.Local).AddTicks(8544),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 11, 22, 3, 15, 27, DateTimeKind.Local).AddTicks(1759));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "comment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 10, 12, 55, 57, 236, DateTimeKind.Local).AddTicks(8276),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 6, 11, 22, 3, 15, 27, DateTimeKind.Local).AddTicks(1455));
        }
    }
}
