#include "stdafx.h"
#include "EnsureSuccess.h"
#include "DiaSdkSourceFileInfo.h"

using namespace System::Runtime::InteropServices;

namespace IKriv {
	namespace IsItMySource {
		namespace DiaSdk {
			DiaSdkSourceFileInfo::DiaSdkSourceFileInfo(IDiaSourceFile* pFile)
			{
				DWORD id;
				EnsureSuccess(L"IDiaSourceFile.get_uniqueId()",
					pFile->get_uniqueId(&id));

				_id = id;

				DWORD checksumType;
				EnsureSuccess(L"IDiaSourceFile.get_checksumType()",
					pFile->get_checksumType(&checksumType));
				_checksumType = static_cast<ChecksumTypeEnum>(checksumType);

				BSTR fileName;
				EnsureSuccess(L"IDiaSourceFile.get_fileName",
					pFile->get_fileName(&fileName));
				_path = Marshal::PtrToStringBSTR(static_cast<IntPtr>(fileName));
				SysFreeString(fileName);

				DWORD cbData = 0;
				EnsureSuccess(L"IDiaSourceFile.get_checksum 1st call", pFile->get_checksum(0, &cbData, NULL));
				if (cbData == 0)
				{
					_checksum = gcnew array<byte>(0);
				}
				else
				{
					_checksum = gcnew array<byte>(cbData);

					std::vector<BYTE> data(cbData);
					EnsureSuccess(L"IDiaSourceFile.get_checksum 2nd call",
						pFile->get_checksum(cbData, &cbData, &data[0]));

					Marshal::Copy(IntPtr((void*)&data[0]), _checksum, 0, cbData);
				}
			}
		}
	}
}
