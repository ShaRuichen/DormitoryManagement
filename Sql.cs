﻿using System.Collections.Generic;
using System.Data;

using MySql.Data.MySqlClient;

namespace DormitoryManagement
{
    public static class Sql
    {
        /// <exception cref="MySqlException" />
        public static void Execute(string sql, params object[] args)
        {
            var command = CreateCommand(sql, args);
            using var transaction = command.Connection.BeginTransaction();
            try
            {
                command.ExecuteNonQuery();
            }
            catch (MySqlException)
            {
                transaction.Rollback();
                throw;
            }
            transaction.Commit();
            ReleaseCommand(command);
        }

        /// <exception cref="MySqlException" />
        public static DataRowCollection Read(string sql, params object[] args)
        {
            var command = CreateCommand(sql, args);
            using var reader = new MySqlDataAdapter(command);
            using var table = new DataTable();
            _ = reader.Fill(table);
            ReleaseCommand(command);
            return table.Rows;
        }

        private const int MinConnections = 5;
        private const int MaxConnections = 10;

        private static readonly List<MySqlConnection> freeConnections;
        private static readonly LinkedList<MySqlCommand> waitingCommands;
        private static int connectionsCount;

        static Sql()
        {
            freeConnections = new List<MySqlConnection>(MinConnections);
            waitingCommands = new LinkedList<MySqlCommand>();

            for (var i = 0; i < MinConnections; i++)
            {
                var connection = CreateConnection();
                freeConnections.Add(connection);
            }
        }

        private static MySqlConnection CreateConnection()
        {
            const string ConnectionString = "server=localhost;database=dormitory_management;uid=root;pwd=123456";
            var connection = new MySqlConnection(ConnectionString);
            try
            {
                connection.Open();
            }
            catch (MySqlException)
            {
                connection.Close();
                connection.Dispose();
                throw;
            }
            connectionsCount++;
            return connection;
        }

        private static MySqlCommand CreateCommand(string sql, params object[] args)
        {
            var command = new MySqlCommand
            {
                CommandText = sql,
            };
            for (var i = 0; i < args.Length; i++)
            {
                var name = $"@{i}";
                var value = args[i];
                var parameter = new MySqlParameter(name, value);
                var parameters = command.Parameters;
                parameters.Add(parameter);
            }
            lock (freeConnections)
            {
                if (freeConnections.Count > 0)
                {
                    var index = freeConnections.Count - 1;
                    command.Connection = freeConnections[index];
                    freeConnections.RemoveAt(index);
                }
                else
                {
                    if (connectionsCount < MaxConnections)
                    {
                        command.Connection = CreateConnection();
                    }
                    else
                    {
                        waitingCommands.AddLast(command);
                    }
                }
            }
            while (command.Connection == null) ;
            return command;
        }

        private static void ReleaseCommand(MySqlCommand command)
        {
            var connection = command.Connection;
            command.Dispose();
            lock (freeConnections)
            {
                if (freeConnections.Count < MinConnections)
                {
                    if (waitingCommands.Count > 0)
                    {
                        var waitingCommand = waitingCommands.First.Value;
                        waitingCommands.RemoveFirst();
                        waitingCommand.Connection = connection;
                    }
                    else
                    {
                        freeConnections.Add(connection);
                    }
                }
                else
                {
                    connection.Dispose();
                    connectionsCount--;
                }
            }
        }
    }
}
