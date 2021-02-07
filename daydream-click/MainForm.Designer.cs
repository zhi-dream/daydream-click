using System.Drawing;

namespace daydream_click
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tabMenu = new System.Windows.Forms.TabControl();
            this.pageMouse = new System.Windows.Forms.TabPage();
            this.pnlMouseParent = new System.Windows.Forms.Panel();
            this.pnlMouse = new System.Windows.Forms.Panel();
            this.btnMouseAppend = new System.Windows.Forms.Button();
            this.pageKeyboard = new System.Windows.Forms.TabPage();
            this.pnlKeyboardParent = new System.Windows.Forms.Panel();
            this.pnlKeyboard = new System.Windows.Forms.Panel();
            this.btnKeyboardAppend = new System.Windows.Forms.Button();
            this.lblClosePrompt = new System.Windows.Forms.Label();
            this.tabMenu.SuspendLayout();
            this.pageMouse.SuspendLayout();
            this.pnlMouseParent.SuspendLayout();
            this.pageKeyboard.SuspendLayout();
            this.pnlKeyboardParent.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabMenu
            // 
            this.tabMenu.Controls.Add(this.pageMouse);
            this.tabMenu.Controls.Add(this.pageKeyboard);
            this.tabMenu.Location = new System.Drawing.Point(12, 12);
            this.tabMenu.Name = "tabMenu";
            this.tabMenu.SelectedIndex = 0;
            this.tabMenu.Size = new System.Drawing.Size(794, 1138);
            this.tabMenu.TabIndex = 0;
            // 
            // pageMouse
            // 
            this.pageMouse.Controls.Add(this.pnlMouseParent);
            this.pageMouse.Controls.Add(this.btnMouseAppend);
            this.pageMouse.Location = new System.Drawing.Point(10, 48);
            this.pageMouse.Name = "pageMouse";
            this.pageMouse.Padding = new System.Windows.Forms.Padding(3);
            this.pageMouse.Size = new System.Drawing.Size(774, 1080);
            this.pageMouse.TabIndex = 0;
            this.pageMouse.Text = "鼠标模拟";
            this.pageMouse.UseVisualStyleBackColor = true;
            // 
            // pnlMouseParent
            // 
            this.pnlMouseParent.Controls.Add(this.pnlMouse);
            this.pnlMouseParent.Location = new System.Drawing.Point(6, 6);
            this.pnlMouseParent.Name = "pnlMouseParent";
            this.pnlMouseParent.Size = new System.Drawing.Size(762, 1000);
            this.pnlMouseParent.TabIndex = 0;
            // 
            // pnlMouse
            // 
            this.pnlMouse.AutoScroll = true;
            this.pnlMouse.BackColor = System.Drawing.SystemColors.Control;
            this.pnlMouse.Location = new System.Drawing.Point(0, 0);
            this.pnlMouse.Margin = new System.Windows.Forms.Padding(0);
            this.pnlMouse.Name = "pnlMouse";
            this.pnlMouse.Size = new System.Drawing.Size(802, 1000);
            this.pnlMouse.TabIndex = 0;
            // 
            // btnMouseAppend
            // 
            this.btnMouseAppend.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMouseAppend.Location = new System.Drawing.Point(6, 1010);
            this.btnMouseAppend.Name = "btnMouseAppend";
            this.btnMouseAppend.Size = new System.Drawing.Size(762, 62);
            this.btnMouseAppend.TabIndex = 1;
            this.btnMouseAppend.Text = "添加";
            this.btnMouseAppend.UseVisualStyleBackColor = true;
            this.btnMouseAppend.Click += new System.EventHandler(this.btnMouseAppend_Click);
            // 
            // pageKeyboard
            // 
            this.pageKeyboard.Controls.Add(this.pnlKeyboardParent);
            this.pageKeyboard.Controls.Add(this.btnKeyboardAppend);
            this.pageKeyboard.Location = new System.Drawing.Point(10, 48);
            this.pageKeyboard.Name = "pageKeyboard";
            this.pageKeyboard.Padding = new System.Windows.Forms.Padding(3);
            this.pageKeyboard.Size = new System.Drawing.Size(774, 1080);
            this.pageKeyboard.TabIndex = 1;
            this.pageKeyboard.Text = "键盘模拟";
            this.pageKeyboard.UseVisualStyleBackColor = true;
            // 
            // pnlKeyboardParent
            // 
            this.pnlKeyboardParent.Controls.Add(this.pnlKeyboard);
            this.pnlKeyboardParent.Location = new System.Drawing.Point(6, 6);
            this.pnlKeyboardParent.Name = "pnlKeyboardParent";
            this.pnlKeyboardParent.Size = new System.Drawing.Size(762, 1000);
            this.pnlKeyboardParent.TabIndex = 0;
            // 
            // pnlKeyboard
            // 
            this.pnlKeyboard.AutoScroll = true;
            this.pnlKeyboard.BackColor = System.Drawing.SystemColors.Control;
            this.pnlKeyboard.Location = new System.Drawing.Point(0, 0);
            this.pnlKeyboard.Margin = new System.Windows.Forms.Padding(0);
            this.pnlKeyboard.Name = "pnlKeyboard";
            this.pnlKeyboard.Size = new System.Drawing.Size(802, 1000);
            this.pnlKeyboard.TabIndex = 2;
            // 
            // btnKeyboardAppend
            // 
            this.btnKeyboardAppend.Location = new System.Drawing.Point(6, 1010);
            this.btnKeyboardAppend.Name = "btnKeyboardAppend";
            this.btnKeyboardAppend.Size = new System.Drawing.Size(762, 62);
            this.btnKeyboardAppend.TabIndex = 3;
            this.btnKeyboardAppend.Text = "添加";
            this.btnKeyboardAppend.UseVisualStyleBackColor = true;
            this.btnKeyboardAppend.Click += new System.EventHandler(this.btnKeyboardAppend_Click);
            // 
            // lblClosePrompt
            // 
            this.lblClosePrompt.Location = new System.Drawing.Point(440, 20);
            this.lblClosePrompt.Name = "lblClosePrompt";
            this.lblClosePrompt.Size = new System.Drawing.Size(375, 35);
            this.lblClosePrompt.TabIndex = 1;
            this.lblClosePrompt.Text = "【Shift+D】结束所有任务";
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(818, 1162);
            this.Controls.Add(this.lblClosePrompt);
            this.Controls.Add(this.tabMenu);
            this.Icon = ((System.Drawing.Icon) (resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DaydreamClick";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabMenu.ResumeLayout(false);
            this.pageMouse.ResumeLayout(false);
            this.pnlMouseParent.ResumeLayout(false);
            this.pageKeyboard.ResumeLayout(false);
            this.pnlKeyboardParent.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Label lblClosePrompt;

        private System.Windows.Forms.Panel pnlKeyboardParent;

        private System.Windows.Forms.Panel pnlMouseParent;

        private System.Windows.Forms.Button btnMouseAppend;
        private System.Windows.Forms.Button btnKeyboardAppend;
        private System.Windows.Forms.Panel pnlMouse;
        private System.Windows.Forms.Panel pnlKeyboard;
        private System.Windows.Forms.TabControl tabMenu;
        private System.Windows.Forms.TabPage pageMouse;
        private System.Windows.Forms.TabPage pageKeyboard;

        #endregion
    }
}