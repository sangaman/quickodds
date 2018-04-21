using System;
using System.Collections.Generic;
using System.Text;

namespace QuickOdds_Tracker
{	
	class PlayerHand
	{
		int playerId;
		int handId;
		
		String playerName;
		double chipCount;
		int position;
		int seatNumber;
		
		//preflop vpip stats
		private bool openRaise;
		private bool raise;
		private bool reraise;

		private bool openLimp;
		private bool overLimp;

		private bool callRaise;

		//preflop limp actions
		private bool limpCall;
		private bool limpRaise;
		private bool limpFold;

		//preflop all-in actions, raises or calls of at least 15 BBs and at least 80% of chips
		private bool overShove; //all-in on top of 1 BB
		private bool shove; //all-in on top of raise

		//postflop stats
		private bool flopBet;
		private bool flopCheckRaise;
		private bool flopCheckCall;
		private bool flopCheckFold;
		private bool flopCheckAround;
		private bool flopBetFold;

		private bool flopPositionBet;
		private bool flopPositionRaise;
		private bool flopPositionCall;
		private bool flopPositionFold;
		private bool flopPositionCheck;
		private bool flopPositionBetFold;

		private bool flopContinueBet;
		private bool flopContinueCheckRaise;
		private bool flopContinueCheckCall;
		private bool flopContinueCheckFold;
		private bool flopContinueCheckAround;
		private bool flopContinueBetFold;

		private bool flopContinuePositionBet;
		private bool flopContinuePositionRaise;
		private bool flopContinuePositionCall;
		private bool flopContinuePositionFold;
		private bool flopContinuePositionCheck;
		private bool flopContinuePositionBetFold;

		private bool turnBet;
		private bool turnCheckRaise;
		private bool turnCheckCall;
		private bool turnCheckFold;
		private bool turnCheckAround;
		private bool turnBetFold;

		private bool turnPositionBet;
		private bool turnPositionRaise;
		private bool turnPositionCall;
		private bool turnPositionFold;
		private bool turnPositionCheck;
		private bool turnPositionBetFold;

		private bool turnContinueBet;
		private bool turnContinueCheckRaise;
		private bool turnContinueCheckCall;
		private bool turnContinueCheckFold;
		private bool turnContinueCheckAround;
		private bool turnContinueBetFold;

		private bool turnContinuePositionBet;
		private bool turnContinuePositionRaise;
		private bool turnContinuePositionCall;
		private bool turnContinuePositionFold;
		private bool turnContinuePositionCheck;
		private bool turnContinuePositionBetFold;

		private bool riverBet;
		private bool riverCheckRaise;
		private bool riverCheckCall;
		private bool riverCheckFold;
		private bool riverCheckAround;
		private bool riverBetFold;

		private bool riverPositionBet;
		private bool riverPositionRaise;
		private bool riverPositionCall;
		private bool riverPositionFold;
		private bool riverPositionCheck;
		private bool riverPositionBetFold;

		private bool riverContinueBet;
		private bool riverContinueCheckRaise;
		private bool riverContinueCheckCall;
		private bool riverContinueCheckFold;
		private bool riverContinueCheckAround;
		private bool riverContinueBetFold;

		private bool riverContinuePositionBet;
		private bool riverContinuePositionRaise;
		private bool riverContinuePositionCall;
		private bool riverContinuePositionFold;
		private bool riverContinuePositionCheck;
		private bool riverContinuePositionBetFold;

		//summary
		private double netWL;
		private double expectedWL;

		public PlayerHand(String playerName, int seatNumber, double chipCount)
		{
			this.playerName = playerName;
			this.seatNumber = seatNumber;
			this.chipCount = chipCount;
		}

		#region Properties
		public int Position
		{
			set { position = value; }
		}
		#endregion
	}
}
