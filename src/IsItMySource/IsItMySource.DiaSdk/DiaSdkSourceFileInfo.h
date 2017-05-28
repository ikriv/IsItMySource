#pragma once

using namespace System;
using namespace System::Collections::Generic;
using namespace IKriv::IsItMySource::Interfaces;
using ChecksumTypeEnum = IKriv::IsItMySource::Interfaces::ChecksumType;


namespace IKriv {
	namespace IsItMySource {
		namespace DiaSdk {

			ref class DiaSdkSourceFileInfo : ISourceFileInfo
			{
			public:
				virtual property String^ Path { String^ get() { return _path; }; }
				virtual property ChecksumTypeEnum ChecksumType { ChecksumTypeEnum get() { return _checksumType; }; }
				virtual property String^ ChecksumTypeStr { String^ get() { return _checksumType.ToString()->ToUpper(); }}
				virtual property array<byte>^ Checksum { array<byte>^ get() { return _checksum; }; }
			internal:
				property UInt32 Id { UInt32 get() { return _id; }}
				DiaSdkSourceFileInfo(IDiaSourceFile* pFile);
			private:
				String^ _path;
				ChecksumTypeEnum _checksumType;
				array<byte>^ _checksum;
				unsigned int _id;
			};
		}
	}
}

