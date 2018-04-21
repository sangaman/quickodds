using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Collections;
using System.Management;
using System.Security.Cryptography;
using System.Diagnostics;
using System.Xml;
using Microsoft.Win32;
using QuickOdds;


namespace QuickOdds_Calculator
{
	public partial class MainForm : Form
	{
		Thread calculateThread;
		Results results;
		String identificationCode;
		String registrationKey;
		int daysRemaining;
		RegistryKey reg;

		TextBox[] handTextBoxes;

        Results loadedHistoryResult;
		ArrayList historyResults;
        ArrayList savedHistoryFileNames;
        Dictionary<int, Results> savedHistoryResults;

		String hhToSimulate;

		public MainForm()
		{		
			InitializeComponent();


			handTextBoxes = new TextBox[9]{	handTextBox1, handTextBox2, handTextBox3, handTextBox4, handTextBox5,
								handTextBox6, handTextBox7, handTextBox8, handTextBox9 };

            loadedHistoryResult = null;
			historyResults = new ArrayList();
            savedHistoryFileNames = new ArrayList();
            savedHistoryResults = new Dictionary<int, Results>();

			daysRemaining = 0;

			analyzeFromHHButton.Text = "Analyze" + Environment.NewLine + "(Shift+Enter)";

			//Validate Software Registration
			bool isRegistered = false;

			identificationCode = String.Empty;
			registrationKey = String.Empty;

			String identifier = "QOCalc";
			
			ManagementClass mc = new System.Management.ManagementClass("Win32_Processor");
			System.Management.ManagementObjectCollection moc = mc.GetInstances();
			foreach (System.Management.ManagementObject mo in moc)
			{
				identifier += mo["ProcessorID"].ToString();
			}

			ManagementObject disk =
			new ManagementObject("win32_logicaldisk.deviceid=\"C:\"");
			disk.Get();
			identifier += disk["VolumeSerialNumber"].ToString();

			mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
			moc = mc.GetInstances();
			foreach (ManagementObject mo in moc)
			{
				if ((bool)mo["IPEnabled"] == true)
				{
					identifier += mo["MacAddress"].ToString().Replace(":", "");
					mo.Dispose();
					break;
				}
				mo.Dispose();
			}

			int identifierHash = identifier.GetHashCode();
			byte[] hashBytes = BitConverter.GetBytes(identifierHash);
			foreach (byte b in hashBytes)
			{
				identificationCode += String.Format("{0,2:X2}", b);
			}

			DESCryptoServiceProvider key = new DESCryptoServiceProvider();
			key.Key = ASCIIEncoding.ASCII.GetBytes("12664961");
			key.IV = ASCIIEncoding.ASCII.GetBytes("QODDCALC");

			MemoryStream ms = new MemoryStream();
			CryptoStream encStream = new CryptoStream(ms, key.CreateEncryptor(), CryptoStreamMode.Write);

			encStream.Write(hashBytes, 0, hashBytes.Length);
			encStream.FlushFinalBlock();

			byte[] encryptedBytes = ms.ToArray();

			encStream.Close();
			ms.Close();

			foreach (byte b in encryptedBytes)
			{
				registrationKey += String.Format("{0,2:X2}", b);
			}
			
			try
			{
				reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\QuickOdds", true);
				String registrationKeyInRegistry = reg.GetValue("RegistrationKey").ToString();

				if (registrationKeyInRegistry.Equals(registrationKey))
					isRegistered = true;
			}
			catch (Exception)
			{
                //key does not exist, therefore create registry data
                reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\", true);
				reg = reg.CreateSubKey("QuickOdds\\");
				reg.SetValue("RegistrationKey", "", RegistryValueKind.String);
				reg.SetValue("FirstRunDate", BitConverter.GetBytes((DateTime.Now.Ticks)));
			}
			if (!isRegistered)
			{
				DateTime firstRunDate;
				try
				{
					 firstRunDate = new DateTime(BitConverter.ToInt64((byte[])reg.GetValue("FirstRunDate"), 0));
				}
				catch (Exception)
				{
					firstRunDate = DateTime.MinValue;
				}
				if (firstRunDate < DateTime.Now.AddDays(-14) || firstRunDate > DateTime.Now)
				{
					//disable
					DisableButtons();
					resultsRichTextBox.Text += "TRIAL EXPIRED. Please register to continue using QuickOdds Calculator.";
				}
				else
				{
					TimeSpan timeRemaining = firstRunDate - DateTime.Now;
					daysRemaining = timeRemaining.Days + 14;
					resultsRichTextBox.Text += "TRIAL VERSION. You have " + daysRemaining + " days remaining.";
				}
			}
			
			SetRegistrationValues(isRegistered);

			try
			{
				Consts.BuildDictionaries();
			}
			catch
			{
				DisableButtons();
				resultsRichTextBox.Text = "Data files are corrupted, please reinstall QuickOdds.\n";
			}

            String[] historyFilenames = Directory.GetFiles("History\\");
            Array.Sort(historyFilenames);
            Array.Reverse(historyFilenames);
            foreach(String filename in historyFilenames)
            {
                FileInfo fileInfo = new FileInfo(filename);
                if (fileInfo.Length < 2048) //if filesize is too small to be valid
                    File.Delete(filename);

                else
                {
                    DateTime filenameDate;
                    String dateString = filename.Substring(8, 10);

                    if (DateTime.TryParse(dateString, out filenameDate))
                    {
                        if (filenameDate < DateTime.Today.AddDays(Properties.Settings.Default.SaveDays * -1.0))
                            File.Delete(filename);
                        else
                        {
                            savedHistoryFileNames.Add(filename);
                            previousSessionsTreeView.Nodes.Add(filenameDate.ToString("d"));
                        }
                    }
                    else
                        File.Delete(filename);
                }
            }

			showPlayerNamesCheckBox.Checked = Properties.Settings.Default.ShowPlayerNames;
			saveSimulationsCheckBox.Checked = Properties.Settings.Default.SaveSimulations;
            saveDaysTextBox.Text = Properties.Settings.Default.SaveDays.ToString();
			showTimeCheckBox.Checked = Properties.Settings.Default.ShowTime;
			gameComboBox.SelectedIndex = Properties.Settings.Default.GameType;
            historyShowPlayerNamesCheckBox.Checked = Properties.Settings.Default.HistoryShowPlayerNames;
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
            if (saveSimulationsCheckBox.Checked)
                saveHistory();
			Properties.Settings.Default.ShowTime = showTimeCheckBox.Checked;
			Properties.Settings.Default.SaveSimulations = saveSimulationsCheckBox.Checked;
            Properties.Settings.Default.SaveDays = Int32.Parse(saveDaysTextBox.Text);
			Properties.Settings.Default.ShowPlayerNames = showPlayerNamesCheckBox.Checked;
            Properties.Settings.Default.GameType = gameComboBox.SelectedIndex;
            Properties.Settings.Default.HistoryShowPlayerNames = historyShowPlayerNamesCheckBox.Checked;
            Properties.Settings.Default.Save();
		}

