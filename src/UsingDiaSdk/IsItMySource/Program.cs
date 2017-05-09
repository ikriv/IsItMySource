using System;
using System.Linq;
using IKriv.IsItMySource.Dia;

namespace IKriv.IsItMySource
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.Error.WriteLine("Usage: isitmysource file.pdb");
                return;
            }

            using (var pdbFile = new PdbFile(args[0]))
            {
                foreach (var file in pdbFile.GetSourceFiles().OrderBy(f=>f.Id))
                {
                    Console.WriteLine($"{file.Id} {file.Path} {file.ChecksumType} {ToHex(file.Checksum)}");
                }
            }

        }

        private static string ToHex(byte[] ba)
        {
            return BitConverter.ToString(ba).Replace("-", "");
        }
    }
}
