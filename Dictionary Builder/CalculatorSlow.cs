using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace QuickOdds
{
	class CalculatorSlow
	{
		ArrayList hands;
		Deck deck;
		Board board;
		ArrayList values;

		public CalculatorSlow(Deck deck, ArrayList hands, Board board)
		{
			this.deck = deck;
			this.hands = hands;
			this.board = board;
			values = new ArrayList(hands.Count);

			for (int n = 0; n < hands.Count; n++)
				values.Add((double)0.0);
		}

		public ArrayList CalculatePreflopEquity()
		{
			for (int n = 0; n < values.Count; n++)
				values[n] = 0.0;
			ArrayList percentages = new ArrayList(hands.Count);

			//0101010101010101
			//SxxxHxxxDxxxCxxx
			int suitCounts = 21845;

			for (int c1 = 0; c1 < deck.NumCards; c1++)
			{
				suitCounts += 1 << (deck.GetCard(c1).Suit * 4);
				for (int c2 = c1 + 1; c2 < deck.NumCards; c2++)
				{
					suitCounts += 1 << (deck.GetCard(c2).Suit * 4);
					for (int c3 = c2 + 1; c3 < deck.NumCards; c3++)
					{
						suitCounts += 1 << (deck.GetCard(c3).Suit * 4);
						for (int c4 = c3 + 1; c4 < deck.NumCards; c4++)
						{
							suitCounts += 1 << (deck.GetCard(c4).Suit * 4);
							for (int c5 = c4 + 1; c5 < deck.NumCards; c5++)
							{
								suitCounts += 1 << (deck.GetCard(c5).Suit * 4);
								EvaluateShowdown(new Board(deck.GetCard(c1), deck.GetCard(c2), deck.GetCard(c3), deck.GetCard(c4), deck.GetCard(c5)), suitCounts);
								suitCounts -= 1 << (deck.GetCard(c5).Suit * 4);
							}
							suitCounts -= 1 << (deck.GetCard(c4).Suit * 4);
						}
						suitCounts -= 1 << (deck.GetCard(c3).Suit * 4);
					}
					suitCounts -= 1 << (deck.GetCard(c2).Suit * 4);
				}
				suitCounts -= 1 << (deck.GetCard(c1).Suit * 4);
			}

			double totalValue = 0;
			for (int n = 0; n < hands.Count; n++)
				totalValue += (double)values[n];
			foreach (double value in values)
				percentages.Add(value / totalValue);
			return percentages;
		}

		public ArrayList CalculateFlopEquity()
		{
			for (int n = 0; n < values.Count; n++)
				values[n] = 0.0;
			ArrayList percentages = new ArrayList(hands.Count);

			int suitCounts = 21845;
			//check flush possibility			
			for (int n = 0; n < 3; n++)
				suitCounts += 1 << (board.GetCard(n).Suit * 4);

			deck.RemoveCards(board.Cards);

			for (int c1 = 0; c1 < deck.NumCards; c1++)
			{
				suitCounts += 1 << (deck.GetCard(c1).Suit * 4);
				for (int c2 = c1 + 1; c2 < deck.NumCards; c2++)
				{
					suitCounts += 1 << (deck.GetCard(c2).Suit * 4);
					Board b = new Board(board.GetCard(0), board.GetCard(1), board.GetCard(2),
										deck.GetCard(c1), deck.GetCard(c2));
					EvaluateShowdown(b, suitCounts);
					suitCounts -= 1 << (deck.GetCard(c2).Suit * 4);
				}
				suitCounts -= 1 << (deck.GetCard(c1).Suit * 4);
			}

			double totalValue = 0;
			for (int n = 0; n < hands.Count; n++)
				totalValue += (double)values[n];
			foreach (double value in values)
				percentages.Add(value / totalValue);
			return percentages;
		}

		public ArrayList CalculateTurnEquity()
		{
			for (int n = 0; n < values.Count; n++)
				values[n] = 0.0;
			ArrayList percentages = new ArrayList(hands.Count);

			int suitCounts = 21845;
			//check flush possibility			
			for (int n = 0; n < 4; n++)
				suitCounts += 1 << (board.GetCard(n).Suit * 4);

			deck.RemoveCards(board.Cards);

			for (int c1 = 0; c1 < deck.NumCards; c1++)
			{
				suitCounts += 1 << (deck.GetCard(c1).Suit * 4);
				Board b = new Board(board.GetCard(0), board.GetCard(1), board.GetCard(2),
									board.GetCard(3), deck.GetCard(c1));
				EvaluateShowdown(b, suitCounts);
				suitCounts -= 1 << (deck.GetCard(c1).Suit * 4);
			}
			double totalValue = 0;
			for (int n = 0; n < hands.Count; n++)
				totalValue += (double)values[n];
			foreach (double value in values)
				percentages.Add(value / totalValue);
			return percentages;
		}

		private void EvaluateShowdown(Board board, int suitCounts)
		{
			double highHandValue = 1.0;
			
			//get ranks
			Dictionary<int, int> rankCounts = new Dictionary<int, int>();
			int boardRankBits = 0;
			for (int n = 0; n < board.NumCards; n++)
			{
				if (rankCounts.ContainsKey(board.GetCard(n).Rank))
					rankCounts[board.GetCard(n).Rank] = rankCounts[board.GetCard(n).Rank] + 1;
				else
					rankCounts.Add(board.GetCard(n).Rank, 1);
				boardRankBits |= Consts.rankMasks[board.GetCard(n).Rank];
			}

			if (Consts.GetBitCount(boardRankBits%256)>2) //if more than 2 low cards are on board
			{
				//find best low hand
				ArrayList lowWinners = new ArrayList(hands.Count);
				int maxLowStrength = 0;
				for(int n=0; n<hands.Count; n++)
				{
					int currentLowStrength = ((HandSlow)hands[n]).CheckLow(boardRankBits);
					if(currentLowStrength>maxLowStrength)
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
					highHandValue = highHandValue/2;
					foreach(int n in lowWinners)
						values[n] = (double)((double)values[n]) + (highHandValue / lowWinners.Count);
				}
			}

			//determine high winner
			ArrayList highWinners = new ArrayList(hands.Count);

			//check for flush possibility
			int flushSuit = -1; //arbitrary assignment
			for (int n = 0; n < 4; n++)
				if ((suitCounts & Consts.suitMasks[n]) == Consts.suitMasks[n])
				{
					flushSuit = n;
					break;
				}

			bool canStraight = false;
			if (Consts.GetBitCount(boardRankBits) > 2)
				canStraight = true;

			//check straightflush possiblity
			if (canStraight && flushSuit >= 0)
			{
				//get RankBits of suited cards on board
				int boardFlushRankBits = 0;
				for (int n = 0; n < board.NumCards; n++)
					if (board.GetCard(n).Suit == flushSuit)
						boardFlushRankBits |= Consts.rankMasks[board.GetCard(n).Rank];
					
				//check for straight flush
				if (Consts.GetBitCount(boardFlushRankBits) > 2)
				{
					int maxStraightFlush = 0;
					for (int n = 0; n < hands.Count; n++)
					{
						int currentStraightFlush = ((HandSlow)hands[n]).CheckStraightFlush(flushSuit, boardFlushRankBits);
						if (currentStraightFlush > maxStraightFlush)
						{
							highWinners.Clear();
							maxStraightFlush = currentStraightFlush;
							highWinners.Add(n);
						}
					}
					if (highWinners.Count > 0)
					{
						foreach (int n in highWinners)
							values[n] = (double)((double)values[n]) + (highHandValue / (double)highWinners.Count);
						return;
					}
				}
			}

			//check for full house or quads, this must be done as it helps check trips, 2 pair, pair, high card
			int maxQuadsBoat = 0;
			for (int n = 0; n < hands.Count; n++)
			{
				int currentQuadsBoat = ((HandSlow)hands[n]).CheckQuadsBoat(rankCounts);
				if (currentQuadsBoat > maxQuadsBoat)
				{
					highWinners.Clear();
					maxQuadsBoat = currentQuadsBoat;
					highWinners.Add(n);
				}
				else if (currentQuadsBoat == maxQuadsBoat)
					highWinners.Add(n);
			}
			if (highWinners.Count > 0)
			{
				foreach (int n in highWinners)
				{
					values[n] = (double)((double)values[n] + (highHandValue / highWinners.Count));
				}
				return;
			}
				
			//check for flush
			if (flushSuit >= 0)
			{
				int maxFlush = 0;
				for (int n = 0; n < hands.Count; n++)
				{
					int currentFlush = ((HandSlow)hands[n]).CheckFlush(flushSuit);
					if (currentFlush > maxFlush)
					{
						highWinners.Clear();
						maxFlush = currentFlush;
						highWinners.Add(n);
					}
				}
				if (highWinners.Count > 0)
				{
					foreach (int n in highWinners)
						values[n] = (double)((double)values[n]) + (highHandValue / highWinners.Count);
					return;
				}
			}

			//check for straight
			if (canStraight)
			{
				int maxStraight = 0;
				for (int n = 0; n < hands.Count; n++)
				{
					int currentStraight = ((HandSlow)hands[n]).CheckStraight(boardRankBits);
					if (currentStraight > maxStraight)
					{
						highWinners.Clear();
						maxStraight = currentStraight;
						highWinners.Add(n);
					}
					else if (currentStraight == maxStraight)
						highWinners.Add(n);
				}
				if (highWinners.Count > 0)
				{
					foreach (int n in highWinners)
						values[n] = (double)((double)values[n]) + (highHandValue / highWinners.Count);
					return;
				}
			}

			//check for trips or set
			int maxSetTrips = 0;
			for (int n = 0; n < hands.Count; n++)
			{
				int currentSetTrips = ((HandSlow)hands[n]).CheckSetTrips();
				if (currentSetTrips > maxSetTrips)
				{
					highWinners.Clear();
					maxSetTrips = currentSetTrips;
					highWinners.Add(n);
				}
				else if (currentSetTrips == maxSetTrips)
					highWinners.Add(n);
			}
			if (highWinners.Count > 0)
			{
				foreach (int n in highWinners)
				{
					values[n] = (double)((double)values[n]) + (highHandValue / highWinners.Count);
				}
				return;
			}

			//check for two pair
			int maxTwoPair = 0;
			for (int n = 0; n < hands.Count; n++)
			{
				int currentTwoPair = ((HandSlow)hands[n]).CheckTwoPair(rankCounts);
				if (currentTwoPair > maxTwoPair)
				{
					highWinners.Clear();
					maxTwoPair = currentTwoPair;
					highWinners.Add(n);
				}
				else if (currentTwoPair == maxTwoPair)
					highWinners.Add(n);
			}
			if (highWinners.Count > 0)
			{
				foreach (int n in highWinners)
					values[n] = (double)((double)values[n]) + (highHandValue / highWinners.Count);
				return;
			}

			//check for one pair
			int maxPair = 0;
			for (int n = 0; n < hands.Count; n++)
			{
				int currentPair = ((HandSlow)hands[n]).CheckPair(rankCounts);
				if (currentPair > maxPair)
				{
					highWinners.Clear();
					maxPair = currentPair;
					highWinners.Add(n);
				}
				else if (currentPair == maxPair)
					highWinners.Add(n);
			}
			if (highWinners.Count > 0)
			{
				foreach (int n in highWinners)
					values[n] = (double)((double)values[n]) + (highHandValue / highWinners.Count);
				return;
			}

			//check for high card
			int maxHighCards = 0;
			for (int n = 0; n < hands.Count; n++)
			{
				int currentHighCards = ((HandSlow)hands[n]).CheckHighCards();
				if (currentHighCards > maxHighCards)
				{
					highWinners.Clear();
					maxHighCards = currentHighCards;
					highWinners.Add(n);
				}
				else if (currentHighCards == maxHighCards)
					highWinners.Add(n);
			}
			foreach (int n in highWinners)
				values[n] = (double)((double)values[n]) + (highHandValue / highWinners.Count);
		}

		public void AddCardToBoard(Card card)
		{
			board.Add(card);
		}
	}
}

