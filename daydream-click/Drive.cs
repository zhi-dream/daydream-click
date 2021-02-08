using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace daydream_click
{
    public static class Drive
    {
        private static readonly Cdd Dd = new();

        public static void Initial()
        {

            if (Dd.Load("DDHID64.dll") != 1)
            {
                MessageBox.Show("驱动加载失败。\n详细原因请查看【使用说明书.docx】", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            if (Dd.Btn(0) != 1)
            {
                MessageBox.Show("驱动初始化失败。\n详细原因请查看【使用说明书.docx】", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public static void MouseLeftKeyClickDown()
        {
            Dd.Btn(1);
        }

        public static void MouseLeftKeyClickUp()
        {
            Dd.Btn(2);
        }

        public static void MouseRightKeyClickDown()
        {
            Dd.Btn(4);
        }

        public static void MouseRightKeyClickUp()
        {
            Dd.Btn(8);
        }

        public static void MouseMove(int x, int y)
        {
            Dd.Mov(x, y);
        }

        public static int KeyboardChange(Keys key)
        {
            return Dd.Todc((int) key);
        }

        public static void KeyboardClickDown(Keys key)
        {
            Dd.Key(Dd.Todc((int) key), 1);
        }

        public static void KeyboardClickUp(Keys key)
        {
            Dd.Key(KeyboardChange(key), 2);
        }
    }

    internal class Cdd
    {
        [DllImport("Kernel32.dll")]
        private static extern IntPtr LoadLibrary(string dllFile);

        [DllImport("Kernel32.dll")]
        private static extern IntPtr GetProcAddress(IntPtr hModule, string lpProcName);

        [DllImport("kernel32.dll")]
        public static extern bool FreeLibrary(IntPtr hModule);


        public delegate int pDD_btn(int btn);

        public delegate int pDD_whl(int whl);

        public delegate int pDD_key(int ddcode, int flag);

        public delegate int pDD_mov(int x, int y);

        public delegate int pDD_movR(int dx, int dy);

        public delegate int pDD_str(string str);

        public delegate int pDD_todc(int vkcode);
        
        public pDD_btn Btn;
        
        public pDD_whl Whl;
        
        public pDD_mov Mov;
        
        public pDD_movR MovR;
        
        public pDD_key Key;
        
        public pDD_str Str;
        
        public pDD_todc Todc;

        private IntPtr _mHinst;

        ~Cdd()
        {
            if (!_mHinst.Equals(IntPtr.Zero))
            {
                bool b = FreeLibrary(_mHinst);
            }
        }


        public int Load(string dllFile)
        {
            _mHinst = LoadLibrary(dllFile);
            if (_mHinst.Equals(IntPtr.Zero))
            {
                return -2;
            }

            return GetDDfunAddress(_mHinst);
        }

        private int GetDDfunAddress(IntPtr hinst)
        {
            var ptr = GetProcAddress(hinst, "DD_btn");
            if (ptr.Equals(IntPtr.Zero))
            {
                return -1;
            }

            Btn = Marshal.GetDelegateForFunctionPointer(ptr, typeof(pDD_btn)) as pDD_btn;

            if (ptr.Equals(IntPtr.Zero))
            {
                return -1;
            }

            ptr = GetProcAddress(hinst, "DD_whl");
            Whl = Marshal.GetDelegateForFunctionPointer(ptr, typeof(pDD_whl)) as pDD_whl;

            if (ptr.Equals(IntPtr.Zero))
            {
                return -1;
            }

            ptr = GetProcAddress(hinst, "DD_mov");
            Mov = Marshal.GetDelegateForFunctionPointer(ptr, typeof(pDD_mov)) as pDD_mov;

            if (ptr.Equals(IntPtr.Zero))
            {
                return -1;
            }

            ptr = GetProcAddress(hinst, "DD_key");
            Key = Marshal.GetDelegateForFunctionPointer(ptr, typeof(pDD_key)) as pDD_key;

            if (ptr.Equals(IntPtr.Zero))
            {
                return -1;
            }

            ptr = GetProcAddress(hinst, "DD_movR");
            MovR = Marshal.GetDelegateForFunctionPointer(ptr, typeof(pDD_movR)) as pDD_movR;

            if (ptr.Equals(IntPtr.Zero))
            {
                return -1;
            }

            ptr = GetProcAddress(hinst, "DD_str");
            Str = Marshal.GetDelegateForFunctionPointer(ptr, typeof(pDD_str)) as pDD_str;

            if (ptr.Equals(IntPtr.Zero))
            {
                return -1;
            }

            ptr = GetProcAddress(hinst, "DD_todc");
            Todc = Marshal.GetDelegateForFunctionPointer(ptr, typeof(pDD_todc)) as pDD_todc;

            return 1;
        }
    }
}