using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace daydream_click
{
    public static class Utils
    {

        public static string KeysChangeChar(Keys key)
        {
            return (int) key switch
            {
                13 => "Enter",
                16 => "Shift",
                17 => "Ctrl",
                18 => "Alt",
                27 => "Esc",
                96 => "Num0",
                97 => "Num1",
                98 => "Num2",
                99 => "Num3",
                100 => "Num4",
                101 => "Num5",
                102 => "Num6",
                103 => "Num7",
                104 => "Num8",
                105 => "Num9",
                106 => "*",
                107 => "+",
                109 => "-",
                110 => ".",
                111 => "/",
                186 => ":",
                187 => "=",
                188 => "<",
                189 => "_",
                190 => ">",
                191 => "?",
                192 => "~",
                219 => "{",
                220 => "|",
                221 => "}",
                222 => "\"",
                _ => key.ToString()
            };
        }

        public static string KeysSplice(IEnumerable<Keys> keys)
        {
            var keysString = keys.Aggregate("", (current, key) => current + KeysChangeChar(key) + "+");
            keysString = keysString.Substring(0, keysString.Length-1);
            return keysString;
        }

        public static int GenerateHotkeyId()
        {
            return new Random().Next();
        }

    }
}