		#region Calculation
		private void calculateButton_Click(object sender, EventArgs e)
		{
			hhToSimulate = Clipboard.GetText();
			StartCalculation();
		}

		private void StartCalculation()
		{
			mainTabControl.SelectedIndex = 0;
			
			resultsRichTextBox.Text = "Calculating...";
			DisableButtons();

			hhRichTextBox.Text = hhToSimulate;
			object[] parameters = new object[2];
			parameters[0] = hhRichTextBox.Text;
			parameters[1] = includeHeroCheckBox.Checked;
			calculateThread = new Thread(Calculate);
			calculateThread.IsBackground = true;
			calculateThread.Start(parameters);
		}

		private void Calculate(object param)
		{
			object[] parameters = (object[])param;
			CompleteCalculationDelegate del = new CompleteCalculationDelegate(CompleteCalculation);
			resultsRichTextBox.Invoke(del, new object[] { Parser.ParseFromHH((String)parameters[0], true, (bool)parameters[1]) });
		}

		delegate void CompleteCalculationDelegate(Results results);
		private void CompleteCalculation(Results results)
		{
			this.results = results;

			EnableButtons();
			DisplayResults();

			if(!results.Error)
				addHistoryResults(results);
		}
		#endregion

		private void DisableButtons()
		{
			calculateButton.Enabled = false;
			analyzeFromHHButton.Enabled = false;
			clearButton.Enabled = false;
			hhRichTextBox.ReadOnly = true;
			boardTextBox.ReadOnly = true;

			clearAnalyzerButton.Enabled = false;
			simulateButton.Enabled = false;
			gameComboBox.Enabled = false;
			foreach (TextBox handTextBox in handTextBoxes)
			{
				handTextBox.ReadOnly = true;
			}
			deadCardsTextBox.ReadOnly = true;

			historyListBox.Enabled = false;
			historyTabControl.Enabled = false;
		}

