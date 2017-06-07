using System;
using System.Collections.Generic;
using System.Linq;
using IKriv.IsItMySource.Interfaces;

namespace IKriv.IsItMySource
{
    internal class SourceFilesFilter
    {
        private readonly Options _options;

        public SourceFilesFilter(Options options)
        {
            _options = options;
        }

        public IEnumerable<SourceFileInfo> Filter(IEnumerable<SourceFileInfo> files)
        {
            var filtered = FilterIgnoredFiles(files);
            var unique = filtered.GroupBy(f => f.Path).Select(g => g.First());
            return unique;
        }

        private IEnumerable<SourceFileInfo> FilterIgnoredFiles(IEnumerable<SourceFileInfo> files)
        {
            if (String.IsNullOrEmpty(_options.IgnoreFiles)) return files;
            var match = new FileMatch(_options.IgnoreFiles);
            return files.Where(f => !match.IsMatch(f.Path));
        }
    }
}
