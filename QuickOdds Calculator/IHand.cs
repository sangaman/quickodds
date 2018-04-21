using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace QuickOdds
{
	interface IHand
	{
		void EvaluateHand();
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
		ArrayList Cards
		{
			get;
		}
		
	}
}
