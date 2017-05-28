#include "Stdafx.h"

using namespace System;
using namespace System::Runtime::InteropServices;

namespace IKriv {
	namespace IsItMySource {
		namespace DiaSdk {
			HRESULT EnsureSuccess(LPCWSTR message, HRESULT hr)
			{
				if (FAILED(hr)) throw gcnew COMException(gcnew String(message) + L" failed", hr);
				return hr;
			}
		}
	}
}