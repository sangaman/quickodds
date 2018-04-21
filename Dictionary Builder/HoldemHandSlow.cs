using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace QuickOdds
{
	class HoldemHandSlow
	{
		ArrayList cards;
		Dictionary<int, int> strengthDictionary;
		double chipCount;

		int handPairRank;
		int handRankBits;
		int handLowCard;
		int handHighCard;
		int setRank;
		int tripsRank;
		int boardTripsRank;
		int boardHighPairRank;
		int boardLowPairRank;

		public HoldemHandSlow(Card c1, Card c2)
		{
			cards = new ArrayList(2);
			cards.Add(c1);
			cards.Add(c2);
		}

		public void EvaluateHand()
		{
			handPairRank = -1;
			handRankBits = 0;
			handLowCard = -1;
			handHighCard = -1;

			for(int n=0; n<cards.Count; n++)
			{
				if (GetCard(n).Rank > handHighCard)
				{
					handLowCard = handHighCard;
					handHighCard = GetCard(n).Rank;
				}
				else if (GetCard(n).Rank == handHighCard)
					handPairRank = GetCard(n).Rank;
				else
					handLowCard = GetCard(n).Rank;
				handRankBits |= Consts.rankMasks[GetCard(n).Rank];
			}
		}

		public int CheckQuadsBoat(Dictionary<int, int> rankCounts, int boardRankBits)
		{
			if (boardRankBits == (Consts.rankMasks[11] | Consts.rankMasks[10] | Consts.rankMasks[8] | Consts.rankMasks[7] | Consts.rankMasks[4]) && handPairRank == 7)
			{
				int ble;
				ble = 1;
			}
			
			int pair = -1;
			setRank = -1;
			tripsRank = -1;
			boardTripsRank = -1;
			boardHighPairRank = -1;
			boardLowPairRank = -1;
			if (rankCounts.ContainsKey(handPairRank))
			{
				if (rankCounts[handPairRank] == 2)
				{
					return 2471 + handPairRank;
				}
				else
				{
					setRank = handPairRank;
				}
			}

			for (int n = 12; n >= 0; n--)
			{
				if (rankCounts.ContainsKey(n))
				{
					//if quads on board
					if (rankCounts[n] == 4)
					{
						boardRankBits -= Consts.rankMasks[n];
						if(IsKicker(boardRankBits, 1, handHighCard))
							return handHighCard;
						return 0;
					}
					//if trips on board
					else if (rankCounts[n] == 3)
					{
						boardTripsRank = n;
						if ((handRankBits & Consts.rankMasks[n]) > 0) //if quads
							return 2471 + n;
						else if (setRank > n) //if hand has overset
						{
							return 2304 + setRank * 13 + n; //return overset full of 2 of trips on board
							break;
						}
						/*else if (handPairRank > -1) //if pair is in hand
						{
							return 2304 + n * 13 + handPairRank; //return trips on board full of pair in hand
							break;
						}
						else if (pair > -1)
						{
							return 2304 + n * 13 + pair; //return trips on board full of pair
						}
						else
							boardTripsRank = n;*/
					}
					else if (rankCounts[n] == 2) //if pair on board
					{
						if ((handRankBits & Consts.rankMasks[n]) > 0) //if hand has these trips
						{
							if (tripsRank < n)
								tripsRank = n; //make it trips if highest trips
						}
						else if(boardHighPairRank > -1)
							boardLowPairRank = n;
						else
							boardHighPairRank = n;
					}
					else if (rankCounts[n] == 1) //check for pair
						if ((handRankBits & Consts.rankMasks[n]) > 0 && pair < n)
							if(setRank != n)
								pair = n; //make this highest pair
				}
			}
			int boat = -1;
			if(boardHighPairRank > pair)
				pair = boardHighPairRank;
			if (setRank > -1 && boardTripsRank > -1)
			{
				if (setRank > boardTripsRank)
					boat = 2304 + setRank * 13 + boardTripsRank;
				else
					boat = 2304 + boardTripsRank * 13 + setRank;
			}
			else if (tripsRank > -1 && boardTripsRank > -1)
			{
				if (tripsRank > boardTripsRank)
					boat = 2304 + tripsRank * 13 + boardTripsRank;
				else
					boat = 2304 + boardTripsRank * 13 + tripsRank;
			}
			if(boardTripsRank > -1 && handPairRank > -1 && (2304 + boardTripsRank * 13 + handPairRank) > boat)
				boat = 2304 + boardTripsRank * 13 + handPairRank;
			if (setRank > -1 && pair > -1 && (2304 + setRank * 13 + pair) > boat)
				boat = 2304 + setRank * 13 + pair;
			if (tripsRank > -1 && pair > -1 && (2304 + tripsRank * 13 + pair) > boat)
				boat = 2304 + tripsRank * 13 + pair;
			if (boardTripsRank > -1 && pair > -1 && (2304 + boardTripsRank * 13 + pair) > boat)
				boat = 2304 + boardTripsRank * 13 + pair;
			return boat;
		}

		public int CheckStraight(int boardRankBits)
		{
			int rankBits = boardRankBits | handRankBits;
			for (int n = 9; n >= 0; n--)
			{
				if ((rankBits >> n) % 32 == 31)
					return 2294 + n;
			}
			return -1;
		}

		public int CheckSetTrips(int boardRankBits)
		{
			int kicker;
			if (setRank > -1)
				return 2127 + setRank * 13;
			else if (tripsRank > -1)
			{
				boardRankBits -= Consts.rankMasks[tripsRank];
				if (tripsRank == handHighCard)
					kicker = handLowCard;
				else
					kicker = handHighCard;
				if (IsKicker(boardRankBits, 2, kicker))
					return 2127 + tripsRank * 13 + kicker;
				return 2127 + tripsRank * 13;
			}
			else if (boardTripsRank > -1)
			{
				boardRankBits -= Consts.rankMasks[boardTripsRank];
				return CheckHighCards(boardRankBits, 2);
			}
			else
				return -1;
		}

		public int CheckTwoPair(int boardRankBits)
		{
			int highPair = -1;
			int lowPair = -1;
			int kicker = -1;
			if(handLowCard>-1)
				if ((boardRankBits & Consts.rankMasks[handLowCard]) > 0)
				{
					highPair = handLowCard;
					kicker = handHighCard;
				}
			if ((boardRankBits & Consts.rankMasks[handHighCard]) > 0)
			{
				if (highPair > -1)
				{
					lowPair = highPair;
					highPair = handHighCard;
					kicker = -1;
				}
				else
				{
					highPair = handHighCard;
					kicker = handLowCard;
				}
			}
			if (boardLowPairRank > -1)
			{
				//two pair on board
				if (boardLowPairRank < handPairRank)
				{
					if (handPairRank > boardHighPairRank)
						return 102 + handPairRank * 12 * 13 + boardHighPairRank * 13;
					return 102 + boardHighPairRank * 12 * 13 + handPairRank * 13;
				}
				else if (boardLowPairRank < highPair)
				{
					if (highPair > boardHighPairRank)
						lowPair = boardHighPairRank;
					else
					{
						lowPair = highPair;
						highPair = boardHighPairRank;
					}
				}
				else
				{
					//using two pair on board
					boardRankBits -= Consts.rankMasks[boardLowPairRank];
					boardRankBits -= Consts.rankMasks[boardHighPairRank];
					if (IsKicker(boardRankBits, 1, handHighCard))
						return handHighCard;
					return 0;
				}
				boardRankBits -= Consts.rankMasks[lowPair];
				boardRankBits -= Consts.rankMasks[highPair];
				if(IsKicker(boardRankBits, 1, kicker))
					return 102 + highPair * 13 * 12 + lowPair * 13 + kicker;
				return 102 + highPair * 13 * 12 + lowPair * 13 ;
			}
			else if (boardHighPairRank > -1)
			{
				//one pair on board
				if (lowPair > boardHighPairRank)
					return 102 + handHighCard * 13 * 12 + handLowCard * 13;
				else if(highPair>-1)
				{
					boardRankBits -= Consts.rankMasks[boardHighPairRank];
					boardRankBits -= Consts.rankMasks[highPair];
					if (highPair > boardHighPairRank)
					{
						if (kicker > -1)
							if (IsKicker(boardRankBits, 1, kicker))
								return 102 + highPair * 13 * 12 + boardHighPairRank * 13 + kicker;
						return 102 + highPair * 13 * 12 + boardHighPairRank * 13;
					}
					else
					{
						if (kicker > -1)
							if (IsKicker(boardRankBits, 1, kicker))
								return 102 + boardHighPairRank * 13 * 12 + highPair * 13 + kicker;
						return 102 + boardHighPairRank * 13 * 12 + highPair * 13;
					}
				}
				else if (handPairRank > -1)
				{
					if(handPairRank > boardHighPairRank)
						return 102 + handPairRank * 13 * 12 + boardHighPairRank * 13;
					else
						return 102 + boardHighPairRank * 13 * 12 + handPairRank * 13;
				}
			}
			else
			{
				//no pairs on board
				if(lowPair > -1)
					return 102 + highPair * 12 * 13 + lowPair * 13;
			}
			return -1;
		}

		public int CheckPair(int boardRankBits)
		{
			int pair = -1;
			int kicker = 0;
			if (boardHighPairRank > -1)
			{
				boardRankBits -= Consts.rankMasks[boardHighPairRank];
				return CheckHighCards(boardRankBits, 3);
			}
			else if (handPairRank > -1)
			{
				return 90 + handPairRank * 13;
			}
			else
			{
				if ((boardRankBits&Consts.rankMasks[handHighCard])>0)
				{
					pair = handHighCard;
					boardRankBits -= Consts.rankMasks[pair];
					if (IsKicker(boardRankBits, 3, handLowCard))
						kicker = handLowCard;
				}
				else if ((boardRankBits & Consts.rankMasks[handLowCard]) > 0)
				{
					pair = handLowCard;
					boardRankBits -= Consts.rankMasks[pair];
					if(IsKicker(boardRankBits, 3, handHighCard))
						kicker = handHighCard;
				}
			}
			if (pair > -1)
				return 90 + pair * 13 + kicker;
			return -1;
		}

		public int CheckHighCards(int boardRankBits, int cardsNeeded)
		{
			int highCards = 0;
			int totalRankBits = boardRankBits | handRankBits;
			if ((totalRankBits % 2) == 1)
				totalRankBits--;
			int bitsToRemove = Consts.GetBitCount(totalRankBits) - cardsNeeded;
			bool highestBit = true;
			for (int n = 0; n < 13; n++)
			{
				if ((totalRankBits & Consts.rankMasks[n]) > 0)
				{
					bitsToRemove--;
					totalRankBits -= Consts.rankMasks[n];
				}
				if (bitsToRemove == 0)
					break;
			}
			totalRankBits &= handRankBits;
			int bits;
			if (cardsNeeded == 5)
			{
				totalRankBits = totalRankBits >> 4;
				bits = 9;
			}
			else
			{
				totalRankBits = totalRankBits >> 2;
				bits = 11;
			}
			for (int n = bits; n >= 0; n--)
			{
				if ((totalRankBits & (1 << n)) > 0)
				{
					if (highestBit)
					{
						highCards += n * bits;
						highestBit = false;
					}
					else
						highCards += n;
				}
			}
			return highCards;
		}

		private bool IsKicker(int boardRankBits, int cardsNeeded, int kicker)
		{			
			if((boardRankBits & Consts.rankMasks[kicker]) > 0)
				return false;
			int totalRankBits = boardRankBits | Consts.rankMasks[kicker];
			if ((totalRankBits % 2) == 1)
				totalRankBits--;
			int bitsToRemove = Consts.GetBitCount(totalRankBits) - cardsNeeded;
			for (int n = 0; n < 13; n++)
			{
				if ((totalRankBits & Consts.rankMasks[n]) > 0)
				{
					bitsToRemove--;
					totalRankBits -= Consts.rankMasks[n];
				}
				if (bitsToRemove == 0)
					break;
			}
			totalRankBits &= Consts.rankMasks[kicker];
			if (totalRankBits > 0)
				return true;
			return false;
		}

		public override string ToString()
		{
			String returnString = "";
			foreach (Card card in cards)
			{
				returnString += card.ToString() + " ";
			}
			return returnString.TrimEnd();
		}

		private Card GetCard(int index)
		{
			return (Card)cards[index];
		}

		public ArrayList Cards
		{
			get { return cards; }
		}
	}
}
