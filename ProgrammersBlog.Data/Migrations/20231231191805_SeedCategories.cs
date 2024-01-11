using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProgrammersBlog.Data.Migrations;

/// <inheritdoc />
public partial class SeedCategories : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql(
            "INSERT INTO [ProgrammersBlog].dbo.Categories (Name,Description,Note,CreatedDate,CreatedByName, ModifiedDate,ModifiedByName,IsActive,IsDeleted) " +
            "VALUES " +
            "('T-SQL','T-SQL Programlama ile En Güncel Bilgiler','T-SQL Kategorisi',GETDATE(),'Migration',GETDATE(),'Migration',1,0)");
        migrationBuilder.Sql(
            "INSERT INTO [ProgrammersBlog].dbo.Categories (Name,Description,Note,CreatedDate,CreatedByName, ModifiedDate,ModifiedByName,IsActive,IsDeleted) " +
            "VALUES " +
            "('PL-SQL','PL-SQL Programlama ile En Güncel Bilgiler','PL-SQL Kategorisi',GETDATE(),'Migration',GETDATE(),'Migration',1,0)");
        migrationBuilder.Sql(
            "INSERT INTO [ProgrammersBlog].dbo.Categories (Name,Description,Note,CreatedDate,CreatedByName, ModifiedDate,ModifiedByName,IsActive,IsDeleted) " +
            "VALUES " +
            "('PostgreSql','PostgreSql Programlama ile En Güncel Bilgiler','PostgreSql Kategorisi',GETDATE(),'Migration',GETDATE(),'Migration',1,0)");

    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        
    }
}