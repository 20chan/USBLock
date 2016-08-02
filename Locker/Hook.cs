using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Locker
{
    public static class Hook
    {
        [DllImport("user32.dll")]
        private static extern int SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);
        [DllImport("user32.dll")]
        private static extern bool UnhookWindowsHookEx(int hhk);
        [DllImport("user32.dll")]
        private static extern int CallNextHookEx(int hhk, int nCode, int wParam, ref KBDLLHOOKSTRUCT lParam);
        [DllImport("kernel32.dll")]
        private static extern IntPtr GetModuleHandle(string lpModuleName);
        public struct KBDLLHOOKSTRUCT
        {
            public Keys vkCode;
            public int scanCode;
            public int flags;
            public int time;
            public int dwExtraInfo;
        }

        private const int WM_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_KEYUP = 0x0101;
        private const int WM_SYSKEYDOWN = 0x0104;
        private const int WM_SYSKEYUP = 0x0105;
        private delegate int LowLevelKeyboardProc(int nCode, int wParam, ref KBDLLHOOKSTRUCT lParam);
        private static int _hookID = 0;
        private static LowLevelKeyboardProc _proc = HookCallback;

        public static void Start()
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                _hookID = SetWindowsHookEx(WM_KEYBOARD_LL, _proc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        public static void End()
        {
            UnhookWindowsHookEx(_hookID);
        }

        private static int HookCallback(int nCode, int wParam, ref KBDLLHOOKSTRUCT lParam)
        {
            if (nCode >= 0)
            {
                if (wParam == WM_KEYDOWN || wParam == WM_SYSKEYDOWN || wParam == WM_KEYUP || wParam == WM_SYSKEYUP)
                {
                    return 1;
                }
                if (lParam.vkCode == Keys.RWin || lParam.vkCode == Keys.LWin)
                    return 1;

                bool blnEat = false;

                switch (wParam)
                {
                    case 256:
                    case 257:
                    case 260:
                    case 261:
                        //Alt+Tab, Alt+Esc, Ctrl+Esc, Windows Key,
                        blnEat = (((int)lParam.vkCode == 9) && (lParam.flags == 32)) | (((int)lParam.vkCode == 27) && (lParam.flags == 32)) | (((int)lParam.vkCode == 27) && (lParam.flags == 0)) | (((int)lParam.vkCode == 91) && ((int)lParam.flags == 1)) | (((int)lParam.vkCode == 92) && (lParam.flags == 1)) | (((int)lParam.vkCode == 73) && (lParam.flags == 0));
                        break;
                }

                if (blnEat == true)
                {
                    return 1;
                }
            }
            return CallNextHookEx(0, nCode, wParam, ref lParam);
        }
    }
}
