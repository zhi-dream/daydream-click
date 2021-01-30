using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace daydream_click
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            _hotkeyIds = new Dictionary<int, string>{{0,"默认"},{101,"定位"}};

            var mouseImitateItem = new MouseImitateItem(_nextMouseItemIndex, RemoveMouseEventHandler,SetUpMouseSwitchHotkeyEventHandler);
            _mouseImitateItems = new Dictionary<int, MouseImitateItem> {{_nextMouseItemIndex, mouseImitateItem}};
            pnlMouse.Controls.Add(mouseImitateItem.GrpMouseItem);
            _nextMouseItemIndex += 1;

            var keyboardImitateItem = new KeyboardImitateItem(_nextKeyboardItemIndex, RemoveKeyboardEventHandler,SetUpKeyBoardSwitchHotkeyEventHandler);
            _keyboardImitateItems = new Dictionary<int, KeyboardImitateItem>
                {{_nextKeyboardItemIndex, keyboardImitateItem}};
            pnlKeyboard.Controls.Add(keyboardImitateItem.GrpKeyboardItem);
            _nextKeyboardItemIndex += 1;
        }

        private int _nextMouseItemIndex = 1;
        private readonly Dictionary<int, MouseImitateItem> _mouseImitateItems;

        private int _nextKeyboardItemIndex = 1;
        private readonly Dictionary<int, KeyboardImitateItem> _keyboardImitateItems;

        private readonly Dictionary<int, string> _hotkeyIds;

        private void btnMouseAppend_Click(object sender, EventArgs e)
        {
            var count = _mouseImitateItems.Count;
            var mouseImitateItem = new MouseImitateItem(count * 220, _nextMouseItemIndex, RemoveMouseEventHandler,SetUpMouseSwitchHotkeyEventHandler);
            pnlMouse.Controls.Add(mouseImitateItem.GrpMouseItem);
            _mouseImitateItems.Add(_nextMouseItemIndex, mouseImitateItem);
            _nextMouseItemIndex += 1;
        }

        private void RemoveMouseEventHandler(int removeIndex)
        {
            foreach (var mouseImitateItem in _mouseImitateItems.Values.Where(mouseImitateItem => mouseImitateItem.Index > removeIndex))
            {
                mouseImitateItem.GrpMouseItem.Location = new Point(mouseImitateItem.GrpMouseItem.Location.X,
                    mouseImitateItem.GrpMouseItem.Location.Y - 220);
            }
            _mouseImitateItems[removeIndex].JobStop();
            Hotkey.UnregisterHotKey(Handle,_mouseImitateItems[removeIndex].HotkeyId);
            _mouseImitateItems.Remove(removeIndex);
            pnlKeyboard.Controls.Remove(_mouseImitateItems[removeIndex].GrpMouseItem);
        }

        private void btnKeyboardAppend_Click(object sender, EventArgs e)
        {
            var count = _keyboardImitateItems.Count;
            var keyboardImitateItem =
                new KeyboardImitateItem(count * 220, _nextKeyboardItemIndex, RemoveKeyboardEventHandler,SetUpKeyBoardSwitchHotkeyEventHandler);
            pnlKeyboard.Controls.Add(keyboardImitateItem.GrpKeyboardItem);
            _keyboardImitateItems.Add(_nextKeyboardItemIndex, keyboardImitateItem);
            _nextKeyboardItemIndex += 1;
        }

        private void RemoveKeyboardEventHandler(int removeIndex)
        {
            foreach (var keyboardImitateItem in _keyboardImitateItems.Values.Where(keyboardImitateItem => keyboardImitateItem.Index > removeIndex))
            {
                keyboardImitateItem.GrpKeyboardItem.Location = new Point(
                    keyboardImitateItem.GrpKeyboardItem.Location.X,
                    keyboardImitateItem.GrpKeyboardItem.Location.Y - 220);
            }
            _keyboardImitateItems[removeIndex].JobStop();
            Hotkey.UnregisterHotKey(Handle,_keyboardImitateItems[removeIndex].HotkeyId);
            _keyboardImitateItems.Remove(removeIndex);
            pnlKeyboard.Controls.Remove(_keyboardImitateItems[removeIndex].GrpKeyboardItem);
        }
        
        private void SetUpMouseSwitchHotkeyEventHandler(int index,Keys keys)
        {
            if (_mouseImitateItems[index].HotkeyId!=default)
            {
                Hotkey.UnregisterHotKey(Handle, _mouseImitateItems[index].HotkeyId);
                _hotkeyIds.Remove(_mouseImitateItems[index].HotkeyId);
            }
            int hotkeyId;
            do
            {
                hotkeyId = Utils.GenerateHotkeyId();
                if (_hotkeyIds.ContainsKey(hotkeyId)) continue;
                _hotkeyIds.Add(hotkeyId,"鼠标");
                _mouseImitateItems[index].HotkeyId = hotkeyId;
                break;
            } while (true);
            Hotkey.RegisterHotKey(Handle, hotkeyId, KeyModifiers.Shift, keys);
        }

        private void SetUpKeyBoardSwitchHotkeyEventHandler(int index, Keys keys)
        {
            if (_keyboardImitateItems[index].HotkeyId!=default)
            {
                Hotkey.UnregisterHotKey(Handle, _keyboardImitateItems[index].HotkeyId);
                _hotkeyIds.Remove(_keyboardImitateItems[index].HotkeyId);
            }
            int hotkeyId;
            do
            {
                hotkeyId = Utils.GenerateHotkeyId();
                if (_hotkeyIds.ContainsKey(hotkeyId)) continue;
                _hotkeyIds.Add(hotkeyId,"键盘");
                _keyboardImitateItems[index].HotkeyId = hotkeyId;
                break;
            } while (true);
            Hotkey.RegisterHotKey(Handle, hotkeyId, KeyModifiers.Ctrl, keys);
        }

        protected override void WndProc(ref Message m)
        {
            const int hotkey = 0x0312;
            switch (m.Msg)
            {
                case hotkey:
                    var hotkeyId = m.WParam.ToInt32();
                    if (_hotkeyIds[hotkeyId]=="鼠标")
                    {
                        foreach (var mouseImitateItem in _mouseImitateItems.Values.Where(mouseImitateItem => mouseImitateItem.HotkeyId==hotkeyId))
                        {
                            mouseImitateItem.JobSwitch();
                        }
                    }
                    if (_hotkeyIds[hotkeyId]=="键盘")
                    {
                        foreach (var keyboardImitateItem in _keyboardImitateItems.Values.Where(keyboardImitateItem => keyboardImitateItem.HotkeyId==hotkeyId))
                        {
                            keyboardImitateItem.JobSwitch();
                        }
                    }
                    break;
            }
            base.WndProc(ref m);
        }
    }
}