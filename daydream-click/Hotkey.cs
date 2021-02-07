using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace daydream_click
{
    public static class Hotkey
    {
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool RegisterHotKey(
            IntPtr hWnd,                
            int id,                     
            KeyModifiers fsModifiers,   
            Keys vk 
        );
        
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool UnregisterHotKey(
            IntPtr hWnd,
            int id
        );
    }
}