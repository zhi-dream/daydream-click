using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace daydream_click
{
    public class KeyboardImitateItem
    {
        private Label _lblKeyboardOperate;
        private Label _lblKeyboardInterval;
        private Label _lblKeyboardUnit;
        private Label _lblKeyboardHotkey;

        private TextBox _txtKeyboardOperate;
        private TextBox _txtKeyboardInterval;
        private TextBox _txtKeyboardHotkey;

        private Button _btnKeyboardKey;
        private Button _btnKeyboardRemove;
        private Button _btnKeyboardRename;
        private Button _btnKeyboardSetUp;
        private Button _btnKeyboardSwitch;

        private KeyboardImitateJob _keyboardImitateJob;

        public delegate void RemoveEventHandler(int removeIndex);

        public delegate bool SetUpSwitchHotkeyEventHandler(int index, Keys keys);

        public KeyboardImitateItem(int index, RemoveEventHandler removeEventHandler,
            SetUpSwitchHotkeyEventHandler setUpSwitchHotkeyEventHandler)
        {
            Index = index;
            Name = "模拟" + index;
            Hotkey = Keys.None;
            InitializeComponent(0, removeEventHandler, setUpSwitchHotkeyEventHandler);
        }

        public KeyboardImitateItem(int offset, int index, RemoveEventHandler removeEventHandler,
            SetUpSwitchHotkeyEventHandler setUpSwitchHotkeyEventHandler)
        {
            Index = index;
            Name = "模拟" + index;
            Hotkey = Keys.None;
            InitializeComponent(offset, removeEventHandler, setUpSwitchHotkeyEventHandler);
        }

        public KeyboardImitateItem(int offset, int index, string name, List<Keys> operate, string interval,
            Keys hotkey, RemoveEventHandler removeEventHandler,
            SetUpSwitchHotkeyEventHandler setUpSwitchHotkeyEventHandler)
        {
            Index = index;
            Name = name;
            Operate = operate;
            Interval = interval;
            Hotkey = hotkey;
            InitializeComponent(offset, removeEventHandler, setUpSwitchHotkeyEventHandler);
        }

        public KeyboardImitateItem()
        {
        }

        public void Save()
        {
            Name = GrpKeyboardItem.Text;
            Interval = _txtKeyboardInterval.Text;
        }

        public void JobStop()
        {
            if (_keyboardImitateJob == null) return;
            _keyboardImitateJob.Stop();
            _btnKeyboardSwitch.Text = "开启";
        }

        public void JobStart()
        {
            if (_txtKeyboardOperate.Text.Trim().Length == 0)
            {
                MessageBox.Show("请捕获模拟按键。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (_txtKeyboardInterval.Text.Trim().Length == 0)
            {
                MessageBox.Show("请输入时间间隙。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (_txtKeyboardHotkey.Text.Trim() == "None")
            {
                if (MessageBox.Show("还未设置开关快捷键，是否仍要开启？\n【Shift+D】可强制关闭所有任务。","提示",MessageBoxButtons.YesNo,MessageBoxIcon.Information)!=DialogResult.Yes)
                {
                    return;
                }
            }

            _keyboardImitateJob = new KeyboardImitateJob(
                Operate,
                int.Parse(_txtKeyboardInterval.Text.Trim())
            );
            new Thread(_keyboardImitateJob.Execute).Start();
            _btnKeyboardSwitch.Text = "结束";
        }

        public void JobSwitch()
        {
            switch (_btnKeyboardSwitch.Text)
            {
                case "开启":
                    JobStart();
                    break;
                case "结束":
                    JobStop();
                    break;
            }
        }

        private void InitializeComponent(int offset, RemoveEventHandler removeEventHandler,
            SetUpSwitchHotkeyEventHandler setUpSwitchHotkeyEventHandler)
        {
            GrpKeyboardItem = new GroupBox();
            _lblKeyboardOperate = new Label();
            _lblKeyboardInterval = new Label();
            _lblKeyboardUnit = new Label();
            _lblKeyboardHotkey = new Label();
            _txtKeyboardOperate = new TextBox();
            _txtKeyboardInterval = new TextBox();
            _txtKeyboardHotkey = new TextBox();
            _btnKeyboardKey = new Button();
            _btnKeyboardRemove = new Button();
            _btnKeyboardRename = new Button();
            _btnKeyboardSetUp = new Button();
            _btnKeyboardSwitch = new Button();

            GrpKeyboardItem.Controls.Add(_lblKeyboardOperate);
            GrpKeyboardItem.Controls.Add(_lblKeyboardInterval);
            GrpKeyboardItem.Controls.Add(_lblKeyboardUnit);
            GrpKeyboardItem.Controls.Add(_lblKeyboardHotkey);
            GrpKeyboardItem.Controls.Add(_txtKeyboardOperate);
            GrpKeyboardItem.Controls.Add(_txtKeyboardInterval);
            GrpKeyboardItem.Controls.Add(_txtKeyboardHotkey);
            GrpKeyboardItem.Controls.Add(_btnKeyboardKey);
            GrpKeyboardItem.Controls.Add(_btnKeyboardRemove);
            GrpKeyboardItem.Controls.Add(_btnKeyboardRename);
            GrpKeyboardItem.Controls.Add(_btnKeyboardSetUp);
            GrpKeyboardItem.Controls.Add(_btnKeyboardSwitch);
            
            GrpKeyboardItem.Size = new Size(756, 214);
            GrpKeyboardItem.Location = new Point(3, 3 + offset);
            GrpKeyboardItem.TabIndex = 0;
            GrpKeyboardItem.TabStop = false;
            GrpKeyboardItem.Text = Name;
            GrpKeyboardItem.Font = new Font("字体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            GrpKeyboardItem.BackColor = Color.Transparent;
            
            _lblKeyboardOperate.Size = new Size(153, 41);
            _lblKeyboardOperate.Location = new Point(6, 63);
            _lblKeyboardOperate.TabIndex = 0;
            _lblKeyboardOperate.Text = "模拟按键:";
            _lblKeyboardOperate.TextAlign = ContentAlignment.MiddleLeft;
            _lblKeyboardOperate.Font = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            
            _lblKeyboardInterval.Size = new Size(153, 41);
            _lblKeyboardInterval.Location = new Point(6, 145);
            _lblKeyboardInterval.TabIndex = 0;
            _lblKeyboardInterval.Text = "时间间隔:";
            _lblKeyboardInterval.TextAlign = ContentAlignment.MiddleLeft;
            _lblKeyboardInterval.Font = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            
            _lblKeyboardUnit.Size = new Size(75, 41);
            _lblKeyboardUnit.Location = new Point(339, 145);
            _lblKeyboardUnit.TabIndex = 0;
            _lblKeyboardUnit.Text = "毫秒";
            _lblKeyboardHotkey.TextAlign = ContentAlignment.MiddleLeft;
            _lblKeyboardUnit.Font = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            
            _lblKeyboardHotkey.Size = new Size(118, 41);
            _lblKeyboardHotkey.Location = new Point(509, 104);
            _lblKeyboardHotkey.TabIndex = 0;
            _lblKeyboardHotkey.Text = "快捷键:";
            _lblKeyboardHotkey.TextAlign = ContentAlignment.MiddleLeft;
            _lblKeyboardHotkey.Font = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            
            _txtKeyboardOperate.Size = new Size(175, 42);
            _txtKeyboardOperate.Location = new Point(160, 63);
            _txtKeyboardOperate.ReadOnly = true;
            _txtKeyboardOperate.TabIndex = 0;
            _txtKeyboardOperate.TabStop = false;
            if (Operate != null)
            {
                _txtKeyboardOperate.Text = Utils.KeysSplice(Operate);
            }
            _txtKeyboardOperate.TextAlign = HorizontalAlignment.Right;
            _txtKeyboardOperate.Font = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            
            _txtKeyboardInterval.Size = new Size(175, 42);
            _txtKeyboardInterval.Location = new Point(160, 142);
            _txtKeyboardInterval.TabIndex = 3;
            _txtKeyboardInterval.Text = Interval;
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
            
            _txtKeyboardHotkey.Size = new Size(120, 35);
            _txtKeyboardHotkey.Location = new Point(630, 115);
            _txtKeyboardHotkey.Location = Hotkey != Keys.None ? new Point(630, 115) : new Point(630, 103);
            _txtKeyboardHotkey.ReadOnly = true;
            _txtKeyboardHotkey.TabIndex = 0;
            _txtKeyboardHotkey.TabStop = false;
            _txtKeyboardHotkey.Text = Hotkey != Keys.None ? KeyModifiers.Ctrl + "+" + Hotkey : Hotkey.ToString();
            _txtKeyboardHotkey.TextAlign = HorizontalAlignment.Center;
            _txtKeyboardHotkey.Font = Hotkey != Keys.None
                ? new Font("宋体", 8F, FontStyle.Regular, GraphicsUnit.Point, 134)
                : new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            _txtKeyboardHotkey.BorderStyle = BorderStyle.None;
            
            _btnKeyboardKey.Size = new Size(83, 42);
            _btnKeyboardKey.Location = new Point(339, 63);
            _btnKeyboardKey.Cursor = Cursors.Hand;
            _btnKeyboardKey.ForeColor = SystemColors.ControlText;
            _btnKeyboardKey.TabIndex = 2;
            _btnKeyboardKey.Text = "捕获";
            _btnKeyboardKey.UseVisualStyleBackColor = true;
            _btnKeyboardKey.BackColor = Color.Transparent;
            _btnKeyboardKey.Click += (_, _) =>
            {
                var getKeyBox =
                    new GetKeyBox(GrpKeyboardItem.Text == string.Empty ? "捕获" : GrpKeyboardItem.Text + "·捕获");
                getKeyBox.ShowDialog();
                var newOperate = getKeyBox.Operate;
                if (newOperate != null)
                {
                    _txtKeyboardOperate.Text = Utils.KeysSplice(newOperate);
                    Operate = newOperate;
                }
            };
            
            _btnKeyboardRemove.Size = new Size(83, 42);
            _btnKeyboardRemove.Location = new Point(650, 43);
            _btnKeyboardRemove.Cursor = Cursors.Hand;
            _btnKeyboardRemove.ForeColor = SystemColors.ControlText;
            _btnKeyboardRemove.TabIndex = 7;
            _btnKeyboardRemove.Text = "删除";
            _btnKeyboardRemove.UseVisualStyleBackColor = false;
            _btnKeyboardRemove.BackColor = Color.Transparent;
            _btnKeyboardRemove.Click += (_, _) => { removeEventHandler(Index); };
            
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
                var inputPromptBox =
                    new InputPromptBox(GrpKeyboardItem.Text == string.Empty ? "命名" : GrpKeyboardItem.Text + "·命名",
                        "请输入名称：", GrpKeyboardItem.Text);
                inputPromptBox.ShowDialog();
                if (inputPromptBox.Flag)
                {
                    GrpKeyboardItem.Text = inputPromptBox.Result;
                }
            };
            
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
                do
                {
                    var setUpHotkeyBox =
                        new SetUpHotkeyBox(GrpKeyboardItem.Text == string.Empty ? "设置" : GrpKeyboardItem.Text + "·设置",
                            KeyModifiers.Ctrl, Hotkey);
                
                    setUpHotkeyBox.ShowDialog();
                
                    if (!setUpHotkeyBox.Flag) return;
                
                    Hotkey = setUpHotkeyBox.Hotkey;
                
                    _txtKeyboardHotkey.Text = 
                        Hotkey != Keys.None
                            ? $"{KeyModifiers.Ctrl}+{Utils.KeysChangeChar(setUpHotkeyBox.Hotkey)}"
                            : KeyModifiers.None.ToString();
                
                    _txtKeyboardHotkey.Location = Hotkey != Keys.None ? new Point(630, 115) : new Point(630, 103);
                    _txtKeyboardHotkey.Font = Hotkey != Keys.None
                        ? new Font("宋体", 8F, FontStyle.Regular, GraphicsUnit.Point, 134)
                        : new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
                } while (!setUpSwitchHotkeyEventHandler(Index, Hotkey));
            };
            
            _btnKeyboardSwitch.Size = new Size(83, 42);
            _btnKeyboardSwitch.Location = new Point(650, 162);
            _btnKeyboardSwitch.Cursor = Cursors.Hand;
            _btnKeyboardSwitch.BackColor = Color.Transparent;
            _btnKeyboardSwitch.ForeColor = SystemColors.ControlText;
            _btnKeyboardSwitch.TabIndex = 4;
            _btnKeyboardSwitch.Text = "开启";
            _btnKeyboardSwitch.UseVisualStyleBackColor = false;
            _btnKeyboardSwitch.Click += (_, _) => { JobSwitch(); };
        }

        [JsonIgnore] public GroupBox GrpKeyboardItem { get; private set; }

        [JsonIgnore] public Label LblKeyboardOperate => _lblKeyboardOperate;

        [JsonIgnore] public Label LblKeyboardInterval => _lblKeyboardInterval;

        [JsonIgnore] public Label LblKeyboardUnit => _lblKeyboardUnit;

        [JsonIgnore] public Label LblKeyboardHotkey => _lblKeyboardHotkey;

        [JsonIgnore] public TextBox TxtKeyboardOperate => _txtKeyboardOperate;

        [JsonIgnore] public TextBox TxtKeyboardInterval => _txtKeyboardInterval;

        [JsonIgnore] public TextBox TxtKeyboardHotkey => _txtKeyboardHotkey;

        [JsonIgnore] public Button BtnKeyboardKey => _btnKeyboardKey;

        [JsonIgnore] public Button BtnKeyboardRemove => _btnKeyboardRemove;

        [JsonIgnore] public Button BtnKeyboardRename => _btnKeyboardRename;

        [JsonIgnore] public Button BtnKeyboardSetUp => _btnKeyboardSetUp;

        [JsonIgnore] public Button BtnKeyboardSwitch => _btnKeyboardSwitch;

        public int Index { get; set; }

        public string Name { get; set; }

        public List<Keys> Operate { get; set; }

        public string Interval { get; set; }

        public int HotkeyId { get; set; }

        public Keys Hotkey { get; set; }
    }
}