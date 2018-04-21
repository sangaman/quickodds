using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace QuickOdds
{
	class HandSlow
	{
		ArrayList cards;
		int[] handFlushRankBits;
		int suitCounts;
		ArrayList highestOfSuit;

		ArrayList pairsInHand;
		int highPairRank;
		int handRankBits;
		int setRank;
		int tripsRank;
		int boardTripsRank;
		int boardPairRank;

		public HandSlow(Card c1, Card c2, Card c3, Card c4)
		{
			cards = new ArrayList(4);
			cards.Add(c1);
			cards.Add(c2);
			cards.Add(c3);
			cards.Add(c4);
		}

		public void EvaluateHand()
		{
			highPairRank = -1; 
			pairsInHand = new ArrayList();
			handRankBits = 0;

			handFlushRankBits = new int[4] { 0, 0, 0, 0 };
			suitCounts = 26214;
			highestOfSuit = new ArrayList(4);

			for (int n = 0; n < highestOfSuit.Capacity; n++)
				highestOfSuit.Add(-1);
			
			for(int n=0; n<cards.Count; n++)
			{
				suitCounts += 1 << (GetCard(n).Suit * 4);
				if ((int)highestOfSuit[GetCard(n).Suit] < GetCard(n).Rank)
					highestOfSuit[GetCard(n).Suit] = GetCard(n).Rank;

				if ((handRankBits & Consts.rankMasks[GetCard(n).Rank]) == Consts.rankMasks[GetCard(n).Rank])
				{
					if(!pairsInHand.Contains(GetCard(n).Rank))
					{
						pairsInHand.Add(GetCard(n).Rank);
						if (GetCard(n).Rank > highPairRank)
							highPairRank = GetCard(n).Rank;
					}
				}
				handRankBits |= Consts.rankMasks[GetCard(n).Rank];
				handFlushRankBits[GetCard(n).Suit] |= Consts.rankMasks[GetCard(n).Rank];
			}
		}

		
		public int CheckStraightFlush(int flushSuit, int boardFlushRankBits)
		{
			for (int n = 9; n >= 0; n--)
			{
				//key is lowest 5 bits of boardRankBits and lowest 5 bits of handRankBits
				//higher 5 bits of key is handRankBits, lower 5 bits is boardRankBits
				//int key = (((handRankBits >> n) % 32) << 5) + ((boardRankBits >> n) % 32);
				int key = (handFlushRankBits[flushSuit] >> n) % 32;
				key = key << 5;
				key += (boardFlushRankBits >> n) % 32;
				if (Consts.straightDictionary.ContainsKey(key))
					return n;
			}
			return -1;
		}

		public int CheckFlush(int flushSuit)
		{
			if ((suitCounts & Consts.suitMasks[flushSuit]) == Consts.suitMasks[flushSuit])
				return (int)highestOfSuit[flushSuit];
			else
				return -1;
		}

		public int CheckQuadsBoat(Dictionary<int,int> rankCounts)
		{
			int quadsBoat = -1;
			setRank = -1;
			tripsRank = -1;
			int pairRank = -1;
			boardTripsRank = -1;
			boardPairRank = -1;
			foreach (int handPairRank in pairsInHand)
			{
				if (rankCounts.ContainsKey(handPairRank))
				{
					if(rankCounts[handPairRank] == 2)
					{
						if (2813 + handPairRank > quadsBoat)
							quadsBoat = 2813 + handPairRank;
					}
					else 
					{
						if (handPairRank > setRank)
							setRank = handPairRank;
					}
				}
			}

			for(int n=12; n >= 0; n--)
			{
				if (rankCounts.ContainsKey(n))
				{
					//if trips on board
					if (rankCounts[n] > 2)
					{
						if (((handRankBits & Consts.rankMasks[n]) > 0) && 2813 + n > quadsBoat) //if quads
						{
							quadsBoat = 2813+n;
							break;
						}
						else if (setRank > n && 2644 + setRank * 13 + n > quadsBoat) //if hand has overset
						{
							quadsBoat = 2644 + setRank * 13 + n; //return overset full of 2 of trips on board
							break;
						}
						else if (highPairRank > -1 && 2644 + (n - 1) * 13 + highPairRank > quadsBoat) //if pair is in hand
						{
							quadsBoat = 2644 + n * 13 + highPairRank; //return trips on board full of pair in hand
							break;
						}
						else
							boardTripsRank = n; 
					}
					else if (rankCounts[n] == 2) //if pair on board
					{
						if ((handRankBits & Consts.rankMasks[n]) > 0) //if hand has these trips
						{
							if(tripsRank < n)
							{
								tripsRank = n; //make it trips if highest trips
							}
							else if(pairRank < n)
								pairRank = n; //make it pair if highest pair
						}
						if(n>boardPairRank)
							boardPairRank = n;
					}
					else if (rankCounts[n] == 1) //check for pair
						if ((handRankBits & Consts.rankMasks[n]) > 0 && pairRank < n)
							pairRank = n; //make this highest pair
				}
			}
			if (pairRank > -1 && tripsRank > -1)
				if (2644 + tripsRank * 13 + pairRank > quadsBoat)
					quadsBoat = 2644 + tripsRank * 13 + pairRank;
			if (setRank > -1 && boardPairRank > -1)
				if (2644 + setRank * 13 + boardPairRank > quadsBoat)
					quadsBoat = 2644 + setRank * 13 + boardPairRank;
			return quadsBoat;
		}

		public int CheckStraight(int boardRankBits)
		{
			for (int n = 9; n >= 0; n--)
			{
				//key is lowest 5 bits of boardRankBits and lowest 5 bits of handRankBits
				//higher 5 bits of key is handRankBits, lower 5 bits is boardRankBits
				//int key = (((handRankBits >> n) % 32) << 5) + ((boardRankBits >> n) % 32);
				int key = (handRankBits >> n) % 32;
				key = key << 5;
				key += (boardRankBits >> n) % 32;
				if (Consts.straightDictionary.ContainsKey(key))
					return 2634+ n;
			}
			return -1;
		}

		public int CheckSetTrips()
		{
			if (setRank > -1)
				return 2465 + setRank * 13;
			else if (tripsRank > -1 && tripsRank > boardTripsRank)
			{
				int kickerRank = 0;
				for (int n = 12; n > 0; n--)
					if ((handRankBits & Consts.rankMasks[n]) > 0 && n != tripsRank)
					{
						kickerRank = n;
						break;
					}
				return 2465 + tripsRank * 13 + kickerRank;
			}
			else if(boardTripsRank > -1)
				return CheckHighCards();
			else
				return -1;
		}

		public int CheckTwoPair(Dictionary<int, int> rankCounts)
		{
			int twoPair = -1;
			int highPair = -1;
			int lowPair = -1;
			int kicker = -1;
			for (int n = 0; n < 13; n++)
			{
				if ((handRankBits & Consts.rankMasks[n]) > 0)
				{
					if (rankCounts.ContainsKey(n))
					{
						lowPair = highPair;
						highPair = n;
					}
					else
						kicker = n;
				}
			}
			if (lowPair > -1)
				twoPair = highPair * 13 * 13 + lowPair * 13;
			
			if (boardPairRank > -1 && (highPair > -1 || highPairRank > -1))
			{
				if (highPair > highPairRank)
				{
					if (boardPairRank > highPair)
						twoPair = boardPairRank * 13 * 13 + highPair * 13 + Math.Max(kicker, lowPair);
					else if (boardPairRank > lowPair)
						twoPair = highPair * 13 * 13 + boardPairRank * 13 + Math.Max(kicker, lowPair);
				}
				else
				{
					if (highPairRank > boardPairRank)
					{
						if((highPairRank * 13 * 13 + boardPairRank * 13) > twoPair)
							twoPair = highPairRank * 13 * 13 + boardPairRank * 13;
					}
					else if(boardPairRank * 13 * 13 + highPairRank * 13 > twoPair)
						twoPair = boardPairRank * 13 * 13 + highPairRank * 13;
				}
			}

			if (twoPair > -1)
				return 268 + twoPair;
			else
				return -1;
		}

		public int CheckPair(Dictionary<int, int> rankCounts)
		{
			int pair = -1;
			int highCard = -1;
			if (boardPairRank > -1)
				return CheckHighCards();
			else
			{
				for (int n = 0; n < 13; n++)
				{
					if ((handRankBits & Consts.rankMasks[n]) > 0)
					{
						if (rankCounts.ContainsKey(n))
							pair = n;
						else
							highCard = n;
					}
				}
			}
			if (pair > highPairRank)
				return 99 + pair * 13 + highCard;
			else if (highPairRank > -1)
				return 99 + highPairRank * 13;
			else
				return -1;
		}

		public int CheckHighCards()
		{
			int highest = -1;
			int second = -1;
			for (int n = 0; n < cards.Count; n++)
			{
				if (GetCard(n).Rank > highest)
				{
					second = highest;
					highest = GetCard(n).Rank;
				}
				else if (GetCard(n).Rank > second)
					second = GetCard(n).Rank;
			}
			return (highest - 3) * 10 + second - 2;
		}

		public int CheckLow(int boardRankBits)
		{
			int key = boardRankBits % 256;
			key += (handRankBits % 256) << 8;
			int low;
			if (Consts.lowDictionary.TryGetValue(key, out low))
				return low;
			else
				return -1;
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

		public Card GetCard(int index)
		{
			return (Card)cards[index];
		}

		public ArrayList Cards
		{
			get { return cards; }
			set { cards = value; }
		}
	}
}
