using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections;
using QuickOdds;
using System.Windows.Forms;

namespace QuickOdds_Calculator
{
	static class Parser
	{
		public static Results ParseFromHH(String hh, bool completeSimulation, bool includeHero) 
		{
			Environment.CurrentDirectory = Path.GetDirectoryName(Application.ExecutablePath);

            Dictionary<String, IHand> hands = new Dictionary<String, IHand>();
			Board board = new Board();
			Dictionary<String, double> stackSizes = new Dictionary<String, double>();
            ArrayList seatsInHand = new ArrayList();
			Dictionary<String, String> playerNames = new Dictionary<String, String>();
			Dictionary<String, String> playerSeats = new Dictionary<String, String>();
			double totalBet = 0;
			Consts.GameTypes gameType;
			String heroName = String.Empty;
			String heroCards = String.Empty;

			StringReader reader = new StringReader(hh);
			try
			{
				double currentBet = 0;
				

				String nextLine = reader.ReadLine();
				while (nextLine == String.Empty)
					nextLine = reader.ReadLine();
				//determine Game
				if (nextLine.Contains("Omaha") || nextLine.Contains("OMAHA"))
				{
					if (nextLine.Contains("/L"))
						gameType = Consts.GameTypes.OmahaHiLo;
					else
						gameType = Consts.GameTypes.Omaha;
				}
                else if (nextLine.Contains("Hold") || nextLine.Contains("HOLD"))
                    gameType = Consts.GameTypes.Holdem;
                else
                {
                    //for Seals with Clubs, game type is on second line
                    nextLine = reader.ReadLine();
                    if (nextLine.Contains("Omaha"))
                    {
                        if (nextLine.Contains("Hi-Lo"))
                            gameType = Consts.GameTypes.OmahaHiLo;
                        else
                            gameType = Consts.GameTypes.Omaha;
                    }
                    else if (nextLine.Contains("Hold"))
                        gameType = Consts.GameTypes.Holdem;
                    else
                        throw new Exception("Could not detect valid game type.");
                }

				//start loop to read HH
				nextLine = reader.ReadLine();
				//loop to read players at table and stack sizes
				while (!nextLine.Contains("big blind"))
				{
					if(nextLine.StartsWith("Seat "))
					{
						String seatNumber;
						int nameStartIndex;
						if (nextLine.Contains(":"))
						{
							//Full Tilt, Stars, Seals
							seatNumber = nextLine.Substring(5, nextLine.IndexOf(':') - 5);
							nameStartIndex = nextLine.IndexOf(": ") + 2;
						}
						else
						{
							//AP
							seatNumber = nextLine.Substring(5, nextLine.IndexOf(" - ") - 5 );
							nameStartIndex = nextLine.IndexOf(" - ") + 3;
						}

						//this is first time we've seen "Seat ", so this is chip count and name
						int nameEndIndex = nextLine.IndexOf(" (", 8);
						String chipCount = nextLine.Substring(nameEndIndex + 2,
						nextLine.IndexOf(")", 10) - nextLine.IndexOf(" (", 8) - 2);
						chipCount = chipCount.Replace("$", "");
						if (chipCount.Contains(" "))
							chipCount = chipCount.Remove(chipCount.IndexOf(" "));
						stackSizes.Add(seatNumber, Double.Parse(chipCount));

						String name = nextLine.Substring(nameStartIndex, nameEndIndex - nameStartIndex);
						playerNames.Add(seatNumber, name);
                        playerSeats.Add(name, seatNumber);
					}
					nextLine = reader.ReadLine();
				}
				if (nextLine.Contains("big blind of ")) //Full Tilt
				{
					String bet = nextLine.Substring(nextLine.IndexOf("big blind of ") + "big blind of ".Length);
					bet = bet.Replace("$", "");
					double.TryParse(bet, out currentBet);
                    nextLine = reader.ReadLine();
				}
				else //Pokerstars, AP, Seals
				{
					String bet = nextLine.Substring(nextLine.IndexOf("big blind ") + "big blind ".Length);
					bet = bet.Replace("$", "");
					double.TryParse(bet, out currentBet);
                    nextLine = reader.ReadLine();
                    /*if (nextLine.Contains("sitout (wait for BB)"))
                    {
                        String sitoutName = nextLine.Substring(0, nextLine.IndexOf(" - "));
                        foreach (KeyValuePair<String, String> playerName in playerNames)
                            if (playerName.Value.Equals(sitoutName))
                                stackSizes.Remove(playerName.Key);
                        nextLine = reader.ReadLine();
                    }*/
				}

				//loop to read board and betting action
				while (!nextLine.StartsWith("Seat ") && !nextLine.StartsWith("** Pot"))
				{
					if (nextLine.Contains(" raises ") || nextLine.Contains(" Raise ") ||nextLine.Contains(" Raises "))
					{
						String bet = nextLine.Substring(nextLine.IndexOf(" to ") + 4);
						bet = bet.Replace("$", "");
                        if (bet.Contains(", and is"))
                            bet = bet.Remove(bet.IndexOf(", and is"));
                        else if (bet.Contains(" and is"))
                            bet = bet.Remove(bet.IndexOf(" and is"));
                        else if (bet.Contains("(All-in)")) //Seals
                            bet = bet.Remove(bet.IndexOf(" ( All-in)"));
						double.TryParse(bet, out currentBet);
					}
					else if (nextLine.Contains(" bets ") || nextLine.Contains(" Bets "))
					{
						String bet;
						if (nextLine.Contains(" bets "))
							bet = nextLine.Substring(nextLine.IndexOf(" bets ") + 6);
						else
							bet = nextLine.Substring(nextLine.IndexOf(" Bets ") + 6);

						bet = bet.Replace("$", "");
						if (bet.Contains(", and is"))
							bet = bet.Remove(bet.IndexOf(", and is"));
						else if (bet.Contains(" and is "))
                            bet = bet.Remove(bet.IndexOf(" and is"));
                        else if (bet.Contains("(All-in)")) //Seals
                            bet = bet.Remove(bet.IndexOf(" ( All-in)"));
						double.TryParse(bet, out currentBet);
					}
					else if (nextLine.Contains("All-In"))
					{
						String bet = nextLine.Substring(nextLine.IndexOf("All-In ") + 7);
						bet = bet.Replace("$", "");
                        double tempCurrentBet = currentBet;
                        if (!double.TryParse(bet, out currentBet))
                        {
                            if (nextLine.Contains("All-In(Raise) "))
                            {
                                bet = nextLine.Substring(nextLine.IndexOf("All-In(Raise) "));
                                bet = bet.Substring(bet.IndexOf("to ") + 3);
                                bet = bet.Replace("$", "");

                                double.TryParse(bet, out currentBet);
                            }
                        }
                        if (tempCurrentBet > currentBet)
                            currentBet = tempCurrentBet;
					}
					else if (nextLine.StartsWith("*** F"))
					{
                        nextLine = nextLine.Replace("10", "T");
                        board.Add(Card.Parse(nextLine.Substring(14, 2)));
						board.Add(Card.Parse(nextLine.Substring(17, 2)));
						board.Add(Card.Parse(nextLine.Substring(20, 2)));
						totalBet += currentBet;
						currentBet = 0;
					}
					else if (nextLine.StartsWith("*** T"))
					{
                        nextLine = nextLine.Replace("10", "T"); 
                        board.Add(Card.Parse(nextLine.Substring(25, 2)));
						totalBet += currentBet;
						currentBet = 0;
					}
					else if (nextLine.StartsWith("*** R"))
					{
                        nextLine = nextLine.Replace("10", "T");
                        board.Add(Card.Parse(nextLine.Substring(29, 2)));
						totalBet += currentBet;
						currentBet = 0;
					}
                    else if (nextLine.StartsWith("** F")) //Seals
                    {
                        board.Add(Card.Parse(nextLine.Substring(12, 2)));
                        board.Add(Card.Parse(nextLine.Substring(15, 2)));
                        board.Add(Card.Parse(nextLine.Substring(18, 2)));
                        totalBet += currentBet;
                        currentBet = 0;
                    }
                    else if (nextLine.StartsWith("** T")) //Seals
                    {
                        board.Add(Card.Parse(nextLine.Substring(12, 2)));
                        totalBet += currentBet;
                        currentBet = 0;
                    }
                    else if (nextLine.StartsWith("** R")) //Seals
                    {
                        board.Add(Card.Parse(nextLine.Substring(13, 2)));
                        totalBet += currentBet;
                        currentBet = 0;
                    }
					else if (nextLine.StartsWith("Uncalled bet "))
					{
						if (nextLine.Contains(" of "))
						{
							//for Full Tilt HH
							String returnedBet = nextLine.Substring(16, nextLine.IndexOf(" returned") - 16);
							returnedBet = returnedBet.Replace("$", "");
							currentBet -= Double.Parse(returnedBet);
						}
						else
						{
							//for PS HH
							String returnedBet = nextLine.Substring(14, nextLine.IndexOf(")") - 14);
							returnedBet = returnedBet.Replace("$", "");
							currentBet -= Double.Parse(returnedBet);
						}
					}
					else if (nextLine.Contains("returned ("))
					{
						//for AP HH
						int returnedBetStartIndex = nextLine.IndexOf("(") + 1;
						int returnedBetEndIndex = nextLine.IndexOf(")");
						String returnedBet = nextLine.Substring(returnedBetStartIndex, returnedBetEndIndex - returnedBetStartIndex);
						returnedBet = returnedBet.Replace("$", "");
						currentBet -= Double.Parse(returnedBet);
					}
                    else if (nextLine.Contains("refunded "))
                    {
                        //Seals
                        int returnedBetStartIndex = nextLine.IndexOf("refunded ") + "refunded ".Length;
                        String returnedBet = nextLine.Substring(returnedBetStartIndex);
                        currentBet -= Double.Parse(returnedBet);
                    }
					else if (nextLine.StartsWith("Dealt to "))
					{
						heroName = nextLine.Substring("Dealt to ".Length);
						heroCards = heroName.Substring(heroName.IndexOf(" [") + 2);
						heroName = heroName.Remove(heroName.IndexOf(" ["));
						heroCards = heroCards.Remove(heroCards.IndexOf(']'));
					}
					nextLine = reader.ReadLine();
				}
				totalBet += currentBet;

                if (nextLine.Contains("Seat "))
                {
                    //loop to read showdown
                    while (nextLine.StartsWith("Seat "))
                    {
                        String seatNumber = nextLine.Substring(5, nextLine.IndexOf(':') - 5);
                        try
                        {
                            //this is second time we've seen "Seat ", so this is showdown
                            if (playerNames[seatNumber].Equals(heroName) && includeHero)
                            {
                                stackSizes[seatNumber] = double.MaxValue;
                                if (gameType == Consts.GameTypes.Holdem)
                                    hands.Add(seatNumber, HoldemHand.Parse(heroCards));
                                else
                                    hands.Add(seatNumber, OmahaHand.Parse(heroCards));
                            }
                            //else if (nextLine.Contains("showed") || nextLine.Contains("mucked"))
                            else if (nextLine.Contains("["))
                            {
                                int index;
                                nextLine = nextLine.Replace("10", "T");
                                if (nextLine.Contains("[Mucked]"))
                                    index = nextLine.IndexOf("[", nextLine.IndexOf("[") + 1);
                                else
                                    index = nextLine.IndexOf("[");
                                if (gameType == Consts.GameTypes.Holdem)
                                    hands.Add(seatNumber, HoldemHand.Parse(nextLine.Substring(index + 1, 5)));
                                else
                                    hands.Add(seatNumber, OmahaHand.Parse(nextLine.Substring(index + 1, 11)));
                                seatsInHand.Add(seatNumber);
                            }
                            else
                                stackSizes.Remove(seatNumber);
                        }
                        catch (Exception)
                        {
                            //player might have sat in previous round and bought in this round
                            //this causes a bug in some FT HHs
                            nextLine = nextLine.Substring(nextLine.Substring(1).IndexOf("Seat") + 1);
                            nextLine = nextLine.Replace("10", "T");
                            int tempSeatNumber = Int32.Parse(seatNumber);
                            tempSeatNumber++;
                            seatNumber = tempSeatNumber.ToString();
                            if (nextLine.Contains("["))
                            {
                                int index = nextLine.IndexOf("[");
                                if (gameType == Consts.GameTypes.Holdem)
                                    hands.Add(seatNumber, HoldemHand.Parse(nextLine.Substring(index + 1, 5)));
                                else
                                    hands.Add(seatNumber, OmahaHand.Parse(nextLine.Substring(index + 1, 11)));
                            }
                            else
                                stackSizes.Remove(seatNumber);
                        }

                        nextLine = reader.ReadLine();
                        if (nextLine == null)
                            break;
                    }
                }
                else if (nextLine.StartsWith("** Pot"))
                {
                    //Seals, different showdown format
                    nextLine = reader.ReadLine();
                    
                    while (nextLine.Contains("shows"))
                    {
                        String playerName = nextLine.Substring(0, nextLine.IndexOf(" shows"));
                        String seatNumber = playerSeats[playerName];

                        if (playerName.Equals(heroName) && includeHero)
                        {
                            stackSizes[seatNumber] = double.MaxValue;
                            if (gameType == Consts.GameTypes.Holdem)
                                hands.Add(seatNumber, HoldemHand.Parse(heroCards));
                            else
                                hands.Add(seatNumber, OmahaHand.Parse(heroCards));
                        }
                        else if (nextLine.Contains("["))
                        {
                            int index = nextLine.IndexOf("[");
                            if (gameType == Consts.GameTypes.Holdem)
                                hands.Add(seatNumber, HoldemHand.Parse(nextLine.Substring(index + 1, 5)));
                            else
                                hands.Add(seatNumber, OmahaHand.Parse(nextLine.Substring(index + 1, 11)));
                            seatsInHand.Add(seatNumber);
                        }
                        nextLine = reader.ReadLine();
                    }
                }
			}
			catch (CardException e)
			{
				if(e.CardString.Equals("Do"))
					return new Results("Error parsing hand history. At least two shown hands are required for simulation.", hh);
				else
					return new Results("Error parsing hand history. Could not parse card '" + e.CardString + "'", hh);
			}
			catch (Exception)
			{
				return new Results("Error parsing hand history.", hh);
			}

			if (board.Cards.Count != 5)
				return new Results("Error parsing hand history. There must be five board cards.", hh);
			else if (hands.Count < 2)
				return new Results("Error parsing hand history. At least two shown hands are required for simulation.", hh);

			Deck deck = new Deck();
            ArrayList handsArrayList = new ArrayList(hands.Values);
            deck.RemoveHands(handsArrayList);
			Calculator calc = new Calculator(deck, handsArrayList, board, gameType);
			Results results = new Results(gameType, board, hh);

            if (hands.Count < stackSizes.Count)
            {
                ArrayList seatsToRemove = new ArrayList();
                foreach (KeyValuePair<String, double> stackSize in stackSizes)
                {
                    if (!seatsInHand.Contains(stackSize.Key))
                        seatsToRemove.Add(stackSize.Key);
                }
                foreach (String seatToRemove in seatsToRemove)
                {
                    stackSizes.Remove(seatToRemove);
                }
            }

			//create results summary
			String summary = "";
			double smallStack = totalBet;
			foreach (KeyValuePair<String, double> stackSize in stackSizes)
			{
				summary += ((IHand)hands[stackSize.Key]).Cards.ToString(false) + " v ";
                ((IHand)hands[stackSize.Key]).ChipCount = stackSize.Value;
                ((IHand)hands[stackSize.Key]).PlayerName = playerNames[stackSize.Key];
                if (((IHand)hands[stackSize.Key]).ChipCount < smallStack)
                    smallStack = ((IHand)hands[stackSize.Key]).ChipCount;
			}
			summary = summary.Remove(summary.Length - 3);
			results.Summary = summary;
			
			DateTime startTime = DateTime.Now;
			bool sidePot;
			do
			{
				sidePot = false;
				
				//Calculate Equities
				double[] preflopEquities = calc.CalculatePreflopEquity();
				double[] flopEquities = calc.CalculateFlopEquity();
				double[] turnEquities = calc.CalculateTurnEquity();
				double[] riverEquities = calc.CalculateRiverEquity();

				Simulation simulation = new Simulation((IHand[])handsArrayList.ToArray(typeof(IHand)), preflopEquities, flopEquities, turnEquities, riverEquities);

				results.AddSimulation(simulation);
				
				if (smallStack < (totalBet-.009))
				{
					double tempSmallStack = smallStack;
					ArrayList tempHands = (ArrayList)handsArrayList.Clone();
					smallStack = totalBet;
					foreach (IHand hand in tempHands)
					{
						if (hand.ChipCount == tempSmallStack)
						{
                            handsArrayList.Remove(hand);
							if(hands.Count > 1)
								sidePot = true;
						}
						else if (hand.ChipCount < smallStack)
							smallStack = hand.ChipCount;
					}
					calc.Deck = deck;
				}
			} while (sidePot && completeSimulation);
			
			TimeSpan totalTime = DateTime.Now - startTime;
			results.Time = totalTime.TotalSeconds;
			return results;
		}
	}
}
