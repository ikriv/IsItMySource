using System;

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
        public string UseMethod { get; private set; }


        public bool Parse(string[] args)
        {
            if (args.Length < 2)
            {
                Usage();
                return false;
            }

            if (!SetOperation(args[0])) return false;
            ExeOrPdbPath = args[1];

            try
            {

                int i = 2;
                while (i < args.Length)
                {
                    switch (args[i].ToLower())
                    {
                        case "--use":
                            UseMethod = GetOptValue(args, ref i);
                            break;
                        case "--root":
                            RootPath = GetOptValue(args, ref i);
                            break;
                        case "--localroot":
                            LocalRootPath = GetOptValue(args, ref i);
                            break;
                        case "--pdbdir":
                            PdbSearchPath = GetOptValue(args, ref i);
                            break;
                        default:
                            throw new InvalidOperationException("Invalid option: " + args[i]);
                    }
                    i++;
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                return false;
            }

            return true;
        }

        private bool SetOperation(string op)
        {
            Operation result;
            if (!Enum.TryParse(op, true, out result))
            {
                Console.Error.WriteLine("Invalid operation: " + op);
                return false;
            }
            Operation = result;
            return true;

        }

        private static string GetOptValue(string[] args, ref int i)
        {
            ++i;
            if (i>=args.Length) throw new InvalidOperationException("Missing value for option " + args[i-1]);
            return args[i];
        }

        private static void Usage()
        {
            Console.Error.WriteLine("Usage goes here"); /*!*/
            
        }
    }
}
