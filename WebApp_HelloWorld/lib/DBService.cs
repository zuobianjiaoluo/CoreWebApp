using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp_HelloWorld.lib
{
    public class DBService
    {
        protected IDbConnection conn;

        public DBService()
        {
            conn = new SqlConnection("server=198.198.198.181;User Id=root;password=mima2100;Database=mysql-db");
        }

        public async Task<T> Single<T>(string sql, object paramPairs = null)
        {
            return await conn.QuerySingleOrDefaultAsync<T>(sql, paramPairs);
        }

        public async Task<int> Count(string sql, object paramPairs = null)
        {
            return await conn.QuerySingleOrDefaultAsync<int>(sql, paramPairs);
        }
    }
}
