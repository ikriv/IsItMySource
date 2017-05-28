#include "stdafx.h"
#include "EnsureSuccess.h"
#include "DiaSdkSourceFileInfo.h"
#include "DiaSdkDebugInfo.h"

using namespace System;
using namespace System::Collections::Generic;
using namespace System::Runtime::InteropServices;

namespace IKriv {
	namespace IsItMySource {
		namespace DiaSdk {
			class DebugInfoImpl
			{
				CComPtr<IDiaDataSource> _dataSource;
				CComPtr<IDiaSession> _session;
				CComPtr<IDiaSymbol> _globalScope;

			public:
				DebugInfoImpl(bool isPdb, LPCOLESTR path, LPCOLESTR pdbSearchPath)
				{
					EnsureSuccess(L"CoCreateInstance(DiaSource)",
						CoCreateInstance(
							__uuidof(DiaSource),
							NULL,
							CLSCTX_INPROC_SERVER,
							__uuidof(IDiaDataSource),
							(void **)&_dataSource));

					if (isPdb)
					{
						EnsureSuccess(L"IDiaDataSource.loadDataFromPdb()", _dataSource->loadDataFromPdb(path));
					}
					else
					{
						EnsureSuccess(L"IDiaDataSource.loadDataFForExe()", _dataSource->loadDataForExe(path, pdbSearchPath, NULL));
					}
					EnsureSuccess(L"IDiaDataSource.openSession()", _dataSource->openSession(&_session));
					EnsureSuccess(L"IDiaSession.get_globalScope()", _session->get_globalScope(&_globalScope));
				}

				IDiaSession* session() { return _session; }
				IDiaSymbol* globalScope() { return _globalScope; }
			};

			DiaSdkDebugInfo::DiaSdkDebugInfo(String^ exeOrPdbPath, String^ searchPath)
			{
				_impl = nullptr;
				if (exeOrPdbPath == nullptr) throw gcnew ArgumentNullException("exeOrPdbPath");

				_path = exeOrPdbPath;
				pin_ptr<const wchar_t> pinnedPath = PtrToStringChars(_path);

				bool isPdb = _path->ToLowerInvariant()->EndsWith(".pdb");
				if (isPdb)
				{
					_impl = new DebugInfoImpl(true, pinnedPath, NULL);
				}
				else
				{
					pin_ptr<const wchar_t> pinnedSearchPath = PtrToStringChars(searchPath);
					_impl = new DebugInfoImpl(false, pinnedPath, pinnedSearchPath);
				}
			}

			DiaSdkDebugInfo::~DiaSdkDebugInfo()
			{
				delete _impl;
			}

			IEnumerable<ISourceFileInfo^>^ DiaSdkDebugInfo::GetSourceFiles()
			{
				std::set<DWORD> sourceFileIds;
				auto list = gcnew List<ISourceFileInfo^>();

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
						list->Add(gcnew DiaSdkSourceFileInfo(pSourceFile));
						pSourceFile.Release();
					}

					pCompiland.Release();
				}

				return list;
			}
		}
	}
}