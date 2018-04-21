using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace QuickOdds
{
	class Deck
	{
		ArrayList cards;
		
		public Deck()
		{
			cards = new ArrayList(52);
			for(byte rank = 0; rank<13; rank++)
				for(byte suit = 0; suit<4; suit++)
					cards.Add(new Card(rank, suit));
		}

		public void RemoveHands(ArrayList hands)
		{
			foreach (IHand hand in hands)
				foreach (Card card in hand.Cards)
					cards.Remove(card);
		}

		public void AddHands(ArrayList hands)
		{
			foreach (IHand hand in hands)
				foreach (Card card in hand.Cards)
					cards.Add(card);
		}

		public void RemoveCards(ArrayList cards)
		{
			foreach (Card card in cards)
				this.cards.Remove(card);
		}

		public void AddCards(ArrayList cards)
		{
			foreach (Card card in cards)
				this.cards.Add(card);
		}

		public Card GetCard(int index)
		{
			return (Card)cards[index];
		}

		public int NumCards
		{
			get { return cards.Count; }
		}

		public ArrayList Cards
		{
			get { return cards; }
		}

	}
}
