namespace QuickOdds_Calculator
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.calculatorTab = new System.Windows.Forms.TabPage();
            this.includeHeroCheckBox = new System.Windows.Forms.CheckBox();
            this.showPlayerNamesCheckBox = new System.Windows.Forms.CheckBox();
            this.showTimeCheckBox = new System.Windows.Forms.CheckBox();
            this.analyzeFromHHButton = new System.Windows.Forms.Button();
            this.clearButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.calculateButton = new System.Windows.Forms.Button();
            this.resultsRichTextBox = new System.Windows.Forms.RichTextBox();
            this.copyContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hhRichTextBox = new System.Windows.Forms.RichTextBox();
            this.analyzerTab = new System.Windows.Forms.TabPage();
            this.deadCardsTextBox = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.Label112 = new System.Windows.Forms.Label();
            this.clearAnalyzerButton = new System.Windows.Forms.Button();
            this.simulateButton = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.boardTextBox = new System.Windows.Forms.TextBox();
            this.analyzerResultsRichTextBox = new System.Windows.Forms.RichTextBox();
            this.handTextBox9 = new System.Windows.Forms.TextBox();
            this.handTextBox8 = new System.Windows.Forms.TextBox();
            this.handTextBox7 = new System.Windows.Forms.TextBox();
            this.handTextBox6 = new System.Windows.Forms.TextBox();
            this.handTextBox5 = new System.Windows.Forms.TextBox();
            this.handTextBox4 = new System.Windows.Forms.TextBox();
            this.handTextBox3 = new System.Windows.Forms.TextBox();
            this.handTextBox2 = new System.Windows.Forms.TextBox();
            this.handTextBox1 = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.gameComboBox = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.historyTab = new System.Windows.Forms.TabPage();
            this.historyShowPlayerNamesCheckBox = new System.Windows.Forms.CheckBox();
            this.historyTabControl = new System.Windows.Forms.TabControl();
            this.resultsTabPage = new System.Windows.Forms.TabPage();
            this.historyResultsRichTextBox = new System.Windows.Forms.RichTextBox();
            this.hhTabPage = new System.Windows.Forms.TabPage();
            this.historyHHRichTextBox = new System.Windows.Forms.RichTextBox();
            this.previousSessionsTabPage = new System.Windows.Forms.TabPage();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.saveDaysTextBox = new System.Windows.Forms.TextBox();
            this.saveSimulationsCheckBox = new System.Windows.Forms.CheckBox();
            this.previousSessionsTreeView = new System.Windows.Forms.TreeView();
            this.previousSessionsContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.previousSessionsDeleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label17 = new System.Windows.Forms.Label();
            this.historyListBox = new System.Windows.Forms.ListBox();
            this.historyContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.historyDeleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.historyAnalyzeButton = new System.Windows.Forms.Button();
            this.aboutTab = new System.Windows.Forms.TabPage();
            this.daysRemainingLabel = new System.Windows.Forms.Label();
            this.webLabel = new System.Windows.Forms.LinkLabel();
            this.emailLabel = new System.Windows.Forms.LinkLabel();
            this.identificationCodeTextBox = new System.Windows.Forms.TextBox();
            this.registerButton = new System.Windows.Forms.Button();
            this.registrationKeyTextBox = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.notificationLabel = new System.Windows.Forms.Label();
            this.nameLabel = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.versionLabel = new System.Windows.Forms.Label();
            this.mainTabControl.SuspendLayout();
            this.calculatorTab.SuspendLayout();
            this.copyContextMenuStrip.SuspendLayout();
            this.analyzerTab.SuspendLayout();
            this.historyTab.SuspendLayout();
            this.historyTabControl.SuspendLayout();
            this.resultsTabPage.SuspendLayout();
            this.hhTabPage.SuspendLayout();
            this.previousSessionsTabPage.SuspendLayout();
            this.previousSessionsContextMenuStrip.SuspendLayout();
            this.historyContextMenuStrip.SuspendLayout();
            this.aboutTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // mainTabControl
            // 
            this.mainTabControl.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.mainTabControl.Controls.Add(this.calculatorTab);
            this.mainTabControl.Controls.Add(this.analyzerTab);
            this.mainTabControl.Controls.Add(this.historyTab);
            this.mainTabControl.Controls.Add(this.aboutTab);
            this.mainTabControl.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mainTabControl.Location = new System.Drawing.Point(0, 2);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(555, 470);
            this.mainTabControl.TabIndex = 17;
            // 
            // calculatorTab
            // 
            this.calculatorTab.BackColor = System.Drawing.Color.Beige;
            this.calculatorTab.Controls.Add(this.includeHeroCheckBox);
            this.calculatorTab.Controls.Add(this.showPlayerNamesCheckBox);
            this.calculatorTab.Controls.Add(this.showTimeCheckBox);
            this.calculatorTab.Controls.Add(this.analyzeFromHHButton);
            this.calculatorTab.Controls.Add(this.clearButton);
            this.calculatorTab.Controls.Add(this.label1);
            this.calculatorTab.Controls.Add(this.label2);
            this.calculatorTab.Controls.Add(this.calculateButton);
            this.calculatorTab.Controls.Add(this.resultsRichTextBox);
            this.calculatorTab.Controls.Add(this.hhRichTextBox);
            this.calculatorTab.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.calculatorTab.Location = new System.Drawing.Point(4, 30);
            this.calculatorTab.Name = "calculatorTab";
            this.calculatorTab.Padding = new System.Windows.Forms.Padding(3);
            this.calculatorTab.Size = new System.Drawing.Size(547, 436);
            this.calculatorTab.TabIndex = 0;
            this.calculatorTab.Text = "Calculator";
            // 
            // includeHeroCheckBox
            // 
            this.includeHeroCheckBox.Location = new System.Drawing.Point(418, 318);
            this.includeHeroCheckBox.Name = "includeHeroCheckBox";
            this.includeHeroCheckBox.Size = new System.Drawing.Size(121, 38);
            this.includeHeroCheckBox.TabIndex = 27;
            this.includeHeroCheckBox.Text = "Include Hero in Simulation";
            this.includeHeroCheckBox.UseVisualStyleBackColor = true;
            this.includeHeroCheckBox.CheckedChanged += new System.EventHandler(this.includeHeroCheckBox_CheckedChanged);
            // 
            // showPlayerNamesCheckBox
            // 
            this.showPlayerNamesCheckBox.Location = new System.Drawing.Point(417, 362);
            this.showPlayerNamesCheckBox.Name = "showPlayerNamesCheckBox";
            this.showPlayerNamesCheckBox.Size = new System.Drawing.Size(122, 38);
            this.showPlayerNamesCheckBox.TabIndex = 26;
            this.showPlayerNamesCheckBox.Text = "Show Player Names";
            this.showPlayerNamesCheckBox.UseVisualStyleBackColor = true;
            this.showPlayerNamesCheckBox.CheckedChanged += new System.EventHandler(this.showPlayerNamesCheckBox_CheckedChanged);
            // 
            // showTimeCheckBox
            // 
            this.showTimeCheckBox.AutoSize = true;
            this.showTimeCheckBox.Location = new System.Drawing.Point(417, 406);
            this.showTimeCheckBox.Name = "showTimeCheckBox";
            this.showTimeCheckBox.Size = new System.Drawing.Size(99, 21);
            this.showTimeCheckBox.TabIndex = 25;
            this.showTimeCheckBox.Text = "Show Time";
            this.showTimeCheckBox.UseVisualStyleBackColor = true;
            this.showTimeCheckBox.CheckedChanged += new System.EventHandler(this.showTimeCheckBox_CheckedChanged);
            // 
            // analyzeFromHHButton
            // 
            this.analyzeFromHHButton.Location = new System.Drawing.Point(418, 109);
            this.analyzeFromHHButton.Name = "analyzeFromHHButton";
            this.analyzeFromHHButton.Size = new System.Drawing.Size(123, 76);
            this.analyzeFromHHButton.TabIndex = 24;
            this.analyzeFromHHButton.Text = "Analyze (Shift + Enter)";
            this.analyzeFromHHButton.UseVisualStyleBackColor = true;
            this.analyzeFromHHButton.Click += new System.EventHandler(this.simulateFromHHButton_Click);
            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(418, 224);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(123, 38);
            this.clearButton.TabIndex = 22;
            this.clearButton.Text = "Clear All";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 17);
            this.label1.TabIndex = 20;
            this.label1.Text = "Hand History:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 168);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 17);
            this.label2.TabIndex = 21;
            this.label2.Text = "Results:";
            // 
            // calculateButton
            // 
            this.calculateButton.Location = new System.Drawing.Point(418, 27);
            this.calculateButton.Name = "calculateButton";
            this.calculateButton.Size = new System.Drawing.Size(123, 76);
            this.calculateButton.TabIndex = 19;
            this.calculateButton.Text = "Calculate (Ctrl+Enter)";
            this.calculateButton.UseVisualStyleBackColor = true;
            this.calculateButton.Click += new System.EventHandler(this.calculateButton_Click);
            // 
            // resultsRichTextBox
            // 
            this.resultsRichTextBox.ContextMenuStrip = this.copyContextMenuStrip;
            this.resultsRichTextBox.Location = new System.Drawing.Point(6, 188);
            this.resultsRichTextBox.Name = "resultsRichTextBox";
            this.resultsRichTextBox.ReadOnly = true;
            this.resultsRichTextBox.Size = new System.Drawing.Size(405, 247);
            this.resultsRichTextBox.TabIndex = 18;
            this.resultsRichTextBox.Text = "";
            this.resultsRichTextBox.WordWrap = false;
            // 
            // copyContextMenuStrip
            // 
            this.copyContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem});
            this.copyContextMenuStrip.Name = "copyContextMenuStrip";
            this.copyContextMenuStrip.Size = new System.Drawing.Size(176, 26);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.copyToolStripMenuItem.Text = "Copy Results";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // hhRichTextBox
            // 
            this.hhRichTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hhRichTextBox.Location = new System.Drawing.Point(6, 26);
            this.hhRichTextBox.Name = "hhRichTextBox";
            this.hhRichTextBox.ReadOnly = true;
            this.hhRichTextBox.Size = new System.Drawing.Size(405, 139);
            this.hhRichTextBox.TabIndex = 17;
            this.hhRichTextBox.Text = "";
            // 
            // analyzerTab
            // 
            this.analyzerTab.BackColor = System.Drawing.Color.Beige;
            this.analyzerTab.Controls.Add(this.deadCardsTextBox);
            this.analyzerTab.Controls.Add(this.label15);
            this.analyzerTab.Controls.Add(this.Label112);
            this.analyzerTab.Controls.Add(this.clearAnalyzerButton);
            this.analyzerTab.Controls.Add(this.simulateButton);
            this.analyzerTab.Controls.Add(this.label13);
            this.analyzerTab.Controls.Add(this.boardTextBox);
            this.analyzerTab.Controls.Add(this.analyzerResultsRichTextBox);
            this.analyzerTab.Controls.Add(this.handTextBox9);
            this.analyzerTab.Controls.Add(this.handTextBox8);
            this.analyzerTab.Controls.Add(this.handTextBox7);
            this.analyzerTab.Controls.Add(this.handTextBox6);
            this.analyzerTab.Controls.Add(this.handTextBox5);
            this.analyzerTab.Controls.Add(this.handTextBox4);
            this.analyzerTab.Controls.Add(this.handTextBox3);
            this.analyzerTab.Controls.Add(this.handTextBox2);
            this.analyzerTab.Controls.Add(this.handTextBox1);
            this.analyzerTab.Controls.Add(this.label12);
            this.analyzerTab.Controls.Add(this.gameComboBox);
            this.analyzerTab.Controls.Add(this.label9);
            this.analyzerTab.Controls.Add(this.label10);
            this.analyzerTab.Controls.Add(this.label11);
            this.analyzerTab.Controls.Add(this.label6);
            this.analyzerTab.Controls.Add(this.label7);
            this.analyzerTab.Controls.Add(this.label8);
            this.analyzerTab.Controls.Add(this.label5);
            this.analyzerTab.Controls.Add(this.label4);
            this.analyzerTab.Controls.Add(this.label3);
            this.analyzerTab.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.analyzerTab.Location = new System.Drawing.Point(4, 30);
            this.analyzerTab.Name = "analyzerTab";
            this.analyzerTab.Padding = new System.Windows.Forms.Padding(3);
            this.analyzerTab.Size = new System.Drawing.Size(547, 436);
            this.analyzerTab.TabIndex = 1;
            this.analyzerTab.Text = "Analyzer";
            // 
            // deadCardsTextBox
            // 
            this.deadCardsTextBox.Location = new System.Drawing.Point(335, 133);
            this.deadCardsTextBox.Name = "deadCardsTextBox";
            this.deadCardsTextBox.Size = new System.Drawing.Size(201, 22);
            this.deadCardsTextBox.TabIndex = 37;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(332, 110);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(87, 17);
            this.label15.TabIndex = 36;
            this.label15.Text = "Dead Cards:";
            // 
            // Label112
            // 
            this.Label112.AutoSize = true;
            this.Label112.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label112.Location = new System.Drawing.Point(6, 241);
            this.Label112.Name = "Label112";
            this.Label112.Size = new System.Drawing.Size(67, 17);
            this.Label112.TabIndex = 35;
            this.Label112.Text = "Results:";
            // 
            // clearAnalyzerButton
            // 
            this.clearAnalyzerButton.Location = new System.Drawing.Point(412, 324);
            this.clearAnalyzerButton.Name = "clearAnalyzerButton";
            this.clearAnalyzerButton.Size = new System.Drawing.Size(123, 50);
            this.clearAnalyzerButton.TabIndex = 34;
            this.clearAnalyzerButton.Text = "Clear All";
            this.clearAnalyzerButton.UseVisualStyleBackColor = true;
            this.clearAnalyzerButton.Click += new System.EventHandler(this.clearAnalyzerButton_Click);
            // 
            // simulateButton
            // 
            this.simulateButton.Location = new System.Drawing.Point(374, 161);
            this.simulateButton.Name = "simulateButton";
            this.simulateButton.Size = new System.Drawing.Size(123, 72);
            this.simulateButton.TabIndex = 33;
            this.simulateButton.Text = "Simulate";
            this.simulateButton.UseVisualStyleBackColor = true;
            this.simulateButton.Click += new System.EventHandler(this.simulateButton_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(332, 58);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(50, 17);
            this.label13.TabIndex = 23;
            this.label13.Text = "Board:";
            // 
            // boardTextBox
            // 
            this.boardTextBox.Location = new System.Drawing.Point(335, 81);
            this.boardTextBox.Name = "boardTextBox";
            this.boardTextBox.Size = new System.Drawing.Size(200, 22);
            this.boardTextBox.TabIndex = 22;
            // 
            // analyzerResultsRichTextBox
            // 
            this.analyzerResultsRichTextBox.ContextMenuStrip = this.copyContextMenuStrip;
            this.analyzerResultsRichTextBox.Location = new System.Drawing.Point(6, 264);
            this.analyzerResultsRichTextBox.Name = "analyzerResultsRichTextBox";
            this.analyzerResultsRichTextBox.ReadOnly = true;
            this.analyzerResultsRichTextBox.Size = new System.Drawing.Size(400, 171);
            this.analyzerResultsRichTextBox.TabIndex = 21;
            this.analyzerResultsRichTextBox.Text = "";
            // 
            // handTextBox9
            // 
            this.handTextBox9.Location = new System.Drawing.Point(69, 211);
            this.handTextBox9.Name = "handTextBox9";
            this.handTextBox9.Size = new System.Drawing.Size(182, 22);
            this.handTextBox9.TabIndex = 19;
            this.handTextBox9.KeyDown += new System.Windows.Forms.KeyEventHandler(this.handTextBox_KeyDown);
            // 
            // handTextBox8
            // 
            this.handTextBox8.Location = new System.Drawing.Point(69, 185);
            this.handTextBox8.Name = "handTextBox8";
            this.handTextBox8.Size = new System.Drawing.Size(182, 22);
            this.handTextBox8.TabIndex = 18;
            this.handTextBox8.KeyDown += new System.Windows.Forms.KeyEventHandler(this.handTextBox_KeyDown);
            // 
            // handTextBox7
            // 
            this.handTextBox7.Location = new System.Drawing.Point(69, 159);
            this.handTextBox7.Name = "handTextBox7";
            this.handTextBox7.Size = new System.Drawing.Size(182, 22);
            this.handTextBox7.TabIndex = 17;
            this.handTextBox7.KeyDown += new System.Windows.Forms.KeyEventHandler(this.handTextBox_KeyDown);
            // 
            // handTextBox6
            // 
            this.handTextBox6.Location = new System.Drawing.Point(69, 133);
            this.handTextBox6.Name = "handTextBox6";
            this.handTextBox6.Size = new System.Drawing.Size(182, 22);
            this.handTextBox6.TabIndex = 16;
            this.handTextBox6.KeyDown += new System.Windows.Forms.KeyEventHandler(this.handTextBox_KeyDown);
            // 
            // handTextBox5
            // 
            this.handTextBox5.Location = new System.Drawing.Point(69, 107);
            this.handTextBox5.Name = "handTextBox5";
            this.handTextBox5.Size = new System.Drawing.Size(182, 22);
            this.handTextBox5.TabIndex = 15;
            this.handTextBox5.KeyDown += new System.Windows.Forms.KeyEventHandler(this.handTextBox_KeyDown);
            // 
            // handTextBox4
            // 
            this.handTextBox4.Location = new System.Drawing.Point(69, 81);
            this.handTextBox4.Name = "handTextBox4";
            this.handTextBox4.Size = new System.Drawing.Size(182, 22);
            this.handTextBox4.TabIndex = 14;
            this.handTextBox4.KeyDown += new System.Windows.Forms.KeyEventHandler(this.handTextBox_KeyDown);
            // 
            // handTextBox3
            // 
            this.handTextBox3.Location = new System.Drawing.Point(69, 55);
            this.handTextBox3.Name = "handTextBox3";
            this.handTextBox3.Size = new System.Drawing.Size(182, 22);
            this.handTextBox3.TabIndex = 13;
            this.handTextBox3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.handTextBox_KeyDown);
            // 
            // handTextBox2
            // 
            this.handTextBox2.Location = new System.Drawing.Point(69, 29);
            this.handTextBox2.Name = "handTextBox2";
            this.handTextBox2.Size = new System.Drawing.Size(182, 22);
            this.handTextBox2.TabIndex = 12;
            this.handTextBox2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.handTextBox_KeyDown);
            // 
            // handTextBox1
            // 
            this.handTextBox1.Location = new System.Drawing.Point(69, 3);
            this.handTextBox1.Name = "handTextBox1";
            this.handTextBox1.Size = new System.Drawing.Size(182, 22);
            this.handTextBox1.TabIndex = 11;
            this.handTextBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.handTextBox_KeyDown);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(332, 6);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(50, 17);
            this.label12.TabIndex = 10;
            this.label12.Text = "Game:";
            // 
            // gameComboBox
            // 
            this.gameComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.gameComboBox.FormattingEnabled = true;
            this.gameComboBox.Items.AddRange(new object[] {
            "Omaha Hi-Lo",
            "Omaha Hi",
            "Hold \'em"});
            this.gameComboBox.Location = new System.Drawing.Point(335, 29);
            this.gameComboBox.Name = "gameComboBox";
            this.gameComboBox.Size = new System.Drawing.Size(201, 24);
            this.gameComboBox.TabIndex = 9;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 211);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(54, 17);
            this.label9.TabIndex = 8;
            this.label9.Text = "Hand 9";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(8, 185);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(54, 17);
            this.label10.TabIndex = 7;
            this.label10.Text = "Hand 8";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(8, 159);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(54, 17);
            this.label11.TabIndex = 6;
            this.label11.Text = "Hand 7";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 133);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 17);
            this.label6.TabIndex = 5;
            this.label6.Text = "Hand 6";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 107);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(54, 17);
            this.label7.TabIndex = 4;
            this.label7.Text = "Hand 5";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 81);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(54, 17);
            this.label8.TabIndex = 3;
            this.label8.Text = "Hand 4";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 55);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 17);
            this.label5.TabIndex = 2;
            this.label5.Text = "Hand 3";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 17);
            this.label4.TabIndex = 1;
            this.label4.Text = "Hand 2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 17);
            this.label3.TabIndex = 0;
            this.label3.Text = "Hand 1";
            // 
            // historyTab
            // 
            this.historyTab.BackColor = System.Drawing.Color.Beige;
            this.historyTab.Controls.Add(this.historyShowPlayerNamesCheckBox);
            this.historyTab.Controls.Add(this.historyTabControl);
            this.historyTab.Controls.Add(this.label17);
            this.historyTab.Controls.Add(this.historyListBox);
            this.historyTab.Controls.Add(this.historyAnalyzeButton);
            this.historyTab.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.historyTab.Location = new System.Drawing.Point(4, 30);
            this.historyTab.Name = "historyTab";
            this.historyTab.Padding = new System.Windows.Forms.Padding(3);
            this.historyTab.Size = new System.Drawing.Size(547, 436);
            this.historyTab.TabIndex = 3;
            this.historyTab.Text = "History";
            // 
            // historyShowPlayerNamesCheckBox
            // 
            this.historyShowPlayerNamesCheckBox.Location = new System.Drawing.Point(6, 353);
            this.historyShowPlayerNamesCheckBox.Name = "historyShowPlayerNamesCheckBox";
            this.historyShowPlayerNamesCheckBox.Size = new System.Drawing.Size(144, 38);
            this.historyShowPlayerNamesCheckBox.TabIndex = 27;
            this.historyShowPlayerNamesCheckBox.Text = "Show Player Names";
            this.historyShowPlayerNamesCheckBox.UseVisualStyleBackColor = true;
            this.historyShowPlayerNamesCheckBox.CheckedChanged += new System.EventHandler(this.historyShowPlayerNamesCheckBox_CheckedChanged);
            // 
            // historyTabControl
            // 
            this.historyTabControl.Controls.Add(this.resultsTabPage);
            this.historyTabControl.Controls.Add(this.hhTabPage);
            this.historyTabControl.Controls.Add(this.previousSessionsTabPage);
            this.historyTabControl.Location = new System.Drawing.Point(157, 5);
            this.historyTabControl.Name = "historyTabControl";
            this.historyTabControl.SelectedIndex = 0;
            this.historyTabControl.Size = new System.Drawing.Size(389, 427);
            this.historyTabControl.TabIndex = 8;
            // 
            // resultsTabPage
            // 
            this.resultsTabPage.Controls.Add(this.historyResultsRichTextBox);
            this.resultsTabPage.Location = new System.Drawing.Point(4, 25);
            this.resultsTabPage.Name = "resultsTabPage";
            this.resultsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.resultsTabPage.Size = new System.Drawing.Size(381, 398);
            this.resultsTabPage.TabIndex = 0;
            this.resultsTabPage.Text = "Results";
            this.resultsTabPage.UseVisualStyleBackColor = true;
            // 
            // historyResultsRichTextBox
            // 
            this.historyResultsRichTextBox.ContextMenuStrip = this.copyContextMenuStrip;
            this.historyResultsRichTextBox.Location = new System.Drawing.Point(0, 3);
            this.historyResultsRichTextBox.Name = "historyResultsRichTextBox";
            this.historyResultsRichTextBox.ReadOnly = true;
            this.historyResultsRichTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.historyResultsRichTextBox.Size = new System.Drawing.Size(380, 392);
            this.historyResultsRichTextBox.TabIndex = 1;
            this.historyResultsRichTextBox.Text = "";
            this.historyResultsRichTextBox.WordWrap = false;
            // 
            // hhTabPage
            // 
            this.hhTabPage.Controls.Add(this.historyHHRichTextBox);
            this.hhTabPage.Location = new System.Drawing.Point(4, 25);
            this.hhTabPage.Name = "hhTabPage";
            this.hhTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.hhTabPage.Size = new System.Drawing.Size(381, 398);
            this.hhTabPage.TabIndex = 1;
            this.hhTabPage.Text = "Hand History";
            this.hhTabPage.UseVisualStyleBackColor = true;
            // 
            // historyHHRichTextBox
            // 
            this.historyHHRichTextBox.Location = new System.Drawing.Point(0, 3);
            this.historyHHRichTextBox.Name = "historyHHRichTextBox";
            this.historyHHRichTextBox.ReadOnly = true;
            this.historyHHRichTextBox.Size = new System.Drawing.Size(380, 392);
            this.historyHHRichTextBox.TabIndex = 0;
            this.historyHHRichTextBox.Text = "";
            // 
            // previousSessionsTabPage
            // 
            this.previousSessionsTabPage.Controls.Add(this.label19);
            this.previousSessionsTabPage.Controls.Add(this.label18);
            this.previousSessionsTabPage.Controls.Add(this.saveDaysTextBox);
            this.previousSessionsTabPage.Controls.Add(this.saveSimulationsCheckBox);
            this.previousSessionsTabPage.Controls.Add(this.previousSessionsTreeView);
            this.previousSessionsTabPage.Location = new System.Drawing.Point(4, 25);
            this.previousSessionsTabPage.Name = "previousSessionsTabPage";
            this.previousSessionsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.previousSessionsTabPage.Size = new System.Drawing.Size(381, 398);
            this.previousSessionsTabPage.TabIndex = 2;
            this.previousSessionsTabPage.Text = "Previous Sessions";
            this.previousSessionsTabPage.UseVisualStyleBackColor = true;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(337, 7);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(38, 17);
            this.label19.TabIndex = 4;
            this.label19.Text = "days";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(220, 7);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(61, 17);
            this.label18.TabIndex = 3;
            this.label18.Text = "Save for";
            // 
            // saveDaysTextBox
            // 
            this.saveDaysTextBox.Location = new System.Drawing.Point(287, 7);
            this.saveDaysTextBox.Name = "saveDaysTextBox";
            this.saveDaysTextBox.Size = new System.Drawing.Size(44, 22);
            this.saveDaysTextBox.TabIndex = 2;
            this.saveDaysTextBox.Text = "30";
            // 
            // saveSimulationsCheckBox
            // 
            this.saveSimulationsCheckBox.AutoSize = true;
            this.saveSimulationsCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.saveSimulationsCheckBox.Location = new System.Drawing.Point(6, 7);
            this.saveSimulationsCheckBox.Name = "saveSimulationsCheckBox";
            this.saveSimulationsCheckBox.Size = new System.Drawing.Size(164, 21);
            this.saveSimulationsCheckBox.TabIndex = 1;
            this.saveSimulationsCheckBox.Text = "Save My Simulations ";
            this.saveSimulationsCheckBox.UseVisualStyleBackColor = true;
            // 
            // previousSessionsTreeView
            // 
            this.previousSessionsTreeView.ContextMenuStrip = this.previousSessionsContextMenuStrip;
            this.previousSessionsTreeView.Location = new System.Drawing.Point(6, 37);
            this.previousSessionsTreeView.Name = "previousSessionsTreeView";
            this.previousSessionsTreeView.Size = new System.Drawing.Size(369, 355);
            this.previousSessionsTreeView.TabIndex = 0;
            this.previousSessionsTreeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.previousSessionsTreeView_NodeMouseDoubleClick);
            this.previousSessionsTreeView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.previousSessionsTreeView_MouseDown);
            this.previousSessionsTreeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.previousSessionsTreeView_NodeMouseClick);
            this.previousSessionsTreeView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.previousSessionsTreeView_KeyDown);
            // 
            // previousSessionsContextMenuStrip
            // 
            this.previousSessionsContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.previousSessionsDeleteToolStripMenuItem});
            this.previousSessionsContextMenuStrip.Name = "previousSessionsContextMenuStrip";
            this.previousSessionsContextMenuStrip.Size = new System.Drawing.Size(133, 26);
            // 
            // previousSessionsDeleteToolStripMenuItem
            // 
            this.previousSessionsDeleteToolStripMenuItem.Name = "previousSessionsDeleteToolStripMenuItem";
            this.previousSessionsDeleteToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.previousSessionsDeleteToolStripMenuItem.Text = "Delete";
            this.previousSessionsDeleteToolStripMenuItem.Click += new System.EventHandler(this.previousSessionsDeleteToolStripMenuItem_Click);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(3, 5);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(96, 17);
            this.label17.TabIndex = 7;
            this.label17.Text = "Simulations:";
            // 
            // historyListBox
            // 
            this.historyListBox.ContextMenuStrip = this.historyContextMenuStrip;
            this.historyListBox.FormattingEnabled = true;
            this.historyListBox.ItemHeight = 16;
            this.historyListBox.Location = new System.Drawing.Point(6, 25);
            this.historyListBox.Name = "historyListBox";
            this.historyListBox.Size = new System.Drawing.Size(144, 324);
            this.historyListBox.TabIndex = 6;
            this.historyListBox.SelectedIndexChanged += new System.EventHandler(this.historyListBox_SelectedIndexChanged);
            this.historyListBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.historyListBox_MouseDown);
            this.historyListBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.historyListBox_KeyDown);
            // 
            // historyContextMenuStrip
            // 
            this.historyContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.historyDeleteToolStripMenuItem});
            this.historyContextMenuStrip.Name = "deleteContextMenuStrip";
            this.historyContextMenuStrip.Size = new System.Drawing.Size(202, 26);
            // 
            // historyDeleteToolStripMenuItem
            // 
            this.historyDeleteToolStripMenuItem.Name = "historyDeleteToolStripMenuItem";
            this.historyDeleteToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.historyDeleteToolStripMenuItem.Text = "Delete Simulation";
            this.historyDeleteToolStripMenuItem.Click += new System.EventHandler(this.historyDeleteToolStripMenuItem_Click);
            // 
            // historyAnalyzeButton
            // 
            this.historyAnalyzeButton.Enabled = false;
            this.historyAnalyzeButton.Location = new System.Drawing.Point(6, 397);
            this.historyAnalyzeButton.Name = "historyAnalyzeButton";
            this.historyAnalyzeButton.Size = new System.Drawing.Size(144, 35);
            this.historyAnalyzeButton.TabIndex = 9;
            this.historyAnalyzeButton.Text = "Analyze";
            this.historyAnalyzeButton.UseVisualStyleBackColor = true;
            this.historyAnalyzeButton.Click += new System.EventHandler(this.historyAnalyzeButton_Click);
            // 
            // aboutTab
            // 
            this.aboutTab.BackColor = System.Drawing.Color.Beige;
            this.aboutTab.Controls.Add(this.daysRemainingLabel);
            this.aboutTab.Controls.Add(this.webLabel);
            this.aboutTab.Controls.Add(this.emailLabel);
            this.aboutTab.Controls.Add(this.identificationCodeTextBox);
            this.aboutTab.Controls.Add(this.registerButton);
            this.aboutTab.Controls.Add(this.registrationKeyTextBox);
            this.aboutTab.Controls.Add(this.label14);
            this.aboutTab.Controls.Add(this.label16);
            this.aboutTab.Controls.Add(this.notificationLabel);
            this.aboutTab.Controls.Add(this.nameLabel);
            this.aboutTab.Controls.Add(this.pictureBox1);
            this.aboutTab.Controls.Add(this.versionLabel);
            this.aboutTab.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.aboutTab.Location = new System.Drawing.Point(4, 30);
            this.aboutTab.Name = "aboutTab";
            this.aboutTab.Padding = new System.Windows.Forms.Padding(3);
            this.aboutTab.Size = new System.Drawing.Size(547, 436);
            this.aboutTab.TabIndex = 2;
            this.aboutTab.Text = "About";
            // 
            // daysRemainingLabel
            // 
            this.daysRemainingLabel.Location = new System.Drawing.Point(110, 231);
            this.daysRemainingLabel.Name = "daysRemainingLabel";
            this.daysRemainingLabel.Size = new System.Drawing.Size(328, 23);
            this.daysRemainingLabel.TabIndex = 26;
            this.daysRemainingLabel.Text = "You have x days remaining during the trial period";
            // 
            // webLabel
            // 
            this.webLabel.AutoSize = true;
            this.webLabel.Location = new System.Drawing.Point(110, 64);
            this.webLabel.Name = "webLabel";
            this.webLabel.Size = new System.Drawing.Size(127, 17);
            this.webLabel.TabIndex = 25;
            this.webLabel.TabStop = true;
            this.webLabel.Text = "www.quickodds.net";
            this.webLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.webLabel_LinkClicked);
            // 
            // emailLabel
            // 
            this.emailLabel.AutoSize = true;
            this.emailLabel.Location = new System.Drawing.Point(110, 83);
            this.emailLabel.Name = "emailLabel";
            this.emailLabel.Size = new System.Drawing.Size(158, 17);
            this.emailLabel.TabIndex = 24;
            this.emailLabel.TabStop = true;
            this.emailLabel.Text = "support@quickodds.net";
            this.emailLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.emailLabel_LinkClicked);
            // 
            // identificationCodeTextBox
            // 
            this.identificationCodeTextBox.Location = new System.Drawing.Point(269, 325);
            this.identificationCodeTextBox.Name = "identificationCodeTextBox";
            this.identificationCodeTextBox.ReadOnly = true;
            this.identificationCodeTextBox.Size = new System.Drawing.Size(95, 22);
            this.identificationCodeTextBox.TabIndex = 22;
            // 
            // registerButton
            // 
            this.registerButton.Location = new System.Drawing.Point(222, 391);
            this.registerButton.Name = "registerButton";
            this.registerButton.Size = new System.Drawing.Size(113, 39);
            this.registerButton.TabIndex = 21;
            this.registerButton.Text = "Register";
            this.registerButton.UseVisualStyleBackColor = true;
            this.registerButton.Click += new System.EventHandler(this.registerButton_Click);
            // 
            // registrationKeyTextBox
            // 
            this.registrationKeyTextBox.Location = new System.Drawing.Point(269, 351);
            this.registrationKeyTextBox.MaxLength = 16;
            this.registrationKeyTextBox.Name = "registrationKeyTextBox";
            this.registrationKeyTextBox.Size = new System.Drawing.Size(169, 22);
            this.registrationKeyTextBox.TabIndex = 20;
            this.registrationKeyTextBox.TextChanged += new System.EventHandler(this.registrationKeyTextBox_TextChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(110, 354);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(138, 17);
            this.label14.TabIndex = 19;
            this.label14.Text = "Registration Key: ";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(110, 328);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(153, 17);
            this.label16.TabIndex = 18;
            this.label16.Text = "Identification Code: ";
            // 
            // notificationLabel
            // 
            this.notificationLabel.Location = new System.Drawing.Point(110, 254);
            this.notificationLabel.Name = "notificationLabel";
            this.notificationLabel.Size = new System.Drawing.Size(328, 68);
            this.notificationLabel.TabIndex = 17;
            this.notificationLabel.Text = "Thank you for registering!/To continue using the software after this time you mus" +
                "t purchase a registration key through the website and enter it below.";
            // 
            // nameLabel
            // 
            this.nameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.nameLabel.Location = new System.Drawing.Point(108, 20);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(204, 23);
            this.nameLabel.TabIndex = 16;
            this.nameLabel.Text = "QuickOdds Calculator";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Image = global::QuickOdds_Calculator.Properties.Resources.suits;
            this.pictureBox1.Location = new System.Drawing.Point(358, 20);
            this.pictureBox1.MaximumSize = new System.Drawing.Size(100, 100);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(82, 82);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 15;
            this.pictureBox1.TabStop = false;
            // 
            // versionLabel
            // 
            this.versionLabel.AutoSize = true;
            this.versionLabel.Location = new System.Drawing.Point(110, 47);
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(201, 17);
            this.versionLabel.TabIndex = 14;
            this.versionLabel.Text = "Registered/Trial Version x.x.x.x";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(552, 471);
            this.Controls.Add(this.mainTabControl);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "QuickOdds Calculator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.mainTabControl.ResumeLayout(false);
            this.calculatorTab.ResumeLayout(false);
            this.calculatorTab.PerformLayout();
            this.copyContextMenuStrip.ResumeLayout(false);
            this.analyzerTab.ResumeLayout(false);
            this.analyzerTab.PerformLayout();
            this.historyTab.ResumeLayout(false);
            this.historyTab.PerformLayout();
            this.historyTabControl.ResumeLayout(false);
            this.resultsTabPage.ResumeLayout(false);
            this.hhTabPage.ResumeLayout(false);
            this.previousSessionsTabPage.ResumeLayout(false);
            this.previousSessionsTabPage.PerformLayout();
            this.previousSessionsContextMenuStrip.ResumeLayout(false);
            this.historyContextMenuStrip.ResumeLayout(false);
            this.aboutTab.ResumeLayout(false);
            this.aboutTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl mainTabControl;
		private System.Windows.Forms.TabPage calculatorTab;
		private System.Windows.Forms.CheckBox showPlayerNamesCheckBox;
		private System.Windows.Forms.CheckBox showTimeCheckBox;
		private System.Windows.Forms.Button analyzeFromHHButton;
		private System.Windows.Forms.Button clearButton;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button calculateButton;
		private System.Windows.Forms.RichTextBox resultsRichTextBox;
		private System.Windows.Forms.RichTextBox hhRichTextBox;
		private System.Windows.Forms.TabPage analyzerTab;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.ComboBox gameComboBox;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox handTextBox9;
		private System.Windows.Forms.TextBox handTextBox8;
		private System.Windows.Forms.TextBox handTextBox7;
		private System.Windows.Forms.TextBox handTextBox6;
		private System.Windows.Forms.TextBox handTextBox5;
		private System.Windows.Forms.TextBox handTextBox4;
		private System.Windows.Forms.TextBox handTextBox3;
		private System.Windows.Forms.TextBox handTextBox2;
		private System.Windows.Forms.TextBox handTextBox1;
		private System.Windows.Forms.RichTextBox analyzerResultsRichTextBox;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.TextBox boardTextBox;
		private System.Windows.Forms.Button clearAnalyzerButton;
		private System.Windows.Forms.Button simulateButton;
		private System.Windows.Forms.Label Label112;
		private System.Windows.Forms.TextBox deadCardsTextBox;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.CheckBox includeHeroCheckBox;
		private System.Windows.Forms.TabPage aboutTab;
		private System.Windows.Forms.TextBox identificationCodeTextBox;
		private System.Windows.Forms.Button registerButton;
		private System.Windows.Forms.TextBox registrationKeyTextBox;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Label notificationLabel;
		private System.Windows.Forms.Label nameLabel;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label versionLabel;
		private System.Windows.Forms.TabPage historyTab;
		private System.Windows.Forms.TabControl historyTabControl;
		private System.Windows.Forms.TabPage resultsTabPage;
		private System.Windows.Forms.RichTextBox historyResultsRichTextBox;
		private System.Windows.Forms.TabPage hhTabPage;
		private System.Windows.Forms.RichTextBox historyHHRichTextBox;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.ListBox historyListBox;
		private System.Windows.Forms.Button historyAnalyzeButton;
		private System.Windows.Forms.LinkLabel emailLabel;
		private System.Windows.Forms.LinkLabel webLabel;
		private System.Windows.Forms.CheckBox historyShowPlayerNamesCheckBox;
		private System.Windows.Forms.Label daysRemainingLabel;
		private System.Windows.Forms.ContextMenuStrip copyContextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
		private System.Windows.Forms.ContextMenuStrip historyContextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem historyDeleteToolStripMenuItem;
		private System.Windows.Forms.TabPage previousSessionsTabPage;
		public System.Windows.Forms.TreeView previousSessionsTreeView;
		private System.Windows.Forms.CheckBox saveSimulationsCheckBox;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox saveDaysTextBox;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.ContextMenuStrip previousSessionsContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem previousSessionsDeleteToolStripMenuItem;
	}
}

