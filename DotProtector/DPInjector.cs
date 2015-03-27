using System;
using Mono.Cecil;
using Mono.Cecil.Cil;
using LibProtection;
using System.Linq;
using System.IO;

namespace DotProtector
{
	public static class DPInjector
	{
		public static void protectAssembly (string path, string gmode)
		{
			AssemblyDefinition assembly = AssemblyDefinition.ReadAssembly (path);
			TypeDefinition mainClass = assembly.MainModule.Types [1];
			MethodDefinition mainMethod = mainClass.Methods.OfType<MethodDefinition> ()
				.Where (m => m.Name == "Main").Single ();

			var injectMethod =
				typeof(Core).GetMethod ("Init", new []{ typeof(string) });
			var method = assembly.MainModule.Import (injectMethod);

			var start = mainMethod.Body.GetILProcessor ().Create (OpCodes.Ldstr, gmode);
			var instr = mainMethod.Body.GetILProcessor ().Create (OpCodes.Call, method);
			mainMethod.Body.GetILProcessor ().InsertBefore (mainMethod.Body.Instructions [0], start);
			mainMethod.Body.GetILProcessor ().InsertAfter (start, instr);
			if (File.Exists (path.Replace (".exe", "").Trim () + "-secure" + ".exe")) {
				File.Delete (path.Replace (".exe", "").Trim () + "-secure" + ".exe");
			}
			assembly.Write (path.Replace (".exe", "").Trim () + "-secure" + ".exe");
			File.WriteAllBytes ("protection.bin", 
				DPCryptoProvider.generateFileHash (path.Replace (".exe", "").Trim () + "-secure" + ".exe"));
		}
	}
}

