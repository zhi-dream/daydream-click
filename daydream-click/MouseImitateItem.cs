using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace daydream_click
{
    public class MouseImitateItem
    {
        private Label _lblMouseOperate;
        private Label _lblMousePosition;
        private Label _lblMousePositionX;
        private Label _lblMousePositionY;
        private Label _lblMouseInterval;
        private Label _lblMouseUnit;
        private Label _lblMouseHotkey;

        private ComboBox _cboMouseOperate;
        private TextBox _txtMousePositionX;
        private TextBox _txtMousePositionY;
        private TextBox _txtMouseInterval;
        private TextBox _txtMouseHotkey;

        private Button _btnMousePosition;
        private Button _btnMouseRemove;
        private Button _btnMouseRename;
        private Button _btnMouseSetUp;
        private Button _btnMouseSwitch;

        private readonly int _index;
        private readonly string _name;
        private readonly string _operate;
        private string _positionX;
        private string _positionY;
        private readonly string _interval;
        private Keys _hotkey;
        private MouseImitateJob _mouseImitateJob;

        public delegate void RemoveEventHandler(int removeIndex);

        public delegate void SetUpSwitchHotkeyEventHandler(int index,Keys keys);
        
        public MouseImitateItem(int index,RemoveEventHandler removeEventHandler,SetUpSwitchHotkeyEventHandler setUpSwitchHotkeyEventHandler)
        {
            _index = index;
            _name = "模拟"+index;
            _operate = "左键";
            _hotkey = Keys.None;
            InitializeComponent(0,removeEventHandler,setUpSwitchHotkeyEventHandler);
        }
        
        public MouseImitateItem(int offset, int index,RemoveEventHandler removeEventHandler,SetUpSwitchHotkeyEventHandler setUpSwitchHotkeyEventHandler)
        {
            _index = index;
            _name = "模拟"+index;
            _operate = "左键";
            _hotkey = Keys.None;
            InitializeComponent(offset,removeEventHandler,setUpSwitchHotkeyEventHandler);
        }
        
        public MouseImitateItem(int offset,int index,string name, string operate, string positionX, string positionY, string interval, Keys hotkey,RemoveEventHandler removeEventHandler,SetUpSwitchHotkeyEventHandler setUpSwitchHotkeyEventHandler)
        {
            _index = index;
            _name = name;
            _operate = operate;
            _positionX = positionX;
            _positionY = positionY;
            _interval = interval;
            _hotkey = hotkey;
            InitializeComponent(offset,removeEventHandler,setUpSwitchHotkeyEventHandler);
        }

        public void JobStop()
        {
            if (_mouseImitateJob == null) return;
            _mouseImitateJob.Stop();
            _btnMouseSwitch.Text = "开启";
        }
        
        public void JobStart()
        {
            if (_txtMousePositionX.Text.Trim().Length==0)
            {
                MessageBox.Show("请定位点击坐标。","提示",MessageBoxButtons.OK);//,MessageBoxIcon.Information
                return;
            }
            if (_txtMouseInterval.Text.Trim().Length == 0)
            {
                MessageBox.Show("请输入时间间隙。", "提示", MessageBoxButtons.OK);
                return;
            }
            _mouseImitateJob = new MouseImitateJob(
                _cboMouseOperate.SelectedItem.ToString(),
                int.Parse(_txtMousePositionX.Text.Trim()),
                int.Parse(_txtMousePositionY.Text.Trim()),
                int.Parse(_txtMouseInterval.Text.Trim())
            );
            new Thread(_mouseImitateJob.Execute).Start();
            _btnMouseSwitch.Text = "结束";
        }
        
        public void JobSwitch()
        {
            switch (_btnMouseSwitch.Text)
            {
                case "开启":
                    JobStart();
                    break;
                case "结束":
                    JobStop();
                    break;
            }
        }
        
        private void InitializeComponent(int offset,RemoveEventHandler removeEventHandler,SetUpSwitchHotkeyEventHandler setUpSwitchHotkeyEventHandler){

            GrpMouseItem = new GroupBox();
            _lblMouseOperate = new Label();
            _lblMousePosition = new Label();
            _lblMousePositionX = new Label();
            _lblMousePositionY = new Label();
            _lblMouseInterval = new Label();
            _lblMouseUnit = new Label();
            _lblMouseHotkey = new Label();
            _cboMouseOperate = new ComboBox();
            _txtMousePositionX = new TextBox();
            _txtMousePositionY = new TextBox();
            _txtMouseInterval = new TextBox();
            _txtMouseHotkey = new TextBox();
            _btnMousePosition = new Button();
            _btnMouseRemove = new Button();
            _btnMouseRename = new Button();
            _btnMouseSetUp = new Button();
            _btnMouseSwitch = new Button();

            GrpMouseItem.Controls.Add(_lblMouseOperate);
            GrpMouseItem.Controls.Add(_lblMousePosition);
            GrpMouseItem.Controls.Add(_lblMousePositionX);
            GrpMouseItem.Controls.Add(_lblMousePositionY);
            GrpMouseItem.Controls.Add(_lblMouseInterval);
            GrpMouseItem.Controls.Add(_lblMouseUnit);
            GrpMouseItem.Controls.Add(_lblMouseHotkey);
            GrpMouseItem.Controls.Add(_cboMouseOperate);
            GrpMouseItem.Controls.Add(_txtMousePositionX);
            GrpMouseItem.Controls.Add(_txtMousePositionY);
            GrpMouseItem.Controls.Add(_txtMouseInterval);
            GrpMouseItem.Controls.Add(_txtMouseHotkey);
            GrpMouseItem.Controls.Add(_btnMousePosition);
            GrpMouseItem.Controls.Add(_btnMouseRemove);
            GrpMouseItem.Controls.Add(_btnMouseRename);
            GrpMouseItem.Controls.Add(_btnMouseSetUp);
            GrpMouseItem.Controls.Add(_btnMouseSwitch);

            GrpMouseItem.Name = "GrpMouseItem";
            GrpMouseItem.Size = new Size(756, 214);
            GrpMouseItem.Location = new Point(3, 3+offset);
            GrpMouseItem.TabIndex = 0;
            GrpMouseItem.TabStop = false;
            GrpMouseItem.Text = _name;
            GrpMouseItem.Font = new Font("字体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            GrpMouseItem.BackColor = Color.Transparent;
            
            _lblMouseOperate.Name = "_lblMouseOperate";
            _lblMouseOperate.Size = new Size(153, 41);
            _lblMouseOperate.Location = new Point(6, 43);
            _lblMouseOperate.TabIndex = 0;
            _lblMouseOperate.Text = "执行操作:";
            _lblMouseOperate.TextAlign = ContentAlignment.MiddleLeft;
            _lblMouseOperate.Font = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            
            _lblMousePosition.Name = "_lblMousePosition";
            _lblMousePosition.Size = new Size(153, 41);
            _lblMousePosition.Location = new Point(6, 104);
            _lblMousePosition.TabIndex = 0;
            _lblMousePosition.Text = "点击位置:";
            _lblMousePosition.TextAlign = ContentAlignment.MiddleLeft;
            _lblMousePosition.Font = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            
            _lblMousePositionX.Name = "_lblMousePositionX";
            _lblMousePositionX.Size = new Size(25, 41);
            _lblMousePositionX.Location = new Point(160, 104);
            _lblMousePositionX.TabIndex = 0;
            _lblMousePositionX.Text = "X";
            _lblMousePositionX.TextAlign = ContentAlignment.MiddleLeft;
            _lblMousePositionX.Font = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            
            _lblMousePositionY.Name = "_lblMousePositionY";
            _lblMousePositionY.Size = new Size(25, 41);
            _lblMousePositionY.Location = new Point(289, 104);
            _lblMousePositionY.TabIndex = 0;
            _lblMousePositionY.Text = "Y";
            _lblMousePositionY.TextAlign = ContentAlignment.MiddleLeft;
            _lblMousePositionY.Font = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            
            _lblMouseInterval.Name = "_lblMouseInterval";
            _lblMouseInterval.Size = new Size(153, 41);
            _lblMouseInterval.Location = new Point(6, 165);
            _lblMouseInterval.TabIndex = 0;
            _lblMouseInterval.Text = "时间间隔:";
            _lblMouseInterval.TextAlign = ContentAlignment.MiddleLeft;
            _lblMouseInterval.Font = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            
            _lblMouseUnit.Name = "_lblMouseUnit";
            _lblMouseUnit.Size = new Size(75, 41);
            _lblMouseUnit.Location = new Point(339, 165);
            _lblMouseUnit.TabIndex = 0;
            _lblMouseUnit.Text = "毫秒";
            _lblMouseHotkey.TextAlign = ContentAlignment.MiddleLeft;
            _lblMouseUnit.Font = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);

            _lblMouseHotkey.Name = "_lblMouseHotkey";
            _lblMouseHotkey.Size = new Size(118, 41);
            _lblMouseHotkey.Location = new Point(509, 104);
            _lblMouseHotkey.TabIndex = 0;
            _lblMouseHotkey.Text = "快捷键:";
            _lblMouseHotkey.TextAlign = ContentAlignment.MiddleLeft;
            _lblMouseHotkey.Font = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            
            _cboMouseOperate.Name = "_cboMouseOperate";
            _cboMouseOperate.Size = new Size(125, 38);
            _cboMouseOperate.Location = new Point(160, 43);
            _cboMouseOperate.Cursor = Cursors.Hand;
            _cboMouseOperate.DropDownStyle = ComboBoxStyle.DropDownList;
            _cboMouseOperate.Items.AddRange(new object[] {"左键", "右键"});
            _cboMouseOperate.TabIndex = 1;
            _cboMouseOperate.SelectedIndex = _operate=="左键"?0:1;
            _cboMouseOperate.Font = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            
            _txtMousePositionX.Name = "_txtMousePositionX";
            _txtMousePositionX.Size = new Size(94, 42);
            _txtMousePositionX.Location = new Point(189, 104);
            _txtMousePositionX.ReadOnly = true;
            _txtMousePositionX.TabIndex = 0;
            _txtMousePositionX.TabStop = false;
            _txtMousePositionX.Text = _positionX;
            _txtMousePositionX.TextAlign = HorizontalAlignment.Right;
            _txtMousePositionX.Font = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);

            _txtMousePositionY.Name = "_txtMousePositionY";
            _txtMousePositionY.Size = new Size(94, 42);
            _txtMousePositionY.Location = new Point(320, 104);
            _txtMousePositionY.ReadOnly = true;
            _txtMousePositionY.TabIndex = 0;
            _txtMousePositionY.TabStop = false;
            _txtMousePositionY.Text = _positionY;
            _txtMousePositionY.TextAlign = HorizontalAlignment.Right;
            _txtMousePositionY.Font = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            
            _txtMouseInterval.Name = "_txtMouseInterval";
            _txtMouseInterval.Size = new Size(175, 42);
            _txtMouseInterval.Location = new Point(160, 162);
            _txtMouseInterval.TabIndex = 3;
            _txtMouseInterval.Text = _interval;
            _txtMouseInterval.TextAlign = HorizontalAlignment.Right;
            _txtMouseInterval.Font = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            _txtMouseInterval.KeyPress += (_,e) =>
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
            
            _txtMouseHotkey.Name = "_txtMouseHotkey";
            _txtMouseHotkey.Size = new Size(120, 35);
            _txtMouseHotkey.Location = _hotkey!=Keys.None?new Point(630, 115):new Point(630, 103);
            _txtMouseHotkey.ReadOnly = true;
            _txtMouseHotkey.TabIndex = 0;
            _txtMouseHotkey.TabStop = false;
            _txtMouseHotkey.Text = _hotkey!=Keys.None?KeyModifiers.Shift+"+"+_hotkey:_hotkey.ToString();
            _txtMouseHotkey.TextAlign = HorizontalAlignment.Center;
            _txtMouseHotkey.Font = _hotkey!=Keys.None?new Font("宋体", 6F, FontStyle.Regular, GraphicsUnit.Point, 134):new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            _txtMouseHotkey.BorderStyle = BorderStyle.None;
            
            _btnMousePosition.Name = "_btnMousePosition";
            _btnMousePosition.Size = new Size(83, 42);
            _btnMousePosition.Location = new Point(420, 104);
            _btnMousePosition.Cursor = Cursors.Hand;
            _btnMousePosition.ForeColor = SystemColors.ControlText;
            _btnMousePosition.TabIndex = 2;
            _btnMousePosition.Text = "定位";
            _btnMousePosition.UseVisualStyleBackColor = true;
            _btnMousePosition.BackColor = Color.Transparent;
            _btnMousePosition.Click += (_, _) =>
            {
                var getPositionBox = new GetPositionBox(GrpMouseItem.Text==string.Empty?"定位":GrpMouseItem.Text+"·定位","Alt+S");
                getPositionBox.ShowDialog();
                _txtMousePositionX.Text = getPositionBox.PositionX.ToString();
                _txtMousePositionY.Text = getPositionBox.PositionY.ToString();
                _positionX = getPositionBox.PositionX.ToString();
                _positionY = getPositionBox.PositionY.ToString();
            };
            
            _btnMouseRemove.Name = "_btnMouseRemove";
            _btnMouseRemove.Size = new Size(83, 42);
            _btnMouseRemove.Location = new Point(650, 43);
            _btnMouseRemove.Cursor = Cursors.Hand;
            _btnMouseRemove.ForeColor = SystemColors.ControlText;
            _btnMouseRemove.TabIndex = 7;
            _btnMouseRemove.Text = "删除";
            _btnMouseRemove.UseVisualStyleBackColor = false;
            _btnMouseRemove.BackColor = Color.Transparent;
            _btnMouseRemove.Click += (_,_) =>
            {
                removeEventHandler(_index);
            };
            
            _btnMouseRename.Name = "_btnMouseRename";
            _btnMouseRename.Size = new Size(83, 42);
            _btnMouseRename.Location = new Point(472, 162);
            _btnMouseRename.Cursor = Cursors.Hand;
            _btnMouseRename.ForeColor = SystemColors.ControlText;
            _btnMouseRename.TabIndex = 6;
            _btnMouseRename.Text = "命名";
            _btnMouseRename.UseVisualStyleBackColor = false;
            _btnMouseRename.BackColor = Color.Transparent;
            _btnMouseRename.Click += (_, _) =>
            {
                var inputPromptBox = new InputPromptBox(GrpMouseItem.Text==string.Empty?"命名":GrpMouseItem.Text+"·命名","请输入名称：",GrpMouseItem.Text);
                inputPromptBox.ShowDialog();
                if (inputPromptBox.Flag)
                {
                    GrpMouseItem.Text = inputPromptBox.Result;
                }
            };

            _btnMouseSetUp.Name = "_btnMouseSetUp";
            _btnMouseSetUp.Size = new Size(83, 42);
            _btnMouseSetUp.Location = new Point(561, 162);
            _btnMouseSetUp.Cursor = Cursors.Hand;
            _btnMouseSetUp.BackColor = Color.Transparent;
            _btnMouseSetUp.ForeColor = SystemColors.ControlText;
            _btnMouseSetUp.TabIndex = 5;
            _btnMouseSetUp.Text = "设置";
            _btnMouseSetUp.UseVisualStyleBackColor = false;
            _btnMouseSetUp.Click += (_, _) =>
            {
                var setUpHotkeyBox = new SetUpHotkeyBox(GrpMouseItem.Text==string.Empty?"设置":GrpMouseItem.Text+"·设置",KeyModifiers.Shift,_hotkey);
                setUpHotkeyBox.ShowDialog();
                if (!setUpHotkeyBox.Flag) return;
                _hotkey = setUpHotkeyBox.Hotkey;
                _txtMouseHotkey.Text = _hotkey!=Keys.None?$"{KeyModifiers.Shift}+{Utils.KeysChangeChar(setUpHotkeyBox.Hotkey)}":KeyModifiers.Shift.ToString();
                _txtMouseHotkey.Location = _hotkey!=Keys.None?new Point(630, 115):new Point(630, 103);
                _txtMouseHotkey.Font = _hotkey!=Keys.None?new Font("宋体", 6F, FontStyle.Regular, GraphicsUnit.Point, 134):new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
                setUpSwitchHotkeyEventHandler(_index,_hotkey);
            };
            
            _btnMouseSwitch.Name = "_btnMouseSwitch";
            _btnMouseSwitch.Size = new Size(83, 42);
            _btnMouseSwitch.Location = new Point(650, 162);
            _btnMouseSwitch.Cursor = Cursors.Hand;
            _btnMouseSwitch.BackColor = Color.Transparent;
            _btnMouseSwitch.ForeColor = SystemColors.ControlText;
            _btnMouseSwitch.TabIndex = 4;
            _btnMouseSwitch.Text = "开启";
            _btnMouseSwitch.UseVisualStyleBackColor = false;
            _btnMouseSwitch.Click += (_, _) =>
            {
                JobSwitch();
            };

        }

        public GroupBox GrpMouseItem { get; private set; }

        public Label LblMouseOperate => _lblMouseOperate;
        
        public Label LblMousePosition => _lblMousePosition;

        public Label LblMousePositionX => _lblMousePositionX;

        public Label LblMousePositionY => _lblMousePositionY;

        public Label LblMouseInterval => _lblMouseInterval;

        public Label LblMouseUnit => _lblMouseUnit;

        public Label LblMouseHotkey => _lblMouseHotkey;

        public ComboBox CboMouseOperate => _cboMouseOperate;

        public TextBox TxtMousePositionX => _txtMousePositionX;

        public TextBox TxtMousePositionY => _txtMousePositionY;

        public TextBox TxtMouseInterval => _txtMouseInterval;

        public TextBox TxtMouseHotkey => _txtMouseHotkey;

        public Button BtnMousePosition => _btnMousePosition;

        public Button BtnMouseRemove => _btnMouseRemove;

        public Button BtnMouseRename => _btnMouseRename;

        public Button BtnMouseSetUp => _btnMouseSetUp;

        public Button BtnMouseSwitch => _btnMouseSwitch;

        public int Index => _index;

        public string Name => _name;

        public string Operate => _operate;

        public string PositionX => _positionX;

        public string PositionY => _positionY;

        public string Interval => _interval;

        public int HotkeyId { get; set; }

        public Keys Hotkey
        {
            get => _hotkey;
            set => _hotkey = value;
        }
        
    }
}