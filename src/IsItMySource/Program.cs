using System;
using System.IO;
using IKriv.IsItMySource.DiaSdk;
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

            var reader = GetReader(options.EngineName);
            using (var debugInfo = GetDebugInfo(reader,options))
            {
                var filter = new SourceFilesFilter(options);
                var sources = filter.Filter(debugInfo.GetSourceFiles());
                var operation = CreateOperation(options.Operation, Console.Out);
                operation.Run(sources, options);
            }
        }

        private static IDebugInfo GetDebugInfo(IDebugInfoReader reader, Options options)
        {
            return reader.GetDebugInfo(options.ExeOrPdbPath, options.PdbSearchPath);
        }

        private static IDebugInfoReader GetReader(string engine)
        {
            // TODO: add dynamic reader plugin loading mechanism
            if (engine == null) engine = Options.EngineNameManaged;
            switch (engine.ToLower())
            {
                case Options.EngineNameManaged:
                    return new DsrDebugInfoReader();

                case Options.EngineNameNative:
                    return new DiaSdkDebugInfoReader();

                default:
                    throw new NotSupportedException("Unknown debug info retrieval engine: " + engine);
            }
        }

        private static IOperation CreateOperation(Operation op, TextWriter output)
        {
            switch (op)
            {
                case Operation.List: return new ListSourcesOperation(output);
                case Operation.Verify: return new VerifySourcesOperation(output);
                default:
                    throw new InvalidOperationException("Unknown operation: " + op);
            }
        }
    }
}