		private void EnableButtons()
		{
			calculateButton.Enabled = true;
			analyzeFromHHButton.Enabled = true;
			clearButton.Enabled = true;
			hhRichTextBox.ReadOnly = false;
			boardTextBox.ReadOnly = false;

			clearAnalyzerButton.Enabled = true;
			simulateButton.Enabled = true;
			gameComboBox.Enabled = true;
			foreach (TextBox handTextBox in handTextBoxes)
			{
				handTextBox.ReadOnly = false;
			}
			deadCardsTextBox.ReadOnly = false;

			historyListBox.Enabled = true;
			historyTabControl.Enabled = true;
		}

		public static void ColorSuits(RichTextBox richTextBox)
		{
			for (int n = 0; n < richTextBox.Text.Length; n++)
			{
				int c = (int)richTextBox.Text[n];
				if (c > 9823 && c < 9831)
				{
					richTextBox.Select(n, 1);
					richTextBox.SelectionFont = new Font("Arial", 10);
					switch (c)
					{
						case 9830:
							richTextBox.SelectionColor = Color.Blue;
							break;
						case 9829:
							richTextBox.SelectionColor = Color.Red;
							break;
						case 9827:
							richTextBox.SelectionColor = Color.Green;
							break;
						default:
							break;
					}
				}
			}
		}

		private void DisplayResults()
		{
			if (results == null)
				return;
			resultsRichTextBox.Text = results.ShowResults(showPlayerNamesCheckBox.Checked, showTimeCheckBox.Checked);
			ColorSuits(resultsRichTextBox);
		}

		private void clearButton_Click(object sender, EventArgs e)
		{
			resultsRichTextBox.ResetText();
			hhRichTextBox.ResetText();
			results = null;
		}

		private void showPlayerNamesCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			DisplayResults();
		}

