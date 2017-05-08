using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.OracleClient;
using Spring.Net.Example.Dao.Config;

namespace Spring.Net.Example.Dao.Database
{
	/// <summary>
	/// A helper class used to execute queries against an Oracle database
	/// </summary>
	public abstract class OracleHelper
	{
		//数据库连接字符串(web.config来配置)，可以动态更改connectionString支持多数据库.		
		public static string connectionString = DBConfig.connString;

		/// <summary>
		/// Execute a database query which does not include a select
		/// </summary>
		/// <param name="connString">Connection string to database</param>
		/// <param name="cmdType">Command type either stored procedure or SQL</param>
		/// <param name="cmdText">Acutall SQL Command</param>
		/// <param name="commandParameters">Parameters to bind to the command</param>
		/// <returns></returns>
		public static int ExecuteNonQuery(CommandType cmdType, string cmdText, params OracleParameter[] commandParameters)
		{
			// Create a new Oracle command
			OracleCommand cmd = new OracleCommand();

			//Create a connection
			using (OracleConnection connection = new OracleConnection(connectionString))
			{

				//Prepare the command
				PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);

				//Execute the command
				int val = cmd.ExecuteNonQuery();
				connection.Close();
				cmd.Parameters.Clear();
				return val;
			}
		}
		/// <summary>
		/// 执行查询语句，返回DataSet
		/// </summary>
		/// <param name="SQLString">查询语句</param>
		/// <returns>DataSet</returns>
		public static DataSet Query(string SQLString)
		{
			using (OracleConnection connection = new OracleConnection(connectionString))
			{
				DataSet ds = new DataSet();
				try
				{
					connection.Open();
					OracleDataAdapter command = new OracleDataAdapter(SQLString, connection);
					command.Fill(ds, "ds");
				}
				catch (OracleException ex)
				{
					throw new Exception(ex.Message);
				}
				finally
				{
					if (connection.State != ConnectionState.Closed)
					{
						connection.Close();
					}
				}
				return ds;
			}
		}

		public static DataSet Query(string SQLString, params OracleParameter[] cmdParms)
		{
			using (OracleConnection connection = new OracleConnection(connectionString))
			{
				OracleCommand cmd = new OracleCommand();
				PrepareCommand(cmd, connection, null, SQLString, cmdParms);
				using (OracleDataAdapter da = new OracleDataAdapter(cmd))
				{
					DataSet ds = new DataSet();
					try
					{
						da.Fill(ds, "ds");
						cmd.Parameters.Clear();
					}
					catch (System.Data.OracleClient.OracleException ex)
					{
						throw new Exception(ex.Message);
					}
					finally
					{
						if (connection.State != ConnectionState.Closed)
						{
							connection.Close();
						}
					}
					return ds;
				}
			}
		}

		public static DataSet Query(CommandType cmdType, string SQLString, params OracleParameter[] cmdParms)
		{
			using (OracleConnection connection = new OracleConnection(connectionString))
			{
				OracleCommand cmd = new OracleCommand();
				PrepareCommand(cmd, connection, null, cmdType, SQLString, cmdParms);
				using (OracleDataAdapter da = new OracleDataAdapter(cmd))
				{
					DataSet ds = new DataSet();
					try
					{
						da.Fill(ds, "ds");
						cmd.Parameters.Clear();
					}
					catch (System.Data.OracleClient.OracleException ex)
					{
						throw new Exception(ex.Message);
					}
					finally
					{
						if (connection.State != ConnectionState.Closed)
						{
							connection.Close();
						}
					}
					return ds;
				}
			}
		}

