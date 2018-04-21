namespace QuickOdds_Tracker
{
	partial class CreateDBForm
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
			this.databaseNameTextBox = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.createDatabaseButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// databaseNameTextBox
			// 
			this.databaseNameTextBox.Location = new System.Drawing.Point(12, 29);
			this.databaseNameTextBox.Name = "databaseNameTextBox";
			this.databaseNameTextBox.Size = new System.Drawing.Size(124, 22);
			this.databaseNameTextBox.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(9, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(110, 17);
			this.label1.TabIndex = 1;
			this.label1.Text = "Database Name";
			// 
			// createDatabaseButton
			// 
			this.createDatabaseButton.Location = new System.Drawing.Point(142, 9);
			this.createDatabaseButton.Name = "createDatabaseButton";
			this.createDatabaseButton.Size = new System.Drawing.Size(86, 42);
			this.createDatabaseButton.TabIndex = 2;
			this.createDatabaseButton.Text = "Create";
			this.createDatabaseButton.UseVisualStyleBackColor = true;
			this.createDatabaseButton.Click += new System.EventHandler(this.createDatabaseButton_Click);
			// 
			// CreateDBForm
			// 
			this.AcceptButton = this.createDatabaseButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(240, 60);
			this.Controls.Add(this.createDatabaseButton);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.databaseNameTextBox);
			this.Name = "CreateDBForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Create Database";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox databaseNameTextBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button createDatabaseButton;
	}
}