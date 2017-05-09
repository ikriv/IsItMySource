#include "stdafx.h"
#include "IsItMySource.Dia.h"

using namespace System::Runtime::InteropServices;

namespace IKriv {
	namespace IsItMySource {
		namespace Dia {
			SourceFile::SourceFile(IDiaSourceFile* pFile)
			{
				HRESULT hr;

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
					EnsureSuccess(L"IDiaSourceFile.get_checksum 2nd call", hr);
					_checksum = gcnew array<byte>(cbData);

					std::vector<BYTE> data(cbData);
					EnsureSuccess(L"IDiaSourceFile.get_checksum",
						pFile->get_checksum(cbData, &cbData, &data[0]));

					Marshal::Copy(IntPtr((void*)&data[0]), _checksum, 0, cbData);
				}
			}
		}
	}
}
