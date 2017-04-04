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
			DataTarget dataTarget = DataTarget.LoadCrashDump(@"D:\Temp\_SampleDumps\2014-05-22 - DRAGON XWT+AUT (no issue) - w3wp\w3wp.DMP");
			ClrRuntime runtime = dataTarget.CreateRuntime(@"C:\ProgramData\dbg\sym\mscordacwks_AMD64_AMD64_4.0.30319.18444.dll\52717F9A96b000\mscordacwks_AMD64_AMD64_4.0.30319.18444.dll");
			ClrHeap heap = runtime.GetHeap();

			foreach (ClrRoot root in heap.EnumerateRoots())
			{
				if (root.Object == 0x00000000ffee0eb8) // adresa rootu, který chceme prozkoumat
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
				}
			}
		}
	}
}
