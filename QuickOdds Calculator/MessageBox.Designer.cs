namespace QuickOdds_Calculator
{
	partial class MessageBox
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
			this.button1 = new System.Windows.Forms.Button();
			this.messageLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(54, 64);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(103, 48);
			this.button1.TabIndex = 0;
			this.button1.Text = "OK";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// messageLabel
			// 
			this.messageLabel.Location = new System.Drawing.Point(12, 9);
			this.messageLabel.Name = "messageLabel";
			this.messageLabel.Size = new System.Drawing.Size(188, 52);
			this.messageLabel.TabIndex = 1;
			this.messageLabel.Text = "Message";
			this.messageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// MessageBox
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(208, 119);
			this.Controls.Add(this.messageLabel);
			this.Controls.Add(this.button1);
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(216, 159);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(216, 159);
			this.Name = "MessageBox";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "MessageBox";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label messageLabel;
	}
}