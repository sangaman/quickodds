using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using QuickOdds;

namespace QuickOdds_Calculator
{
	public class Simulation
	{
		Cards[] cards;
        String[] playerNames;
		double[] preflopEquities;
		double[] flopEquities;
		double[] turnEquities;
		double[] riverEquities;

		public Simulation(IHand[] hands, double[] preflopEquities, double[] flopEquities, double[] turnEquities, double[] riverEquities)
		{
            cards = new Cards[hands.Length];
            playerNames = new String[hands.Length];
            for (int n = 0; n < hands.Length; n++)
            {
                cards[n] = hands[n].Cards;
                playerNames[n] = hands[n].PlayerName;
            }
			this.preflopEquities = preflopEquities;
			this.flopEquities = flopEquities;
			this.turnEquities = turnEquities;
			this.riverEquities = riverEquities; 
		}

        public Simulation(Cards[] cards, String[] playerNames, double[] preflopEquities, double[] flopEquities, double[] turnEquities, double[] riverEquities)
        {
            this.cards = cards;
            this.playerNames = playerNames;
            this.preflopEquities = preflopEquities;
            this.flopEquities = flopEquities;
            this.turnEquities = turnEquities;
            this.riverEquities = riverEquities;
        }

		public Cards[] Cards
		{
			get { return cards; }
		}

        public String[] PlayerNames
        {
            get { return playerNames; }
        }

		public double[] PreflopEquities
		{
			get { return preflopEquities; }
		}

		public double[] FlopEquities
		{
			get { return flopEquities; }
		}

		public double[] TurnEquities
		{
			get { return turnEquities; }
		}

		public double[] RiverEquities
		{
			get { return riverEquities; }
		}

		public int NumHands
		{
			get { return cards.Length; }
		}
	}
}
