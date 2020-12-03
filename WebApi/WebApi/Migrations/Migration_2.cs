using Dapper;
using FluentMigrator;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Linq;

namespace Infotecs.WebApi.Migrations
{
    [Migration(2)]
    public class Migration_2 : Migration
    {
        public override void Up()
        {
            Alter.Table("events".ToLower())
                .AddColumn("level".ToLower())
                .AsString().Nullable().WithDefaultValue("low");

            Alter.Table("users".ToLower())
                .AddColumn("description".ToLower())
                .AsString().Nullable();

            Alter.Table("users".ToLower())
                .AlterColumn("name".ToLower())
                .AsString(256).NotNullable();
        }

        public override void Down()
        {
            Delete.Column("description".ToLower()).FromTable("events".ToLower());
        }
    }
}