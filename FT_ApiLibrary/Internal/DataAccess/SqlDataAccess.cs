using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FT_ApiLibrary.Internal.DataAccess
{
    public class SqlDataAccess : IDisposable, ISqlDataAccess
    {
        private IDbConnection? _connection;
        private IDbTransaction? _transaction;
        private bool _isClosed = false;
        private readonly IConfiguration _config;
        private readonly ILogger<SqlDataAccess> _logger;

        public SqlDataAccess(IConfiguration config, ILogger<SqlDataAccess> logger)
        {
            _config = config;
            _logger = logger;
        }

        public string GetConnectionString(string name)
        {
            return _config.GetConnectionString(name);
        }


        //The following is for load data from database
        public List<T> LoadData<T, U>(string storedProcedure, U parameters, string connectionStringName)
        {
            string? conn = GetConnectionString(connectionStringName);

            using (IDbConnection dbconn = new SqlConnection(conn))
            {
                List<T> rows = dbconn.Query<T>(storedProcedure, parameters,
                    commandType: CommandType.StoredProcedure).ToList();
                return rows;
            }
        }

        //The following is for save data to database
        public void SaveData<T>(string storedProcedure, T parameters, string connectionStringName)
        {
            string conn = GetConnectionString(connectionStringName);

            using (IDbConnection dbconn = new SqlConnection(conn))
            {
                dbconn.Execute(storedProcedure, parameters,
                    commandType: CommandType.StoredProcedure);

            }
        }

        //Open connect/start transaction mehod
        public void StartTransaction(string connectionStringName)
        {
            string conn = GetConnectionString(connectionStringName);
            _connection = new SqlConnection(conn);
            _connection.Open();

            _transaction = _connection.BeginTransaction();

            _isClosed = false;
        }

        public void CommitTransaction()
        {
            _transaction?.Commit();
            _connection?.Close();

            _isClosed = true;
        }

        public void RollbackTransaction()
        {
            _transaction?.Rollback();
            _connection?.Close();

            _isClosed = true;
        }

        //Load using transaction
        public List<T> LoadDataInTransaction<T, U>(string storedProcedure, U parameters)
        {
            List<T> rows = _connection.Query<T>(storedProcedure, parameters,
                commandType: CommandType.StoredProcedure, transaction: _transaction).ToList();
            return rows;

        }

        //Save using transaction
        public void SaveDataInTransaction<T>(string storedProcedure, T parameters)
        {
            _connection.Execute(storedProcedure, parameters,
                    commandType: CommandType.StoredProcedure, transaction: _transaction);
        }

        //Close connection/stop transaction method
        //Dispose

        public void Dispose() //This will be called by the system once done transaction
        {
            if (_isClosed == false)
            {
                try
                {
                    CommitTransaction();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Commit transaction failed in the dispose method");
                }
            }

            _transaction = null;
            _connection = null;
        }
    }
}
