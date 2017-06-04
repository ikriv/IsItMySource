using System;
using System.Collections.Generic;

namespace IKriv.IsItMySource.Interfaces
{
    public interface IDebugInfo : IDisposable
    {
        IEnumerable<SourceFileInfo> GetSourceFiles();
    }
}
