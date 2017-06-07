using System.Collections.Generic;
using System.IO;
using System.Linq;
using IKriv.IsItMySource.Interfaces;

namespace IKriv.IsItMySource
{
    internal class ListSourcesOperation : IOperation
    {
        private readonly TextWriter _output;
        public ListSourcesOperation(TextWriter output)
        {
            _output = output;
        }

        public void Run(IEnumerable<SourceFileInfo> sources, Options options)
        {
            int nLeftOut = 0;

            foreach (var doc in sources.OrderBy(s => s.Path))
            {
                var relativePath = Util.GetRelativePath(doc.Path, options.RootPath);
                if (relativePath == null)
                {
                    ++nLeftOut;
                    continue;
                }
                string hex = Util.ToHex(doc.Checksum);
                string filler = (hex == "")? "" : " ";
                _output.WriteLine($"{relativePath} {doc.ChecksumTypeStr}{filler}{hex}");
            }

            if (nLeftOut > 0)
            {
                _output.WriteLine($"{nLeftOut} file(s) outside of {options.RootPath}");
            }
        }
    }
}
