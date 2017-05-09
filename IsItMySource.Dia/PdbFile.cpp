#include "stdafx.h"
#include "IsItMySource.Dia.h"

using namespace System;
using namespace System::Collections::Generic;
using namespace System::Runtime::InteropServices;

namespace IKriv {
	namespace IsItMySource {
		namespace Dia {
			class PdbFileImpl
			{
				CComPtr<IDiaDataSource> _dataSource;
				CComPtr<IDiaSession> _session;
				CComPtr<IDiaSymbol> _globalScope;

			public:
				PdbFileImpl(LPCOLESTR path)
				{
					EnsureSuccess(L"CoCreateInstance(DiaSource)",
						CoCreateInstance(
							__uuidof(DiaSource),
							NULL,
							CLSCTX_INPROC_SERVER,
							__uuidof(IDiaDataSource),
							(void **)&_dataSource));

					EnsureSuccess(L"IDiaDataSource.loadDataFromPdb()", _dataSource->loadDataFromPdb(path));
					EnsureSuccess(L"IDiaDataSource.openSession()", _dataSource->openSession(&_session));
					EnsureSuccess(L"IDiaSession.get_globalScope()", _session->get_globalScope(&_globalScope));
				}

				IDiaSession* session() { return _session; }
				IDiaSymbol* globalScope() { return _globalScope; }
			};

			PdbFile::PdbFile(String^ path)
			{
				_path = path;
				pin_ptr<const wchar_t> pinnedPath = PtrToStringChars(path);
				_impl = new PdbFileImpl(pinnedPath);
			}

			PdbFile::~PdbFile()
			{
				delete _impl;
			}

			IList<SourceFile^>^ PdbFile::GetSourceFiles()
			{
				std::set<DWORD> sourceFileIds;
				auto list = gcnew List<SourceFile^>();

				CComPtr<IDiaEnumSymbols> pEnumSymbols;
				EnsureSuccess(L"Retrieving list of compilands",
					_impl->globalScope()->findChildren(SymTagCompiland, NULL, nsNone, &pEnumSymbols));

				auto pSession = _impl->session();

				CComPtr<IDiaSymbol> pCompiland;
				ULONG celt = 0;
				while (SUCCEEDED(pEnumSymbols->Next(1, &pCompiland, &celt)) && (celt == 1))
				{
					CComPtr<IDiaEnumSourceFiles> pEnumSourceFiles;

					// should think of better error message here that does not sacrifice performance
					EnsureSuccess(L"Retrieving list of source files for a compiland",
						pSession->findFile(pCompiland, NULL, nsNone, &pEnumSourceFiles));

					CComPtr<IDiaSourceFile> pSourceFile;
					while (SUCCEEDED(pEnumSourceFiles->Next(1, &pSourceFile, &celt)) && (celt == 1))
					{
						DWORD id;
						EnsureSuccess(L"Retrieving source file id", pSourceFile->get_uniqueId(&id));
						if (sourceFileIds.find(id) != sourceFileIds.end()) continue; // this source is already found
						sourceFileIds.insert(id);
						list->Add(gcnew SourceFile(pSourceFile));
						pSourceFile.Release();
					}

					pCompiland.Release();
				}

				return list;
			}
		}
	}
}