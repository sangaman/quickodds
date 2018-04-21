using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using QuickOdds;

namespace QuickOdds_Calculator
{
	class Calculator
	{
		ArrayList hands;
		Deck deck;
		Board board;
		double[] values;
		private delegate void EvaluateShowdown(Board board, int suitCounts);
		EvaluateShowdown evaluator;

		public Calculator(Deck deck, ArrayList hands, Board board, Consts.GameTypes gameType)
		{
			this.deck = deck;
			this.hands = hands;
			this.board = board;
			values = new double[hands.Count];

			if (gameType == Consts.GameTypes.OmahaHiLo)
				evaluator = EvaluateOmahaHiLoShowdown;
			else if (gameType == Consts.GameTypes.Omaha)
				evaluator = EvaluateOmahaHiShowdown;
			else
				evaluator = EvaluateHoldemShowdown;
		}

		public double[] CalculatePreflopEquity()
		{
			for (int n = 0; n < values.Length; n++)
				values[n] = 0.0;
			double[] percentages = new double[values.Length];

			//0101010101010101
			//SxxxHxxxDxxxCxxx
			//When three of a suit are added, its 4 bit block becomes 1000, the highest bit indicates flush is possible
			int suitCounts = 21845;

			for (int c1 = 0; c1 < deck.NumCards; c1++)
			{
				suitCounts += 1 << (deck.Cards.GetCard(c1).Suit * 4);
				for (int c2 = c1 + 1; c2 < deck.NumCards; c2++)
				{
					suitCounts += 1 << (deck.Cards.GetCard(c2).Suit * 4);
					for (int c3 = c2 + 1; c3 < deck.NumCards; c3++)
					{
						suitCounts += 1 << (deck.Cards.GetCard(c3).Suit * 4);
						for (int c4 = c3 + 1; c4 < deck.NumCards; c4++)
						{
							suitCounts += 1 << (deck.Cards.GetCard(c4).Suit * 4);
							for (int c5 = c4 + 1; c5 < deck.NumCards; c5++)
							{
								suitCounts += 1 << (deck.Cards.GetCard(c5).Suit * 4);
								evaluator(new Board(deck.Cards.GetCard(c1), deck.Cards.GetCard(c2), deck.Cards.GetCard(c3), deck.Cards.GetCard(c4), deck.Cards.GetCard(c5)), suitCounts);
								suitCounts -= 1 << (deck.Cards.GetCard(c5).Suit * 4);
							}
							suitCounts -= 1 << (deck.Cards.GetCard(c4).Suit * 4);
						}
						suitCounts -= 1 << (deck.Cards.GetCard(c3).Suit * 4);
					}
					suitCounts -= 1 << (deck.Cards.GetCard(c2).Suit * 4);
				}
				suitCounts -= 1 << (deck.Cards.GetCard(c1).Suit * 4);
			}

			double totalValue = 0;
			for (int n = 0; n < values.Length; n++)
				totalValue += values[n];
			for (int n = 0; n < values.Length; n++)
				percentages[n] = values[n] / totalValue;
			return percentages;
		}

		public double[] CalculateFlopEquity()
		{
			for (int n = 0; n < values.Length; n++)
				values[n] = 0.0;
			double[] percentages = new double[values.Length];

			int suitCounts = 21845;
			//check flush possibility			
			for (int n = 0; n < 3; n++)
				suitCounts += 1 << (board.Cards.GetCard(n).Suit * 4);

			deck.RemoveCards(board.Cards.GetRange(0, 3));

			for (int c1 = 0; c1 < deck.NumCards; c1++)
			{
				suitCounts += 1 << (deck.Cards.GetCard(c1).Suit * 4);
				for (int c2 = c1 + 1; c2 < deck.NumCards; c2++)
				{
					suitCounts += 1 << (deck.Cards.GetCard(c2).Suit * 4);
					Board b = new Board(board.Cards.GetCard(0), board.Cards.GetCard(1), board.Cards.GetCard(2),
										deck.Cards.GetCard(c1), deck.Cards.GetCard(c2));
					evaluator(b, suitCounts);
					suitCounts -= 1 << (deck.Cards.GetCard(c2).Suit * 4);
				}
				suitCounts -= 1 << (deck.Cards.GetCard(c1).Suit * 4);
			}

			deck.AddCards(board.Cards.GetRange(0, 3));
			double totalValue = 0;
			for (int n = 0; n < values.Length; n++)
				totalValue += values[n];
			for (int n = 0; n < values.Length; n++)
				percentages[n] = values[n] / totalValue;
			return percentages;
		}

