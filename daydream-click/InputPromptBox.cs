using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace daydream_click
{
    public class InputPromptBox :Form
    {
        private Label _lblInputPromptMessage;
        private TextBox _txtInputPromptInputBox;
        private Button _btnInputPromptConfirm;
        private Button _btnInputPromptCancel;

        public InputPromptBox()
        {
            Title = "标题";
            Message =  "请输入信息：";
            Confirm = "确认";
            Cancel = "取消";
            InitializeComponent();
        }
        
        public InputPromptBox(string defaultValue)
        {
            Title = "标题";
            Message =  "请输入信息：";
            DefaultValue = defaultValue;
            Confirm = "确认";
            Cancel = "取消";
            InitializeComponent();
        }
        
        public InputPromptBox(string title, string message, string defaultValue)
        {
            Title = title;
            Message = message;
            DefaultValue = defaultValue;
            Confirm = "确认";
            Cancel = "取消";
            InitializeComponent();
        }
        
        public InputPromptBox(string title, string message, string defaultValue, string confirm, string cancel)
        {
            Title = title;
            Message = message;
            DefaultValue = defaultValue;
            Confirm = confirm;
            Cancel = cancel;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            _lblInputPromptMessage = new Label();
            _txtInputPromptInputBox = new TextBox();
            _btnInputPromptConfirm = new Button();
            _btnInputPromptCancel = new Button();

            Controls.Add(_lblInputPromptMessage);
            Controls.Add(_txtInputPromptInputBox);
            Controls.Add(_btnInputPromptConfirm);
            Controls.Add(_btnInputPromptCancel);
            
            Icon = (Icon) new ComponentResourceManager(typeof(MainForm)).GetObject("$this.Icon");
            ClientSize = new Size(450, 200);
            StartPosition = FormStartPosition.CenterScreen;
            MaximizeBox = false;
            MinimizeBox = false;
            Text = Title;
            
            _lblInputPromptMessage.Size = new Size(398, 51);
            _lblInputPromptMessage.Location = new Point(25, 10);
            _lblInputPromptMessage.TabIndex = 0;
            _lblInputPromptMessage.Text = Message;
            
            _txtInputPromptInputBox.Size = new Size(398, 42);
            _txtInputPromptInputBox.Location = new Point(25, 65);
            _txtInputPromptInputBox.TabIndex = 1;
            _txtInputPromptInputBox.Text = DefaultValue;
            
            _btnInputPromptConfirm.Size = new Size(175, 50);
            _btnInputPromptConfirm.Location = new Point(25, 125);
            _btnInputPromptConfirm.TabIndex = 2;
            _btnInputPromptConfirm.Text = Confirm;
            _btnInputPromptConfirm.UseVisualStyleBackColor = true;
            _btnInputPromptConfirm.Click += (_, _) =>
            {
                Flag = true;
                Result = _txtInputPromptInputBox.Text;
                Close();
            };
            
            _btnInputPromptCancel.Size = new Size(175, 50);
            _btnInputPromptCancel.Location = new Point(249, 125);
            _btnInputPromptCancel.TabIndex = 3;
            _btnInputPromptCancel.Text = Cancel;
            _btnInputPromptCancel.UseVisualStyleBackColor = true;
            _btnInputPromptCancel.Click += (_, _) =>
            {
                Flag = false;
                Result = string.Empty;
                Close();
            };
            
        }

        public Label LblInputPromptMessage => _lblInputPromptMessage;

        public TextBox TxtInputPromptInputBox => _txtInputPromptInputBox;

        public Button BtnInputPromptConfirm => _btnInputPromptConfirm;

        public Button BtnInputPromptCancel => _btnInputPromptCancel;

        public string Title { get; }

        public string Message { get; }

        public string DefaultValue { get; }

        public string Confirm { get; }

        public string Cancel { get; }

        public bool Flag { get; private set; }

        public string Result { get; private set; }
    }
}