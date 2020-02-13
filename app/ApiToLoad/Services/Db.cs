using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.Sqlite;

namespace ApiToLoad.Services
{
    public class Db : IDb
    {
        private readonly string _connectionString;

        public Db()
        {
            var dbFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ApiToLoadDb.db");
            _connectionString = $"Filename={dbFileName}";

            using (var conn = GetConnection())
            {
                conn.Open();
                conn.Execute(@"
create table if not exists Num 
(
    Id uniqueidentifier not null, 
    Name nvarchar(200) NOT NULL,
    PRIMARY KEY(Id)
)");
            }
        }

        public async Task<IReadOnlyList<Num>> GetAll()
        {
            using (var conn = GetConnection())
            {
                return (await conn.QueryAsync<Num>("select * from Num")).ToList();
            }
        }

        public async Task<Num> Get(Guid id)
        {
            using (var conn = GetConnection())
            {
                return await conn.QueryFirstAsync<Num>("select * from Num where Id = @id", new { id });
            }
        }

        public async Task Add(Num num)
        {
            using (var conn = GetConnection())
            {
                await conn.ExecuteAsync("insert into Num values (@id, @name)", new { id = num.Id, name = num.Name });
            }
        }

        public async Task Update(Num num)
        {
            using (var conn = GetConnection())
            {
                await conn.ExecuteAsync("update Num set Name = @name where Id = @id", new { id = num.Id, name = num.Name });
            }
        }

        public async Task Delete(Guid id)
        {
            using (var conn = GetConnection())
            {
                await conn.ExecuteAsync("delete from Num where Id = @id", new { id });
            }
        }

        private IDbConnection GetConnection() => new SqliteConnection(_connectionString);
    }
}
