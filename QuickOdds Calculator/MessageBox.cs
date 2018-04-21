using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace QuickOdds_Calculator
{
	public partial class MessageBox : Form
	{
		public MessageBox()
		{
			InitializeComponent(); 
		}

		public void setMessage(String label, String message)
		{
			this.Text = label;
			messageLabel.Text = message;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}