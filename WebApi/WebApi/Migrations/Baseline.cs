using FluentMigrator;

namespace Infotecs.WebApi.Migrations
{
    /// <summary>
    /// 
    /// </summary>
    [Migration(0)]
    public class Baseline : Migration
    {
        /// <summary>
        /// 
        /// </summary>
        public override void Up()
        {
            Create.Table("users".ToLower())
                .WithColumn("ID".ToLower()).AsString()
                .WithColumn("nameOfNode".ToLower()).AsString();

            Create.Table("statistics".ToLower())
                .WithColumn("UserID".ToLower()).AsString()
                .WithColumn("nameOfNode".ToLower()).AsString()
                .WithColumn("DateTimeOfLastStatistics".ToLower()).AsString()
                .WithColumn("versionOfClient".ToLower()).AsString()
                .WithColumn("typeOfDevice".ToLower()).AsString()
                .WithColumn("statisticsID".ToLower()).AsString();

            Create.Table("events".ToLower())
                .WithColumn("statisticsID".ToLower()).AsString()
                .WithColumn("name".ToLower()).AsString()
                .WithColumn("date".ToLower()).AsString();
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Down()
        {
            Delete.Table("users".ToLower());
            Delete.Table("statistics".ToLower());
            Delete.Table("events".ToLower());
        }
    }
}
