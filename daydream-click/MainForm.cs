using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace daydream_click
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            Hotkey.RegisterHotKey(Handle, 100, KeyModifiers.Shift,
                Keys.D);
            _imitateItem = new ImitateItem();
            Drive.Initial();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (!File.Exists("imitateItem.json"))
            {
                Initial();
            }
            else
            {
                var imitateItem = new ImitateItem();
                try
                {
                    imitateItem = JsonConvert.DeserializeObject<ImitateItem>(File.ReadAllText("imitateItem.json"));
                }
                catch (Exception)
                {
                    Initial();
                }
                
                foreach (var mouseImitateItem in from mouseImitate in imitateItem.MouseImitateItems.Values
                    let count = pnlMouse.Controls.Count
                    select new MouseImitateItem(count * 220, mouseImitate.Index, mouseImitate.Name,
                        mouseImitate.Operate, mouseImitate.PositionX, mouseImitate.PositionY, mouseImitate.Interval,
                        mouseImitate.Hotkey, RemoveMouseEventHandler,
                        SetUpMouseSwitchHotkeyEventHandler))
                {
                    Hotkey.RegisterHotKey(Handle, mouseImitateItem.HotkeyId, KeyModifiers.Alt,
                        mouseImitateItem.Hotkey);

                    pnlMouse.Controls.Add(mouseImitateItem.GrpMouseItem);

                    _imitateItem.MouseImitateItems.Add(mouseImitateItem.Index, mouseImitateItem);
                }

                foreach (var keyboardImitateItem in from keyboardImitate in imitateItem.KeyboardImitateItems.Values
                    let count = pnlKeyboard.Controls.Count
                    select new KeyboardImitateItem(count * 220, keyboardImitate.Index, keyboardImitate.Name,
                        keyboardImitate.Operate, keyboardImitate.Interval, keyboardImitate.Hotkey,
                        RemoveKeyboardEventHandler,
                        SetUpKeyBoardSwitchHotkeyEventHandler))
                {
                    Hotkey.RegisterHotKey(Handle, keyboardImitateItem.HotkeyId, KeyModifiers.Ctrl,
                        keyboardImitateItem.Hotkey);

                    pnlKeyboard.Controls.Add(keyboardImitateItem.GrpKeyboardItem);

                    _imitateItem.KeyboardImitateItems.Add(keyboardImitateItem.Index, keyboardImitateItem);
                }
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (var mouseImitateItem in _imitateItem.MouseImitateItems.Values)
            {
                mouseImitateItem.Save();
            }

            foreach (var keyboardImitateItem in _imitateItem.KeyboardImitateItems.Values)
            {
                keyboardImitateItem.Save();
            }

            foreach (var hotkeyId in _imitateItem.HotkeyIds)
            {
                Hotkey.UnregisterHotKey(Handle, hotkeyId.Key);
            }

            File.WriteAllText("imitateItem.json", JsonConvert.SerializeObject(_imitateItem));
        }

        private readonly ImitateItem _imitateItem;

        private void Initial()
        {
            _imitateItem.HotkeyIds = new Dictionary<int, string> {{0, "默认"}, {100, "停止"},{101, "定位"}};
            _imitateItem.NextMouseItemIndex = 1;

            var mouseImitateItem = new MouseImitateItem(_imitateItem.NextMouseItemIndex, RemoveMouseEventHandler,
                SetUpMouseSwitchHotkeyEventHandler);

            _imitateItem.MouseImitateItems = new Dictionary<int, MouseImitateItem>
                {{_imitateItem.NextMouseItemIndex, mouseImitateItem}};

            pnlMouse.Controls.Add(mouseImitateItem.GrpMouseItem);

            _imitateItem.NextMouseItemIndex += 1;

            _imitateItem.NextKeyboardItemIndex = 1;

            var keyboardImitateItem = new KeyboardImitateItem(_imitateItem.NextKeyboardItemIndex,
                RemoveKeyboardEventHandler,
                SetUpKeyBoardSwitchHotkeyEventHandler);

            _imitateItem.KeyboardImitateItems = new Dictionary<int, KeyboardImitateItem>
                {{_imitateItem.NextKeyboardItemIndex, keyboardImitateItem}};

            pnlKeyboard.Controls.Add(keyboardImitateItem.GrpKeyboardItem);

            _imitateItem.NextKeyboardItemIndex += 1;
        }

        private void btnMouseAppend_Click(object sender, EventArgs e)
        {
            var count = _imitateItem.MouseImitateItems.Count;
            
            var mouseImitateItem = new MouseImitateItem(count * 220, _imitateItem.NextMouseItemIndex,
                RemoveMouseEventHandler,
                SetUpMouseSwitchHotkeyEventHandler);

            pnlMouse.Controls.Add(mouseImitateItem.GrpMouseItem);

            _imitateItem.MouseImitateItems.Add(_imitateItem.NextMouseItemIndex, mouseImitateItem);
            
            _imitateItem.NextMouseItemIndex += 1;
        }

        private void RemoveMouseEventHandler(int removeIndex)
        {
            foreach (var mouseImitateItem in _imitateItem.MouseImitateItems.Values.Where(mouseImitateItem =>
                mouseImitateItem.Index > removeIndex))
            {
                mouseImitateItem.GrpMouseItem.Location = new Point(mouseImitateItem.GrpMouseItem.Location.X,
                    mouseImitateItem.GrpMouseItem.Location.Y - 220);
            }
            
            _imitateItem.MouseImitateItems[removeIndex].JobStop();
            
            pnlMouse.Controls.Remove(_imitateItem.MouseImitateItems[removeIndex].GrpMouseItem);

            Hotkey.UnregisterHotKey(Handle, _imitateItem.MouseImitateItems[removeIndex].HotkeyId);
            _imitateItem.HotkeyIds.Remove(_imitateItem.MouseImitateItems[removeIndex].HotkeyId);
            
            _imitateItem.MouseImitateItems.Remove(removeIndex);
        }

        private bool SetUpMouseSwitchHotkeyEventHandler(int index, Keys keys)
        {

            if (_imitateItem.MouseImitateItems.Values.Count(mouseImitateItem => mouseImitateItem.Hotkey == keys)>1)
            {
                MessageBox.Show("此快捷键已存在\n相同快捷键只有第一个会生效\n请重新设置", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (_imitateItem.MouseImitateItems[index].HotkeyId != default)
            {
                Hotkey.UnregisterHotKey(Handle, _imitateItem.MouseImitateItems[index].HotkeyId);
                _imitateItem.HotkeyIds.Remove(_imitateItem.MouseImitateItems[index].HotkeyId);
            }

            int hotkeyId;
            do
            {
                hotkeyId = Utils.GenerateHotkeyId();
                if (_imitateItem.HotkeyIds.ContainsKey(hotkeyId)) continue;
                _imitateItem.HotkeyIds.Add(hotkeyId, "鼠标");
                _imitateItem.MouseImitateItems[index].HotkeyId = hotkeyId;
                break;
            } while (true);

            Hotkey.RegisterHotKey(Handle, hotkeyId, KeyModifiers.Alt, keys);
            return true;
        }

        private void btnKeyboardAppend_Click(object sender, EventArgs e)
        {
            var count = _imitateItem.KeyboardImitateItems.Count;
            var keyboardImitateItem =
                new KeyboardImitateItem(count * 220, _imitateItem.NextKeyboardItemIndex, RemoveKeyboardEventHandler,
                    SetUpKeyBoardSwitchHotkeyEventHandler);
            pnlKeyboard.Controls.Add(keyboardImitateItem.GrpKeyboardItem);
            _imitateItem.KeyboardImitateItems.Add(_imitateItem.NextKeyboardItemIndex, keyboardImitateItem);
            _imitateItem.NextKeyboardItemIndex += 1;
        }

        private void RemoveKeyboardEventHandler(int removeIndex)
        {
            foreach (var keyboardImitateItem in _imitateItem.KeyboardImitateItems.Values.Where(keyboardImitateItem =>
                keyboardImitateItem.Index > removeIndex))
            {
                keyboardImitateItem.GrpKeyboardItem.Location = new Point(
                    keyboardImitateItem.GrpKeyboardItem.Location.X,
                    keyboardImitateItem.GrpKeyboardItem.Location.Y - 220);
            }

            _imitateItem.KeyboardImitateItems[removeIndex].JobStop();
            pnlKeyboard.Controls.Remove(_imitateItem.KeyboardImitateItems[removeIndex].GrpKeyboardItem);
            
            Hotkey.UnregisterHotKey(Handle, _imitateItem.KeyboardImitateItems[removeIndex].HotkeyId);
            _imitateItem.HotkeyIds.Remove(_imitateItem.KeyboardImitateItems[removeIndex].HotkeyId);
            
            _imitateItem.KeyboardImitateItems.Remove(removeIndex);
        }

        private bool SetUpKeyBoardSwitchHotkeyEventHandler(int index, Keys keys)
        {
            if (_imitateItem.KeyboardImitateItems.Values.Count(keyboardImitateItem => keyboardImitateItem.Hotkey == keys)>1)
            {
                MessageBox.Show("此快捷键已存在\n相同快捷键只有第一个会生效\n请重新设置", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            
            if (_imitateItem.KeyboardImitateItems[index].HotkeyId != default)
            {
                Hotkey.UnregisterHotKey(Handle, _imitateItem.KeyboardImitateItems[index].HotkeyId);
                _imitateItem.HotkeyIds.Remove(_imitateItem.KeyboardImitateItems[index].HotkeyId);
            }

            int hotkeyId;
            do
            {
                hotkeyId = Utils.GenerateHotkeyId();
                if (_imitateItem.HotkeyIds.ContainsKey(hotkeyId)) continue;
                _imitateItem.HotkeyIds.Add(hotkeyId, "键盘");
                _imitateItem.KeyboardImitateItems[index].HotkeyId = hotkeyId;
                break;
            } while (true);

            Hotkey.RegisterHotKey(Handle, hotkeyId, KeyModifiers.Ctrl, keys);
            return true;
        }

        protected override void WndProc(ref Message m)
        {
            const int hotkey = 0x0312;
            switch (m.Msg)
            {
                case hotkey:
                    var hotkeyId = m.WParam.ToInt32();

                    if (_imitateItem.HotkeyIds[hotkeyId] == "停止")
                    {
                        foreach (var mouseImitateItem in _imitateItem.MouseImitateItems.Values)
                        {
                            mouseImitateItem.JobStop();
                        }
                        foreach (var keyboardImitateItem in _imitateItem.KeyboardImitateItems.Values)
                        {
                            keyboardImitateItem.JobStop();
                        }
                    }
                    
                    if (_imitateItem.HotkeyIds[hotkeyId] == "鼠标")
                    {
                        foreach (var mouseImitateItem in _imitateItem.MouseImitateItems.Values.Where(mouseImitateItem =>
                            mouseImitateItem.HotkeyId == hotkeyId))
                        {
                            mouseImitateItem.JobSwitch();
                        }
                    }

                    if (_imitateItem.HotkeyIds[hotkeyId] == "键盘")
                    {
                        foreach (var keyboardImitateItem in _imitateItem.KeyboardImitateItems.Values.Where(
                            keyboardImitateItem =>
                                keyboardImitateItem.HotkeyId == hotkeyId))
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