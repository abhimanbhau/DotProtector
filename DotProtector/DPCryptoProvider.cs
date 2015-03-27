using System;
using System.Security.Cryptography;
using System.IO;

namespace DotProtector
{
	public static class DPCryptoProvider
	{
		public static byte[] generateFileHash (string path)
		{
			byte[] hash = MD5.Create ().ComputeHash (File.ReadAllBytes (path));
			return hash;
		}
	}
}