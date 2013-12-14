using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TVShowRename
{
    public class NativeMethods
    {
        [DllImport("user32")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
    }
}
