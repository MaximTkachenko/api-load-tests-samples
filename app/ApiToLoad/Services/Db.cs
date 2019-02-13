using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using Dapper;

namespace ApiToLoad.Services
{
    public class Db : IDb
    {
        private readonly string _connectionString;

        public Db()
        {
            var dbFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"ApiToLoadDb.sqlite");
            _connectionString = $"Data Source={dbFileName};Version=3;";

            using (var conn = GetConnection())
            {
                conn.Execute(@"
create table if not exists Num 
(
    Id uniqueidentifier not null, 
    Name nvarchar(200) NOT NULL,
    PRIMARY KEY(Id)
)");
            }
        }

        public IReadOnlyList<Num> GetAll()
        {
            using (var conn = GetConnection())
            {
                return conn.Query<Num>("select * from Num").ToList();
            }
        }

        public Num Get(Guid id)
        {
            using (var conn = GetConnection())
            {
                return conn.QueryFirst<Num>("select * from Num where Id = @id", new { id });
            }
        }

        public void Add(Num num)
        {
            using (var conn = GetConnection())
            {
                conn.Execute("insert into Num values (@id, @name)", new { id = num.Id, name = num.Name });
            }
        }

        public void Update(Num num)
        {
            using (var conn = GetConnection())
            {
                conn.Execute("update Num set Name = @name where Id = @id", new { id = num.Id, name = num.Name });
            }
        }

        public void Delete(Guid id)
        {
            using (var conn = GetConnection())
            {
                conn.Execute("delete Num where Id = @id", new { id });
            }
        }

        private IDbConnection GetConnection() => new SQLiteConnection(_connectionString, true);
    }
}
