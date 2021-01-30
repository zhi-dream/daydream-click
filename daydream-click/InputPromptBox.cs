using System;
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
        
        private readonly string _title;
        private readonly string _message;
        private readonly string _confirm;
        private readonly string _cancel;
        private readonly string _defaultValue;

        public InputPromptBox()
        {
            _title = "标题";
            _message =  "请输入信息：";
            _confirm = "确认";
            _cancel = "取消";
            InitializeComponent();
        }
        
        public InputPromptBox(string defaultValue)
        {
            _title = "标题";
            _message =  "请输入信息：";
            _defaultValue = defaultValue;
            _confirm = "确认";
            _cancel = "取消";
            InitializeComponent();
        }
        
        public InputPromptBox(string title, string message, string defaultValue)
        {
            _title = title;
            _message = message;
            _defaultValue = defaultValue;
            _confirm = "确认";
            _cancel = "取消";
            InitializeComponent();
        }
        
        public InputPromptBox(string title, string message, string defaultValue, string confirm, string cancel)
        {
            _title = title;
            _message = message;
            _defaultValue = defaultValue;
            _confirm = confirm;
            _cancel = cancel;
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
            
            Name = "_inputPrompt";
            ClientSize = new Size(450, 200);
            StartPosition = FormStartPosition.CenterScreen;
            MaximizeBox = false;
            MinimizeBox = false;
            Text = _title;

            _lblInputPromptMessage.Name = "_lblInputPromptMessage";
            _lblInputPromptMessage.Size = new Size(398, 51);
            _lblInputPromptMessage.Location = new Point(25, 10);
            _lblInputPromptMessage.TabIndex = 0;
            _lblInputPromptMessage.Text = _message;

            _txtInputPromptInputBox.Name = "_txtInputPromptInputBox";
            _txtInputPromptInputBox.Size = new Size(398, 42);
            _txtInputPromptInputBox.Location = new Point(25, 65);
            _txtInputPromptInputBox.TabIndex = 1;
            _txtInputPromptInputBox.Text = _defaultValue;

            _btnInputPromptConfirm.Name = "_btnInputPromptConfirm";
            _btnInputPromptConfirm.Size = new Size(175, 50);
            _btnInputPromptConfirm.Location = new Point(25, 125);
            _btnInputPromptConfirm.TabIndex = 2;
            _btnInputPromptConfirm.Text = _confirm;
            _btnInputPromptConfirm.UseVisualStyleBackColor = true;
            _btnInputPromptConfirm.Click += (_, _) =>
            {
                Flag = true;
                Result = _txtInputPromptInputBox.Text;
                Close();
            };

            _btnInputPromptCancel.Name = "_btnInputPromptCancel";
            _btnInputPromptCancel.Size = new Size(175, 50);
            _btnInputPromptCancel.Location = new Point(249, 125);
            _btnInputPromptCancel.TabIndex = 3;
            _btnInputPromptCancel.Text = _cancel;
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

        public string Title => _title;

        public string Message => _message;

        public string DefaultValue => _defaultValue;
        
        public string Confirm => _confirm;

        public string Cancel => _cancel;

        public bool Flag { get; private set; }

        public string Result { get; private set; }
    }
}