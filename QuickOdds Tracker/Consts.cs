using System;
using System.Collections.Generic;
using System.Text;
using Npgsql;

namespace QuickOdds_Tracker
{
	static class Consts
	{
		private static String serverName = "localhost";
		private static String port = "5432";
		private static String postgresUserId = "postgres";
		private static String postgresPassword = "soyfl";
		public static String postgresConnstring = String.Format("Server={0};Port={1};" +
					"User Id={2};Password={3};",
					serverName, port, postgresUserId,
					postgresPassword);

		public static String[] fullTiltSeparator = new String[] { " - " };
		//public static int fullTiltMaxPlayers = 9;

		public static void ExecuteNonQuery(String query, NpgsqlConnection conn)
		{
			using (NpgsqlCommand command = new NpgsqlCommand(query, conn))
			{
				command.ExecuteNonQuery();
			}
		}
	}
}