		private void showTimeCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			DisplayResults();
		}

		private void includeHeroCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (results != null)
			{
				//redo calculation
				StartCalculation();
			}
		}

		private void MainForm_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && e.KeyValue == 13)
			{
				hhToSimulate = Clipboard.GetText();
				StartCalculation();
			}
			else if (e.Shift && e.KeyValue == 13)
			{
				hhToSimulate = Clipboard.GetText();
				BeginSimulationFromHH();
			}
		}

		#region Analyzer
		private void clearAnalyzerButton_Click(object sender, EventArgs e)
		{
			ClearAnalyzer();
		}

		private void handTextBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && e.KeyValue.Equals(Keys.A))
				((TextBox)sender).SelectAll();
		}

		private void ClearAnalyzer()
		{
			foreach(TextBox handTextBox in handTextBoxes)
			{
				handTextBox.Clear();
			}

			boardTextBox.Clear();
			deadCardsTextBox.Clear();
			analyzerResultsRichTextBox.Clear();
		}

		private void simulateFromHHButton_Click(object sender, EventArgs e)
		{
			hhToSimulate = Clipboard.GetText();
			BeginSimulationFromHH();
		}

		private void BeginSimulationFromHH()
		{
			mainTabControl.SelectedIndex = 1;

			ClearAnalyzer();
			analyzerResultsRichTextBox.Text = "Simulating...";
			DisableButtons();

			object[] parameters = new object[2];
			parameters[0] = hhToSimulate;
			parameters[1] = includeHeroCheckBox.Checked;
			calculateThread = new Thread(SimulateFromHH);
			calculateThread.IsBackground = true;
			calculateThread.Start(parameters);
		}

		private void simulateButton_Click(object sender, EventArgs e)
		{
			analyzerResultsRichTextBox.Text = "Simulating...";
			DisableButtons();

			object[] parameters = new object[4];

			Consts.GameTypes gameType = (Consts.GameTypes)gameComboBox.SelectedIndex;
			parameters[0] = gameType;

			ArrayList hands = new ArrayList();
			try
			{
				foreach (TextBox handTextBox in handTextBoxes)
				{
					if (handTextBox.Text.Length>0)
					{
						String handText = handTextBox.Text.Replace(" ", "");
						if (gameType == Consts.GameTypes.Holdem)
						{
							hands.Add(new HoldemHand(Card.Parse(handText.Substring(0, 2)),
														Card.Parse(handText.Substring(2, 2))));
						}
						else
						{
							hands.Add(new OmahaHand(Card.Parse(handText.Substring(0, 2)),
														Card.Parse(handText.Substring(2, 2)),
														Card.Parse(handText.Substring(4, 2)),
														Card.Parse(handText.Substring(6, 2))));
						}
					}
				}
			}
			catch (Exception)
			{
				analyzerResultsRichTextBox.Text = "Error parsing hands.";
				EnableButtons();
				return;
			}
			parameters[1] = hands;

			Board board = new Board();
			try
			{
				String boardText = boardTextBox.Text.Replace(" ", "");
				for (int n = 0; n < boardText.Length - 1; n += 2)
				{
					board.Add(Card.Parse(boardText.Substring(n, 2)));
				}
			}
			catch (Exception)
			{
				analyzerResultsRichTextBox.Text = "Error parsing board.";
				EnableButtons();
				return;
			}
			if ((board.Cards.Count < 3 && board.Cards.Count > 0) || board.Cards.Count > 5)
			{
				analyzerResultsRichTextBox.Text = "Error parsing board. Invalid number of cards.";
				EnableButtons();
				return;
			}
			parameters[2] = board;

			ArrayList deadCards = new ArrayList();
			try
			{
				String deadCardsText = deadCardsTextBox.Text.Replace(" ", "");
				for (int n = 0; n < deadCardsText.Length - 1; n += 2)
				{
					deadCards.Add(Card.Parse(deadCardsText.Substring(n, 2)));
				}
			}
			catch (Exception)
			{
				analyzerResultsRichTextBox.Text = "Error parsing dead cards.";
				EnableButtons();
				return;
			}


			Deck deck = new Deck();
			foreach (IHand hand in hands)
			{
				foreach (Card card in hand.Cards)
				{
					if (deck.Cards.Contains(card))
						deck.Cards.Remove(card);
					else
					{
						analyzerResultsRichTextBox.Text = "Error. Duplicate cards detected in simulation.";
						EnableButtons();
						return;
					}
				}
			}
			foreach (Card card in deadCards)
			{
				if (deck.Cards.Contains(card))
					deck.Cards.Remove(card);
				else
				{
					analyzerResultsRichTextBox.Text = "Error. Duplicate cards detected in simulation.";
					EnableButtons();
					return;
				}
			}
			foreach (Card card in board.Cards)
			{
				if (deck.Cards.Contains(card))
					deck.Cards.Remove(card);
				else
				{
					analyzerResultsRichTextBox.Text = "Error. Duplicate cards detected in simulation.";
					EnableButtons();
					return;
				}
			}
			foreach (Card card in board.Cards)
			{
				deck.Cards.Add(card);
			}
			parameters[3] = deck;

			calculateThread = new Thread(SimulateFromAnalyzer);
			calculateThread.IsBackground = true;
			calculateThread.Start(parameters);
		}

		private void SimulateFromHH(object param)
		{
			object[] parameters = (object[])param;
			CompleteSimulationDelegate del = new CompleteSimulationDelegate(CompleteSimulationFromHH);
			analyzerResultsRichTextBox.Invoke(del, new object[] { Parser.ParseFromHH((String)parameters[0], false, (bool)parameters[1]) });
		}					

		private void SimulateFromAnalyzer(object param)
		{
			object[] parameters = (object[])param;

			Consts.GameTypes gameType = (Consts.GameTypes)parameters[0];
			ArrayList hands = (ArrayList)parameters[1];
			Board board = (Board)parameters[2];
			Deck deck = (Deck)parameters[3];

			Calculator calc = new Calculator(deck, hands, board, gameType);

			//Calculate Equities
			double[] preflopEquities = null;
			double[] flopEquities = null;
			double[] turnEquities = null;
			double[] riverEquities = null;

			preflopEquities = calc.CalculatePreflopEquity();
			if(board.Cards.Count > 2)
				flopEquities = calc.CalculateFlopEquity();
			if(board.Cards.Count > 3)
				turnEquities = calc.CalculateTurnEquity();
			if(board.Cards.Count > 4)
				riverEquities = calc.CalculateRiverEquity();

			Simulation simulation = new Simulation((IHand[])hands.ToArray(typeof(IHand)), preflopEquities, flopEquities, turnEquities, riverEquities);

			Results tempResults = new Results(gameType, board, "");
			tempResults.AddSimulation(simulation);

			CompleteSimulationDelegate del = new CompleteSimulationDelegate(CompleteSimulationFromAnalyzer);
			analyzerResultsRichTextBox.Invoke(del, new object[] { tempResults });
		}

		delegate void CompleteSimulationDelegate(Results results);
		private void CompleteSimulationFromHH(Results results)
		{
			EnableButtons();

			if (results.Board != null)
			{
				boardTextBox.Text = results.Board.ToString();
				gameComboBox.SelectedIndex = (int)results.GameType;
			}
			

			analyzerResultsRichTextBox.Text = results.ShowResults(false, false);
			ColorSuits(analyzerResultsRichTextBox);

			Simulation sim;
			if (results.Simulations != null)
				sim = (Simulation)results.Simulations[0];
			else
				return;
			int i = 0;
			if (sim.Cards.Length > 9)
			{
				analyzerResultsRichTextBox.Text += "\n\nError: Maximum of nine hands at showdown exceeded.";
				return;
			}
			foreach (Cards card in sim.Cards)
			{
				handTextBoxes[i].Text = card.ToString();
				i++;
			}
		}
		private void CompleteSimulationFromAnalyzer(Results results)
		{
			EnableButtons();

			analyzerResultsRichTextBox.Text = results.ShowResults(false, false);
			ColorSuits(analyzerResultsRichTextBox);
		}
