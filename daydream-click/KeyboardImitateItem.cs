using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace daydream_click
{
    public class KeyboardImitateItem
    {
        private GroupBox _grpKeyboardItem;
        private Label _lblKeyboardKey;
        private Label _lblKeyboardInterval;
        private Label _lblKeyboardUnit;
        private Label _lblKeyboardHotkey;

        private TextBox _txtKeyboardKey;
        private TextBox _txtKeyboardInterval;
        private TextBox _txtKeyboardHotkey;

        private Button _btnKeyboardKey;
        private Button _btnKeyboardRemove;
        private Button _btnKeyboardRename;
        private Button _btnKeyboardSetUp;
        private Button _btnKeyboardSwitch;

        private readonly int _index;
        private readonly string _name;
        private readonly List<Keys> _operate;
        private readonly string _interval;
        private readonly Keys _hotkey;

        public delegate void RemoveEventHandler(object sender, EventArgs e, int removeIndex);

        private KeyboardImitateItem(int index, RemoveEventHandler removeEventHandler)
        {
            _index = index;
            _name = "模拟" + index;
            _hotkey = Keys.None;
            InitializeComponent(0, removeEventHandler);
        }

        private KeyboardImitateItem(int offset, int index, RemoveEventHandler removeEventHandler)
        {
            _index = index;
            _name = "模拟" + index;
            _hotkey = Keys.None;
            InitializeComponent(offset, removeEventHandler);
        }

        private KeyboardImitateItem(int offset, int index, string name, List<Keys> operate, string interval,
            Keys hotkey, RemoveEventHandler removeEventHandler)
        {
            _index = index;
            _name = name;
            _operate = operate;
            _interval = interval;
            _hotkey = hotkey;
            InitializeComponent(offset, removeEventHandler);
        }

        public static KeyboardImitateItem GetKeyboardImitateItem(int index, RemoveEventHandler removeEventHandler)
        {
            return new(index, removeEventHandler);
        }

        public static KeyboardImitateItem GetKeyboardImitateItem(int offset, int index,
            RemoveEventHandler removeEventHandler)
        {
            return new(offset, index, removeEventHandler);
        }

        public static KeyboardImitateItem GetKeyboardImitateItem(int offset, int index, string name, List<Keys> operate,
            string interval, Keys hotkey, RemoveEventHandler removeEventHandler)
        {
            return new(offset, index, name, operate, interval, hotkey, removeEventHandler);
        }

        private void InitializeComponent(int offset, RemoveEventHandler removeEventHandler)
        {
            _grpKeyboardItem = new GroupBox();
            _lblKeyboardKey = new Label();
            _lblKeyboardInterval = new Label();
            _lblKeyboardUnit = new Label();
            _lblKeyboardHotkey = new Label();
            _txtKeyboardKey = new TextBox();
            _txtKeyboardInterval = new TextBox();
            _txtKeyboardHotkey = new TextBox();
            _btnKeyboardKey = new Button();
            _btnKeyboardRemove = new Button();
            _btnKeyboardRename = new Button();
            _btnKeyboardSetUp = new Button();
            _btnKeyboardSwitch = new Button();

            _grpKeyboardItem.Controls.Add(_lblKeyboardKey);
            _grpKeyboardItem.Controls.Add(_lblKeyboardInterval);
            _grpKeyboardItem.Controls.Add(_lblKeyboardUnit);
            _grpKeyboardItem.Controls.Add(_lblKeyboardHotkey);
            _grpKeyboardItem.Controls.Add(_txtKeyboardKey);
            _grpKeyboardItem.Controls.Add(_txtKeyboardInterval);
            _grpKeyboardItem.Controls.Add(_txtKeyboardHotkey);
            _grpKeyboardItem.Controls.Add(_btnKeyboardKey);
            _grpKeyboardItem.Controls.Add(_btnKeyboardRemove);
            _grpKeyboardItem.Controls.Add(_btnKeyboardRename);
            _grpKeyboardItem.Controls.Add(_btnKeyboardSetUp);
            _grpKeyboardItem.Controls.Add(_btnKeyboardSwitch);

            _grpKeyboardItem.Name = "_grpKeyboardItem";
            _grpKeyboardItem.Size = new Size(756, 214);
            _grpKeyboardItem.Location = new Point(3, 3 + offset);
            _grpKeyboardItem.TabIndex = 0;
            _grpKeyboardItem.TabStop = false;
            _grpKeyboardItem.Text = _name;
            _grpKeyboardItem.Font = new Font("字体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            _grpKeyboardItem.BackColor = Color.Transparent;

            _lblKeyboardKey.Name = "_lblKeyboardKey";
            _lblKeyboardKey.Size = new Size(153, 41);
            _lblKeyboardKey.Location = new Point(6, 63);
            _lblKeyboardKey.TabIndex = 0;
            _lblKeyboardKey.Text = "模拟按键:";
            _lblKeyboardKey.TextAlign = ContentAlignment.MiddleLeft;
            _lblKeyboardKey.Font = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);

            _lblKeyboardInterval.Name = "_lblKeyboardInterval";
            _lblKeyboardInterval.Size = new Size(153, 41);
            _lblKeyboardInterval.Location = new Point(6, 145);
            _lblKeyboardInterval.TabIndex = 0;
            _lblKeyboardInterval.Text = "时间间隔:";
            _lblKeyboardInterval.TextAlign = ContentAlignment.MiddleLeft;
            _lblKeyboardInterval.Font = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);

            _lblKeyboardUnit.Name = "_lblKeyboardUnit";
            _lblKeyboardUnit.Size = new Size(75, 41);
            _lblKeyboardUnit.Location = new Point(339, 145);
            _lblKeyboardUnit.TabIndex = 0;
            _lblKeyboardUnit.Text = "毫秒";
            _lblKeyboardHotkey.TextAlign = ContentAlignment.MiddleLeft;
            _lblKeyboardUnit.Font = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);

            _lblKeyboardHotkey.Name = "_lblKeyboardHotkey";
            _lblKeyboardHotkey.Size = new Size(118, 41);
            _lblKeyboardHotkey.Location = new Point(509, 104);
            _lblKeyboardHotkey.TabIndex = 0;
            _lblKeyboardHotkey.Text = "快捷键:";
            _lblKeyboardHotkey.TextAlign = ContentAlignment.MiddleLeft;
            _lblKeyboardHotkey.Font = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);

            _txtKeyboardKey.Name = "_txtKeyboardKey";
            _txtKeyboardKey.Size = new Size(125, 42);
            _txtKeyboardKey.Location = new Point(160, 63);
            _txtKeyboardKey.ReadOnly = true;
            _txtKeyboardKey.TabIndex = 0;
            _txtKeyboardKey.TabStop = false;
            // var operate = _operate.Aggregate("", (current, key) => current + key + "+");
            // operate = operate.Substring(0, operate.Length-1);
            // _txtKeyboardKey.Text = operate;
            _txtKeyboardKey.TextAlign = HorizontalAlignment.Center;
            _txtKeyboardKey.Font = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);

            _txtKeyboardInterval.Name = "_txtKeyboardInterval";
            _txtKeyboardInterval.Size = new Size(175, 42);
            _txtKeyboardInterval.Location = new Point(160, 142);
            _txtKeyboardInterval.TabIndex = 3;
            _txtKeyboardInterval.Text = _interval;
            _txtKeyboardInterval.TextAlign = HorizontalAlignment.Right;
            _txtKeyboardInterval.Font = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            _txtKeyboardInterval.KeyPress += (_, e) =>
            {
                switch (e.KeyChar)
                {
                    case '\b':
                        return;
                    case < '0':
                    case > '9':
                        e.Handled = true;
                        break;
                }
            };

            _txtKeyboardHotkey.Name = "_txtKeyboardHotkey";
            _txtKeyboardHotkey.Size = new Size(120, 35);
            _txtKeyboardHotkey.Location = new Point(630, 109);
            _txtKeyboardHotkey.ReadOnly = true;
            _txtKeyboardHotkey.TabIndex = 0;
            _txtKeyboardHotkey.TabStop = false;
            _txtKeyboardHotkey.Text = _hotkey != Keys.None ? "Alt+" + _hotkey : _hotkey.ToString();
            _txtKeyboardHotkey.TextAlign = HorizontalAlignment.Center;
            _txtKeyboardHotkey.Font = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            _txtKeyboardHotkey.BorderStyle = BorderStyle.None;

            _btnKeyboardKey.Name = "_btnKeyboardKey";
            _btnKeyboardKey.Size = new Size(83, 42);
            _btnKeyboardKey.Location = new Point(291, 63);
            _btnKeyboardKey.Cursor = Cursors.Hand;
            _btnKeyboardKey.ForeColor = SystemColors.ControlText;
            _btnKeyboardKey.TabIndex = 2;
            _btnKeyboardKey.Text = "捕获";
            _btnKeyboardKey.UseVisualStyleBackColor = true;
            _btnKeyboardKey.BackColor = Color.Transparent;
            _btnKeyboardKey.Click += (_, _) =>
            {
                // todo 弹出获取定位窗口
            };

            _btnKeyboardRemove.Name = "_btnKeyboardRemove";
            _btnKeyboardRemove.Size = new Size(83, 42);
            _btnKeyboardRemove.Location = new Point(650, 43);
            _btnKeyboardRemove.Cursor = Cursors.Hand;
            _btnKeyboardRemove.ForeColor = SystemColors.ControlText;
            _btnKeyboardRemove.TabIndex = 7;
            _btnKeyboardRemove.Text = "删除";
            _btnKeyboardRemove.UseVisualStyleBackColor = false;
            _btnKeyboardRemove.BackColor = Color.Transparent;
            _btnKeyboardRemove.Click += (o, e) => { removeEventHandler(o, e, _index); };

            _btnKeyboardRename.Name = "_btnKeyboardRename";
            _btnKeyboardRename.Size = new Size(83, 42);
            _btnKeyboardRename.Location = new Point(472, 162);
            _btnKeyboardRename.Cursor = Cursors.Hand;
            _btnKeyboardRename.ForeColor = SystemColors.ControlText;
            _btnKeyboardRename.TabIndex = 6;
            _btnKeyboardRename.Text = "命名";
            _btnKeyboardRename.UseVisualStyleBackColor = false;
            _btnKeyboardRename.BackColor = Color.Transparent;
            _btnKeyboardRename.Click += (_, _) =>
            {
                // todo 弹出模拟项命名窗口
            };

            _btnKeyboardSetUp.Name = "_btnKeyboardSetUp";
            _btnKeyboardSetUp.Size = new Size(83, 42);
            _btnKeyboardSetUp.Location = new Point(561, 162);
            _btnKeyboardSetUp.Cursor = Cursors.Hand;
            _btnKeyboardSetUp.BackColor = Color.Transparent;
            _btnKeyboardSetUp.ForeColor = SystemColors.ControlText;
            _btnKeyboardSetUp.TabIndex = 5;
            _btnKeyboardSetUp.Text = "设置";
            _btnKeyboardSetUp.UseVisualStyleBackColor = false;
            _btnKeyboardSetUp.Click += (_, _) =>
            {
                // todo 弹出快捷键设置窗口
            };

            _btnKeyboardSwitch.Name = "_btnKeyboardSwitch";
            _btnKeyboardSwitch.Size = new Size(83, 42);
            _btnKeyboardSwitch.Location = new Point(650, 162);
            _btnKeyboardSwitch.Cursor = Cursors.Hand;
            _btnKeyboardSwitch.BackColor = Color.Transparent;
            _btnKeyboardSwitch.ForeColor = SystemColors.ControlText;
            _btnKeyboardSwitch.TabIndex = 4;
            _btnKeyboardSwitch.Text = "开始";
            _btnKeyboardSwitch.UseVisualStyleBackColor = false;
            _btnKeyboardSwitch.Click += (_, _) =>
            {
                // todo 调用开启鼠标任务方法
            };
        }

        public GroupBox GrpKeyboardItem => _grpKeyboardItem;

        public Label LblKeyboardOperate => _lblKeyboardKey;

        public Label LblKeyboardInterval => _lblKeyboardInterval;

        public Label LblKeyboardUnit => _lblKeyboardUnit;

        public Label LblKeyboardHotkey => _lblKeyboardHotkey;

        public TextBox TxtKeyboardOperate => _txtKeyboardKey;

        public TextBox TxtKeyboardInterval => _txtKeyboardInterval;

        public TextBox TxtKeyboardHotkey => _txtKeyboardHotkey;

        public Button BtnKeyboardPosition => _btnKeyboardKey;

        public Button BtnKeyboardRemove => _btnKeyboardRemove;

        public Button BtnKeyboardRename => _btnKeyboardRename;

        public Button BtnKeyboardSetUp => _btnKeyboardSetUp;

        public Button BtnKeyboardSwitch => _btnKeyboardSwitch;

        public int Index => _index;

        public string Name => _name;

        public List<Keys> Operate => _operate;

        public string Interval => _interval;

        public Keys Hotkey => _hotkey;
    }
}