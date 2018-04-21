using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;

namespace QuickOdds_KeyGen
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void goButton_Click(object sender, EventArgs e)
		{
			String identificationCode = identificationCodeTextBox.Text;
			/*byte[] tempBytes = {	byte.Parse(identificationCode.Substring(0,2), System.Globalization.NumberStyles.HexNumber), 
									byte.Parse(identificationCode.Substring(2,2), System.Globalization.NumberStyles.HexNumber),
									byte.Parse(identificationCode.Substring(4,2), System.Globalization.NumberStyles.HexNumber), 
									byte.Parse(identificationCode.Substring(6,2), System.Globalization.NumberStyles.HexNumber) };
			*/
			byte[] tempBytes = GetBytesFromHex(identificationCode);
			DESCryptoServiceProvider key = new DESCryptoServiceProvider();
			key.Key = ASCIIEncoding.ASCII.GetBytes("12664961");
			key.IV = ASCIIEncoding.ASCII.GetBytes("QODDCALC");

			MemoryStream ms = new MemoryStream();
			CryptoStream encStream = new CryptoStream(ms, key.CreateEncryptor(), CryptoStreamMode.Write);

			encStream.Write(tempBytes, 0, tempBytes.Length);
			encStream.FlushFinalBlock();

			byte[] encryptedBytes = ms.ToArray();

			encStream.Close();
			ms.Close();

			String registrationKey = String.Empty;

			foreach (byte b in encryptedBytes)
			{
				registrationKey += String.Format("{0,2:X2}", b);
			}
			registrationKeyTextBox.Text = registrationKey;
		}

		private static byte[] GetBytesFromHex(String hex)
		{
			if (hex.Length % 2 == 1)
				return null;
			byte[] bytes = new byte[hex.Length / 2];
			for (int n = 0; n < hex.Length / 2; n++)
			{
				char digit = hex[n*2];
				int value = ConvertHexToInt(digit) * 16;
				digit = hex[n*2+1];
				value += ConvertHexToInt(digit);
				bytes[n] = BitConverter.GetBytes(value)[0];
			}
			return bytes;
		}

		private static int ConvertHexToInt(char c)
		{
			switch (c)
			{
				case '0':
					return 0;
				case '1':
					return 1;
				case '2':
					return 2;
				case '3':
					return 3;
				case '4':
					return 4;
				case '5':
					return 5;
				case '6':
					return 6;
				case '7':
					return 7;
				case '8':
					return 8;
				case '9':
					return 9;
				case 'A':
					return 10;
				case 'B':
					return 11;
				case 'C':
					return 12;
				case 'D':
					return 13;
				case 'E':
					return 14;
				case 'F':
					return 15;
				default:
					return 0;
			}
		}
	}
}