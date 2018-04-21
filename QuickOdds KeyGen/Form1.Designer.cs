namespace QuickOdds_KeyGen
{
	partial class Form1
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
			this.identificationCodeTextBox = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.registrationKeyTextBox = new System.Windows.Forms.TextBox();
			this.goButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// identificationCodeTextBox
			// 
			this.identificationCodeTextBox.Location = new System.Drawing.Point(16, 33);
			this.identificationCodeTextBox.MaxLength = 8;
			this.identificationCodeTextBox.Name = "identificationCodeTextBox";
			this.identificationCodeTextBox.Size = new System.Drawing.Size(134, 22);
			this.identificationCodeTextBox.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(124, 17);
			this.label1.TabIndex = 1;
			this.label1.Text = "Identification Code";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 58);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(112, 17);
			this.label2.TabIndex = 3;
			this.label2.Text = "Registration Key";
			// 
			// registrationKeyTextBox
			// 
			this.registrationKeyTextBox.Location = new System.Drawing.Point(15, 78);
			this.registrationKeyTextBox.Name = "registrationKeyTextBox";
			this.registrationKeyTextBox.Size = new System.Drawing.Size(135, 22);
			this.registrationKeyTextBox.TabIndex = 2;
			// 
			// goButton
			// 
			this.goButton.Location = new System.Drawing.Point(156, 28);
			this.goButton.Name = "goButton";
			this.goButton.Size = new System.Drawing.Size(89, 72);
			this.goButton.TabIndex = 4;
			this.goButton.Text = "Go";
			this.goButton.UseVisualStyleBackColor = true;
			this.goButton.Click += new System.EventHandler(this.goButton_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(257, 113);
			this.Controls.Add(this.goButton);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.registrationKeyTextBox);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.identificationCodeTextBox);
			this.Name = "Form1";
			this.Text = "QuickOdds KeyGen";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox identificationCodeTextBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox registrationKeyTextBox;
		private System.Windows.Forms.Button goButton;
	}
}

