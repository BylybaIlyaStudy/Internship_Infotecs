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
                .WithColumn("Name".ToLower()).AsString();
            Create.PrimaryKey("ID".ToLower()).OnTable("users".ToLower()).Column("ID".ToLower());

            Create.Table("statistics".ToLower())
                .WithColumn("ID".ToLower()).AsString()
                .WithColumn("Name".ToLower()).AsString()
                .WithColumn("Date".ToLower()).AsString()
                .WithColumn("Version".ToLower()).AsString()
                .WithColumn("OS".ToLower()).AsString();
            Create.ForeignKey("ID".ToLower()).FromTable("statistics".ToLower()).ForeignColumn("ID".ToLower()).ToTable("users".ToLower()).PrimaryColumn("ID".ToLower());

            Create.Table("events".ToLower())
                .WithColumn("ID".ToLower()).AsString()
                .WithColumn("Name".ToLower()).AsString(50)
                .WithColumn("Date".ToLower()).AsDateTime();
            Create.ForeignKey("ID".ToLower()).FromTable("events".ToLower()).ForeignColumn("ID".ToLower()).ToTable("users".ToLower()).PrimaryColumn("ID".ToLower());
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Down()
        {
            Delete.ForeignKey("ID".ToLower()).OnTable("events".ToLower());
            Delete.ForeignKey("ID".ToLower()).OnTable("statistics".ToLower());

            Delete.Table("users".ToLower());
            Delete.Table("statistics".ToLower());
            Delete.Table("events".ToLower());
        }
    }
}
