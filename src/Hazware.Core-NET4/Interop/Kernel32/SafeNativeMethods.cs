using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace Hazware.Interop.Kernel32
{
  [SuppressUnmanagedCodeSecurity]
  static class SafeNativeMethods
  {
    [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
    public static extern int GetModuleFileName(IntPtr hModule, StringBuilder path, int size);
  }
}
