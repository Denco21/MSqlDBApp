﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;


namespace DataAccessLibrary
{
	
	public class SqlDataAccess
	{
		
		public List<T> LoadData<T, U>(string sqlStatement, U parameters, string connectionStringName) 
		{
			
			using (IDbConnection connection = new SqlConnection(connectionStringName)) 
			{
				List<T> rows = connection.Query<T>(sqlStatement, parameters).ToList(); 
				return rows; 
			}
		}

		public void SaveData<T>(string sqlStatement, T parameters, string connectionStringName)  
		{
			using (IDbConnection connection = new SqlConnection(connectionStringName))
			{
				connection.Execute(sqlStatement, parameters); 
			}
		}
	}
}
