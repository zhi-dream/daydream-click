using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace daydream_click
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            var mouseImitateItem = MouseImitateItem.GetMouseImitateItem(_nextMouseItemIndex,RemoveMouseEventHandler);
            _mouseImitateItems = new Dictionary<int, MouseImitateItem> {{_nextMouseItemIndex, mouseImitateItem}};
            pnlMouse.Controls.Add(mouseImitateItem.GrpMouseItem);
            _nextMouseItemIndex += 1;

            var keyboardImitateItem = KeyboardImitateItem.GetKeyboardImitateItem(_nextKeyboardItemIndex, RemoveKeyboardEventHandler);
            _keyboardImitateItems = new Dictionary<int, KeyboardImitateItem> {{_nextKeyboardItemIndex,keyboardImitateItem}};
            pnlKeyboard.Controls.Add(keyboardImitateItem.GrpKeyboardItem);
            _nextKeyboardItemIndex += 1;
        }
        
        private int _nextMouseItemIndex = 1;
        private readonly Dictionary<int,MouseImitateItem> _mouseImitateItems;
        
        private int _nextKeyboardItemIndex = 1;
        private readonly Dictionary<int,KeyboardImitateItem> _keyboardImitateItems;

        private void btnMouseAppend_Click(object sender, EventArgs e)
        {
            var count = _mouseImitateItems.Count;
            var mouseImitateItem = MouseImitateItem.GetMouseImitateItem(count*220,_nextMouseItemIndex,RemoveMouseEventHandler);
            pnlMouse.Controls.Add(mouseImitateItem.GrpMouseItem);
            _mouseImitateItems.Add(_nextMouseItemIndex,mouseImitateItem);
            _nextMouseItemIndex += 1;
        }

        private void RemoveMouseEventHandler(object sender, EventArgs e,int removeIndex)
        {
            foreach (var mouseImitateItem in _mouseImitateItems.Values)
            {
                if (mouseImitateItem.Index>removeIndex)
                {
                    mouseImitateItem.GrpMouseItem.Location = new Point(mouseImitateItem.GrpMouseItem.Location.X,mouseImitateItem.GrpMouseItem.Location.Y-220);
                }
                if (mouseImitateItem.Index != removeIndex) continue;
                // todo 停止任务 销毁快捷键
                _mouseImitateItems.Remove(removeIndex);
                pnlMouse.Controls.Remove(mouseImitateItem.GrpMouseItem);
            }
        }

        private void btnKeyboardAppend_Click(object sender, EventArgs e)
        {
            var count = _keyboardImitateItems.Count;
            var keyboardImitateItem = KeyboardImitateItem.GetKeyboardImitateItem(count*220,_nextKeyboardItemIndex,RemoveKeyboardEventHandler);
            pnlKeyboard.Controls.Add(keyboardImitateItem.GrpKeyboardItem);
            _keyboardImitateItems.Add(_nextKeyboardItemIndex,keyboardImitateItem);
            _nextKeyboardItemIndex += 1;
        }
        
        private void RemoveKeyboardEventHandler(object sender, EventArgs e,int removeIndex)
        {
            foreach (var keyboardImitateItem in _keyboardImitateItems.Values)
            {
                if (keyboardImitateItem.Index>removeIndex)
                {
                    keyboardImitateItem.GrpKeyboardItem.Location = new Point(keyboardImitateItem.GrpKeyboardItem.Location.X,keyboardImitateItem.GrpKeyboardItem.Location.Y-220);
                }
                if (keyboardImitateItem.Index != removeIndex) continue;
                // todo 停止任务 销毁快捷键
                _keyboardImitateItems.Remove(removeIndex);
                pnlKeyboard.Controls.Remove(keyboardImitateItem.GrpKeyboardItem);
            }
        }
        
    }
}