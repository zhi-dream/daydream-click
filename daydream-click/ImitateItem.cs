using System.Collections.Generic;

namespace daydream_click
{
    public class ImitateItem
    {
        
        public int NextMouseItemIndex { get; set; }
        
        public Dictionary<int, MouseImitateItem> MouseImitateItems { get; set; }

        public int NextKeyboardItemIndex { get; set; }
        
        public Dictionary<int, KeyboardImitateItem> KeyboardImitateItems { get; set; }

        public Dictionary<int, string> HotkeyIds { get; set; }

        public ImitateItem()
        {
            MouseImitateItems = new Dictionary<int, MouseImitateItem>();
            KeyboardImitateItems = new Dictionary<int, KeyboardImitateItem>();
            HotkeyIds = new Dictionary<int, string>();
        }

    }
}