using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Npgsql;
using System.Collections.Generic;
using System.Collections;

namespace QuickOdds_Tracker
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
			
			// Making connection with Npgsql provider
			NpgsqlConnection conn = new NpgsqlConnection(Consts.postgresConnstring);
			conn.Open();
			/*// quite complex sql statement
			String query = "SELECT * FROM simple_table";
			// data adapter making request from our connection
			NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
			// filling DataSet with result from NpgsqlDataAdapter
			da.Fill(ds);
			// since it C# DataSet can handle multiple tables, we will select first
			dt = ds.Tables[0];
			// connect grid to DataTable
			dataGridView1.DataSource = dt;
			String query = "INSERT INTO test (test_int) VALUES ('5')";
			using (NpgsqlCommand command = new NpgsqlCommand(query, conn))
			{
				command.ExecuteNonQuery();
			}*/

			// since we only showing the result we don't need connection anymore
			conn.Close();

		}

		private void createDatabaseMenuItem_Click(object sender, EventArgs e)
		{
			CreateDBForm createDBForm = new CreateDBForm();
			createDBForm.Location = new Point(this.Width / 2 + this.Location.X - createDBForm.Width / 2,
											this.Height / 2 + this.Location.Y - createDBForm.Height / 2);
			createDBForm.Show();
		}

		private void Parse(String handHistory)
		{
			StringReader reader = new StringReader(handHistory);
			//advance to first line of hand history
			String nextLine;
			do
			{
				nextLine = reader.ReadLine();
			} while (!nextLine.Contains("Poker"));
			
			/*//determine Game
			if (nextLine.Contains("Omaha"))
			{
				if (nextLine.Contains("/L"))
					game = 0;
				else
					game = 1;
			}
			else if(nextLine.Contains("Hold"))
				game = 2;
			else
				throw new Exception("Could not detect valid game type.");*/

			String[] tokens = handHistory.Split(Consts.fullTiltSeparator, StringSplitOptions.None);
			int handNumberIndex = tokens[0].IndexOf("#") + 1;
			int colonIndex = tokens[0].IndexOf(":");
			int tableIndex = tokens[0].IndexOf("Table ") + "Table ".Length;
			int parenthesesIndex = tokens[0].IndexOf(" (");
			long handNumber = Int64.Parse(tokens[0].Substring(handNumberIndex, colonIndex - handNumberIndex));
			String tableName = tokens[0].Remove(parenthesesIndex).Substring(tableIndex);
			String limit = tokens[1];
			String gameType = tokens[2];
			String timeZone = tokens[3].Substring(tokens[3].IndexOf(" ") + 1);
			String timeString = tokens[3].Remove(tokens[3].IndexOf(" ") + 1);
			DateTime time = DateTime.Parse(timeString + tokens[4]);
			
			ArrayList seatNumbers = new ArrayList();
			Dictionary<int, PlayerHand> playerHands = new Dictionary<int, PlayerHand>();

			nextLine = reader.ReadLine();

			while (nextLine.StartsWith("Seat"))
			{
				int seatNumber = Int32.Parse(nextLine.Substring(5, 1));
				seatNumbers.Add(seatNumber);

				int nameEndIndex = nextLine.IndexOf(" (", 8);
				int nameStartIndex = nextLine.IndexOf(": ") + 2;
				String name = nextLine.Substring(nameStartIndex, nameEndIndex - nameStartIndex);

				String chipCount = nextLine.Substring(nameEndIndex + 2,
					nextLine.IndexOf(")", 10) - nextLine.IndexOf(" (", 8) - 2);
				chipCount = chipCount.Replace("$", "");
				if (chipCount.Contains(" "))
					chipCount = chipCount.Remove(chipCount.IndexOf(" "));

				playerHands.Add(seatNumber, new PlayerHand(name, seatNumber, chipCount));

				nextLine = reader.ReadLine();
			}
			reader.ReadLine(); 
			nextLine = reader.ReadLine();
			int buttonSeat = Int32.Parse(nextLine.Substring("The button is in seat #".Length));
			int buttonPosition=0; 
			//cycle through positions until you find one with button's seat
			while (((int)seatNumbers[buttonPosition]) != buttonSeat)
				buttonPosition++;

			for (int n = 1; n <= seatNumbers.Count; n++)
				playerHands[seatNumbers[(buttonPosition + n])%seatNumbers.Count].Position = n;

			while (nextLine != null)
			{
				
			}
			
			
			
			//PlayerHand[] playerHands = new PlayerHand[];
		}

		private void button1_Click(object sender, EventArgs e)
		{
			Parse(richTextBox1.Text);
		}
	}
}