#endregion

#region History
		private void historyListBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (historyListBox.Items.Count == 0 || historyListBox.SelectedIndex == -1)
			{
				historyHHRichTextBox.Clear();
				historyResultsRichTextBox.Clear();
				return;
			}
			displayHistoryResults((Results)historyResults[historyListBox.SelectedIndex]);
		}

        private void displayHistoryResults(Results result)
        {
            loadedHistoryResult = result;

            historyResultsRichTextBox.Text = result.ShowResults(historyShowPlayerNamesCheckBox.Checked, false);
            ColorSuits(historyResultsRichTextBox);
            historyHHRichTextBox.Text = result.HandHistory;

            if (!historyAnalyzeButton.Enabled)
                historyAnalyzeButton.Enabled = true;
        }

		private void historyListBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == Keys.Delete)
			{
                historyListBoxDelete();
			}
		}

        private void historyDeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            historyListBoxDelete();
        }

        void historyListBoxDelete()
        {
            if (historyListBox.SelectedIndex < 0)
					return;
            historyResults.RemoveAt(historyListBox.SelectedIndex);
            historyListBox.Items.RemoveAt(historyListBox.SelectedIndex);
        }

        private void previousSessionsTreeView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
            {
                previousSessionsTreeViewDelete();
            }
        }


        private void previousSessionsDeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            previousSessionsTreeViewDelete();
        }

        private void previousSessionsTreeViewDelete()
        {
            if (previousSessionsTreeView.SelectedNode == null)
                return;
            if (previousSessionsTreeView.SelectedNode.Text.Equals(String.Empty))
                return;
            if (previousSessionsTreeView.SelectedNode.Parent != null)
            {
                XmlDocument doc = new XmlDocument();
                doc.Load((String)savedHistoryFileNames[previousSessionsTreeView.SelectedNode.Parent.Index]);

                XmlNode results = doc.FirstChild.NextSibling;
                XmlNode resultToDelete = results.FirstChild;
                for (int n = 0; n < previousSessionsTreeView.SelectedNode.Index; n++)
                    resultToDelete = resultToDelete.NextSibling;

                results.RemoveChild(resultToDelete);

                doc.Save((String)savedHistoryFileNames[previousSessionsTreeView.SelectedNode.Parent.Index]);

                previousSessionsTreeView.SelectedNode.Text = String.Empty;
            }
            else
            {
                File.Delete((String)savedHistoryFileNames[previousSessionsTreeView.SelectedNode.Index]);
                previousSessionsTreeView.SelectedNode.Text = String.Empty;
                previousSessionsTreeView.SelectedNode.Nodes.Clear();
            }
            
        }

		private void addHistoryResults(Results results)
		{
			historyResults.Insert(0, results);
			historyListBox.Items.Insert(0, results.ToString());
		}

		private void historyAnalyzeButton_Click(object sender, EventArgs e)
		{
			hhToSimulate = historyHHRichTextBox.Text;
			BeginSimulationFromHH();
		}

		private void historyShowPlayerNamesCheckBox_CheckedChanged(object sender, EventArgs e)
		{
            if (loadedHistoryResult != null)
            {
                historyResultsRichTextBox.Text = loadedHistoryResult.ShowResults(historyShowPlayerNamesCheckBox.Checked, false);
                ColorSuits(historyResultsRichTextBox);
            }
		}


        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switch (mainTabControl.SelectedIndex)
            {
                case 0:
                    if (!String.IsNullOrEmpty(resultsRichTextBox.Text))
                        Clipboard.SetText(resultsRichTextBox.Text);
                    break;
                case 1:
                    if (!String.IsNullOrEmpty(analyzerResultsRichTextBox.Text))
                        Clipboard.SetText(analyzerResultsRichTextBox.Text);
                    break;
                case 2:
                    if (!String.IsNullOrEmpty(historyResultsRichTextBox.Text))
                        Clipboard.SetText(historyResultsRichTextBox.Text);
                    break;
                default:
                    break;
            }
        }

        private void historyListBox_MouseDown(object sender, MouseEventArgs e)
        {
            int indexOver = historyListBox.IndexFromPoint(e.X, e.Y);
            if (indexOver >= 0 && indexOver < historyListBox.Items.Count)
            {
                historyListBox.SelectedIndex = indexOver;
            }
            historyListBox.Refresh();
        }

        private void previousSessionsTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Parent != null)
            {
                int key = e.Node.Index + (e.Node.Parent.Index<<16);
                displayHistoryResults(savedHistoryResults[key]);
            }
            else if (e.Node.Nodes.Count == 0 && !e.Node.Text.Equals(String.Empty))
            {
                XmlTextReader reader = new XmlTextReader((String)savedHistoryFileNames[e.Node.Index]);
                reader.WhitespaceHandling = WhitespaceHandling.None;
                reader.Read(); reader.Read();
                if (!reader.Name.Equals("session"))
                    return; //invalid file
                reader.Read();
                int index = 0;
                while (reader.Name.Equals("result"))
                {
                    reader.MoveToNextAttribute();
                    String summary = reader.Value;
                    reader.Read();
                    Board board = Board.Parse(reader.ReadElementContentAsString());
                    double time = reader.ReadElementContentAsDouble();
                    Consts.GameTypes gameType = (Consts.GameTypes)Enum.Parse(typeof(Consts.GameTypes), reader.ReadElementContentAsString());
                    reader.Read();

                    ArrayList simulations = new ArrayList();
                    while (reader.Name.Equals("simulation"))
                    {
                        ArrayList cards = new ArrayList();
                        ArrayList playerNames = new ArrayList();
                        ArrayList preflopEquities = new ArrayList();
                        ArrayList flopEquities = new ArrayList();
                        ArrayList turnEquities = new ArrayList();
                        ArrayList riverEquities = new ArrayList();
                        reader.Read();
                        while (reader.Name.Equals("hand"))
                        {
                            reader.MoveToNextAttribute();
                            cards.Add(Cards.Parse(reader.Value));
                            reader.MoveToNextAttribute();
                            playerNames.Add(reader.Value);
                            reader.Read();
                            preflopEquities.Add(reader.ReadElementContentAsDouble());
                            flopEquities.Add(reader.ReadElementContentAsDouble());
                            turnEquities.Add(reader.ReadElementContentAsDouble());
                            riverEquities.Add(reader.ReadElementContentAsDouble());
                            reader.Read();
                        }
                        simulations.Add(new Simulation( (Cards[])cards.ToArray(typeof(Cards)), (String[])playerNames.ToArray(typeof(String)),
                                                        (double[])preflopEquities.ToArray(typeof(double)),
                                                        (double[])flopEquities.ToArray(typeof(double)), 
                                                        (double[])turnEquities.ToArray(typeof(double)),
                                                        (double[])riverEquities.ToArray(typeof(double))));
                        reader.Read();
                    }
                    reader.Read();
                    String handHistory = reader.ReadElementContentAsString();

                    Results result = new Results(gameType, board, handHistory);
                    foreach (Simulation simulation in simulations)
                    {
                        result.AddSimulation(simulation);
                    }
                    result.Time = time;
                    result.Summary = summary;

                    int key = index + (e.Node.Index << 16);
                    savedHistoryResults[key] = result;
                    e.Node.Nodes.Add(result.Summary);
                    reader.Read();
                    index++;
                }
                e.Node.Expand();
                reader.Close();
            }
        }


        private void previousSessionsTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Parent != null)
            {
                historyTabControl.SelectedIndex = 0;
            }
        }

        private void saveHistory()
        {
            XmlDocument doc = new XmlDocument();
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "utf-16", "yes");
            doc.AppendChild(dec);
            XmlElement root = doc.CreateElement("session");
            doc.AppendChild(root);

            foreach(Results result in historyResults)
            {
                XmlElement child = doc.CreateElement("result");
                child.SetAttribute("summary", result.Summary);
                XmlElement board = doc.CreateElement("board");
                board.InnerText = result.Board.ToString();
                XmlElement time = doc.CreateElement("time");
                time.InnerText = result.Time.ToString();
                XmlElement gameType = doc.CreateElement("gametype");
                gameType.InnerText = result.GameType.ToString();
                XmlElement simulations = doc.CreateElement("simulations");
                foreach (Simulation simulation in result.Simulations)
                {
                    XmlElement simulationsChild = doc.CreateElement("simulation");
                    for (int n = 0; n < simulation.NumHands; n++)
                    {
                        XmlElement hand = doc.CreateElement("hand");
                        
                        hand.SetAttribute("cards", simulation.Cards[n].ToString());
                        hand.SetAttribute("playername", simulation.PlayerNames[n]);

                        XmlElement preflopEquities = doc.CreateElement("preflopequities");
                        preflopEquities.InnerText = simulation.PreflopEquities[n].ToString();
                        XmlElement flopEquities = doc.CreateElement("flopequities");
                        flopEquities.InnerText = simulation.FlopEquities[n].ToString();
                        XmlElement turnEquities = doc.CreateElement("turnequities");
                        turnEquities.InnerText = simulation.TurnEquities[n].ToString();
                        XmlElement riverEquities = doc.CreateElement("riverequities");
                        riverEquities.InnerText = simulation.RiverEquities[n].ToString();

                        hand.AppendChild(preflopEquities);
                        hand.AppendChild(flopEquities);
                        hand.AppendChild(turnEquities);
                        hand.AppendChild(riverEquities);

                        simulationsChild.AppendChild(hand);
                    }
                    simulations.AppendChild(simulationsChild);
                }
                XmlElement handHistory = doc.CreateElement("handhistory");
                handHistory.InnerText = result.HandHistory;

                child.AppendChild(board);
                child.AppendChild(time);
                child.AppendChild(gameType);
                child.AppendChild(simulations);
                child.AppendChild(handHistory);

                root.AppendChild(child);
            }
            if (historyResults.Count > 0)
            {
                String path = "History\\" + DateTime.Now.ToString("s") + ".xml";
                path = path.Replace(':', '+');
                doc.Save(path);
            }
        }