		public double[] CalculateTurnEquity()
		{
			for (int n = 0; n < values.Length; n++)
				values[n] = 0.0;
			double[] percentages = new double[values.Length];

			int suitCounts = 21845;
			//check flush possibility			
			for (int n = 0; n < 4; n++)
				suitCounts += 1 << (board.Cards.GetCard(n).Suit * 4);

			deck.RemoveCards(board.Cards.GetRange(0, 4));

			for (int c1 = 0; c1 < deck.NumCards; c1++)
			{
				suitCounts += 1 << (deck.Cards.GetCard(c1).Suit * 4);
				Board b = new Board(board.Cards.GetCard(0), board.Cards.GetCard(1), board.Cards.GetCard(2),
									board.Cards.GetCard(3), deck.Cards.GetCard(c1));
				evaluator(b, suitCounts);
				suitCounts -= 1 << (deck.Cards.GetCard(c1).Suit * 4);
			}

			deck.AddCards(board.Cards.GetRange(0, 4));
			double totalValue = 0;
			for (int n = 0; n < values.Length; n++)
				totalValue += values[n];
			for (int n = 0; n < values.Length; n++)
				percentages[n] = values[n] / totalValue;
			return percentages;
		}

		public double[] CalculateRiverEquity()
		{
			for (int n = 0; n < values.Length; n++)
				values[n] = 0.0;
			double[] percentages = new double[values.Length];

			int suitCounts = 21845;
			//check flush possibility			
			for (int n = 0; n < 5; n++)
				suitCounts += 1 << (board.Cards.GetCard(n).Suit * 4);

			evaluator(board, suitCounts);

			double totalValue = 0;
			for (int n = 0; n < values.Length; n++)
				totalValue += values[n];
			for (int n = 0; n< values.Length; n++)
				percentages[n]=values[n] / totalValue;
			return percentages;
		}

		private void EvaluateOmahaHiLoShowdown(Board board, int suitCounts)
		{
			double highHandValue = 1.0;

			int boardRanks = 1;
			for (int n = 0; n < board.NumCards; n++)
				boardRanks *= Consts.primeNumbers[board.Cards.GetCard(n).Rank];

			ArrayList highWinners = new ArrayList(hands.Count);
			ArrayList lowWinners = new ArrayList(hands.Count);
			int maxLowStrength = 1;
			int maxHighStrength = 0;
			for (int n = 0; n < hands.Count; n++)
			{
				int currentLowStrength;
				int currentHighStrength = ((OmahaHand)hands[n]).GetStrength(boardRanks, out currentLowStrength);
				if (currentHighStrength > maxHighStrength)
				{
					highWinners.Clear();
					maxHighStrength = currentHighStrength;
					highWinners.Add(n);
				}
				else if (currentHighStrength == maxHighStrength)
					highWinners.Add(n);
				if (currentLowStrength > maxLowStrength)
				{
					lowWinners.Clear();
					maxLowStrength = currentLowStrength;
					lowWinners.Add(n);
				}
				else if (currentLowStrength == maxLowStrength)
					lowWinners.Add(n);
			}
			if (lowWinners.Count > 0)
			{
				highHandValue = highHandValue / 2;
				foreach (int n in lowWinners)
					values[n] += (double)highHandValue / lowWinners.Count;
			}

			//check for suit-specific hands
			for (int flushSuit = 0; flushSuit < 4; flushSuit++)
				if ((suitCounts & Consts.suitMasks[flushSuit]) == Consts.suitMasks[flushSuit])
				{
					//get RankBits of suited cards on board
					int boardFlushRankBits = 0;
					for (int n = 0; n < board.NumCards; n++)
						if (board.Cards.GetCard(n).Suit == flushSuit)
							boardFlushRankBits |= Consts.rankMasks[board.Cards.GetCard(n).Rank];

					int maxStraightFlush = -1;
					for (int n = 0; n < hands.Count; n++)
					{
						int currentStraightFlush = ((OmahaHand)hands[n]).CheckStraightFlush(flushSuit, boardFlushRankBits);
						if (currentStraightFlush > maxStraightFlush)
						{
							highWinners.Clear();
							maxStraightFlush = currentStraightFlush;
							highWinners.Add(n);
						}
					}
					if (maxStraightFlush > -1)
					{
						foreach (int n in highWinners)
							values[n] += (double)highHandValue / highWinners.Count;
						return;
					}

					//check for flush if hand is weaker than full house
					if (maxHighStrength < 2644)
					{
						int maxFlush = 0;
						for (int n = 0; n < hands.Count; n++)
						{
							int currentFlush = ((OmahaHand)hands[n]).CheckFlush(flushSuit);
							if (currentFlush > maxFlush)
							{
								highWinners.Clear();
								maxFlush = currentFlush;
								highWinners.Add(n);
							}
						}
					}
					break; //break loop since no other flushes are possible
				}

			foreach (int n in highWinners)
				values[n] += (double)highHandValue / highWinners.Count;
		}

