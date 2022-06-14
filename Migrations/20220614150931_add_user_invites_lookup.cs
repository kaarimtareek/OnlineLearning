using Microsoft.EntityFrameworkCore.Migrations;

using OnlineLearning.Models;
using System;

namespace OnlineLearning.Migrations
{
    public partial class add_user_invites_lookup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("USE [OnlineLearning] update[dbo].LookupUserRoomStatuses set[NameArabic] = N'مدعو', [NameEnglish] = N'Invited' where id = N'INVITED'; if @@ROWCOUNT = 0 INSERT[dbo].[LookupUserRoomStatuses]([Id], [NameArabic], [NameEnglish], [CreatedAt], [UpdatedAt], [IsDeleted]) VALUES(N'INVITED', N'مدعو', N'Invited', CAST(N'2021-11-06T09:29:59.5745403' AS DateTime2), CAST(N'2021-11-06T09:29:59.5745403' AS DateTime2), 0)");
        }
protected override void Down(MigrationBuilder migrationBuilder)
{
}
    }
}
