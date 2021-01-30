using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace daydream_click
{
    public class GetKeyBox :Form
    {
        private Label _lblKeyMessageTitle;
        
        private Label _lblKeyMessage;

        private readonly string _title;

        private List<Keys> _operate;

        public GetKeyBox(string title)
        {
            _title = title;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            _lblKeyMessageTitle = new Label();
            _lblKeyMessage = new Label();

            Controls.Add(_lblKeyMessageTitle);
            Controls.Add(_lblKeyMessage);
            
            Size = new Size(700, 350);
            StartPosition = FormStartPosition.CenterScreen;
            MaximizeBox = false;
            MinimizeBox = false;
            KeyPreview = true;
            Text = _title;
            KeyDown += (_, e) =>
            {
                _operate ??= new List<Keys>();
                if (_operate.Contains(e.KeyCode)) return;
                _operate.Add(e.KeyCode);
            };
            KeyUp += (_, _) =>
            {
                Close();
            };

            _lblKeyMessageTitle.Size = new Size(700, 82);
            _lblKeyMessageTitle.Location = new Point(-15, 50);
            _lblKeyMessageTitle.TabIndex = 0;
            _lblKeyMessageTitle.Text = "按下想要模拟的按键后松开即可";
            _lblKeyMessageTitle.TextAlign = ContentAlignment.MiddleCenter;
            _lblKeyMessageTitle.Font = new Font("宋体", 12F, FontStyle.Bold, GraphicsUnit.Point, 134);

            _lblKeyMessage.Size = new Size(700, 82);
            _lblKeyMessage.Location = new Point(-15, 135);
            _lblKeyMessage.TabIndex = 0;
            _lblKeyMessage.Text = "支持组合键";
            _lblKeyMessage.TextAlign = ContentAlignment.MiddleCenter;
            _lblKeyMessage.Font = new Font("宋体", 12F, FontStyle.Bold, GraphicsUnit.Point, 134);
            
        }

        public Label LblKeyMessageTitle => _lblKeyMessageTitle;
        
        public Label LblKeyMessage => _lblKeyMessage;

        public string Title => _title;

        public List<Keys> Operate => _operate;
    }
}