using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.IO;

namespace QuickOdds
{
	class HoldemHand : IHand
	{
		ArrayList cards;
		int handRanks;
		int[] handFlushRankBits;
		Dictionary<int, short> strengthDictionary;
		double chipCount;

		public HoldemHand(Card c1, Card c2)
		{
			cards = new ArrayList(2);
			cards.Add(c1);
			cards.Add(c2);
		}

		public void EvaluateHand()
		{
			handRanks = 1;
			handFlushRankBits = new int[4] { 0, 0, 0, 0 };
			strengthDictionary = new Dictionary<int, short>();

			for(int n=0; n<cards.Count; n++)
			{
				handRanks *= Consts.primeNumbers[GetCard(n).Rank];
				handFlushRankBits[GetCard(n).Suit] |= Consts.rankMasks[GetCard(n).Rank];
			}
			
			String s = File.ReadAllText("HoldemDictionaries\\strengthDict" + handRanks + ".txt");
			String[] tokens = s.Split(new char[] { ':', ',' },
				StringSplitOptions.RemoveEmptyEntries);
			for (int i = 0; i < tokens.Length; i += 2)
				strengthDictionary[int.Parse(tokens[i])] = short.Parse(tokens[i + 1]);
		}

		public int GetStrength(int boardRanks, out int lowStrength)
		{
			lowStrength = -1;
			return -1;
		}

		public int GetStrength(int boardRanks)
		{
			return strengthDictionary[boardRanks];
		}
		
		public int CheckStraightFlush(int flushSuit, int boardFlushRankBits)
		{
			return -1;
		}

		public int CheckFlush(int flushSuit)
		{
			return -1;
		}

		public int CheckStraightFlushAndFlush(int flushSuit, int boardFlushRankBits)
		{
			int flushRankBits = boardFlushRankBits | handFlushRankBits[flushSuit];
			short flushStrength;
			if (!Consts.flushDictionary.TryGetValue((short)flushRankBits, out flushStrength))
				return -2;
			return flushStrength;
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
