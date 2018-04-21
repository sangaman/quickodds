using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace QuickOdds
{
    public class Cards
    {
        ArrayList cards;

        public Cards(int capacity)
        {
            cards = new ArrayList(capacity);
        }

        public Cards(ArrayList cards)
        {
            this.cards = cards;
        }

        public void Sort()
        {
            cards.Sort();
        }

        public void Add(Card card)
        {
            cards.Add(card);
        }

        public void Remove(Card card)
        {
            cards.Remove(card);
        }

        public bool Contains(Card card)
        {
            return cards.Contains(card);
        }

        public Cards GetRange(int index, int count)
        {
            return new Cards(cards.GetRange(index, count));
        }

        public IEnumerator GetEnumerator()
        {
            return cards.GetEnumerator();
        }

        public static Cards Parse(String cardsString)
        {
            ArrayList cards = new ArrayList();
            for(int n=0; n<cardsString.Length; n+=3)
            {
                cards.Add(Card.Parse(cardsString.Substring(n, 2)));
            }
            return new Cards(cards);
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

        public string ToString(bool showSuit)
        {
            String returnString = "";
            foreach (Card card in cards)
            {
                returnString += card.ToString(showSuit);
                if (showSuit)
                    returnString += " ";
            }
            return returnString.TrimEnd();
        }

        public Card GetCard(int index)
        {
            return (Card)cards[index];
        }

        public int Count
        {
            get { return cards.Count; }
        }
    }
}
