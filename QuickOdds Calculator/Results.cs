using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using QuickOdds;

namespace QuickOdds_Calculator
{
	public class Results
	{
		String summary;
		Board board;
		double time;
		Consts.GameTypes gameType;
		ArrayList simulations;

		bool error;
		String errorMessage;

		String handHistory;

		public ArrayList Simulations
		{
			get { return simulations; }
            set { simulations = value; }
		}

		public Results(Consts.GameTypes gameType, Board board, String handHistory)
		{
			error = false;
			simulations = new ArrayList();
			this.board = board;
			this.gameType = gameType;

			this.handHistory = handHistory;
		}

		public Results(String errorMessage, String handHistory)
		{
			error = true;
			this.errorMessage = errorMessage;

			this.handHistory = handHistory;
		}

		/*public Results(String resultsString)
		{
			String[] tokens = resultsString.Split(new String[1] {"|||ENDOFHH|||"}, StringSplitOptions.RemoveEmptyEntries);
			simulations = new ArrayList();
			handHistory = tokens[0];

			tokens = tokens[1].Split(new String[1] { ";" }, StringSplitOptions.RemoveEmptyEntries);
			error = bool.Parse(tokens[0]);
			if (!error)
			{
				board = Board.Parse(tokens[1]);
				time = double.Parse(tokens[2]);
				gameType = (Consts.GameTypes)Int32.Parse(tokens[3]);
				for (int n = 4; n < tokens.Length; n++)
					simulations.Add(new Simulation(tokens[n]));
			}
			else
			{
				errorMessage = tokens[1];
			}
		}*/

		public String Summary
		{
            get { return summary; }
            set { summary = value; }
		}
		
		public double Time
		{
            get { return time; }
            set { time = value; }
		}

		public String HandHistory
		{
			get { return handHistory; }
		}

		public String ShowResults(bool showPlayerNames, bool showTime)
		{
			if (error)
				return errorMessage;
			else
			{
				String resultsString;

				if (gameType == Consts.GameTypes.OmahaHiLo)
					resultsString = "Omaha Hi-Lo";
				else if (gameType == Consts.GameTypes.Omaha)
					resultsString = "Omaha Hi";
				else
					resultsString = "Hold'em";

				if (board.Cards.Count > 0)
					resultsString += " Board: " + board.ToString(true);
				else
					resultsString += ":";
				resultsString += "\n";

				foreach (Simulation sim in simulations)
				{
					resultsString += "\n";
					if (gameType != Consts.GameTypes.Holdem)
						resultsString += "\t";

					if (sim.PreflopEquities != null)
						resultsString += "\tPreflop";
					if (sim.FlopEquities != null)
						resultsString += "\tFlop";
					if (sim.TurnEquities != null)
						resultsString += "\tTurn";
					if (sim.RiverEquities != null)
						resultsString += "\tRiver";
					resultsString += "\n";

					for (int n = 0; n < sim.NumHands; n++)
					{
						if (showPlayerNames)
							resultsString += sim.PlayerNames[n] + "\n";

                        String equity = String.Empty ;

                        //show hand with suits
                        foreach (Card card in sim.Cards[n])
                        {
                            equity += card.ToString(true);
                            equity += " ";
                        }
                        equity = equity.TrimEnd() + "\t";

						if(sim.PreflopEquities != null)
							equity += (100*sim.PreflopEquities[n]).ToString("N") + "%\t";
						if (sim.FlopEquities != null)
							equity += (100*sim.FlopEquities[n]).ToString("N") + "%\t";
						if (sim.TurnEquities != null)
							equity += (100*sim.TurnEquities[n]).ToString("N") + "%\t";
						if (sim.RiverEquities != null)
							equity += (100*sim.RiverEquities[n]).ToString("N") + "%";
						equity += "\n";
						resultsString += equity;
					}
				}
				if (showTime)
					resultsString += "\nTotal time: " + time + " seconds";

				return resultsString;
			}
		}


		public void AddSimulation(Simulation sim)
		{
			simulations.Add(sim);
		}

		public override string ToString()
		{
			if (error)
				return "Simulation Error";
			else
				return summary;
		}

		public Consts.GameTypes GameType
		{
			get { return gameType; }
		}

		public Board Board
		{
			get { return board; }
		}

		public bool Error
		{
			get { return error; }
		}
	}
}
