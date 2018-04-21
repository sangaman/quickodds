using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.IO;

namespace QuickOdds
{
	public class HoldemHand : IHand
	{
		Cards cards;
		int[] handFlushRankBits;
		Dictionary<int, short> strengthDictionary;
		double chipCount;
		String playerName;

		public HoldemHand(Card c1, Card c2)
		{
            cards = new Cards(4);
			cards.Add(c1);
			cards.Add(c2);
			cards.Sort();

			//Evaluate hand
			int handRanks = 1;
			handFlushRankBits = new int[4] { 0, 0, 0, 0 };
			strengthDictionary = new Dictionary<int, short>();

			for (int n = 0; n < cards.Count; n++)
			{
				handRanks *= Consts.primeNumbers[cards.GetCard(n).Rank];
				handFlushRankBits[cards.GetCard(n).Suit] |= Consts.rankMasks[cards.GetCard(n).Rank];
			}

			String s = (String)Consts.Deserialize("HoldemDictionaries\\strengthDict" + handRanks + ".dat");
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

        public static HoldemHand Parse(String handString)
        {
            return new HoldemHand(  Card.Parse(handString.Substring(0, 2)),
                                    Card.Parse(handString.Substring(3, 2)));
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
            return cards.ToString();
		}

		public Cards Cards
		{
			get { return cards; }
		}

		public double ChipCount
		{
			get { return chipCount; }
			set { chipCount = value; }
		}

		public String PlayerName
		{
			get { return playerName; }
			set { playerName = value; }
		}
	}
}
