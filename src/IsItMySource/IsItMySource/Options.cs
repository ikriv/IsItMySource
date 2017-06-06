using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace IKriv.IsItMySource
{
    enum Operation
    {
        List,
        Verify
    }

    internal class Options
    {
        public string ExeOrPdbPath { get; set; }
        public string PdbSearchPath { get; set; }
        public Operation Operation { get; set; }
        public string RootPath { get; set; }
        public string LocalRootPath { get; set; }
        public string EngineName { get; set; }
        public string IgnoreFiles { get; set; }

        public const string EngineNameManaged = "DiaSymReader";
        public const string EngineNameNative = "DiaSdk.Managed";

        public bool Parse(string[] args)
        {
            if (args.Length < 1)
            {
                Usage();
                return false;
            }

            ParseAppConfig();

            var realArgs = new List<string>();
            EngineName = EngineNameManaged;

            try
            {
                for (int i=0; i< args.Length; ++i)
                {
                    var arg = args[i];
                    if (!arg.StartsWith("--"))
                    {
                        realArgs.Add(arg);
                        continue;
                    }
                    switch (args[i].ToLower())
                    {
                        case "--managed":
                            // set it explicitly: in case someone specified both --unmanaged and --managed the last will win
                            EngineName = EngineNameManaged; 
                            break;

                        case "--unmanaged":
                        case "--native":
                            EngineName = EngineNameNative;
                            break;

                        case "--root":
                            RootPath = GetOptValue(args, i);
                            ++i;
                            break;

                        case "--pdbdir":
                            PdbSearchPath = GetOptValue(args, i);
                            ++i;
                            break;

                        case "--ignore":
                            IgnoreFiles += ";" + GetOptValue(args, i);
                            ++i;
                            break;

                        case "--allfiles":
                            IgnoreFiles = "";
                            break;

                        default:
                            throw new InvalidOperationException("Invalid option: " + args[i]);
                    }
                }

                switch (realArgs.Count)
                {
                    case 1:
                        ExeOrPdbPath = realArgs[0];
                        Operation = Operation.List;
                        break;

                    case 2:
                        ExeOrPdbPath = realArgs[0];
                        LocalRootPath = realArgs[1];
                        Operation = Operation.Verify;
                        break;

                    default:
                        Usage();
                        return false;
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                return false;
            }

            return true;
        }

        private void ParseAppConfig()
        {
            var settings = ConfigurationManager.AppSettings;
            IgnoreFiles = settings["IgnoreFiles"] ?? ""; 
        }

        private static string GetOptValue(string[] args, int i)
        {
            if (i+1>=args.Length) throw new InvalidOperationException("Missing value for option " + args[i]);
            return args[i+1];
        }

        private static void Usage()
        {
            Console.Error.WriteLine(
@"Usage: IsItMySource [options] exe_or_pdb_file [folder]

    If folder is specified, checks whether source files in the folder match 
    those specified in the PDB or EXE file. If folder is not specified, shows
    list of source files of EXE or PDB file.

    Loading PDB files directly is only supported for unmanaged debug info.
    Managed debug info reader starts with an EXE file and searches for PDB.
   
OPTIONS
    --allfiles  Include system files that are ignored by default.

    --ignore pattern1[;pattern2...]
                Ignore files that match specified wildcard patterns. Allowed
                wildcard characters are 
                **\  empty string or any character sequence followed by backslash
                **   any character sequence
                *    any character sequence not containing backslash
                ?    single character except backslash

    --managed   Read managed debug info via DIASymReader. This is the default.
                Supports only EXE files. PDB file must be next to the EXE file
                or in the path specified by --search.

    --native    Read native debug info via DIA SDK. This won't return source 
                file checksums for managed executables. Supports EXE and PDB.

     --pdbdir dir1[;dir2...]
                Use this semicolon-separated path to look for PDB file 
                if EXE is specified

    --root dir  Root path for source files inside the PDB/EXE file. File 
                {root}/relative.path referenced by EXE/PDB will be matched to 
                {folder}/relative.path on local disk.
                Source files outside of the root will be ignored. 
                E.g. if proj.exe refers to c:\mysources\proj\foo\bar.cs, and
                IsItMySource --root d:\projects\proj proj.exe, 
                is run, then the program will expect local file 
                d:\projects\proj\foo\bar.cs to match 
                c:\mysources\proj\foo\bar.cs referenced proj.exe.

    --unmanaged Same as --native"
); 
            
        }
    }
}
