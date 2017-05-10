using System.Runtime.InteropServices;

namespace IKriv.IsItMySource.DiaSymReader
{
    internal class Ensure
    {
        public static void Success(string message, int hr)
        {
            if (hr<0) throw new COMException(message, hr);
        }
    }
}
