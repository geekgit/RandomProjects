using System;
using System.IO;
using System.Text;

namespace FileNameConverter
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			string[] files=Directory.GetFiles ("/some/test/path");
			ConvertFilenames (files);
		}
		public static string[] ConvertFilenames(string[] input)
		{
			string[] output = (string[])input.Clone ();

			foreach (string filename in input) {
				Console.WriteLine ("Path: {0}",filename);
				Console.WriteLine ();
				HardWork (filename);
			}

			return output;
		}
		public static string HardWork(string hardname)
		{
			string newname = hardname;

			string wrong_part=Path.GetFileNameWithoutExtension(hardname);
			Console.WriteLine ("Wrong part: {0}", wrong_part);
			byte[] wrong_bytes=ByteView (wrong_part);
			Console.WriteLine ();
			Console.WriteLine ("string @ KOI8-R: {0}", ByteToStr(wrong_bytes,20866));
			Console.WriteLine ("string @ CP1251: {0}", ByteToStr(wrong_bytes,1251));
			Console.WriteLine ("string @ OEM866: {0}", ByteToStr(wrong_bytes,866));
			Console.WriteLine ("string @ UTF-7: {0}", ByteToStr(wrong_bytes,65000));
			Console.WriteLine ("string @ UTF-8: {0}", ByteToStr(wrong_bytes,65001));
			Console.WriteLine ("string @ UTF-16 Little Endian: {0}", ByteToStr(wrong_bytes,1200));
			Console.WriteLine ("string @ UTF-16 Big Endian: {0}", ByteToStr(wrong_bytes,1201));
			Console.WriteLine ("string @ Shift JIS: {0}", ByteToStr(wrong_bytes,932));
			Console.WriteLine ("string @ Japanese (Mac): {0}", ByteToStr(wrong_bytes,10001));
			Console.WriteLine ("string @ EUC-JP: {0}", ByteToStr(wrong_bytes,20932));

			string fixed_part = wrong_part;
			newname = newname.Replace (wrong_part, fixed_part);
			return newname;
		}
		public static string ByteToStr(byte[] data,int codepage)
		{
			try
			{
				return Encoding.GetEncoding (codepage).GetString (data);
			}
			catch(Exception E) {
				return E.Message;
			}
		}
		public static byte[] ByteView(string str)
		{
			byte[]data=Encoding.UTF8.GetBytes (str);
			Console.WriteLine ("Hex view:");
			for (int i = 0; i < data.Length; ++i) {
				byte b = data [i];
				Console.Write ("0x{0:X} ", b);
			}
			Console.WriteLine ();
			return data;
		}
	}
}
