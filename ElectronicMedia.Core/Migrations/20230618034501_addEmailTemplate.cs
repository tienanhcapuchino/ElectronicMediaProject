using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElectronicMedia.Core.Migrations
{
    public partial class addEmailTemplate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Dob",
                table: "user",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 18, 10, 45, 1, 641, DateTimeKind.Local).AddTicks(4823),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 6, 16, 9, 56, 0, 203, DateTimeKind.Local).AddTicks(9549));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "replyComment",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 18, 10, 45, 1, 642, DateTimeKind.Local).AddTicks(4276),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 16, 9, 56, 0, 205, DateTimeKind.Local).AddTicks(5620));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "replyComment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 18, 10, 45, 1, 642, DateTimeKind.Local).AddTicks(4035),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 6, 16, 9, 56, 0, 205, DateTimeKind.Local).AddTicks(5263));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "post",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 18, 3, 45, 1, 642, DateTimeKind.Utc).AddTicks(8585),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 6, 16, 2, 56, 0, 206, DateTimeKind.Utc).AddTicks(2639));

            migrationBuilder.AlterColumn<DateTime>(
                name: "PublishedDate",
                table: "post",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 18, 3, 45, 1, 642, DateTimeKind.Utc).AddTicks(8104),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 6, 16, 2, 56, 0, 206, DateTimeKind.Utc).AddTicks(1901));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "post",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 18, 3, 45, 1, 642, DateTimeKind.Utc).AddTicks(8337),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 6, 16, 2, 56, 0, 206, DateTimeKind.Utc).AddTicks(2345));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "comment",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 18, 10, 45, 1, 642, DateTimeKind.Local).AddTicks(1941),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 16, 9, 56, 0, 205, DateTimeKind.Local).AddTicks(1733));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "comment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 18, 10, 45, 1, 642, DateTimeKind.Local).AddTicks(1726),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 6, 16, 9, 56, 0, 205, DateTimeKind.Local).AddTicks(1307));

            migrationBuilder.CreateTable(
                name: "emailTemplate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    MailType = table.Column<byte>(type: "tinyint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MailTo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ModifedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_emailTemplate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_emailTemplate_user_ModifedBy",
                        column: x => x.ModifedBy,
                        principalTable: "user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "emailSetting",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmailTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ResourceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_emailSetting", x => x.Id);
                    table.ForeignKey(
                        name: "FK_emailSetting_emailTemplate_EmailTemplateId",
                        column: x => x.EmailTemplateId,
                        principalTable: "emailTemplate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_emailSetting_EmailTemplateId",
                table: "emailSetting",
                column: "EmailTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_emailTemplate_ModifedBy",
                table: "emailTemplate",
                column: "ModifedBy");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "emailSetting");

            migrationBuilder.DropTable(
                name: "emailTemplate");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Dob",
                table: "user",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 16, 9, 56, 0, 203, DateTimeKind.Local).AddTicks(9549),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 6, 18, 10, 45, 1, 641, DateTimeKind.Local).AddTicks(4823));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "replyComment",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 16, 9, 56, 0, 205, DateTimeKind.Local).AddTicks(5620),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 18, 10, 45, 1, 642, DateTimeKind.Local).AddTicks(4276));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "replyComment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 16, 9, 56, 0, 205, DateTimeKind.Local).AddTicks(5263),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 6, 18, 10, 45, 1, 642, DateTimeKind.Local).AddTicks(4035));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "post",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 16, 2, 56, 0, 206, DateTimeKind.Utc).AddTicks(2639),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 6, 18, 3, 45, 1, 642, DateTimeKind.Utc).AddTicks(8585));

            migrationBuilder.AlterColumn<DateTime>(
                name: "PublishedDate",
                table: "post",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 16, 2, 56, 0, 206, DateTimeKind.Utc).AddTicks(1901),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 6, 18, 3, 45, 1, 642, DateTimeKind.Utc).AddTicks(8104));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "post",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 16, 2, 56, 0, 206, DateTimeKind.Utc).AddTicks(2345),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 6, 18, 3, 45, 1, 642, DateTimeKind.Utc).AddTicks(8337));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "comment",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 16, 9, 56, 0, 205, DateTimeKind.Local).AddTicks(1733),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 18, 10, 45, 1, 642, DateTimeKind.Local).AddTicks(1941));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "comment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 16, 9, 56, 0, 205, DateTimeKind.Local).AddTicks(1307),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 6, 18, 10, 45, 1, 642, DateTimeKind.Local).AddTicks(1726));
        }
    }
}
