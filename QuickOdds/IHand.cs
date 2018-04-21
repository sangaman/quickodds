using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace QuickOdds
{
	public interface IHand
	{
		int GetStrength(int boardRanks, out int lowStrength);
		int GetStrength(int boardRanks);
		int CheckStraightFlush(int flushSuit, int boardFlushRankBits);
		int CheckFlush(int flushSuit);
		int CheckStraightFlushAndFlush(int flushSuit, int boardFlushRankBits);

		double ChipCount
		{
			get;
			set;
		}
		String PlayerName
		{
			get;
			set;
		}
		Cards Cards
		{
			get;
		}
		
	}
}
