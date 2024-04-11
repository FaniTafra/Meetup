using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KingMeetup.Repository.Migrations
{
    /// <inheritdoc />
    public partial class PopulateInterestsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                IF NOT EXISTS (SELECT TOP 1 1 FROM [dbo].[Interests])
                BEGIN

                SET IDENTITY_INSERT [dbo].[Interests] ON 

                INSERT [dbo].[Interests] ([Id], [Name], [Active], [DateCreated], [CreatedBy], [DateModified], [ModifiedBy]) VALUES (1, N'Development', 1, CAST(N'2023-08-18T11:22:12.373' AS DateTime), N'Fani', CAST(N'2023-08-18T11:22:12.373' AS DateTime), NULL)
                INSERT [dbo].[Interests] ([Id], [Name], [Active], [DateCreated], [CreatedBy], [DateModified], [ModifiedBy]) VALUES (2, N'QA', 1, CAST(N'2023-08-18T11:23:30.890' AS DateTime), N'Fani', CAST(N'2023-08-18T09:23:30.890' AS DateTime), NULL)
                INSERT [dbo].[Interests] ([Id], [Name], [Active], [DateCreated], [CreatedBy], [DateModified], [ModifiedBy]) VALUES (3, N'Project Management', 1, CAST(N'2023-08-18T11:24:12.897' AS DateTime), N'Fani', CAST(N'2023-08-18T09:24:12.897' AS DateTime), NULL)
                INSERT [dbo].[Interests] ([Id], [Name], [Active], [DateCreated], [CreatedBy], [DateModified], [ModifiedBy]) VALUES (4, N'Business analysis', 1, CAST(N'2023-08-18T11:25:04.263' AS DateTime), N'Fani', CAST(N'2023-08-18T09:25:04.263' AS DateTime), NULL)

                SET IDENTITY_INSERT [dbo].[Interests] OFF

                END"
            );
        }

        // <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
