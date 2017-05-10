using System.Collections.Generic;
using IKriv.IsItMySource.Interfaces;

namespace IKriv.IsItMySource
{
    internal interface IOperation
    {
        void Run(IEnumerable<ISourceFileInfo> sources, Options options);
    }
}
