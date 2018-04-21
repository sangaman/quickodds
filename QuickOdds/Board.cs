using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace QuickOdds
{
	public class Board
	{
		Cards cards;
		
		public Board()
		{
			cards = new Cards(5);
		}

		public Board(Card c1, Card c2, Card c3, Card c4, Card c5)
		{
			cards = new Cards(5);
			cards.Add(c1);
			cards.Add(c2);
			cards.Add(c3);
			cards.Add(c4);
			cards.Add(c5);
		}

		public Board(Cards cards)
		{
			this.cards = cards;
		}

		public static Board Parse(String boardString)
		{
			Board board = new Board();
			for(int i=0; i<15; i+=3)
				board.Add(Card.Parse(boardString.Substring(i, 2)));

			return board;
		}

		public void Add(Card card)
		{
			cards.Add(card);
		}

		public void Remove(Card card)
		{
			this.cards.Remove(card);
		}

		public Cards Cards
		{
			get { return cards; }
			set { cards = value; }
		}

		public int NumCards
		{
			get { return cards.Count; }
		}

		public override string ToString()
		{
			String returnString = String.Empty;
			foreach (Card card in cards)
			{
				returnString = returnString + card.ToString() + " ";
			}
			return returnString.Trim();
		}

		public string ToString(bool showSuit)
		{
			String returnString = "";
			foreach (Card card in cards)
			{
				returnString += card.ToString(showSuit);
				if (showSuit)
					returnString += " ";
			}
			return returnString.Trim();
		}
	}
}