		private static void PrepareCommand(OracleCommand cmd, OracleConnection conn, OracleTransaction trans, string cmdText, OracleParameter[] cmdParms)
		{
			if (conn.State != ConnectionState.Open)
				conn.Open();
			cmd.Connection = conn;
			cmd.CommandText = cmdText;
			if (trans != null)
				cmd.Transaction = trans;
			cmd.CommandType = CommandType.Text;//cmdType;
			if (cmdParms != null)
			{
				foreach (OracleParameter parameter in cmdParms)
				{
					if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
						(parameter.Value == null))
					{
						parameter.Value = DBNull.Value;
					}
					cmd.Parameters.Add(parameter);
				}
			}
		}

		/// <summary>
		/// 执行一条计算查询结果语句，返回查询结果（object）。
		/// </summary>
		/// <param name="SQLString">计算查询结果语句</param>
		/// <returns>查询结果（object）</returns>
		public static object GetSingle(string SQLString)
		{
			using (OracleConnection connection = new OracleConnection(connectionString))
			{
				using (OracleCommand cmd = new OracleCommand(SQLString, connection))
				{
					try
					{
						connection.Open();
						object obj = cmd.ExecuteScalar();
						if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
						{
							return null;
						}
						else
						{
							return obj;
						}
					}
					catch (OracleException ex)
					{
						throw new Exception(ex.Message);
					}
					finally
					{
						if (connection.State != ConnectionState.Closed)
						{
							connection.Close();
						}
					}
				}
			}
		}

		public static bool Exists(string strOracle)
		{
			object obj = OracleHelper.GetSingle(strOracle);
			int cmdresult;
			if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
			{
				cmdresult = 0;
			}
			else
			{
				cmdresult = int.Parse(obj.ToString());
			}
			if (cmdresult == 0)
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		/// <summary>
		/// Execute an OracleCommand (that returns no resultset) against an existing database transaction 
		/// using the provided parameters.
		/// </summary>
		/// <remarks>
		/// e.g.:  
		///  int result = ExecuteNonQuery(trans, CommandType.StoredProcedure, "PublishOrders", new OracleParameter(":prodid", 24));
		/// </remarks>
		/// <param name="trans">an existing database transaction</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or PL/SQL command</param>
		/// <param name="commandParameters">an array of OracleParamters used to execute the command</param>
		/// <returns>an int representing the number of rows affected by the command</returns>
		public static int ExecuteNonQuery(OracleTransaction trans, CommandType cmdType, string cmdText, params OracleParameter[] commandParameters)
		{
			OracleCommand cmd = new OracleCommand();
			PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, commandParameters);
			int val = cmd.ExecuteNonQuery();
			cmd.Parameters.Clear();
			return val;
		}

		/// <summary>
		/// Execute an OracleCommand (that returns no resultset) against an existing database connection 
		/// using the provided parameters.
		/// </summary>
		/// <remarks>
		/// e.g.:  
		///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new OracleParameter(":prodid", 24));
		/// </remarks>
		/// <param name="conn">an existing database connection</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or PL/SQL command</param>
		/// <param name="commandParameters">an array of OracleParamters used to execute the command</param>
		/// <returns>an int representing the number of rows affected by the command</returns>
		public static int ExecuteNonQuery(OracleConnection connection, CommandType cmdType, string cmdText, params OracleParameter[] commandParameters)
		{

			OracleCommand cmd = new OracleCommand();

			PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
			int val = cmd.ExecuteNonQuery();
			cmd.Parameters.Clear();
			return val;
		}
		/// <summary>
		/// Execute an OracleCommand (that returns no resultset) against an existing database connection 
		/// using the provided parameters.
		/// </summary>
		/// <remarks>
		/// e.g.:  
		///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new OracleParameter(":prodid", 24));
		/// </remarks>
		/// <param name="conn">an existing database connection</param>
		/// <param name="commandText">the stored procedure name or PL/SQL command</param>
		/// <returns>an int representing the number of rows affected by the command</returns>
		public static int ExecuteNonQuery(string cmdText)
		{
			OracleCommand cmd = new OracleCommand();
			using (OracleConnection connection = new OracleConnection(connectionString))
			{
				PrepareCommand(cmd, connection, null, CommandType.Text, cmdText, null);
				int val = cmd.ExecuteNonQuery();
				cmd.Parameters.Clear();
				return val;
			}
		}

		/// <summary>
		/// Execute a select query that will return a result set
		/// </summary>
		/// <param name="connString">Connection string</param>
		//// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or PL/SQL command</param>
		/// <param name="commandParameters">an array of OracleParamters used to execute the command</param>
		/// <returns></returns>
		public static OracleDataReader ExecuteReader(CommandType cmdType, string cmdText, params OracleParameter[] commandParameters)
		{
			OracleCommand cmd = new OracleCommand();
			using (OracleConnection conn = new OracleConnection(connectionString))
			{
				//Prepare the command to execute
				PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
				OracleDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
				cmd.Parameters.Clear();
				return rdr;
			}
		}

		/// <summary>
		/// Execute an OracleCommand that returns the first column of the first record against the database specified in the connection string 
		/// using the provided parameters.
		/// </summary>
		/// <remarks>
		/// e.g.:  
		///  Object obj = ExecuteScalar(connString, CommandType.StoredProcedure, "PublishOrders", new OracleParameter(":prodid", 24));
		/// </remarks>
		/// <param name="connectionString">a valid connection string for a SqlConnection</param>
		/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">the stored procedure name or PL/SQL command</param>
		/// <param name="commandParameters">an array of OracleParamters used to execute the command</param>
		/// <returns>An object that should be converted to the expected type using Convert.To{Type}</returns>
		public static object ExecuteScalar(CommandType cmdType, string cmdText, params OracleParameter[] commandParameters)
		{
			OracleCommand cmd = new OracleCommand();

			using (OracleConnection conn = new OracleConnection(connectionString))
			{
				PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
				object val = cmd.ExecuteScalar();
				cmd.Parameters.Clear();
				return val;
			}
		}

		/// <summary>
		/// Internal function to prepare a command for execution by the database
		/// </summary>
		/// <param name="cmd">Existing command object</param>
		/// <param name="conn">Database connection object</param>
		/// <param name="trans">Optional transaction object</param>
		/// <param name="cmdType">Command type, e.g. stored procedure</param>
		/// <param name="cmdText">Command test</param>
		/// <param name="commandParameters">Parameters for the command</param>
		private static void PrepareCommand(OracleCommand cmd, OracleConnection conn, OracleTransaction trans, CommandType cmdType, string cmdText, OracleParameter[] commandParameters)
		{

			//Open the connection if required
			if (conn.State != ConnectionState.Open)
				conn.Open();

			//Set up the command
			cmd.Connection = conn;
			cmd.CommandText = cmdText;
			cmd.CommandType = cmdType;

			//Bind it to the transaction if it exists
			if (trans != null)
				cmd.Transaction = trans;
			cmd.Parameters.Clear();
			// Bind the parameters passed in
			if (commandParameters != null)
			{
				foreach (OracleParameter parm in commandParameters)
					cmd.Parameters.Add(parm);
			}
		}

		/// <summary>
		/// 执行多条SQL语句，实现数据库事务。
		/// </summary>
		/// <param name="SQLStringList">多条SQL语句</param>		
		public static bool ExecuteSqlTran(List<CommandInfo> cmdList)
		{
			using (OracleConnection conn = new OracleConnection(connectionString))
			{
				conn.Open();
				OracleCommand cmd = new OracleCommand();
				cmd.Connection = conn;
				OracleTransaction tx = conn.BeginTransaction();
				cmd.Transaction = tx;
				try
				{
					foreach (CommandInfo c in cmdList)
					{
						if (!String.IsNullOrEmpty(c.CommandText))
						{
							PrepareCommand(cmd, conn, tx, CommandType.Text, c.CommandText, (OracleParameter[])c.Parameters);
							if (c.EffentNextType == EffentNextType.WhenHaveContine || c.EffentNextType == EffentNextType.WhenNoHaveContine)
							{
								if (c.CommandText.ToLower().IndexOf("count(") == -1)
								{
									tx.Rollback();
									throw new Exception("Oracle:违背要求" + c.CommandText + "必须符合select count(..的格式");
									//return false;
								}

								object obj = cmd.ExecuteScalar();
								bool isHave = false;
								if (obj == null && obj == DBNull.Value)
								{
									isHave = false;
								}
								isHave = Convert.ToInt32(obj) > 0;

								if (c.EffentNextType == EffentNextType.WhenHaveContine && !isHave)
								{
									tx.Rollback();
									throw new Exception("Oracle:违背要求" + c.CommandText + "返回值必须大于0");
									//return false;
								}
								if (c.EffentNextType == EffentNextType.WhenNoHaveContine && isHave)
								{
									tx.Rollback();
									throw new Exception("Oracle:违背要求" + c.CommandText + "返回值必须等于0");
									//eturn false;
								}
								continue;
							}
							int res = cmd.ExecuteNonQuery();
							if (c.EffentNextType == EffentNextType.ExcuteEffectRows && res == 0)
							{
								tx.Rollback();
								throw new Exception("Oracle:违背要求" + c.CommandText + "必须有影响行");
								// return false;
							}
						}
					}
					tx.Commit();
					return true;
				}
				catch (System.Data.OracleClient.OracleException E)
				{
					tx.Rollback();
					throw E;
				}
				finally
				{
					if (conn.State != ConnectionState.Closed)
					{
						conn.Close();
					}
				}
			}
		}
		/// <summary>
		/// 执行多条SQL语句，实现数据库事务。
		/// </summary>
		/// <param name="SQLStringList">多条SQL语句</param>		
		public static void ExecuteSqlTran(List<String> SQLStringList)
		{
			using (OracleConnection conn = new OracleConnection(connectionString))
			{
				conn.Open();
				OracleCommand cmd = new OracleCommand();
				cmd.Connection = conn;
				OracleTransaction tx = conn.BeginTransaction();
				cmd.Transaction = tx;
				try
				{
					foreach (string sql in SQLStringList)
					{
						if (!String.IsNullOrEmpty(sql))
						{
							cmd.CommandText = sql;
							cmd.ExecuteNonQuery();
						}
					}
					tx.Commit();
				}
				catch (System.Data.OracleClient.OracleException E)
				{
					tx.Rollback();
					throw new Exception(E.Message);
				}
				finally
				{
					if (conn.State != ConnectionState.Closed)
					{
						conn.Close();
					}
				}
			}
		}
	}
}
