using System;

namespace DotProtector
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			if (args.Length <= 0) {
				printHelp ();
				return;
			}
			if (args [0] == "-h") {
				printHelp ();
				return;
			}
			DPInjector.protectAssembly (args [1], args [0]);
		}

		static void printHelp ()
		{
			Console.WriteLine 
			(
				"DotProtector\n" +
				"version 1.0.1b\n" +
				"Usage : DotProtector.exe [OPTIONS] <FILENAME>\n" +
				"-h\t\t Print this info\n" +
				"-iG\t\t Inject child-safe protection\n" +
				"-iX\t\t Inject vulgar protection\n"
			);
		}
	}
}
