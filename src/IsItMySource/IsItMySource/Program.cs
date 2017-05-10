using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using IKriv.IsItMySource.DiaSymReader;
using IKriv.IsItMySource.Interfaces;

namespace IKriv.IsItMySource
{
    class Program
    {
        static int Main(string[] args)
        {
            var options = new Options();
            if (!options.Parse(args)) return 2;

            try
            {
                Process(options);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return 1;
            }

            return 0;
        }

        private static void Process(Options options)
        {
            if (!File.Exists(options.ExeOrPdbPath))
            {
                throw new InvalidOperationException("File does not exist: " + options.ExeOrPdbPath);
            }

            var reader = GetReader(options.UseMethod);
            using (var debugInfo = reader.GetDebugInfo(options.ExeOrPdbPath, options.PdbSearchPath))
            {
                var sources = debugInfo.GetSourceFiles();
                CreateOperation(options.Operation).Run(sources, options);
            }
        }

        private static IDebugInfoReader GetReader(string method)
        {
            // TODO: add dynamic reader plugin loading mechanism
            if (method == null) method = "DiaSymReader";
            switch (method.ToLower())
            {
                case "diasymreader":
                    return new DsrDebugInfoReader();

                default:
                    throw new NotSupportedException("Unknown debug info retrieval method: " + method);
            }
        }

        private static IOperation CreateOperation(Operation op)
        {
            switch (op)
            {
                case Operation.List: return new ListSources();
                case Operation.Verify: return new VerifySources();
                default:
                    throw new InvalidOperationException("Unknown operation: " + op);
            }
        }
    }
}
