using System;
using System.Collections.Generic;
using System.Linq;
using IKriv.IsItMySource.Interfaces;

namespace IKriv.IsItMySource
{
    internal class ListSources : IOperation
    {
        public void Run(IEnumerable<ISourceFileInfo> sources, Options options)
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
                Console.WriteLine($"{relativePath} {doc.ChecksumTypeStr} {Util.ToHex(doc.Checksum)}");
            }

            if (nLeftOut > 0)
            {
                Console.WriteLine($"{nLeftOut} file(s) outside of {options.RootPath}");
            }
        }
    }
}
