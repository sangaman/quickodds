using System;
using System.Collections.Generic;
using System.Text;

namespace QuickOdds
{
	class Card
	{
		int rank; //0 deuce, up to 12 ace
		int suit; //0 club, 1 diamond, 2 heart, 3 spade

		public Card(int rank, int suit)
		{
			this.rank = rank;
			this.suit = suit;
		}

		public Card(String rank, String suit)
		{
			switch (rank)
			{
				case "2":
					this.rank = 0;
					break;
				case "3":
					this.rank = 1;
					break;
				case "4":
					this.rank = 2;
					break;
				case "5":
					this.rank = 3;
					break;
				case "6":
					this.rank = 4;
					break;
				case "7":
					this.rank = 5;
					break;
				case "8":
					this.rank = 6;
					break;
				case "9":
					this.rank = 7;
					break;
				case "T":
					this.rank = 8;
					break;
				case "J":
					this.rank = 9;
					break;
				case "Q":
					this.rank = 10;
					break;
				case "K":
					this.rank = 11;
					break;
				case "A":
					this.rank = 12;
					break;
				default:
					throw new CardException(rank + suit);
			}
			switch (suit)
			{
				case "c":
					this.suit = 0;
					break;
				case "d":
					this.suit = 1;
					break;
				case "h":
					this.suit = 2;
					break;
				case "s":
					this.suit = 3;
					break;
				default:
					throw new CardException(rank + suit);
			}
		}

		public int Rank
		{
			get { return rank; }
		}

		public int Suit
		{
			get { return suit; }
		}

		public override string ToString()
		{
			String rankBitString, suitString;
			switch (rank)
			{
				case 8:
					rankBitString = "T";
					break;
				case 9:
					rankBitString = "J";
					break;
				case 10:
					rankBitString = "Q";
					break;
				case 11:
					rankBitString = "K";
					break;
				case 12:
					rankBitString = "A";
					break;
				default:
					rankBitString = (rank + 2).ToString();
					break;
			}
			switch (suit)
			{
				case 0:
					suitString = "\u2663";
					break;
				case 1:
					suitString = "\u2666";
					break;
				case 2:
					suitString = "\u2665";
					break;
				case 3:
					suitString = "\u2660";
					break;
				default:
					suitString = "";
					break;
			}
			return rankBitString + suitString;
		}

		public override bool Equals(object obj)
		{
			Card c = (Card)obj;
			if (suit == c.Suit && rank == c.Rank)
				return true;
			return false;
		}
	}
}
