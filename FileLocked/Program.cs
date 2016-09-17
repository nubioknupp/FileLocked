using System;
using System.IO;
using System.Runtime.InteropServices;

namespace FileLocked
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			if (!IsFileLocked("file.txt")) 
			{ 
				Console.WriteLine("O arquivo não está sendo usado por outro processo!");
				return;
			}

			Console.WriteLine("O arquivo está sendo usado por outro processo");
		}

		/// <summary>
		///  Verifica se o arquivo está sendo usado por outro processo
		/// </summary>
		public static bool IsFileLocked(string filePath)
		{
			try
			{
				using (File.Open(filePath, FileMode.Open)) { }
			}
			catch (IOException e)
			{
				var errorCode = Marshal.GetHRForException(e) & ((1 << 16) - 1);

				return errorCode == 32 || errorCode == 33;
			}

			return false;
		}
	}
}