#endregion


#region About
		private void registrationKeyTextBox_TextChanged(object sender, EventArgs e)
		{
			if(registrationKeyTextBox.Text.Equals(String.Empty))
				registerButton.Enabled = false;
			else
				registerButton.Enabled = true;
		}

		public void SetRegistrationValues(bool isRegistered)
		{
			if (isRegistered)
			{
				versionLabel.Text = "Registered Version " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
				daysRemainingLabel.Visible=false;
				notificationLabel.Text = "Thank you for registering!";
				registrationKeyTextBox.Text = registrationKey;
				registrationKeyTextBox.ReadOnly = true;
				registerButton.Enabled = false;
			}
			else
			{
				versionLabel.Text = "Trial Version" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
				if (daysRemaining > 0)
				{
					daysRemainingLabel.Text = "You have " + daysRemaining + " days remaining during the trial period.";
					notificationLabel.Text = "To continue using the software after this time";
				}
				else
				{
					daysRemainingLabel.Text = "The trial period has ended. To continue using the software";
					notificationLabel.Text = "To continue using the software";
				}
				notificationLabel.Text += " you must purchase a registration key through the website and enter it below.";

			}
			identificationCodeTextBox.Text = identificationCode;
		}

		private void registerButton_Click(object sender, EventArgs e)
		{
			MessageBox messageBox = new MessageBox();
			Point p = new Point(this.Location.X + (this.Width - messageBox.Width) / 2,
								this.Location.Y + (this.Height - messageBox.Height) / 2);
			messageBox.Location = p;

			if (registrationKey.Equals(registrationKeyTextBox.Text))
			{
				try
				{
					RegistryKey reg = Registry.CurrentUser;
					reg = reg.OpenSubKey("SOFTWARE\\QuickOdds", true);
					reg.SetValue("RegistrationKey", registrationKey);

					messageBox.setMessage("Success", "Registration successful!");
					messageBox.Show(this);

					//Register the software
					EnableButtons();
					if(results==null)
                        resultsRichTextBox.Clear();

					SetRegistrationValues(true);
				}
				catch (Exception)
				{
					messageBox.setMessage("Error", "Error registering software.");
					messageBox.Show(this); 
					
					registrationKeyTextBox.Clear();
				}
			}
			else
			{
				messageBox.setMessage("Error", "Registration key is incorrect.");
				messageBox.Show(this); 
				
				registrationKeyTextBox.Clear();
			}
		}

		private void webLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start("http://www.quickodds.net/");
		}

		private void emailLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start("mailto:support@quickodds.net");
		}
#endregion	

        private void previousSessionsTreeView_MouseDown(object sender, MouseEventArgs e)
        {
            TreeNode treeNodeOver = previousSessionsTreeView.GetNodeAt(e.X, e.Y);
            if (treeNodeOver != null)// && indexover < historyListBox.Items.Count)
            {
                previousSessionsTreeView.SelectedNode = treeNodeOver;
            }
            previousSessionsTreeView.Refresh();
        }
	}
}