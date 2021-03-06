﻿using Dapper;
using FluentMigrator;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Linq;

namespace Infotecs.WebApi.Migrations
{
    /// <summary>
    /// Нулевая миграция.
    /// </summary>
    [Migration(0)]
    public class Baseline : Migration
    {
        /// <summary>
        /// Создание базы данных.
        /// </summary>
        public override void Up()
        {
            string name = "webapidb";
            var parameters = new DynamicParameters();
            parameters.Add("name", name);
            NpgsqlConnection connection = new NpgsqlConnection(Program.Configuration.GetConnectionString("TestConnection"));
            var records = connection.Query("SELECT DATNAME FROM pg_catalog.pg_database WHERE DATNAME = @name",
                 parameters);
            if (!records.Any())
            {
                connection.Execute($"CREATE DATABASE {name}");
            }

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
        /// Удаление базы данных.
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
