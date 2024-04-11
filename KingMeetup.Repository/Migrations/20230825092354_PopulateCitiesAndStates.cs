using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KingMeetup.Repository.Migrations
{
    /// <inheritdoc />
    public partial class PopulateCitiesAndStates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                IF NOT EXISTS (SELECT TOP 1 1 FROM [dbo].[States])
                AND NOT EXISTS (SELECT TOP 1 1 FROM [dbo].[Cities])
                BEGIN
                    SET IDENTITY_INSERT [dbo].[States] ON 

                    INSERT [dbo].[States] ([Id], [Name], [Active], [DateCreated], [CreatedBy], [DateModified], [ModifiedBy]) VALUES (11, N'Japan', 1, CAST(N'2023-08-24T14:18:17.430' AS DateTime), N'Duje', CAST(N'2023-08-24T12:18:17.430' AS DateTime), NULL)
                    INSERT [dbo].[States] ([Id], [Name], [Active], [DateCreated], [CreatedBy], [DateModified], [ModifiedBy]) VALUES (12, N'Finska', 1, CAST(N'2023-08-24T14:18:41.460' AS DateTime), N'Duje', CAST(N'2023-08-24T12:18:41.460' AS DateTime), NULL)
                    INSERT [dbo].[States] ([Id], [Name], [Active], [DateCreated], [CreatedBy], [DateModified], [ModifiedBy]) VALUES (13, N'Madarska', 0, CAST(N'2023-08-24T14:18:51.627' AS DateTime), N'Duje', CAST(N'2023-08-24T12:18:51.627' AS DateTime), NULL)
                    INSERT [dbo].[States] ([Id], [Name], [Active], [DateCreated], [CreatedBy], [DateModified], [ModifiedBy]) VALUES (10, N'Hrvatska', 1, CAST(N'2023-08-24T14:18:51.627' AS DateTime), N'Duje', CAST(N'2023-08-24T12:18:51.627' AS DateTime), NULL)

                    SET IDENTITY_INSERT [dbo].[States] OFF

                    SET IDENTITY_INSERT [dbo].[Cities] ON 

                    INSERT [dbo].[Cities] ([Id], [Name], [StateId], [Active], [DateCreated], [CreatedBy], [DateModified], [ModifiedBy]) VALUES (14, N'Nagasaki', 11, 1, CAST(N'2023-08-24T14:20:21.170' AS DateTime), N'Duje', CAST(N'2023-08-24T12:20:21.170' AS DateTime), NULL)
                    INSERT [dbo].[Cities] ([Id], [Name], [StateId], [Active], [DateCreated], [CreatedBy], [DateModified], [ModifiedBy]) VALUES (15, N'Tokyo', 11, 1, CAST(N'2023-08-24T14:20:28.057' AS DateTime), N'Duje', CAST(N'2023-08-24T12:20:28.057' AS DateTime), NULL)
                    INSERT [dbo].[Cities] ([Id], [Name], [StateId], [Active], [DateCreated], [CreatedBy], [DateModified], [ModifiedBy]) VALUES (16, N'Budimpešta', 13, 1, CAST(N'2023-08-24T14:20:52.383' AS DateTime), N'Duje', CAST(N'2023-08-24T12:20:52.383' AS DateTime), NULL)
                    INSERT [dbo].[Cities] ([Id], [Name], [StateId], [Active], [DateCreated], [CreatedBy], [DateModified], [ModifiedBy]) VALUES (17, N'Helsinki', 12, 1, CAST(N'2023-08-24T14:21:13.263' AS DateTime), N'Duje', CAST(N'2023-08-24T12:21:13.263' AS DateTime), NULL)
                    INSERT [dbo].[Cities] ([Id], [Name], [StateId], [Active], [DateCreated], [CreatedBy], [DateModified], [ModifiedBy]) VALUES (18, N'Kristiinankaupunki', 12, 1, CAST(N'2023-08-24T14:21:45.087' AS DateTime), N'Duje', CAST(N'2023-08-24T12:21:45.087' AS DateTime), NULL)
                    INSERT [dbo].[Cities] ([Id], [Name], [StateId], [Active], [DateCreated], [CreatedBy], [DateModified], [ModifiedBy]) VALUES (12, N'Split', 10, 1, CAST(N'2023-08-24T14:21:13.263' AS DateTime), N'Duje', CAST(N'2023-08-24T12:21:13.263' AS DateTime), NULL)
                    INSERT [dbo].[Cities] ([Id], [Name], [StateId], [Active], [DateCreated], [CreatedBy], [DateModified], [ModifiedBy]) VALUES (13, N'Zagreb', 10, 1, CAST(N'2023-08-24T14:21:45.087' AS DateTime), N'Duje', CAST(N'2023-08-24T12:21:45.087' AS DateTime), NULL) 

                    SET IDENTITY_INSERT [dbo].[Cities] OFF
                END"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
