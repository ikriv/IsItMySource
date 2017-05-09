#include "stdafx.h"
#include "IsItMySource.Dia.h"

using namespace System::Runtime::InteropServices;

namespace IKriv {
	namespace IsItMySource {
		namespace Dia {

			HRESULT EnsureSuccess(LPCWSTR message, HRESULT hr)
			{
				if (FAILED(hr)) throw gcnew COMException(gcnew String(message) + L" failed", hr);
				return hr;
			}
		}
	}
}