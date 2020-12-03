using Dapper;
using FluentMigrator;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Linq;

namespace Infotecs.WebApi.Migrations
{
    [Migration(1)]
    public class Migration_1 : Migration
    {
        public override void Up()
        {
            Alter.Table("events".ToLower())
                .AddColumn("description".ToLower())
                .AsString().Nullable();
        }

        public override void Down()
        {
            Delete.Column("description".ToLower()).FromTable("events".ToLower());
        }
    }
}
