using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace Erp.Domain.Crm.Helper
{
    #region SqlHelper

    public static class SqlHelper
    {
        private const string ConnectionStringKey = "ErpDbContext";

        #region Query

        public static IEnumerable<dynamic> QuerySP(string storedProcedure, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, bool buffered = true, int? commandTimeout = null, string connectionString = null)
        {
            CombineParameters(ref param, outParam);

            using (SqlConnection connection = new SqlConnection(GetConnectionString(connectionString)))
            {
                connection.Open();
                return connection.Query(storedProcedure, param: (object)param, transaction: transaction, buffered: buffered, commandTimeout: GetTimeout(commandTimeout), commandType: CommandType.StoredProcedure);
            }
        }

        public static IEnumerable<dynamic> QuerySQL(string sql, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, bool buffered = true, int? commandTimeout = null, string connectionString = null)
        {
            CombineParameters(ref param, outParam);

            using (SqlConnection connection = new SqlConnection(GetConnectionString(connectionString)))
            {
                connection.Open();
                return connection.Query(sql, param: (object)param, transaction: transaction, buffered: buffered, commandTimeout: GetTimeout(commandTimeout), commandType: CommandType.Text);
            }
        }

        public static IEnumerable<T> QuerySP<T>(string storedProcedure, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, bool buffered = true, int? commandTimeout = null, string connectionString = null)
        {
            CombineParameters(ref param, outParam);

            using (SqlConnection connection = new SqlConnection(GetConnectionString(connectionString)))
            {
                connection.Open();
                return connection.Query<T>(storedProcedure, param: (object)param, transaction: transaction, buffered: buffered, commandTimeout: GetTimeout(commandTimeout), commandType: CommandType.StoredProcedure);
            }
        }

        public static IEnumerable<T> QuerySQL<T>(string sql, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, bool buffered = true, int? commandTimeout = null, string connectionString = null)
        {
            CombineParameters(ref param, outParam);

            using (SqlConnection connection = new SqlConnection(GetConnectionString(connectionString)))
            {
                connection.Open();
                return connection.Query<T>(sql, param: (object)param, transaction: transaction, buffered: buffered, commandTimeout: GetTimeout(commandTimeout), commandType: CommandType.Text);
            }
        }

        public static IEnumerable<object> QuerySP(Type type, string storedProcedure, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, bool buffered = true, int? commandTimeout = null, string connectionString = null)
        {
            CombineParameters(ref param, outParam);

            using (SqlConnection connection = new SqlConnection(GetConnectionString(connectionString)))
            {
                connection.Open();
                return connection.Query(type, storedProcedure, param: (object)param, transaction: transaction, buffered: buffered, commandTimeout: GetTimeout(commandTimeout), commandType: CommandType.StoredProcedure);
            }
        }

        public static IEnumerable<object> QuerySQL(Type type, string sql, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, bool buffered = true, int? commandTimeout = null, string connectionString = null)
        {
            CombineParameters(ref param, outParam);

            using (SqlConnection connection = new SqlConnection(GetConnectionString(connectionString)))
            {
                connection.Open();
                return connection.Query(type, sql, param: (object)param, transaction: transaction, buffered: buffered, commandTimeout: GetTimeout(commandTimeout), commandType: CommandType.Text);
            }
        }

        #endregion

        #region QueryMultiple

        #region IEnumerable<IEnumerable<dynamic>>

        public static IEnumerable<IEnumerable<dynamic>> QueryMultipleSP(string storedProcedure, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, string connectionString = null)
        {
            return QueryMultiple(storedProcedure, param, outParam, transaction, commandTimeout, CommandType.StoredProcedure, connectionString);
        }

        public static IEnumerable<IEnumerable<dynamic>> QueryMultipleSQL(string sql, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, string connectionString = null)
        {
            return QueryMultiple(sql, param, outParam, transaction, commandTimeout, CommandType.Text, connectionString);
        }

        private static IEnumerable<IEnumerable<dynamic>> QueryMultiple(string sql, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null, string connectionString = null)
        {
            CombineParameters(ref param, outParam);

            using (SqlConnection connection = new SqlConnection(GetConnectionString(connectionString)))
            {
                connection.Open();
                SqlMapper.GridReader gr = connection.QueryMultiple(sql, param: (object)param, transaction: transaction, commandTimeout: GetTimeout(commandTimeout), commandType: commandType);
                List<IEnumerable<dynamic>> lists = new List<IEnumerable<dynamic>>();
                while (!gr.IsConsumed)
                    lists.Add(gr.Read());
                return lists;
            }
        }

        #endregion

        #region IEnumerable<IEnumerable<T>>

        public static IEnumerable<IEnumerable<T>> QueryMultipleSP<T>(string storedProcedure, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, string connectionString = null)
        {
            return QueryMultiple<T>(storedProcedure, param, outParam, transaction, commandTimeout, CommandType.StoredProcedure, connectionString);
        }

        public static IEnumerable<IEnumerable<T>> QueryMultipleSQL<T>(string sql, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, string connectionString = null)
        {
            return QueryMultiple<T>(sql, param, outParam, transaction, commandTimeout, CommandType.Text, connectionString);
        }

        private static IEnumerable<IEnumerable<T>> QueryMultiple<T>(string sql, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null, string connectionString = null)
        {
            CombineParameters(ref param, outParam);

            using (SqlConnection connection = new SqlConnection(GetConnectionString(connectionString)))
            {
                connection.Open();
                SqlMapper.GridReader gr = connection.QueryMultiple(sql, param: (object)param, transaction: transaction, commandTimeout: GetTimeout(commandTimeout), commandType: commandType);
                List<IEnumerable<T>> lists = new List<IEnumerable<T>>();
                while (!gr.IsConsumed)
                    lists.Add(gr.Read<T>());
                return lists;
            }
        }

        #endregion

        #region Tuples

        public static Tuple<IEnumerable<T1>, IEnumerable<T2>> QueryMultipleSP<T1, T2>(string storedProcedure, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, string connectionString = null)
        {
            IEnumerable[] lists = QueryMultiple<T1, T2, object, object, object, object, object, object, object, object, object, object, object, object>(2, storedProcedure, param, outParam, transaction, commandTimeout, CommandType.StoredProcedure, connectionString);
            return new Tuple<IEnumerable<T1>, IEnumerable<T2>>(
                (IEnumerable<T1>)lists[0],
                (IEnumerable<T2>)lists[1]
            );
        }

        public static Tuple<IEnumerable<T1>, IEnumerable<T2>> QueryMultipleSQL<T1, T2>(string sql, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, string connectionString = null)
        {
            IEnumerable[] lists = QueryMultiple<T1, T2, object, object, object, object, object, object, object, object, object, object, object, object>(2, sql, param, outParam, transaction, commandTimeout, CommandType.Text, connectionString);
            return new Tuple<IEnumerable<T1>, IEnumerable<T2>>(
                (IEnumerable<T1>)lists[0],
                (IEnumerable<T2>)lists[1]
            );
        }

        public static Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>> QueryMultipleSP<T1, T2, T3>(string storedProcedure, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, string connectionString = null)
        {
            IEnumerable[] lists = QueryMultiple<T1, T2, T3, object, object, object, object, object, object, object, object, object, object, object>(3, storedProcedure, param, outParam, transaction, commandTimeout, CommandType.StoredProcedure, connectionString);
            return new Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>>(
                (IEnumerable<T1>)lists[0],
                (IEnumerable<T2>)lists[1],
                (IEnumerable<T3>)lists[2]
            );
        }

        public static Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>> QueryMultipleSQL<T1, T2, T3>(string sql, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, string connectionString = null)
        {
            IEnumerable[] lists = QueryMultiple<T1, T2, T3, object, object, object, object, object, object, object, object, object, object, object>(3, sql, param, outParam, transaction, commandTimeout, CommandType.Text, connectionString);
            return new Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>>(
                (IEnumerable<T1>)lists[0],
                (IEnumerable<T2>)lists[1],
                (IEnumerable<T3>)lists[2]
            );
        }

        public static Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>> QueryMultipleSP<T1, T2, T3, T4>(string storedProcedure, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, string connectionString = null)
        {
            IEnumerable[] lists = QueryMultiple<T1, T2, T3, T4, object, object, object, object, object, object, object, object, object, object>(4, storedProcedure, param, outParam, transaction, commandTimeout, CommandType.StoredProcedure, connectionString);
            return new Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>>(
                (IEnumerable<T1>)lists[0],
                (IEnumerable<T2>)lists[1],
                (IEnumerable<T3>)lists[2],
                (IEnumerable<T4>)lists[3]
            );
        }

        public static Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>> QueryMultipleSQL<T1, T2, T3, T4>(string sql, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, string connectionString = null)
        {
            IEnumerable[] lists = QueryMultiple<T1, T2, T3, T4, object, object, object, object, object, object, object, object, object, object>(4, sql, param, outParam, transaction, commandTimeout, CommandType.Text, connectionString);
            return new Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>>(
                (IEnumerable<T1>)lists[0],
                (IEnumerable<T2>)lists[1],
                (IEnumerable<T3>)lists[2],
                (IEnumerable<T4>)lists[3]
            );
        }

        public static Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>> QueryMultipleSP<T1, T2, T3, T4, T5>(string storedProcedure, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, string connectionString = null)
        {
            IEnumerable[] lists = QueryMultiple<T1, T2, T3, T4, T5, object, object, object, object, object, object, object, object, object>(5, storedProcedure, param, outParam, transaction, commandTimeout, CommandType.StoredProcedure, connectionString);
            return new Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>>(
                (IEnumerable<T1>)lists[0],
                (IEnumerable<T2>)lists[1],
                (IEnumerable<T3>)lists[2],
                (IEnumerable<T4>)lists[3],
                (IEnumerable<T5>)lists[4]
            );
        }

        public static Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>> QueryMultipleSQL<T1, T2, T3, T4, T5>(string sql, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, string connectionString = null)
        {
            IEnumerable[] lists = QueryMultiple<T1, T2, T3, T4, T5, object, object, object, object, object, object, object, object, object>(5, sql, param, outParam, transaction, commandTimeout, CommandType.Text, connectionString);
            return new Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>>(
                (IEnumerable<T1>)lists[0],
                (IEnumerable<T2>)lists[1],
                (IEnumerable<T3>)lists[2],
                (IEnumerable<T4>)lists[3],
                (IEnumerable<T5>)lists[4]
            );
        }

        public static Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>> QueryMultipleSP<T1, T2, T3, T4, T5, T6>(string storedProcedure, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, string connectionString = null)
        {
            IEnumerable[] lists = QueryMultiple<T1, T2, T3, T4, T5, T6, object, object, object, object, object, object, object, object>(6, storedProcedure, param, outParam, transaction, commandTimeout, CommandType.StoredProcedure, connectionString);
            return new Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>>(
                (IEnumerable<T1>)lists[0],
                (IEnumerable<T2>)lists[1],
                (IEnumerable<T3>)lists[2],
                (IEnumerable<T4>)lists[3],
                (IEnumerable<T5>)lists[4],
                (IEnumerable<T6>)lists[5]
            );
        }

        public static Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>> QueryMultipleSQL<T1, T2, T3, T4, T5, T6>(string sql, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, string connectionString = null)
        {
            IEnumerable[] lists = QueryMultiple<T1, T2, T3, T4, T5, T6, object, object, object, object, object, object, object, object>(6, sql, param, outParam, transaction, commandTimeout, CommandType.Text, connectionString);
            return new Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>>(
                (IEnumerable<T1>)lists[0],
                (IEnumerable<T2>)lists[1],
                (IEnumerable<T3>)lists[2],
                (IEnumerable<T4>)lists[3],
                (IEnumerable<T5>)lists[4],
                (IEnumerable<T6>)lists[5]
            );
        }

        public static Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>> QueryMultipleSP<T1, T2, T3, T4, T5, T6, T7>(string storedProcedure, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, string connectionString = null)
        {
            IEnumerable[] lists = QueryMultiple<T1, T2, T3, T4, T5, T6, T7, object, object, object, object, object, object, object>(7, storedProcedure, param, outParam, transaction, commandTimeout, CommandType.StoredProcedure, connectionString);
            return new Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>>(
                (IEnumerable<T1>)lists[0],
                (IEnumerable<T2>)lists[1],
                (IEnumerable<T3>)lists[2],
                (IEnumerable<T4>)lists[3],
                (IEnumerable<T5>)lists[4],
                (IEnumerable<T6>)lists[5],
                (IEnumerable<T7>)lists[6]
            );
        }

        public static Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>> QueryMultipleSQL<T1, T2, T3, T4, T5, T6, T7>(string sql, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, string connectionString = null)
        {
            IEnumerable[] lists = QueryMultiple<T1, T2, T3, T4, T5, T6, T7, object, object, object, object, object, object, object>(7, sql, param, outParam, transaction, commandTimeout, CommandType.Text, connectionString);
            return new Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>>(
                (IEnumerable<T1>)lists[0],
                (IEnumerable<T2>)lists[1],
                (IEnumerable<T3>)lists[2],
                (IEnumerable<T4>)lists[3],
                (IEnumerable<T5>)lists[4],
                (IEnumerable<T6>)lists[5],
                (IEnumerable<T7>)lists[6]
            );
        }

        public static Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>, Tuple<IEnumerable<T8>>> QueryMultipleSP<T1, T2, T3, T4, T5, T6, T7, T8>(string storedProcedure, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, string connectionString = null)
        {
            IEnumerable[] lists = QueryMultiple<T1, T2, T3, T4, T5, T6, T7, T8, object, object, object, object, object, object>(8, storedProcedure, param, outParam, transaction, commandTimeout, CommandType.StoredProcedure, connectionString);
            return new Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>, Tuple<IEnumerable<T8>>>(
                (IEnumerable<T1>)lists[0],
                (IEnumerable<T2>)lists[1],
                (IEnumerable<T3>)lists[2],
                (IEnumerable<T4>)lists[3],
                (IEnumerable<T5>)lists[4],
                (IEnumerable<T6>)lists[5],
                (IEnumerable<T7>)lists[6],
                new Tuple<IEnumerable<T8>>(
                    (IEnumerable<T8>)lists[7]
                )
            );
        }

        public static Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>, Tuple<IEnumerable<T8>>> QueryMultipleSQL<T1, T2, T3, T4, T5, T6, T7, T8>(string sql, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, string connectionString = null)
        {
            IEnumerable[] lists = QueryMultiple<T1, T2, T3, T4, T5, T6, T7, T8, object, object, object, object, object, object>(8, sql, param, outParam, transaction, commandTimeout, CommandType.Text, connectionString);
            return new Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>, Tuple<IEnumerable<T8>>>(
                (IEnumerable<T1>)lists[0],
                (IEnumerable<T2>)lists[1],
                (IEnumerable<T3>)lists[2],
                (IEnumerable<T4>)lists[3],
                (IEnumerable<T5>)lists[4],
                (IEnumerable<T6>)lists[5],
                (IEnumerable<T7>)lists[6],
                new Tuple<IEnumerable<T8>>(
                    (IEnumerable<T8>)lists[7]
                )
            );
        }

        public static Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>, Tuple<IEnumerable<T8>, IEnumerable<T9>>> QueryMultipleSP<T1, T2, T3, T4, T5, T6, T7, T8, T9>(string storedProcedure, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, string connectionString = null)
        {
            IEnumerable[] lists = QueryMultiple<T1, T2, T3, T4, T5, T6, T7, T8, T9, object, object, object, object, object>(9, storedProcedure, param, outParam, transaction, commandTimeout, CommandType.StoredProcedure, connectionString);
            return new Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>, Tuple<IEnumerable<T8>, IEnumerable<T9>>>(
                (IEnumerable<T1>)lists[0],
                (IEnumerable<T2>)lists[1],
                (IEnumerable<T3>)lists[2],
                (IEnumerable<T4>)lists[3],
                (IEnumerable<T5>)lists[4],
                (IEnumerable<T6>)lists[5],
                (IEnumerable<T7>)lists[6],
                new Tuple<IEnumerable<T8>, IEnumerable<T9>>(
                    (IEnumerable<T8>)lists[7],
                    (IEnumerable<T9>)lists[8]
                )
            );
        }

        public static Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>, Tuple<IEnumerable<T8>, IEnumerable<T9>>> QueryMultipleSQL<T1, T2, T3, T4, T5, T6, T7, T8, T9>(string sql, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, string connectionString = null)
        {
            IEnumerable[] lists = QueryMultiple<T1, T2, T3, T4, T5, T6, T7, T8, T9, object, object, object, object, object>(9, sql, param, outParam, transaction, commandTimeout, CommandType.Text, connectionString);
            return new Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>, Tuple<IEnumerable<T8>, IEnumerable<T9>>>(
                (IEnumerable<T1>)lists[0],
                (IEnumerable<T2>)lists[1],
                (IEnumerable<T3>)lists[2],
                (IEnumerable<T4>)lists[3],
                (IEnumerable<T5>)lists[4],
                (IEnumerable<T6>)lists[5],
                (IEnumerable<T7>)lists[6],
                new Tuple<IEnumerable<T8>, IEnumerable<T9>>(
                    (IEnumerable<T8>)lists[7],
                    (IEnumerable<T9>)lists[8]
                )
            );
        }

        public static Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>, Tuple<IEnumerable<T8>, IEnumerable<T9>, IEnumerable<T10>>> QueryMultipleSP<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(string storedProcedure, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, string connectionString = null)
        {
            IEnumerable[] lists = QueryMultiple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, object, object, object, object>(10, storedProcedure, param, outParam, transaction, commandTimeout, CommandType.StoredProcedure, connectionString);
            return new Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>, Tuple<IEnumerable<T8>, IEnumerable<T9>, IEnumerable<T10>>>(
                (IEnumerable<T1>)lists[0],
                (IEnumerable<T2>)lists[1],
                (IEnumerable<T3>)lists[2],
                (IEnumerable<T4>)lists[3],
                (IEnumerable<T5>)lists[4],
                (IEnumerable<T6>)lists[5],
                (IEnumerable<T7>)lists[6],
                new Tuple<IEnumerable<T8>, IEnumerable<T9>, IEnumerable<T10>>(
                    (IEnumerable<T8>)lists[7],
                    (IEnumerable<T9>)lists[8],
                    (IEnumerable<T10>)lists[9]
                )
            );
        }

        public static Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>, Tuple<IEnumerable<T8>, IEnumerable<T9>, IEnumerable<T10>>> QueryMultipleSQL<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(string sql, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, string connectionString = null)
        {
            IEnumerable[] lists = QueryMultiple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, object, object, object, object>(10, sql, param, outParam, transaction, commandTimeout, CommandType.Text, connectionString);
            return new Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>, Tuple<IEnumerable<T8>, IEnumerable<T9>, IEnumerable<T10>>>(
                (IEnumerable<T1>)lists[0],
                (IEnumerable<T2>)lists[1],
                (IEnumerable<T3>)lists[2],
                (IEnumerable<T4>)lists[3],
                (IEnumerable<T5>)lists[4],
                (IEnumerable<T6>)lists[5],
                (IEnumerable<T7>)lists[6],
                new Tuple<IEnumerable<T8>, IEnumerable<T9>, IEnumerable<T10>>(
                    (IEnumerable<T8>)lists[7],
                    (IEnumerable<T9>)lists[8],
                    (IEnumerable<T10>)lists[9]
                )
            );
        }

        public static Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>, Tuple<IEnumerable<T8>, IEnumerable<T9>, IEnumerable<T10>, IEnumerable<T11>>> QueryMultipleSP<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(string storedProcedure, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, string connectionString = null)
        {
            IEnumerable[] lists = QueryMultiple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, object, object, object>(11, storedProcedure, param, outParam, transaction, commandTimeout, CommandType.StoredProcedure, connectionString);
            return new Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>, Tuple<IEnumerable<T8>, IEnumerable<T9>, IEnumerable<T10>, IEnumerable<T11>>>(
                (IEnumerable<T1>)lists[0],
                (IEnumerable<T2>)lists[1],
                (IEnumerable<T3>)lists[2],
                (IEnumerable<T4>)lists[3],
                (IEnumerable<T5>)lists[4],
                (IEnumerable<T6>)lists[5],
                (IEnumerable<T7>)lists[6],
                new Tuple<IEnumerable<T8>, IEnumerable<T9>, IEnumerable<T10>, IEnumerable<T11>>(
                    (IEnumerable<T8>)lists[7],
                    (IEnumerable<T9>)lists[8],
                    (IEnumerable<T10>)lists[9],
                    (IEnumerable<T11>)lists[10]
                )
            );
        }

        public static Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>, Tuple<IEnumerable<T8>, IEnumerable<T9>, IEnumerable<T10>, IEnumerable<T11>>> QueryMultipleSQL<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(string sql, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, string connectionString = null)
        {
            IEnumerable[] lists = QueryMultiple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, object, object, object>(11, sql, param, outParam, transaction, commandTimeout, CommandType.Text, connectionString);
            return new Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>, Tuple<IEnumerable<T8>, IEnumerable<T9>, IEnumerable<T10>, IEnumerable<T11>>>(
                (IEnumerable<T1>)lists[0],
                (IEnumerable<T2>)lists[1],
                (IEnumerable<T3>)lists[2],
                (IEnumerable<T4>)lists[3],
                (IEnumerable<T5>)lists[4],
                (IEnumerable<T6>)lists[5],
                (IEnumerable<T7>)lists[6],
                new Tuple<IEnumerable<T8>, IEnumerable<T9>, IEnumerable<T10>, IEnumerable<T11>>(
                    (IEnumerable<T8>)lists[7],
                    (IEnumerable<T9>)lists[8],
                    (IEnumerable<T10>)lists[9],
                    (IEnumerable<T11>)lists[10]
                )
            );
        }

        public static Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>, Tuple<IEnumerable<T8>, IEnumerable<T9>, IEnumerable<T10>, IEnumerable<T11>, IEnumerable<T12>>> QueryMultipleSP<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(string storedProcedure, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, string connectionString = null)
        {
            IEnumerable[] lists = QueryMultiple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, object, object>(12, storedProcedure, param, outParam, transaction, commandTimeout, CommandType.StoredProcedure, connectionString);
            return new Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>, Tuple<IEnumerable<T8>, IEnumerable<T9>, IEnumerable<T10>, IEnumerable<T11>, IEnumerable<T12>>>(
                (IEnumerable<T1>)lists[0],
                (IEnumerable<T2>)lists[1],
                (IEnumerable<T3>)lists[2],
                (IEnumerable<T4>)lists[3],
                (IEnumerable<T5>)lists[4],
                (IEnumerable<T6>)lists[5],
                (IEnumerable<T7>)lists[6],
                new Tuple<IEnumerable<T8>, IEnumerable<T9>, IEnumerable<T10>, IEnumerable<T11>, IEnumerable<T12>>(
                    (IEnumerable<T8>)lists[7],
                    (IEnumerable<T9>)lists[8],
                    (IEnumerable<T10>)lists[9],
                    (IEnumerable<T11>)lists[10],
                    (IEnumerable<T12>)lists[11]
                )
            );
        }

        public static Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>, Tuple<IEnumerable<T8>, IEnumerable<T9>, IEnumerable<T10>, IEnumerable<T11>, IEnumerable<T12>>> QueryMultipleSQL<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(string sql, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, string connectionString = null)
        {
            IEnumerable[] lists = QueryMultiple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, object, object>(12, sql, param, outParam, transaction, commandTimeout, CommandType.Text, connectionString);
            return new Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>, Tuple<IEnumerable<T8>, IEnumerable<T9>, IEnumerable<T10>, IEnumerable<T11>, IEnumerable<T12>>>(
                (IEnumerable<T1>)lists[0],
                (IEnumerable<T2>)lists[1],
                (IEnumerable<T3>)lists[2],
                (IEnumerable<T4>)lists[3],
                (IEnumerable<T5>)lists[4],
                (IEnumerable<T6>)lists[5],
                (IEnumerable<T7>)lists[6],
                new Tuple<IEnumerable<T8>, IEnumerable<T9>, IEnumerable<T10>, IEnumerable<T11>, IEnumerable<T12>>(
                    (IEnumerable<T8>)lists[7],
                    (IEnumerable<T9>)lists[8],
                    (IEnumerable<T10>)lists[9],
                    (IEnumerable<T11>)lists[10],
                    (IEnumerable<T12>)lists[11]
                )
            );
        }

        public static Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>, Tuple<IEnumerable<T8>, IEnumerable<T9>, IEnumerable<T10>, IEnumerable<T11>, IEnumerable<T12>, IEnumerable<T13>>> QueryMultipleSP<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(string storedProcedure, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, string connectionString = null)
        {
            IEnumerable[] lists = QueryMultiple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, object>(13, storedProcedure, param, outParam, transaction, commandTimeout, CommandType.StoredProcedure, connectionString);
            return new Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>, Tuple<IEnumerable<T8>, IEnumerable<T9>, IEnumerable<T10>, IEnumerable<T11>, IEnumerable<T12>, IEnumerable<T13>>>(
                (IEnumerable<T1>)lists[0],
                (IEnumerable<T2>)lists[1],
                (IEnumerable<T3>)lists[2],
                (IEnumerable<T4>)lists[3],
                (IEnumerable<T5>)lists[4],
                (IEnumerable<T6>)lists[5],
                (IEnumerable<T7>)lists[6],
                new Tuple<IEnumerable<T8>, IEnumerable<T9>, IEnumerable<T10>, IEnumerable<T11>, IEnumerable<T12>, IEnumerable<T13>>(
                    (IEnumerable<T8>)lists[7],
                    (IEnumerable<T9>)lists[8],
                    (IEnumerable<T10>)lists[9],
                    (IEnumerable<T11>)lists[10],
                    (IEnumerable<T12>)lists[11],
                    (IEnumerable<T13>)lists[12]
                )
            );
        }

        public static Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>, Tuple<IEnumerable<T8>, IEnumerable<T9>, IEnumerable<T10>, IEnumerable<T11>, IEnumerable<T12>, IEnumerable<T13>>> QueryMultipleSQL<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(string sql, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, string connectionString = null)
        {
            IEnumerable[] lists = QueryMultiple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, object>(13, sql, param, outParam, transaction, commandTimeout, CommandType.Text, connectionString);
            return new Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>, Tuple<IEnumerable<T8>, IEnumerable<T9>, IEnumerable<T10>, IEnumerable<T11>, IEnumerable<T12>, IEnumerable<T13>>>(
                (IEnumerable<T1>)lists[0],
                (IEnumerable<T2>)lists[1],
                (IEnumerable<T3>)lists[2],
                (IEnumerable<T4>)lists[3],
                (IEnumerable<T5>)lists[4],
                (IEnumerable<T6>)lists[5],
                (IEnumerable<T7>)lists[6],
                new Tuple<IEnumerable<T8>, IEnumerable<T9>, IEnumerable<T10>, IEnumerable<T11>, IEnumerable<T12>, IEnumerable<T13>>(
                    (IEnumerable<T8>)lists[7],
                    (IEnumerable<T9>)lists[8],
                    (IEnumerable<T10>)lists[9],
                    (IEnumerable<T11>)lists[10],
                    (IEnumerable<T12>)lists[11],
                    (IEnumerable<T13>)lists[12]
                )
            );
        }

        public static Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>, Tuple<IEnumerable<T8>, IEnumerable<T9>, IEnumerable<T10>, IEnumerable<T11>, IEnumerable<T12>, IEnumerable<T13>, IEnumerable<T14>>> QueryMultipleSP<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(string storedProcedure, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, string connectionString = null)
        {
            IEnumerable[] lists = QueryMultiple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(14, storedProcedure, param, outParam, transaction, commandTimeout, CommandType.StoredProcedure, connectionString);
            return new Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>, Tuple<IEnumerable<T8>, IEnumerable<T9>, IEnumerable<T10>, IEnumerable<T11>, IEnumerable<T12>, IEnumerable<T13>, IEnumerable<T14>>>(
                (IEnumerable<T1>)lists[0],
                (IEnumerable<T2>)lists[1],
                (IEnumerable<T3>)lists[2],
                (IEnumerable<T4>)lists[3],
                (IEnumerable<T5>)lists[4],
                (IEnumerable<T6>)lists[5],
                (IEnumerable<T7>)lists[6],
                new Tuple<IEnumerable<T8>, IEnumerable<T9>, IEnumerable<T10>, IEnumerable<T11>, IEnumerable<T12>, IEnumerable<T13>, IEnumerable<T14>>(
                    (IEnumerable<T8>)lists[7],
                    (IEnumerable<T9>)lists[8],
                    (IEnumerable<T10>)lists[9],
                    (IEnumerable<T11>)lists[10],
                    (IEnumerable<T12>)lists[11],
                    (IEnumerable<T13>)lists[12],
                    (IEnumerable<T14>)lists[13]
                )
            );
        }

        public static Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>, Tuple<IEnumerable<T8>, IEnumerable<T9>, IEnumerable<T10>, IEnumerable<T11>, IEnumerable<T12>, IEnumerable<T13>, IEnumerable<T14>>> QueryMultipleSQL<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(string sql, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, string connectionString = null)
        {
            IEnumerable[] lists = QueryMultiple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(14, sql, param, outParam, transaction, commandTimeout, CommandType.Text, connectionString);
            return new Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>, Tuple<IEnumerable<T8>, IEnumerable<T9>, IEnumerable<T10>, IEnumerable<T11>, IEnumerable<T12>, IEnumerable<T13>, IEnumerable<T14>>>(
                (IEnumerable<T1>)lists[0],
                (IEnumerable<T2>)lists[1],
                (IEnumerable<T3>)lists[2],
                (IEnumerable<T4>)lists[3],
                (IEnumerable<T5>)lists[4],
                (IEnumerable<T6>)lists[5],
                (IEnumerable<T7>)lists[6],
                new Tuple<IEnumerable<T8>, IEnumerable<T9>, IEnumerable<T10>, IEnumerable<T11>, IEnumerable<T12>, IEnumerable<T13>, IEnumerable<T14>>(
                    (IEnumerable<T8>)lists[7],
                    (IEnumerable<T9>)lists[8],
                    (IEnumerable<T10>)lists[9],
                    (IEnumerable<T11>)lists[10],
                    (IEnumerable<T12>)lists[11],
                    (IEnumerable<T13>)lists[12],
                    (IEnumerable<T14>)lists[13]
                )
            );
        }

        private static IEnumerable[] QueryMultiple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(int readCount, string sql, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null, string connectionString = null)
        {
            CombineParameters(ref param, outParam);

            using (SqlConnection connection = new SqlConnection(GetConnectionString(connectionString)))
            {
                connection.Open();
                SqlMapper.GridReader gr = connection.QueryMultiple(sql, param: (object)param, transaction: transaction, commandTimeout: GetTimeout(commandTimeout), commandType: commandType);

                IEnumerable[] lists = new IEnumerable[readCount];

                if (readCount >= 1 && !gr.IsConsumed)
                {
                    lists[0] = gr.Read<T1>();
                    if (readCount >= 2 && !gr.IsConsumed)
                    {
                        lists[1] = gr.Read<T2>();
                        if (readCount >= 3 && !gr.IsConsumed)
                        {
                            lists[2] = gr.Read<T3>();
                            if (readCount >= 4 && !gr.IsConsumed)
                            {
                                lists[3] = gr.Read<T4>();
                                if (readCount >= 5 && !gr.IsConsumed)
                                {
                                    lists[4] = gr.Read<T5>();
                                    if (readCount >= 6 && !gr.IsConsumed)
                                    {
                                        lists[5] = gr.Read<T6>();
                                        if (readCount >= 7 && !gr.IsConsumed)
                                        {
                                            lists[6] = gr.Read<T7>();
                                            if (readCount >= 8 && !gr.IsConsumed)
                                            {
                                                lists[7] = gr.Read<T8>();
                                                if (readCount >= 9 && !gr.IsConsumed)
                                                {
                                                    lists[8] = gr.Read<T9>();
                                                    if (readCount >= 10 && !gr.IsConsumed)
                                                    {
                                                        lists[9] = gr.Read<T10>();
                                                        if (readCount >= 11 && !gr.IsConsumed)
                                                        {
                                                            lists[10] = gr.Read<T11>();
                                                            if (readCount >= 12 && !gr.IsConsumed)
                                                            {
                                                                lists[11] = gr.Read<T12>();
                                                                if (readCount >= 13 && !gr.IsConsumed)
                                                                {
                                                                    lists[12] = gr.Read<T13>();
                                                                    if (readCount >= 14 && !gr.IsConsumed)
                                                                    {
                                                                        lists[13] = gr.Read<T14>();
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                return lists;
            }
        }

        #endregion

        #endregion

        #region ExecuteScalar

        public static object ExecuteScalarSP(string storedProcedure, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, string connectionString = null)
        {
            CombineParameters(ref param, outParam);

            using (SqlConnection connection = new SqlConnection(GetConnectionString(connectionString)))
            {
                connection.Open();
                return connection.ExecuteScalar(storedProcedure, param: (object)param, transaction: transaction, commandTimeout: GetTimeout(commandTimeout), commandType: CommandType.StoredProcedure);
            }
        }

        public static object ExecuteScalarSQL(string sql, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, string connectionString = null)
        {
            CombineParameters(ref param, outParam);

            using (SqlConnection connection = new SqlConnection(GetConnectionString(connectionString)))
            {
                connection.Open();
                return connection.ExecuteScalar(sql, param: (object)param, transaction: transaction, commandTimeout: GetTimeout(commandTimeout), commandType: CommandType.Text);
            }
        }

        public static T ExecuteScalarSP<T>(string storedProcedure, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, string connectionString = null)
        {
            CombineParameters(ref param, outParam);

            using (SqlConnection connection = new SqlConnection(GetConnectionString(connectionString)))
            {
                connection.Open();
                return connection.ExecuteScalar<T>(storedProcedure, param: (object)param, transaction: transaction, commandTimeout: GetTimeout(commandTimeout), commandType: CommandType.StoredProcedure);
            }
        }

        public static T ExecuteScalarSQL<T>(string sql, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, string connectionString = null)
        {
            CombineParameters(ref param, outParam);

            using (SqlConnection connection = new SqlConnection(GetConnectionString(connectionString)))
            {
                connection.Open();
                return connection.ExecuteScalar<T>(sql, param: (object)param, transaction: transaction, commandTimeout: GetTimeout(commandTimeout), commandType: CommandType.Text);
            }
        }

        #endregion

        #region Execute

        public static int ExecuteSP(string storedProcedure, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, string connectionString = null)
        {
            CombineParameters(ref param, outParam);

            using (SqlConnection connection = new SqlConnection(GetConnectionString(connectionString)))
            {
                connection.Open();
                return connection.Execute(storedProcedure, param: (object)param, transaction: transaction, commandTimeout: GetTimeout(commandTimeout), commandType: CommandType.StoredProcedure);
            }
        }

        public static int ExecuteSQL(string sql, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, string connectionString = null)
        {
            CombineParameters(ref param, outParam);

            using (SqlConnection connection = new SqlConnection(GetConnectionString(connectionString)))
            {
                connection.Open();
                return connection.Execute(sql, param: (object)param, transaction: transaction, commandTimeout: GetTimeout(commandTimeout), commandType: CommandType.Text);
            }
        }

        #endregion

        #region CombineParameters

        private static void CombineParameters(ref dynamic param, dynamic outParam = null)
        {
            if (outParam != null)
            {
                if (param != null)
                {
                    param = new DynamicParameters(param);
                    ((DynamicParameters)param).AddDynamicParams(outParam);
                }
                else
                {
                    param = outParam;
                }
            }
        }

        #endregion

        #region Connection String & Timeout

        public static string GetConnectionString(string connectionString = null)
        {
            if (string.IsNullOrEmpty(connectionString))
                return ConfigurationManager.ConnectionStrings[ConnectionStringKey].ConnectionString;
            else
                return connectionString;
        }

        public static int ConnectionTimeout { get; set; }

        public static int GetTimeout(int? commandTimeout = null)
        {
            if (commandTimeout.HasValue)
                return commandTimeout.Value;

            return ConnectionTimeout;
        }

        #endregion

        #region ToDataTable

        public static DataTable ToDataTable(this Type type, string typeName = null, MemberTypes memberTypes = MemberTypes.Field | MemberTypes.Property)
        {
            return ToDataTable(null, type, typeName, memberTypes, null);
        }

        public static DataTable ToDataTable<T>(this IEnumerable<T> instances, string typeName = null, MemberTypes memberTypes = MemberTypes.Field | MemberTypes.Property, DataTable table = null)
        {
            return ToDataTable(instances, typeof(T), typeName, memberTypes, table);
        }

        private static DataTable ToDataTable(IEnumerable instances, Type type, string typeName, MemberTypes memberTypes, DataTable table)
        {
            bool isField = ((memberTypes & MemberTypes.Field) == MemberTypes.Field);
            bool isProperty = ((memberTypes & MemberTypes.Property) == MemberTypes.Property);

            var columns =
                type.GetFields(BindingFlags.Public | BindingFlags.Instance)
                    .Where(f => isField)
                    .Select(f => new
                    {
                        ColumnName = f.Name,
                        ColumnType = f.FieldType,
                        IsField = true,
                        MemberInfo = (MemberInfo)f
                    })
                    .Union(
                        type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                            .Where(p => isProperty)
                            .Where(p => p.CanRead)
                            .Where(p => p.GetGetMethod(true).IsPublic)
                            .Where(p => p.GetIndexParameters().Length == 0)
                            .Select(p => new
                            {
                                ColumnName = p.Name,
                                ColumnType = p.PropertyType,
                                IsField = false,
                                MemberInfo = (MemberInfo)p
                            })
                    )
                    .OrderBy(c => c.MemberInfo.MetadataToken);

            if (table == null)
            {
                table = new DataTable();

                table.Columns.AddRange(
                    columns.Select(c => new DataColumn(
                        c.ColumnName,
                        (c.ColumnType.IsGenericType && c.ColumnType.GetGenericTypeDefinition() == typeof(Nullable<>) ? c.ColumnType.GetGenericArguments()[0] : c.ColumnType)
                    )).ToArray()
                );
            }

            if (instances != null)
            {
                table.BeginLoadData();

                try
                {
                    foreach (var instance in instances)
                    {
                        if (instance != null)
                        {
                            DataRow row = table.NewRow();

                            foreach (var column in columns)
                                row[column.ColumnName] = (column.IsField ? ((FieldInfo)column.MemberInfo).GetValue(instance) : ((PropertyInfo)column.MemberInfo).GetValue(instance, null)) ?? DBNull.Value;

                            table.Rows.Add(row);
                        }
                    }
                }
                finally
                {
                    table.EndLoadData();
                }
            }

            table.SetTypeName(typeName);

            return table;
        }

        public static void SetOrdinal(this DataTable table, params string[] columnNames)
        {
            if (table == null || columnNames == null || columnNames.Length == 0)
                return;

            int index = 0;
            foreach (string columnName in columnNames)
                table.Columns[columnName].SetOrdinal(index++);
        }

        #endregion

        #region ToDataSet

        public static DataSet ToDataSet(this IEnumerable<IEnumerable<dynamic>> instances, string[] typeNames = null, MemberTypes memberTypes = MemberTypes.Field | MemberTypes.Property, DataSet dataSet = null)
        {
            if (instances == null)
                return null;

            if (dataSet == null)
                dataSet = new DataSet();

            int index = 0;
            foreach (IEnumerable<dynamic> instance in instances)
            {
                if (instance != null)
                    dataSet.Tables.Add(instance.ToDataTable(typeName: (typeNames != null && typeNames.Length > index ? typeNames[index] : null), memberTypes: memberTypes));
                index++;
            }

            return dataSet;
        }

        public static DataSet ToDataSet<T>(this IEnumerable<IEnumerable<T>> instances, string[] typeNames = null, MemberTypes memberTypes = MemberTypes.Field | MemberTypes.Property, DataSet dataSet = null)
        {
            if (instances == null)
                return null;

            if (dataSet == null)
                dataSet = new DataSet();

            int index = 0;
            foreach (IEnumerable<T> instance in instances)
            {
                dataSet.Tables.Add(ToDataTable(instance, typeof(T), (typeNames != null && typeNames.Length > index ? typeNames[index] : null), memberTypes, null));
                index++;
            }

            return dataSet;
        }

        public static DataSet ToDataSet<T1, T2>(this Tuple<IEnumerable<T1>, IEnumerable<T2>> instances, string[] typeNames = null, MemberTypes memberTypes = MemberTypes.Field | MemberTypes.Property, DataSet dataSet = null)
        {
            if (instances == null)
                return null;

            if (dataSet == null)
                dataSet = new DataSet();

            dataSet.Tables.Add(ToDataTable(instances.Item1, typeof(T1), (typeNames != null && typeNames.Length > 0 ? typeNames[0] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Item2, typeof(T2), (typeNames != null && typeNames.Length > 1 ? typeNames[1] : null), memberTypes, null));

            return dataSet;
        }

        public static DataSet ToDataSet<T1, T2, T3>(this Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>> instances, string[] typeNames = null, MemberTypes memberTypes = MemberTypes.Field | MemberTypes.Property, DataSet dataSet = null)
        {
            if (instances == null)
                return null;

            if (dataSet == null)
                dataSet = new DataSet();

            dataSet.Tables.Add(ToDataTable(instances.Item1, typeof(T1), (typeNames != null && typeNames.Length > 0 ? typeNames[0] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Item2, typeof(T2), (typeNames != null && typeNames.Length > 1 ? typeNames[1] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Item3, typeof(T3), (typeNames != null && typeNames.Length > 2 ? typeNames[2] : null), memberTypes, null));

            return dataSet;
        }

        public static DataSet ToDataSet<T1, T2, T3, T4>(this Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>> instances, string[] typeNames = null, MemberTypes memberTypes = MemberTypes.Field | MemberTypes.Property, DataSet dataSet = null)
        {
            if (instances == null)
                return null;

            if (dataSet == null)
                dataSet = new DataSet();

            dataSet.Tables.Add(ToDataTable(instances.Item1, typeof(T1), (typeNames != null && typeNames.Length > 0 ? typeNames[0] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Item2, typeof(T2), (typeNames != null && typeNames.Length > 1 ? typeNames[1] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Item3, typeof(T3), (typeNames != null && typeNames.Length > 2 ? typeNames[2] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Item4, typeof(T4), (typeNames != null && typeNames.Length > 3 ? typeNames[3] : null), memberTypes, null));

            return dataSet;
        }

        public static DataSet ToDataSet<T1, T2, T3, T4, T5>(this Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>> instances, string[] typeNames = null, MemberTypes memberTypes = MemberTypes.Field | MemberTypes.Property, DataSet dataSet = null)
        {
            if (instances == null)
                return null;

            if (dataSet == null)
                dataSet = new DataSet();

            dataSet.Tables.Add(ToDataTable(instances.Item1, typeof(T1), (typeNames != null && typeNames.Length > 0 ? typeNames[0] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Item2, typeof(T2), (typeNames != null && typeNames.Length > 1 ? typeNames[1] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Item3, typeof(T3), (typeNames != null && typeNames.Length > 2 ? typeNames[2] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Item4, typeof(T4), (typeNames != null && typeNames.Length > 3 ? typeNames[3] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Item5, typeof(T5), (typeNames != null && typeNames.Length > 4 ? typeNames[4] : null), memberTypes, null));

            return dataSet;
        }

        public static DataSet ToDataSet<T1, T2, T3, T4, T5, T6>(this Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>> instances, string[] typeNames = null, MemberTypes memberTypes = MemberTypes.Field | MemberTypes.Property, DataSet dataSet = null)
        {
            if (instances == null)
                return null;

            if (dataSet == null)
                dataSet = new DataSet();

            dataSet.Tables.Add(ToDataTable(instances.Item1, typeof(T1), (typeNames != null && typeNames.Length > 0 ? typeNames[0] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Item2, typeof(T2), (typeNames != null && typeNames.Length > 1 ? typeNames[1] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Item3, typeof(T3), (typeNames != null && typeNames.Length > 2 ? typeNames[2] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Item4, typeof(T4), (typeNames != null && typeNames.Length > 3 ? typeNames[3] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Item5, typeof(T5), (typeNames != null && typeNames.Length > 4 ? typeNames[4] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Item6, typeof(T6), (typeNames != null && typeNames.Length > 5 ? typeNames[5] : null), memberTypes, null));

            return dataSet;
        }

        public static DataSet ToDataSet<T1, T2, T3, T4, T5, T6, T7>(this Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>> instances, string[] typeNames = null, MemberTypes memberTypes = MemberTypes.Field | MemberTypes.Property, DataSet dataSet = null)
        {
            if (instances == null)
                return null;

            if (dataSet == null)
                dataSet = new DataSet();

            dataSet.Tables.Add(ToDataTable(instances.Item1, typeof(T1), (typeNames != null && typeNames.Length > 0 ? typeNames[0] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Item2, typeof(T2), (typeNames != null && typeNames.Length > 1 ? typeNames[1] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Item3, typeof(T3), (typeNames != null && typeNames.Length > 2 ? typeNames[2] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Item4, typeof(T4), (typeNames != null && typeNames.Length > 3 ? typeNames[3] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Item5, typeof(T5), (typeNames != null && typeNames.Length > 4 ? typeNames[4] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Item6, typeof(T6), (typeNames != null && typeNames.Length > 5 ? typeNames[5] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Item7, typeof(T7), (typeNames != null && typeNames.Length > 6 ? typeNames[6] : null), memberTypes, null));

            return dataSet;
        }

        public static DataSet ToDataSet<T1, T2, T3, T4, T5, T6, T7, T8>(this Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>, Tuple<IEnumerable<T8>>> instances, string[] typeNames = null, MemberTypes memberTypes = MemberTypes.Field | MemberTypes.Property, DataSet dataSet = null)
        {
            if (instances == null)
                return null;

            if (dataSet == null)
                dataSet = new DataSet();

            dataSet.Tables.Add(ToDataTable(instances.Item1, typeof(T1), (typeNames != null && typeNames.Length > 0 ? typeNames[0] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Item2, typeof(T2), (typeNames != null && typeNames.Length > 1 ? typeNames[1] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Item3, typeof(T3), (typeNames != null && typeNames.Length > 2 ? typeNames[2] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Item4, typeof(T4), (typeNames != null && typeNames.Length > 3 ? typeNames[3] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Item5, typeof(T5), (typeNames != null && typeNames.Length > 4 ? typeNames[4] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Item6, typeof(T6), (typeNames != null && typeNames.Length > 5 ? typeNames[5] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Item7, typeof(T7), (typeNames != null && typeNames.Length > 6 ? typeNames[6] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Rest.Item1, typeof(T8), (typeNames != null && typeNames.Length > 7 ? typeNames[7] : null), memberTypes, null));

            return dataSet;
        }

        public static DataSet ToDataSet<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>, Tuple<IEnumerable<T8>, IEnumerable<T9>>> instances, string[] typeNames = null, MemberTypes memberTypes = MemberTypes.Field | MemberTypes.Property, DataSet dataSet = null)
        {
            if (instances == null)
                return null;

            if (dataSet == null)
                dataSet = new DataSet();

            dataSet.Tables.Add(ToDataTable(instances.Item1, typeof(T1), (typeNames != null && typeNames.Length > 0 ? typeNames[0] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Item2, typeof(T2), (typeNames != null && typeNames.Length > 1 ? typeNames[1] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Item3, typeof(T3), (typeNames != null && typeNames.Length > 2 ? typeNames[2] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Item4, typeof(T4), (typeNames != null && typeNames.Length > 3 ? typeNames[3] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Item5, typeof(T5), (typeNames != null && typeNames.Length > 4 ? typeNames[4] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Item6, typeof(T6), (typeNames != null && typeNames.Length > 5 ? typeNames[5] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Item7, typeof(T7), (typeNames != null && typeNames.Length > 6 ? typeNames[6] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Rest.Item1, typeof(T8), (typeNames != null && typeNames.Length > 7 ? typeNames[7] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Rest.Item2, typeof(T9), (typeNames != null && typeNames.Length > 8 ? typeNames[8] : null), memberTypes, null));

            return dataSet;
        }

        public static DataSet ToDataSet<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>, Tuple<IEnumerable<T8>, IEnumerable<T9>, IEnumerable<T10>>> instances, string[] typeNames = null, MemberTypes memberTypes = MemberTypes.Field | MemberTypes.Property, DataSet dataSet = null)
        {
            if (instances == null)
                return null;

            if (dataSet == null)
                dataSet = new DataSet();

            dataSet.Tables.Add(ToDataTable(instances.Item1, typeof(T1), (typeNames != null && typeNames.Length > 0 ? typeNames[0] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Item2, typeof(T2), (typeNames != null && typeNames.Length > 1 ? typeNames[1] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Item3, typeof(T3), (typeNames != null && typeNames.Length > 2 ? typeNames[2] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Item4, typeof(T4), (typeNames != null && typeNames.Length > 3 ? typeNames[3] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Item5, typeof(T5), (typeNames != null && typeNames.Length > 4 ? typeNames[4] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Item6, typeof(T6), (typeNames != null && typeNames.Length > 5 ? typeNames[5] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Item7, typeof(T7), (typeNames != null && typeNames.Length > 6 ? typeNames[6] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Rest.Item1, typeof(T8), (typeNames != null && typeNames.Length > 7 ? typeNames[7] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Rest.Item2, typeof(T9), (typeNames != null && typeNames.Length > 8 ? typeNames[8] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Rest.Item3, typeof(T10), (typeNames != null && typeNames.Length > 9 ? typeNames[9] : null), memberTypes, null));

            return dataSet;
        }

        public static DataSet ToDataSet<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(this Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>, Tuple<IEnumerable<T8>, IEnumerable<T9>, IEnumerable<T10>, IEnumerable<T11>>> instances, string[] typeNames = null, MemberTypes memberTypes = MemberTypes.Field | MemberTypes.Property, DataSet dataSet = null)
        {
            if (instances == null)
                return null;

            if (dataSet == null)
                dataSet = new DataSet();

            dataSet.Tables.Add(ToDataTable(instances.Item1, typeof(T1), (typeNames != null && typeNames.Length > 0 ? typeNames[0] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Item2, typeof(T2), (typeNames != null && typeNames.Length > 1 ? typeNames[1] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Item3, typeof(T3), (typeNames != null && typeNames.Length > 2 ? typeNames[2] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Item4, typeof(T4), (typeNames != null && typeNames.Length > 3 ? typeNames[3] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Item5, typeof(T5), (typeNames != null && typeNames.Length > 4 ? typeNames[4] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Item6, typeof(T6), (typeNames != null && typeNames.Length > 5 ? typeNames[5] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Item7, typeof(T7), (typeNames != null && typeNames.Length > 6 ? typeNames[6] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Rest.Item1, typeof(T8), (typeNames != null && typeNames.Length > 7 ? typeNames[7] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Rest.Item2, typeof(T9), (typeNames != null && typeNames.Length > 8 ? typeNames[8] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Rest.Item3, typeof(T10), (typeNames != null && typeNames.Length > 9 ? typeNames[9] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Rest.Item4, typeof(T11), (typeNames != null && typeNames.Length > 10 ? typeNames[10] : null), memberTypes, null));

            return dataSet;
        }

        public static DataSet ToDataSet<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(this Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>, Tuple<IEnumerable<T8>, IEnumerable<T9>, IEnumerable<T10>, IEnumerable<T11>, IEnumerable<T12>>> instances, string[] typeNames = null, MemberTypes memberTypes = MemberTypes.Field | MemberTypes.Property, DataSet dataSet = null)
        {
            if (instances == null)
                return null;

            if (dataSet == null)
                dataSet = new DataSet();

            dataSet.Tables.Add(ToDataTable(instances.Item1, typeof(T1), (typeNames != null && typeNames.Length > 0 ? typeNames[0] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Item2, typeof(T2), (typeNames != null && typeNames.Length > 1 ? typeNames[1] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Item3, typeof(T3), (typeNames != null && typeNames.Length > 2 ? typeNames[2] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Item4, typeof(T4), (typeNames != null && typeNames.Length > 3 ? typeNames[3] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Item5, typeof(T5), (typeNames != null && typeNames.Length > 4 ? typeNames[4] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Item6, typeof(T6), (typeNames != null && typeNames.Length > 5 ? typeNames[5] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Item7, typeof(T7), (typeNames != null && typeNames.Length > 6 ? typeNames[6] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Rest.Item1, typeof(T8), (typeNames != null && typeNames.Length > 7 ? typeNames[7] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Rest.Item2, typeof(T9), (typeNames != null && typeNames.Length > 8 ? typeNames[8] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Rest.Item3, typeof(T10), (typeNames != null && typeNames.Length > 9 ? typeNames[9] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Rest.Item4, typeof(T11), (typeNames != null && typeNames.Length > 10 ? typeNames[10] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Rest.Item5, typeof(T12), (typeNames != null && typeNames.Length > 11 ? typeNames[11] : null), memberTypes, null));

            return dataSet;
        }

        public static DataSet ToDataSet<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(this Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>, Tuple<IEnumerable<T8>, IEnumerable<T9>, IEnumerable<T10>, IEnumerable<T11>, IEnumerable<T12>, IEnumerable<T13>>> instances, string[] typeNames = null, MemberTypes memberTypes = MemberTypes.Field | MemberTypes.Property, DataSet dataSet = null)
        {
            if (instances == null)
                return null;

            if (dataSet == null)
                dataSet = new DataSet();

            dataSet.Tables.Add(ToDataTable(instances.Item1, typeof(T1), (typeNames != null && typeNames.Length > 0 ? typeNames[0] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Item2, typeof(T2), (typeNames != null && typeNames.Length > 1 ? typeNames[1] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Item3, typeof(T3), (typeNames != null && typeNames.Length > 2 ? typeNames[2] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Item4, typeof(T4), (typeNames != null && typeNames.Length > 3 ? typeNames[3] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Item5, typeof(T5), (typeNames != null && typeNames.Length > 4 ? typeNames[4] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Item6, typeof(T6), (typeNames != null && typeNames.Length > 5 ? typeNames[5] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Item7, typeof(T7), (typeNames != null && typeNames.Length > 6 ? typeNames[6] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Rest.Item1, typeof(T8), (typeNames != null && typeNames.Length > 7 ? typeNames[7] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Rest.Item2, typeof(T9), (typeNames != null && typeNames.Length > 8 ? typeNames[8] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Rest.Item3, typeof(T10), (typeNames != null && typeNames.Length > 9 ? typeNames[9] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Rest.Item4, typeof(T11), (typeNames != null && typeNames.Length > 10 ? typeNames[10] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Rest.Item5, typeof(T12), (typeNames != null && typeNames.Length > 11 ? typeNames[11] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Rest.Item6, typeof(T13), (typeNames != null && typeNames.Length > 12 ? typeNames[12] : null), memberTypes, null));

            return dataSet;
        }

        public static DataSet ToDataSet<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(this Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>, Tuple<IEnumerable<T8>, IEnumerable<T9>, IEnumerable<T10>, IEnumerable<T11>, IEnumerable<T12>, IEnumerable<T13>, IEnumerable<T14>>> instances, string[] typeNames = null, MemberTypes memberTypes = MemberTypes.Field | MemberTypes.Property, DataSet dataSet = null)
        {
            if (instances == null)
                return null;

            if (dataSet == null)
                dataSet = new DataSet();

            dataSet.Tables.Add(ToDataTable(instances.Item1, typeof(T1), (typeNames != null && typeNames.Length > 0 ? typeNames[0] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Item2, typeof(T2), (typeNames != null && typeNames.Length > 1 ? typeNames[1] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Item3, typeof(T3), (typeNames != null && typeNames.Length > 2 ? typeNames[2] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Item4, typeof(T4), (typeNames != null && typeNames.Length > 3 ? typeNames[3] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Item5, typeof(T5), (typeNames != null && typeNames.Length > 4 ? typeNames[4] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Item6, typeof(T6), (typeNames != null && typeNames.Length > 5 ? typeNames[5] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Item7, typeof(T7), (typeNames != null && typeNames.Length > 6 ? typeNames[6] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Rest.Item1, typeof(T8), (typeNames != null && typeNames.Length > 7 ? typeNames[7] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Rest.Item2, typeof(T9), (typeNames != null && typeNames.Length > 8 ? typeNames[8] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Rest.Item3, typeof(T10), (typeNames != null && typeNames.Length > 9 ? typeNames[9] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Rest.Item4, typeof(T11), (typeNames != null && typeNames.Length > 10 ? typeNames[10] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Rest.Item5, typeof(T12), (typeNames != null && typeNames.Length > 11 ? typeNames[11] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Rest.Item6, typeof(T13), (typeNames != null && typeNames.Length > 12 ? typeNames[12] : null), memberTypes, null));
            dataSet.Tables.Add(ToDataTable(instances.Rest.Item7, typeof(T14), (typeNames != null && typeNames.Length > 13 ? typeNames[13] : null), memberTypes, null));

            return dataSet;
        }

        #endregion

        #region ToEnumerable

        #region DataTable

        public static IEnumerable<T> Cast<T>(this DataTable table, Func<string, Type, object, object> valueHandler = null) where T : new()
        {
            return ToEnumerable<T>(table, delegate() { return new T(); }, valueHandler);
        }

        public static IEnumerable<T> Cast<T>(this DataTable table, Func<T> instanceHandler, Func<string, Type, object, object> valueHandler = null)
        {
            return ToEnumerable<T>(table, instanceHandler, valueHandler);
        }

        public static T[] ToArray<T>(this DataTable table, Func<string, Type, object, object> valueHandler = null) where T : new()
        {
            return ToEnumerable<T>(table, delegate() { return new T(); }, valueHandler);
        }

        public static T[] ToArray<T>(this DataTable table, Func<T> instanceHandler, Func<string, Type, object, object> valueHandler = null)
        {
            return ToEnumerable<T>(table, instanceHandler, valueHandler);
        }

        public static List<T> ToList<T>(this DataTable table, Func<string, Type, object, object> valueHandler = null) where T : new()
        {
            return ToEnumerable<T>(table, delegate() { return new T(); }, valueHandler).ToList<T>();
        }

        public static List<T> ToList<T>(this DataTable table, Func<T> instanceHandler, Func<string, Type, object, object> valueHandler = null)
        {
            return ToEnumerable<T>(table, instanceHandler, valueHandler).ToList<T>();
        }

        private static T[] ToEnumerable<T>(DataTable table, Func<T> instanceHandler, Func<string, Type, object, object> valueHandler)
        {
            if (table == null)
                return null;

            if (table.Rows.Count == 0)
                return new T[0];

            Type type = typeof(T);

            var columns =
                type.GetFields(BindingFlags.Public | BindingFlags.Instance)
                    .Select(f => new
                    {
                        ColumnName = f.Name,
                        ColumnType = f.FieldType,
                        IsField = true,
                        MemberInfo = (MemberInfo)f
                    })
                    .Union(
                        type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                            .Where(p => p.CanWrite)
                            .Where(p => p.CanRead)
                            .Where(p => p.GetGetMethod(true).IsPublic)
                            .Where(p => p.GetIndexParameters().Length == 0)
                            .Select(p => new
                            {
                                ColumnName = p.Name,
                                ColumnType = p.PropertyType,
                                IsField = false,
                                MemberInfo = (MemberInfo)p
                            })
                    )
                    .Where(c => table.Columns.Contains(c.ColumnName)); // columns exist

            T[] instances = new T[table.Rows.Count];

            int index = 0;
            foreach (DataRow row in table.Rows)
            {
                T instance = instanceHandler();

                foreach (var column in columns)
                {
                    object value = row[column.ColumnName];
                    if (valueHandler != null)
                        value = valueHandler(column.ColumnName, column.ColumnType, value);

                    if (value is DBNull)
                    {
                        value = null;
                    }
                    else if (value != null && column.ColumnType != typeof(System.Type))
                    {
                        if (value.GetType() != column.ColumnType)
                        {
                            if (column.ColumnType.IsGenericType && column.ColumnType.GetGenericTypeDefinition() == typeof(Nullable<>))
                            {
                                if (value.GetType() != Nullable.GetUnderlyingType(column.ColumnType))
                                    value = Convert.ChangeType(value, Nullable.GetUnderlyingType(column.ColumnType));
                            }
                            else
                            {
                                value = Convert.ChangeType(value, column.ColumnType);
                            }
                        }
                    }

                    if (column.IsField)
                        ((FieldInfo)column.MemberInfo).SetValue(instance, value);
                    else
                        ((PropertyInfo)column.MemberInfo).SetValue(instance, value, null);
                }

                instances[index++] = instance;
            }

            return instances;
        }

        #endregion

        #region DataView

        public static IEnumerable<T> Cast<T>(this DataView view, Func<string, Type, object, object> valueHandler = null) where T : new()
        {
            return ToEnumerable<T>(view, delegate() { return new T(); }, valueHandler);
        }

        public static IEnumerable<T> Cast<T>(this DataView view, Func<T> instanceHandler, Func<string, Type, object, object> valueHandler = null)
        {
            return ToEnumerable<T>(view, instanceHandler, valueHandler);
        }

        public static T[] ToArray<T>(this DataView view, Func<string, Type, object, object> valueHandler = null) where T : new()
        {
            return ToEnumerable<T>(view, delegate() { return new T(); }, valueHandler);
        }

        public static T[] ToArray<T>(this DataView view, Func<T> instanceHandler, Func<string, Type, object, object> valueHandler = null)
        {
            return ToEnumerable<T>(view, instanceHandler, valueHandler);
        }

        public static List<T> ToList<T>(this DataView view, Func<string, Type, object, object> valueHandler = null) where T : new()
        {
            return ToEnumerable<T>(view, delegate() { return new T(); }, valueHandler).ToList<T>();
        }

        public static List<T> ToList<T>(this DataView view, Func<T> instanceHandler, Func<string, Type, object, object> valueHandler = null)
        {
            return ToEnumerable<T>(view, instanceHandler, valueHandler).ToList<T>();
        }

        private static T[] ToEnumerable<T>(DataView view, Func<T> instanceHandler, Func<string, Type, object, object> valueHandler)
        {
            if (view == null)
                return null;

            if (view.Count == 0)
                return new T[0];

            Type type = typeof(T);

            var columns =
                type.GetFields(BindingFlags.Public | BindingFlags.Instance)
                    .Select(f => new
                    {
                        ColumnName = f.Name,
                        ColumnType = f.FieldType,
                        IsField = true,
                        MemberInfo = (MemberInfo)f
                    })
                    .Union(
                        type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                            .Where(p => p.CanWrite)
                            .Where(p => p.CanRead)
                            .Where(p => p.GetGetMethod(true).IsPublic)
                            .Where(p => p.GetIndexParameters().Length == 0)
                            .Select(p => new
                            {
                                ColumnName = p.Name,
                                ColumnType = p.PropertyType,
                                IsField = false,
                                MemberInfo = (MemberInfo)p
                            })
                    )
                    .Where(c => view.Table.Columns.Contains(c.ColumnName)); // columns exist

            T[] instances = new T[view.Count];

            int index = 0;
            foreach (DataRowView row in view)
            {
                T instance = instanceHandler();

                foreach (var column in columns)
                {
                    object value = row[column.ColumnName];
                    if (valueHandler != null)
                        value = valueHandler(column.ColumnName, column.ColumnType, value);

                    if (value is DBNull)
                    {
                        value = null;
                    }
                    else if (value != null && column.ColumnType != typeof(System.Type))
                    {
                        if (value.GetType() != column.ColumnType)
                        {
                            if (column.ColumnType.IsGenericType && column.ColumnType.GetGenericTypeDefinition() == typeof(Nullable<>))
                            {
                                if (value.GetType() != Nullable.GetUnderlyingType(column.ColumnType))
                                    value = Convert.ChangeType(value, Nullable.GetUnderlyingType(column.ColumnType));
                            }
                            else
                            {
                                value = Convert.ChangeType(value, column.ColumnType);
                            }
                        }
                    }

                    if (column.IsField)
                        ((FieldInfo)column.MemberInfo).SetValue(instance, value);
                    else
                        ((PropertyInfo)column.MemberInfo).SetValue(instance, value, null);
                }

                instances[index++] = instance;
            }

            return instances;
        }

        #endregion

        #endregion
    }

    #endregion

    #region DynamicParameters

    partial class DynamicParameters
    {
        // http://msdn.microsoft.com/en-us/library/cc716729(v=vs.100).aspx
        static readonly Dictionary<SqlDbType, DbType?> sqlDbTypeMap = new Dictionary<SqlDbType, DbType?>
        {
            {SqlDbType.BigInt, DbType.Int64},
            {SqlDbType.Binary, DbType.Binary},
            {SqlDbType.Bit, DbType.Boolean},
            {SqlDbType.Char, DbType.AnsiStringFixedLength},
            {SqlDbType.DateTime, DbType.DateTime},
            {SqlDbType.Decimal, DbType.Decimal},
            {SqlDbType.Float, DbType.Double},
            {SqlDbType.Image, DbType.Binary},
            {SqlDbType.Int, DbType.Int32},
            {SqlDbType.Money, DbType.Decimal},
            {SqlDbType.NChar, DbType.StringFixedLength},
            {SqlDbType.NText, DbType.String},
            {SqlDbType.NVarChar, DbType.String},
            {SqlDbType.Real, DbType.Single},
            {SqlDbType.UniqueIdentifier, DbType.Guid},
            {SqlDbType.SmallDateTime, DbType.DateTime},
            {SqlDbType.SmallInt, DbType.Int16},
            {SqlDbType.SmallMoney, DbType.Decimal},
            {SqlDbType.Text, DbType.String},
            {SqlDbType.Timestamp, DbType.Binary},
            {SqlDbType.TinyInt, DbType.Byte},
            {SqlDbType.VarBinary, DbType.Binary},
            {SqlDbType.VarChar, DbType.AnsiString},
            {SqlDbType.Variant, DbType.Object},
            {SqlDbType.Xml, DbType.Xml},
            {SqlDbType.Udt,(DbType?)null}, // Dapper will take care of it
            {SqlDbType.Structured,(DbType?)null}, // Dapper will take care of it
            {SqlDbType.Date, DbType.Date},
            {SqlDbType.Time, DbType.Time},
            {SqlDbType.DateTime2, DbType.DateTime2},
            {SqlDbType.DateTimeOffset, DbType.DateTimeOffset}
        };

        public DynamicParameters(string name, object value = null, DbType? dbType = null, ParameterDirection? direction = null, int? size = null, byte? precision = null, byte? scale = null)
            : this()
        {
            Add(name, value, dbType, direction, size, precision, scale);
        }

        public DynamicParameters(string name, object value = null, SqlDbType? sqlDbType = null, ParameterDirection? direction = null, int? size = null, byte? precision = null, byte? scale = null)
            : this()
        {
            Add(name, value, sqlDbType, direction, size, precision, scale);
        }

        public void Add(string name, object value = null, SqlDbType? sqlDbType = null, ParameterDirection? direction = null, int? size = null, byte? precision = null, byte? scale = null)
        {
            Add(name, value, (sqlDbType != null ? sqlDbTypeMap[sqlDbType.Value] : (DbType?)null), direction, size, precision, scale);
        }

        public Dictionary<string, object> Get()
        {
            return Get<object>();
        }

        // all the parameters are of the same type T
        public Dictionary<string, T> Get<T>()
        {
            Dictionary<string, T> values = new Dictionary<string, T>();
            foreach (string parameterName in ParameterNames)
                values.Add(parameterName, Get<T>(parameterName));
            return values;
        }
    }

    #endregion
}