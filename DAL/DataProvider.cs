
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;

namespace Store_X.DAL
{
    public class DataProvider
    {
        private static DataProvider _instance;
        public static DataProvider Instance => _instance ?? (_instance = new DataProvider());
        private readonly string _connectionString;
        private DataProvider()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["StoreX_ConnStr"].ConnectionString;
        }

        public DataTable ExecuteQuery(string query, object[] parameter = null)
        {
            DataTable data = new DataTable();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                if (parameter != null) AddParametersToCommand(command, query, parameter);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(data);
            }
            return data;
        }

        public int ExecuteNonQuery(string query, object[] parameter = null)
        {
            int data = 0;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                if (parameter != null) AddParametersToCommand(command, query, parameter);
                data = command.ExecuteNonQuery();
            }
            return data;
        }

        public object ExecuteScalar(string query, object[] parameter = null)
        {
            object data = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                if (parameter != null) AddParametersToCommand(command, query, parameter);
                data = command.ExecuteScalar();
            }
            return data;
        }

        public object ExecuteScalarStoredProcedure(string procedureName, Dictionary<string, object> parameters)
        {
            object data = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(procedureName, connection);
                command.CommandType = CommandType.StoredProcedure;
                if (parameters != null)
                {
                    foreach (var param in parameters)
                    {
                        SqlParameter sqlParam = command.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                        if (param.Value is DataTable) sqlParam.SqlDbType = SqlDbType.Structured;
                    }
                }
                data = command.ExecuteScalar();
            }
            return data;
        }

        // << PHƯƠNG THỨC CÒN THIẾU NẰM Ở ĐÂY >>
        /// <summary>
        /// Thực thi một Stored Procedure không trả về giá trị (INSERT, UPDATE, DELETE).
        /// </summary>
        public int ExecuteNonQueryStoredProcedure(string procedureName, Dictionary<string, object> parameters)
        {
            int data = 0;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(procedureName, connection);
                command.CommandType = CommandType.StoredProcedure;

                if (parameters != null)
                {
                    foreach (var param in parameters)
                    {
                        SqlParameter sqlParam = command.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                        if (param.Value is DataTable)
                        {
                            sqlParam.SqlDbType = SqlDbType.Structured;
                        }
                    }
                }
                data = command.ExecuteNonQuery();
            }
            return data;
        }

        private void AddParametersToCommand(SqlCommand command, string query, object[] parameter)
        {
            var parameterNames = Regex.Matches(query, @"@\w+").Cast<Match>().Select(m => m.Value).ToList();
            if (parameterNames.Count != parameter.Length) throw new ArgumentException("Số lượng tham số không khớp.");
            for (int i = 0; i < parameter.Length; i++)
            {
                command.Parameters.AddWithValue(parameterNames[i], parameter[i] ?? DBNull.Value);
            }
        }
    }
}