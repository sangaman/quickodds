using System;
using System.Collections.Generic;
using System.Text;

namespace QuickOdds
{
	public class CardException : Exception
	{
		private String cardString;
		public CardException(String cardString)
		{
			this.cardString = cardString;
		}

		public String CardString
		{
			get { return cardString; }
		}
	}
}
