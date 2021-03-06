using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace daydream_click
{
    public class SetUpHotkeyBox:Form
    {
        private Label _lblKeyMessageTitle;
        private Label _lblKeyModifiers;
        private TextBox _txtHotkey;
        private Button _btnSetUpHotkeyClear;
        private Button _btnSetUpHotkeySave;
        private Button _btnSetUpHotkeyCancel;

        public SetUpHotkeyBox(string title, KeyModifiers keyModifiers,Keys hotkey)
        {
            Title = title;
            KeyModifiers = keyModifiers;
            Hotkey = hotkey;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            _lblKeyMessageTitle = new Label();
            _lblKeyModifiers = new Label();
            _txtHotkey = new TextBox();
            _btnSetUpHotkeyClear = new Button();
            _btnSetUpHotkeySave = new Button();
            _btnSetUpHotkeyCancel = new Button();

            Controls.Add(_lblKeyMessageTitle);
            Controls.Add(_lblKeyModifiers);
            Controls.Add(_txtHotkey);
            Controls.Add(_btnSetUpHotkeyClear);
            Controls.Add(_btnSetUpHotkeySave);
            Controls.Add(_btnSetUpHotkeyCancel);
            Icon = (Icon) new ComponentResourceManager(typeof(MainForm)).GetObject("$this.Icon");
            Size = new Size(700, 350);
            StartPosition = FormStartPosition.CenterScreen;
            MaximizeBox = false;
            MinimizeBox = false;
            KeyPreview = true;
            Text = Title;
            KeyDown += (_, e) =>
            {
                Hotkey = e.KeyCode;
                Utils.KeysChangeChar(e.KeyCode);
                _txtHotkey.Text = Utils.KeysChangeChar(e.KeyCode);
            };
            
            _lblKeyMessageTitle.Size = new Size(700, 82);
            _lblKeyMessageTitle.Location = new Point(-15, 20);
            _lblKeyMessageTitle.TabIndex = 0;
            _lblKeyMessageTitle.Text = "在本窗口按下要设置的快捷键后点击保存即可";
            _lblKeyMessageTitle.TextAlign = ContentAlignment.MiddleCenter;
            _lblKeyMessageTitle.Font = new Font("宋体", 8F, FontStyle.Bold, GraphicsUnit.Point, 134);

            _lblKeyModifiers.Size = new Size(300, 82);
            _lblKeyModifiers.Location = new Point(-15, 85);
            _lblKeyModifiers.TabIndex = 0;
            _lblKeyModifiers.Text = KeyModifiers+"+";
            _lblKeyModifiers.TextAlign = ContentAlignment.MiddleRight;
            _lblKeyModifiers.Font = new Font("宋体", 12F, FontStyle.Bold, GraphicsUnit.Point, 134);
            
            _txtHotkey.BackColor = SystemColors.Menu;
            _txtHotkey.Multiline = true;
            _txtHotkey.ReadOnly = true;
            _txtHotkey.Enabled = false;
            _txtHotkey.Size = new Size(175, 50);
            _txtHotkey.Location = new Point(300, 101);
            _txtHotkey.TabIndex = 0;
            _txtHotkey.TabStop = false;
            _txtHotkey.Text = Hotkey.ToString();
            _txtHotkey.TextAlign = HorizontalAlignment.Center;
            
            _btnSetUpHotkeyClear.Size = new Size(175, 50);
            _btnSetUpHotkeyClear.Location = new Point(25, 190);
            _btnSetUpHotkeyClear.TabIndex = 2;
            _btnSetUpHotkeyClear.Text = "清空";
            _btnSetUpHotkeyClear.UseVisualStyleBackColor = true;
            _btnSetUpHotkeyClear.Click += (_, _) =>
            {
                _txtHotkey.Text = Keys.None.ToString();
                Hotkey = Keys.None;
            };
            
            _btnSetUpHotkeySave.Size = new Size(175, 50);
            _btnSetUpHotkeySave.Location = new Point(249, 190);
            _btnSetUpHotkeySave.TabIndex = 1;
            _btnSetUpHotkeySave.Text = "保存";
            _btnSetUpHotkeySave.UseVisualStyleBackColor = true;
            _btnSetUpHotkeySave.Click += (_, _) =>
            {
                Flag = true;
                Close();
            };
            
            _btnSetUpHotkeyCancel.Size = new Size(175, 50);
            _btnSetUpHotkeyCancel.Location = new Point(470, 190);
            _btnSetUpHotkeyCancel.TabIndex = 3;
            _btnSetUpHotkeyCancel.Text = "取消";
            _btnSetUpHotkeyCancel.UseVisualStyleBackColor = true;
            _btnSetUpHotkeyCancel.Click += (_, _) =>
            {
                Flag = false;
                Close();
            };
            
        }

        public Label LblKeyMessageTitle => _lblKeyMessageTitle;
        
        public Label LblKeyModifiers => _lblKeyModifiers;

        public Button BtnSetUpHotkeyClear => _btnSetUpHotkeyClear;

        public Button BtnSetUpHotkeySave => _btnSetUpHotkeySave;

        public Button BtnSetUpHotkeyCancel => _btnSetUpHotkeyCancel;

        public string Title { get; }

        public KeyModifiers KeyModifiers { get; }

        public Keys Hotkey { get; private set; }

        public bool Flag { get; private set; }

    }
}