		private void EvaluateOmahaHiShowdown(Board board, int suitCounts)
		{
			int boardRanks = 1;
			for (int n = 0; n < board.NumCards; n++)
				boardRanks *= Consts.primeNumbers[board.Cards.GetCard(n).Rank];

			ArrayList highWinners = new ArrayList(hands.Count);
			int maxHighStrength = 0;
			for (int n = 0; n < hands.Count; n++)
			{
				int currentHighStrength = ((OmahaHand)hands[n]).GetStrength(boardRanks);
				if (currentHighStrength > maxHighStrength)
				{
					highWinners.Clear();
					maxHighStrength = currentHighStrength;
					highWinners.Add(n);
				}
				else if (currentHighStrength == maxHighStrength)
					highWinners.Add(n);
			}

			//check for suit-specific hands
			for (int flushSuit = 0; flushSuit < 4; flushSuit++)
				if ((suitCounts & Consts.suitMasks[flushSuit]) == Consts.suitMasks[flushSuit])
				{
					//get RankBits of suited cards on board
					int boardFlushRankBits = 0;
					for (int n = 0; n < board.NumCards; n++)
						if (board.Cards.GetCard(n).Suit == flushSuit)
							boardFlushRankBits |= Consts.rankMasks[board.Cards.GetCard(n).Rank];

					int maxStraightFlush = -1;
					for (int n = 0; n < hands.Count; n++)
					{
						int currentStraightFlush = ((OmahaHand)hands[n]).CheckStraightFlush(flushSuit, boardFlushRankBits);
						if (currentStraightFlush > maxStraightFlush)
						{
							highWinners.Clear();
							maxStraightFlush = currentStraightFlush;
							highWinners.Add(n);
						}
					}
					if (maxStraightFlush > -1)
					{
						foreach (int n in highWinners)
							values[n] += (double)1.0 / highWinners.Count;
						return;
					}

					//check for flush if hand is weaker than full house
					if (maxHighStrength < 2644)
					{
						int maxFlush = 0;
						for (int n = 0; n < hands.Count; n++)
						{
							int currentFlush = ((OmahaHand)hands[n]).CheckFlush(flushSuit);
							if (currentFlush > maxFlush)
							{
								highWinners.Clear();
								maxFlush = currentFlush;
								highWinners.Add(n);
							}
						}
					}
					break; //break loop since no other flushes are possible
				}

			foreach (int n in highWinners)
				values[n] += (double)1.0 / highWinners.Count;
		}

		private void EvaluateHoldemShowdown(Board board, int suitCounts)
		{
			int boardRanks = 1;
			for (int n = 0; n < board.NumCards; n++)
				boardRanks *= Consts.primeNumbers[board.Cards.GetCard(n).Rank];

			ArrayList highWinners = new ArrayList(hands.Count);
			int maxHighStrength = 0;
			for (int n = 0; n < hands.Count; n++)
			{
				int currentHighStrength = ((HoldemHand)hands[n]).GetStrength(boardRanks);
				if (currentHighStrength > maxHighStrength)
				{
					highWinners.Clear();
					maxHighStrength = currentHighStrength;
					highWinners.Add(n);
				}
				else if (currentHighStrength == maxHighStrength)
					highWinners.Add(n);
			}

			//check for suit-specific hands
			for (int flushSuit = 0; flushSuit < 4; flushSuit++)
				if ((suitCounts & Consts.suitMasks[flushSuit]) == Consts.suitMasks[flushSuit])
				{
					//get RankBits of suited cards on board
					int boardFlushRankBits = 0;
					for (int n = 0; n < board.NumCards; n++)
						if (board.Cards.GetCard(n).Suit == flushSuit)
							boardFlushRankBits |= Consts.rankMasks[board.Cards.GetCard(n).Rank];

					//check for straight flush and flush
					int maxStraightFlushOrFlush;
					if (maxHighStrength > 2303) //if hand is better than straight
						maxStraightFlushOrFlush = 7762; //require straight flush
					else
						maxStraightFlushOrFlush = -1; //require any flush
					for (int n = 0; n < hands.Count; n++)
					{
						int currentStraightFlushOrFlush = ((HoldemHand)hands[n]).CheckStraightFlushAndFlush(flushSuit, boardFlushRankBits);
						if (currentStraightFlushOrFlush > maxStraightFlushOrFlush)
						{
							highWinners.Clear();
							maxStraightFlushOrFlush = currentStraightFlushOrFlush;
							highWinners.Add(n);
						}
						else if (currentStraightFlushOrFlush == maxStraightFlushOrFlush)
							highWinners.Add(n);
					}
					break; //break loop since no other flushes are possible
				}

			foreach (int n in highWinners)
				values[n] += (double)1.0 / highWinners.Count;
		}

		public Deck Deck
		{
			set { deck = value; }
		}
	}
}

