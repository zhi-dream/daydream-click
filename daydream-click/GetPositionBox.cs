using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace daydream_click
{
    public class GetPositionBox :Form
    {
        private Label _lblPositionMessage;
        private Label _lblPositionHotkey;

        public GetPositionBox(string title, string hotkeySwitch)
        {
            Title = title;
            HotkeySwitch = hotkeySwitch;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            Hotkey.RegisterHotKey(Handle, 101, KeyModifiers.Shift, Keys.S);
            
            _lblPositionMessage = new Label();
            _lblPositionHotkey = new Label();

            Controls.Add(_lblPositionMessage);
            Controls.Add(_lblPositionHotkey);
            
            Icon = (Icon) new ComponentResourceManager(typeof(MainForm)).GetObject("$this.Icon");
            Size = new Size(700, 350);
            StartPosition = FormStartPosition.CenterScreen;
            MaximizeBox = false;
            MinimizeBox = false;
            Text = Title;
            FormClosing += (_, _) =>
            {
                Hotkey.UnregisterHotKey(Handle, 101);
            };
            
            _lblPositionMessage.Size = new Size(700, 82);
            _lblPositionMessage.Location = new Point(-15, 50);
            _lblPositionMessage.TabIndex = 0;
            _lblPositionMessage.Text = "鼠标指针移动至要获取坐标的位置";
            _lblPositionMessage.TextAlign = ContentAlignment.MiddleCenter;
            _lblPositionMessage.Font = new Font("宋体", 12F, FontStyle.Bold, GraphicsUnit.Point, 134);
            
            _lblPositionHotkey.Size = new Size(700, 82);
            _lblPositionHotkey.Location = new Point(-15, 135);
            _lblPositionHotkey.TabIndex = 0;
            _lblPositionHotkey.Text = "按下【"+HotkeySwitch+"】即可";
            _lblPositionHotkey.TextAlign = ContentAlignment.MiddleCenter;
            _lblPositionHotkey.Font = new Font("宋体", 12F, FontStyle.Bold, GraphicsUnit.Point, 134);
            
        }

        protected override void WndProc(ref Message m)
        {
            const int hotkey = 0x0312;
            switch (m.Msg)
            {
                case hotkey:
                    switch (m.WParam.ToInt32())
                    {
                        case 101:
                            PositionX = MousePosition.X;
                            PositionY = MousePosition.Y;
                            Close();
                            break;
                    }
                    break;
            }
            base.WndProc(ref m);
        }

        public Label LblPositionMessage => _lblPositionMessage;

        public Label LblPositionHotkey => _lblPositionHotkey;

        public string Title { get; }

        public string HotkeySwitch { get; }

        public int PositionX { get; private set; }

        public int PositionY { get; private set; }
    }
}