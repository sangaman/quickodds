using System.Collections.Generic;
using System.IO;
using System.Text;
using System;
using System.Collections;

namespace QuickOdds
{
	public static class Consts
	{
		public static int[] suitMasks= { 8, 128, 2048, 32768 };
		public static int[] rankMasks = { 2, 4, 8, 16, 32, 64, 128, 256, 512, 1024, 2048, 4096, 8193 };
		public static int[] primeNumbers = { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41 };
		public static Dictionary<int, bool> straightDictionary = new Dictionary<int, bool>();
		public static Dictionary<short, short> flushDictionary = new Dictionary<short, short>();

		public enum GameTypes { OmahaHiLo, Omaha, Holdem };

		static Consts()
		{
			String s = (String)Deserialize("OmahaDictionaries\\straightDict.dat");
			String[] tokens = s.Split(new char[] { ',' },
				StringSplitOptions.RemoveEmptyEntries);
			for (int i = 0; i < tokens.Length; i++)
				straightDictionary[int.Parse(tokens[i])] = true;

			s = (String)Deserialize("HoldemDictionaries\\flushDict.dat");
			tokens = s.Split(new char[] { ':', ',' },
				StringSplitOptions.RemoveEmptyEntries);
			for (int i = 0; i < tokens.Length; i += 2)
				flushDictionary[short.Parse(tokens[i])] = short.Parse(tokens[i + 1]);
		}

		public static int GetBitCount(int x)
		{
			int count = 0;
			while (x > 0)
			{
				count++;
				x &= x - 1;
			}
			return count;
		}

		public static void BuildDictionaries()
		{
			//dummy method, triggers constructor
		}


		public static object Deserialize(string file)
		{
			using (FileStream stream = File.OpenRead(file))
			{
				using (System.IO.Compression.DeflateStream ds = new System.IO.Compression.DeflateStream(stream, System.IO.Compression.CompressionMode.Decompress))
				{
					System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
					return formatter.Deserialize(ds);
				}
			}
		}

		/*foreach (String file in Directory.GetFiles("HoldemDictionariesOld\\"))
			{
				Serialize(File.ReadAllText(file), "HoldemDictionaries\\" + file.Remove(file.IndexOf(".")).Remove(0, "HoldemDictionariesOld\\".Length) + ".dat");
			}
			foreach (String file in Directory.GetFiles("OmahaDictionariesOld\\"))
			{
				Serialize(File.ReadAllText(file), "OmahaDictionaries\\" + file.Remove(file.IndexOf(".")).Remove(0, "OmahaDictionariesOld\\".Length) + ".dat");
			}*/

		/*public static void Serialize(object obj, string file)
		{
			using (FileStream stream = File.Open(file, FileMode.Create))
			{
				using (System.IO.Compression.DeflateStream ds = new System.IO.Compression.DeflateStream(stream, System.IO.Compression.CompressionMode.Compress))
				{
					System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
					formatter.Serialize(ds, obj);
				}
			}
		}*/
	}
}

