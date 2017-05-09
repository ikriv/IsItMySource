// IsItMySource.Dia.h

#pragma once

using namespace System;
using namespace System::Collections::Generic;

namespace IKriv {
	namespace IsItMySource {
		namespace Dia {
			public enum class ChecksumTypeEnum
			{
				None = 0,
				Md5 = 1,
				Sha1 = 2
			};

			public ref class SourceFile
			{
			public:
				property String^ Path { String^ get() { return _path; }; }
				property ChecksumTypeEnum ChecksumType { ChecksumTypeEnum get() { return _checksumType; }; }
				property array<byte>^ Checksum { array<byte>^ get() { return _checksum; }; }
				property UInt32 Id { UInt32 get() { return _id; }}
			internal:
				SourceFile(IDiaSourceFile* pFile);
			private:
				String^ _path;
				ChecksumTypeEnum _checksumType;
				array<byte>^ _checksum;
				unsigned int _id;
			};

			class PdbFileImpl;

			public ref class PdbFile : IDisposable
			{
			public:
				PdbFile(String^ path);
				~PdbFile();
				IList<SourceFile^>^ GetSourceFiles();

			private:
				String^ _path;
				PdbFileImpl* _impl;
			};

			HRESULT EnsureSuccess(LPCWSTR message, HRESULT hr);
		}
	}
}
