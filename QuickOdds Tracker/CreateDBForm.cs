using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Npgsql;

namespace QuickOdds_Tracker
{
	public partial class CreateDBForm : Form
	{
		public CreateDBForm()
		{
			InitializeComponent();
		}

		private void createDatabaseButton_Click(object sender, EventArgs e)
		{
			NpgsqlConnection conn = new NpgsqlConnection(Consts.postgresConnstring);
			conn.Open();
			String query = "CREATE DATABASE " + databaseNameTextBox.Text;
			try
			{
				Consts.ExecuteNonQuery(query, conn);
			}
			catch (NpgsqlException ex)
			{
				//error handling
			}
			conn.ChangeDatabase(databaseNameTextBox.Text); //is this necessary?

			query = @"CREATE TABLE Players
			(
			playerId int,
			playerName varchar(32),
			pokerRoomId int, 
			PRIMARY KEY (playerId)
			);";
			Consts.ExecuteNonQuery(query, conn);

			query = @"CREATE TABLE Hands
			(
			handId int,
			handNumber bigint,
			pokerRoomId int,
			gameTypeId int, 
			gameLimitId int,
			datetime date,
			potSize numeric,
			rake numeric,
			players int,
			handHistory text,
			PRIMARY KEY (handId)
			);";
			Consts.ExecuteNonQuery(query, conn);

			query = @"CREATE TABLE PlayerHands
			(
			handId int,
			playerId int,
			position int,
			holeCards char(4), 
			holeCardsSuitedness char(4), 
			openRaise bit,
			raise bit,
			reraise bit,
			openLimp bit,
			overLimp bit,
			callRaise bit,
			limpCall bit,
			limpRaise bit,
			limpFold bit,
			overshove bit,
			shove bit,
			flopBet bit,
			flopCheckRaise bit,
			flopCheckCall bit,
			flopCheckFold bit,
			flopCheckAround bit,
			flopBetFold bit,
			flopPBet bit,
			flopPRaise bit,
			flopPCall bit,
			flopPFold bit,
			flopPCheck bit,
			flopPBetFold bit,
			flopCBet bit,
			flopCCheckRaise bit,
			flopCCheckCall bit,
			flopCCheckFold bit,
			flopCCheckAround bit,
			flopCBetFold bit,
			flopCPBet bit,
			flopCPRaise bit,
			flopCPCall bit,
			flopCPFold bit,
			flopCPCheck bit,
			flopCPBetFold bit,
			turnBet bit,
			turnCheckRaise bit,
			turnCheckCall bit,
			turnCheckFold bit,
			turnCheckAround bit,
			turnBetFold bit,
			turnPBet bit,
			turnPRaise bit,
			turnPCall bit,
			turnPFold bit,
			turnPCheck bit,
			turnPBetFold bit,
			turnCBet bit,
			turnCCheckRaise bit,
			turnCCheckCall bit,
			turnCCheckFold bit,
			turnCCheckAround bit,
			turnCBetFold bit,
			turnCPBet bit,
			turnCPRaise bit,
			turnCPCall bit,
			turnCPFold bit,
			turnCPCheck bit,
			turnCPBetFold bit,
			riverBet bit,
			riverCheckRaise bit,
			riverCheckCall bit,
			riverCheckFold bit,
			riverCheckAround bit,
			riverBetFold bit,
			riverPBet bit,
			riverPRaise bit,
			riverPCall bit,
			riverPFold bit,
			riverPCheck bit,
			riverPBetFold bit,
			riverCBet bit,
			riverCCheckRaise bit,
			riverCCheckCall bit,
			riverCCheckFold bit,
			riverCCheckAround bit,
			riverCBetFold bit,
			riverCPBet bit,
			riverCPRaise bit,
			riverCPCall bit,
			riverCPFold bit,
			riverCPCheck bit,
			riverCPBetFold bit,
			wentToShowdown bit,
			winnings decimal,
			expectedWinning decimal,
			PRIMARY KEY (handId, playerId),
			FOREIGN KEY (handId) REFERENCES Hands(handId),
			FOREIGN KEY (playerId) REFERENCES Players(playerId)
			);";
			Consts.ExecuteNonQuery(query, conn);
			
			conn.Close();
			this.Close();
		}
	}
}