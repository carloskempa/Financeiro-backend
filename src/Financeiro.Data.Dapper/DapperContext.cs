using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.Data.Dapper
{
    public class DapperContext
    {
        private readonly string _connectionString;
        public DapperContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SqlConnection ObterConexao()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
