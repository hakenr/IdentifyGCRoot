using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Diagnostics.Runtime;

namespace IdentifyGCRoot
{
	class Program
	{
		static void Main(string[] args)
		{
			using (DataTarget dataTarget = DataTarget.LoadCrashDump(@"D:\Temp\2017-09-12 - BTF - BeTy DX Sessions\w3wp.DMP"))
			{
				ClrInfo runtimeInfo = dataTarget.ClrVersions[0];  // just using the first runtime
				ClrRuntime runtime = runtimeInfo.CreateRuntime(@"C:\ProgramData\dbg\sym\mscordacwks_AMD64_AMD64_4.6.1087.00.dll\583E5E56990000\mscordacwks_AMD64_AMD64_4.6.1087.00.dll"); // .cordll -ve -u -l

				foreach (ClrRoot root in runtime.Heap.EnumerateRoots())
				{
					if (root.Object == 0x000000e9288f8f30) // address of root we want to examine
					{
						Console.WriteLine(root.ToString());
						Console.WriteLine($"\tAddress: {root.Address:x8}");
						Console.WriteLine($"\tAppDomain: {root.AppDomain?.Name}");
						Console.WriteLine($"\tIsInterior: {root.IsInterior}");
						Console.WriteLine($"\tIsPinned: {root.IsPinned}");
						Console.WriteLine($"\tIsPossibleFalsePositive: {root.IsPossibleFalsePositive}");
						Console.WriteLine($"\tKind: {root.Kind}");
						Console.WriteLine($"\tName: {root.Name}");
						Console.WriteLine($"\tObject: {root.Object:x8}");
						Console.WriteLine($"\tThread: {root.Thread}");
						Console.WriteLine($"\tType: {root.Type}");
						Console.WriteLine();
					};
				}
			}
		}
	}
}
