using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.IO;

namespace QuickOdds
{
	class OmahaHand : IHand
	{
		ArrayList cards;
		int handRanks;
		int[] handFlushRankBits;
		int suitCounts;
		int[] highestOfSuit;
		Dictionary<int, int> strengthDictionary;
		ArrayList[] straightFlushesToCheck;
		double chipCount;
		

		public OmahaHand(Card c1, Card c2, Card c3, Card c4)
		{
			cards = new ArrayList(4);
			cards.Add(c1);
			cards.Add(c2);
			cards.Add(c3);
			cards.Add(c4);
		}

		public void EvaluateHand()
		{
			handRanks = 1;
			handFlushRankBits = new int[4] { 0, 0, 0, 0 };
			suitCounts = 26214;
			highestOfSuit = new int[4] { 0, 0, 0, 0 };
			strengthDictionary = new Dictionary<int, int>();
			
			for(int n=0; n<cards.Count; n++)
			{
				suitCounts += 1 << (GetCard(n).Suit * 4);
				if (highestOfSuit[GetCard(n).Suit] < GetCard(n).Rank)
					highestOfSuit[GetCard(n).Suit] = GetCard(n).Rank;

				handRanks *= Consts.primeNumbers[GetCard(n).Rank];
				handFlushRankBits[GetCard(n).Suit] |= Consts.rankMasks[GetCard(n).Rank];
			}

			String s = File.ReadAllText("OmahaDictionaries\\strengthDict" + handRanks + ".txt");
			String[] tokens = s.Split(new char[] { ':', ',' },
				StringSplitOptions.RemoveEmptyEntries);
			for (int i = 0; i < tokens.Length; i += 2)
				strengthDictionary[int.Parse(tokens[i])] = int.Parse(tokens[i + 1]);

			//optimization for straight flushes
			straightFlushesToCheck = new ArrayList[4];
			
			for (int m = 0; m < 4; m++)
			{
				if((suitCounts & Consts.suitMasks[m]) == Consts.suitMasks[m])
				//if (Consts.GetBitCount(handFlushRankBits[m]) > 1)
				{
					straightFlushesToCheck[m] = new ArrayList(8);
					for (int n = 9; n >= 0; n--)
					{
						int bits = (((int)handFlushRankBits[m]) >> n) % 32;
						if (Consts.GetBitCount(bits) == 2)
							straightFlushesToCheck[m].Add(n);
					}
				}
				else
					straightFlushesToCheck[m] = new ArrayList(0);
			}
		}

		public int GetStrength(int boardRanks, out int lowStrength)
		{
			int result = strengthDictionary[boardRanks];
			lowStrength = result >> 12;
			return result % 4096;
		}

		public int GetStrength(int boardRanks)
		{
			return strengthDictionary[boardRanks] % 4096;
		}
		
		public int CheckStraightFlush(int flushSuit, int boardFlushRankBits)
		{
			foreach (int n in straightFlushesToCheck[flushSuit])
			{
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
				return highestOfSuit[flushSuit];
			else
				return -1;
		}

		public int CheckStraightFlushAndFlush(int flushSuit, int boardFlushRankBits)
		{
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

		private Card GetCard(int index)
		{
			return (Card)cards[index];
		}

		public ArrayList Cards
		{
			get { return cards; }
		}

		public double ChipCount
		{
			get { return chipCount; }
			set { chipCount = value; }
		}
	}
}