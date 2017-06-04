using System.Collections.Generic;
using IKriv.IsItMySource.Interfaces;

namespace IKriv.IsItMySource
{
    internal interface IOperation
    {
        void Run(IEnumerable<SourceFileInfo> sources, Options options);
    }
}
