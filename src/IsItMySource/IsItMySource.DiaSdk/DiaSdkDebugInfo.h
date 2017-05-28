#pragma once

using namespace System;
using namespace System::Collections::Generic;
using namespace IKriv::IsItMySource::Interfaces;

namespace IKriv {
	namespace IsItMySource {
		namespace DiaSdk {
			class DebugInfoImpl;

			ref class DiaSdkDebugInfo : IDebugInfo
			{
			public:
				DiaSdkDebugInfo(String^ exeOrPdbPath, String^ searchPath);
				~DiaSdkDebugInfo();
				virtual IEnumerable<ISourceFileInfo^>^ GetSourceFiles();

			private:
				String^ _path;
				String^ _searchPath;
				DebugInfoImpl* _impl;
			};

			public ref class DiaSdkDebugInfoReader : IDebugInfoReader
			{
			public:
				virtual IDebugInfo^ GetDebugInfo(String^ exeOrPdbfilePath, String^ pdbSearchPath)
				{
					return gcnew DiaSdkDebugInfo(exeOrPdbfilePath, pdbSearchPath);
				}
			};
		}
	}
}
