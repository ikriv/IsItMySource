using System;
using System.Collections.Generic;

namespace IKriv.IsItMySource
{
    enum Operation
    {
        List,
        Verify
    }

    internal class Options
    {
        public string ExeOrPdbPath { get; private set; }
        public string PdbSearchPath { get; private set; }
        public Operation Operation { get; private set; }
        public string RootPath { get; private set; }
        public string LocalRootPath { get; private set; }
        public string EngineName { get; private set; }


        public bool Parse(string[] args)
        {
            if (args.Length < 1)
            {
                Usage();
                return false;
            }

            var realArgs = new List<string>();
            EngineName = "DiaSymReader";

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
                        case "--managed": break;

                        case "--unmanaged":
                        case "--native":
                            EngineName = "DiaSdk.Managed";
                            break;

                        case "--root":
                            RootPath = GetOptValue(args, i);
                            ++i;
                            break;

                        case "--pdbdir":
                            PdbSearchPath = GetOptValue(args, i);
                            ++i;
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
    --managed   Read managed debug info via DIASymReader. This is the default.
                Supports only EXE files. PDB file must be next to the EXE file
                or in the path specified by --search.

    --native    Read native debug info via DIA SDK. This won't return source 
                file checksums for managed executables. Supports EXE and PDB.

    --unmanaged Same as --native

    --root      Root path for source files inside the PDB/EXE file. File 
                {root}/relative.path referenced by EXE/PDB will be matched to 
                {folder}/relative.path on local disk.
                Source files outside of the root will be ignored. 
                E.g. if proj.exe refers to c:\mysources\proj\foo\bar.cs, and
                IsItMySource --root d:\projects\proj proj.exe, 
                is run, then the program will expect local file 
                d:\projects\proj\foo\bar.cs to match 
                c:\mysources\proj\foo\bar.cs referenced proj.exe.
                
    --pdbdir    Use this path to look for PDB file if EXE is specified"
); 
            
        }
    }
}
