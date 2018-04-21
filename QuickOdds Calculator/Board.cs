using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace QuickOdds
{
	class Board
	{
		ArrayList cards;
		
		public Board()
		{
			cards = new ArrayList(5);
		}

		public Board(Card c1, Card c2, Card c3, Card c4, Card c5)
		{
			cards = new ArrayList(5);
			cards.Add(c1);
			cards.Add(c2);
			cards.Add(c3);
			cards.Add(c4);
			cards.Add(c5);
		}

		public Board(ArrayList cards)
		{
			this.cards = cards;
		}

		public void Add(Card card)
		{
			cards.Add(card);
		}

		public void Remove(Card card)
		{
			this.cards.Remove(card);
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

		public int NumCards
		{
			get { return cards.Count; }
		}

		public override string ToString()
		{
			String returnString = "";
			foreach (Card card in cards)
			{
				returnString = returnString + card.ToString() + " ";
			}
			return returnString;
		}
	}
}

