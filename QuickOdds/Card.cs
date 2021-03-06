using System;
using System.Collections.Generic;
using System.Text;

namespace QuickOdds
{
	public class Card : IComparable
	{
		public enum Ranks { Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King, Ace };
		public enum Suits { Club, Diamond, Heart, Spade, NoSuit };
		
		Ranks rank; 
		Suits suit;

		public Card(int rank, int suit)
		{
			this.rank = (Ranks)rank;
			this.suit = (Suits)suit;
		}

		public Card(Ranks rank, Suits suit)
		{
			this.rank = rank;
			this.suit = suit;
		}

		public static Card Parse(String cardString)
		{
			Ranks rank;
			Suits suit;
			switch (cardString[0])
			{
				case '2':
					rank = Ranks.Two;
					break;
				case '3':
					rank = Ranks.Three;
					break;
				case '4':
					rank = Ranks.Four;
					break;
				case '5':
					rank = Ranks.Five;
					break;
				case '6':
					rank = Ranks.Six;
					break;
				case '7':
					rank = Ranks.Seven;
					break;
				case '8':
					rank = Ranks.Eight;
					break;
				case '9':
					rank = Ranks.Nine;
					break;
				case 'T':
					rank = Ranks.Ten;
					break;
				case 'J':
					rank = Ranks.Jack;
					break;
				case 'Q':
					rank = Ranks.Queen;
					break;
				case 'K':
					rank = Ranks.King;
					break;
				case 'A':
					rank = Ranks.Ace;
					break;
				case 't':
					rank = Ranks.Ten;
					break;
				case 'j':
					rank = Ranks.Jack;
					break;
				case 'q':
					rank = Ranks.Queen;
					break;
				case 'k':
					rank = Ranks.King;
					break;
				case 'a':
					rank = Ranks.Ace;
					break;
				default:
					throw new CardException(cardString);
			}
			switch (cardString[1])
			{
				case 'c':
					suit = Suits.Club;
					break;
				case 'd':
					suit = Suits.Diamond;
					break;
				case 'h':
					suit = Suits.Heart;
					break;
				case 's':
					suit = Suits.Spade;
					break;
				case 'C':
					suit = Suits.Club;
					break;
				case 'D':
					suit = Suits.Diamond;
					break;
				case 'H':
					suit = Suits.Heart;
					break;
				case 'S':
					suit = Suits.Spade;
					break;
				default:
					throw new CardException(cardString);
			}
			return new Card(rank, suit);
		}

		public int Rank
		{
			get { return (int)rank; }
		}

		public int Suit
		{
			get { return (int)suit; }
		}

		public override string ToString()
		{
			String rankString, suitString;
			switch (rank)
			{
				case Ranks.Ten:
					rankString = "T";
					break;
				case Ranks.Jack:
					rankString = "J";
					break;
				case Ranks.Queen:
					rankString = "Q";
					break;
				case Ranks.King:
					rankString = "K";
					break;
				case Ranks.Ace:
					rankString = "A";
					break;
				default:
					rankString = ((int)rank + 2).ToString();
					break;
			}
			switch (suit)
			{
				case Suits.Club:
					suitString = "c";
					break;
				case Suits.Diamond:
					suitString = "d";
					break;
				case Suits.Heart:
					suitString = "h";
					break;
				case Suits.Spade:
					suitString = "s";
					break;
				default:
					suitString = "";
					break;
			}
			return rankString + suitString;
		}

		public String ToString(bool showSuit)
		{
			String rankString; 
			switch (rank)
			{
				case Ranks.Ten:
					rankString = "T";
					break;
				case Ranks.Jack:
					rankString = "J";
					break;
				case Ranks.Queen:
					rankString = "Q";
					break;
				case Ranks.King:
					rankString = "K";
					break;
				case Ranks.Ace:
					rankString = "A";
					break;
				default:
					rankString = ((int)rank + 2).ToString();
					break;
			}
			if (showSuit)
			{
				String suitString;
				switch (suit)
				{
					case Suits.Club:
						suitString = "\u2663";
						break;
					case Suits.Diamond:
						suitString = "\u2666";
						break;
					case Suits.Heart:
						suitString = "\u2665";
						break;
					case Suits.Spade:
						suitString = "\u2660";
						break;
					default:
						suitString = "";
						break;
				}
				return rankString + suitString;
			}
			else
				return rankString;
		}

		public int CompareTo(object obj)
		{
			if (obj is Card)
			{
				Ranks sortRank = (Ranks)((Card)obj).Rank;
				return sortRank.CompareTo(rank);
			}
			else
				throw new ArgumentException("Object is not a card.");
		}
		
		public override bool Equals(object obj)
		{
			Card c = (Card)obj;
			if (suit == (Suits)c.Suit && rank == (Ranks)c.Rank)
				return true;
			return false;
		}

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }
	}
}
