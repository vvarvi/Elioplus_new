using System;
using System.Linq;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace WdS.ElioPlus.Lib.DB
{

    public class DBLiveSession : IDisposable
    {
        #region Connection Members

        internal readonly Guid SessionGuid = Guid.NewGuid();

        private string connectionString;
        public string ConnectionString
        {
            get { return connectionString; }
        }

        private SqlConnection connection;
        public SqlConnection Connection
        {
            get { return connection; }
            set { connection = value; }
        }

        private SqlTransaction transaction;
        public SqlTransaction Transaction
        {
            get { return transaction; }
            set { transaction = value; }
        }

        private int commandTimeout = 120;
        public int CommandTimeout
        {
            get { return commandTimeout; }
            set { commandTimeout = value; }
        }

        private int virtualTransactions = 0;
        private bool transactionRolledBack = false;

        public DBLiveSession()
        {
            this.connectionString = ConfigurationManager.ConnectionStrings["ConnectionString2"].ConnectionString;
            this.Connection = new SqlConnection(this.ConnectionString);
        }

        public DBLiveSession(string conString)
        {
            this.connectionString = conString;
            this.Connection = new SqlConnection(this.ConnectionString);
        }

        #endregion

        #region Connection States

        public void OpenConnection()
        {
            if (this.Connection.State == ConnectionState.Closed)
                this.Connection.Open();
        }

        public void CloseConnection()
        {
            if (this.Connection.State == ConnectionState.Open)
            {
                this.transactionRolledBack = false;
                this.virtualTransactions = 0;
                if (this.Transaction != null)
                    this.Transaction.Commit();
                this.Connection.Close();
                this.Transaction = null;
            }
        }

        #endregion

        #region Transaction States

        public void BeginTransaction()
        {
            if (this.transactionRolledBack)
            {
                throw new Exception("Transaction has been rolled back");
            }
            if (this.virtualTransactions == 0)
            {
                this.Transaction = this.Connection.BeginTransaction(IsolationLevel.Serializable);
            }
            this.virtualTransactions++;
        }

        public void BeginTransaction(IsolationLevel level)
        {
            if (this.transactionRolledBack)
            {
                throw new Exception("Transaction has been rolled back");
            }
            if (this.virtualTransactions == 0)
            {
                this.Transaction = this.Connection.BeginTransaction(level);
            }
            this.virtualTransactions++;
        }

        public void CommitTransaction()
        {
            if (this.transactionRolledBack)
            {
                throw new Exception("Transaction has been rolled back");
            }
            if (this.virtualTransactions > 1)
                this.virtualTransactions--;
            else if (this.virtualTransactions == 1)
            {
                this.Transaction.Commit();
                this.virtualTransactions = 0;
                this.transactionRolledBack = false;
                this.Transaction = null;
            }
            else
            {
                throw new Exception("No Transaction found");
            }

        }

        public void RollBackTransaction()
        {
            if (this.virtualTransactions > 1)
            {
                this.virtualTransactions--;
                this.transactionRolledBack = true;
            }
            else if (this.virtualTransactions == 1)
            {
                this.Transaction.Rollback();
                this.virtualTransactions = 0;
                this.transactionRolledBack = false;
                this.Transaction = null;
            }
            else
            {
                throw new Exception("No Transaction found");
            }

        }

        #endregion

        #region Sql Ececution Types

        public int ExecuteQuery(string sql, params SqlParameter[] parameters)
        {
            SqlCommand cmd = new SqlCommand(sql, this.Connection);
            DateTime startTime = DateTime.Now;

            cmd.CommandTimeout = this.CommandTimeout;

            if (parameters != null && parameters.Length > 0)
            {
                for (int i = 0; i < parameters.Length; i++)
                {
                    cmd.Parameters.Add(parameters[i]);
                }
            }

            cmd.Transaction = this.Transaction;
            int result = cmd.ExecuteNonQuery();

            DBLogger.AppendToDbLog(startTime, SessionGuid.ToString(), sql, 0, result);

            return result;
        }

        public int ExecuteScalarIntQuery(string sql, params SqlParameter[] parameters)
        {
            SqlCommand cmd = new SqlCommand(sql, this.Connection);
            DateTime startTime = DateTime.Now;

            cmd.CommandTimeout = this.CommandTimeout;

            if (parameters != null && parameters.Length > 0)
            {
                for (int i = 0; i < parameters.Length; i++)
                {
                    cmd.Parameters.Add(parameters[i]);
                }
            }

            cmd.Transaction = this.Transaction;

            object result = cmd.ExecuteScalar();

            DBLogger.AppendToDbLog(startTime, SessionGuid.ToString(), sql, 0, (result == null ? 0 : 1));

            if (result == null)
                return 0;
            else if (result == DBNull.Value)
                return 0;
            else
                return Convert.ToInt32(result);

        }

        public DataTable GetDataTable(string sql, params SqlParameter[] parameters)
        {
            DateTime startTime = DateTime.Now;

            SqlCommand cmd = new SqlCommand(sql, this.Connection);
            cmd.CommandTimeout = this.CommandTimeout;

            if (parameters != null && parameters.Length > 0)
            {
                for (int i = 0; i < parameters.Length; i++)
                {
                    cmd.Parameters.Add(parameters[i]);
                }
            }
            DataTable table = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            cmd.Transaction = this.Transaction;
            da.Fill(table);

            DBLogger.AppendToDbLog(startTime, SessionGuid.ToString(), sql, 0, table.Rows.Count);

            return table;
        }

        #endregion

        //~DBSession()
        //{
        //    if (this.Connection != null)
        //        this.Connection.Dispose();
        //}

        #region IDisposable Members

        public void Dispose()
        {
            if (this.Connection != null)
            {
                this.Connection.Close();
                this.Connection.Dispose();
            }
        }

        #endregion
    }
}