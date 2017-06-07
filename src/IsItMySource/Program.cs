using System;
using System.IO;
using System.Linq;
using System.Reflection;
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

        private static IDebugInfoReader GetReader(string engineAssemblyName)
        {
            if (engineAssemblyName == null) engineAssemblyName = Options.EngineNameManaged;
            var engineAssembly = Assembly.Load(engineAssemblyName);

            if (engineAssembly == null)
            {
                throw new NotSupportedException("Could not load " + engineAssemblyName);
            }

            var debugEngineAttr = (DebugInfoEngineAttribute)engineAssembly.GetCustomAttributes(typeof(DebugInfoEngineAttribute), true).FirstOrDefault();
            if (debugEngineAttr == null)
            {
                throw new InvalidOperationException(engineAssemblyName + " assembly does not have [assembly:DebugInfoEngine] attribute");
            }

            var engineType = debugEngineAttr.Type;
            if (engineType == null)
            {
                throw new InvalidOperationException(engineAssemblyName + " assembly has [assembly:DebugInfoEngine] attribute with null type");
            }

            var engineTypeName = engineType.FullName + "," + engineType.Assembly.FullName;

            if (!typeof(IDebugInfoReader).IsAssignableFrom(engineType))
            {
                throw new InvalidOperationException($"Cannot load debug engine from assembly {engineAssemblyName}. " +
                    $"Type {engineTypeName} does not implement interface IDebugInfoReader");
            }

            IDebugInfoReader result;

            try
            {
                result = Activator.CreateInstance(engineType) as IDebugInfoReader;
            }
            catch (Exception ex)
            {
                throw new TargetInvocationException($"Could not create instance of type {engineTypeName}", ex);
            }


            if (result == null) throw new InvalidOperationException($"Activator.CreateInstance() returned null for {engineTypeName}");

            return result;
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
