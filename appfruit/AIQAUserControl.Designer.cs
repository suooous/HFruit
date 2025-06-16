namespace appfruit
{
    partial class AIQAUserControl
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.chatBrowser = new System.Windows.Forms.WebBrowser();
            this.inputBox = new System.Windows.Forms.TextBox();
            this.sendButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // chatBrowser
            // 
            this.chatBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chatBrowser.Location = new System.Drawing.Point(0, 0);
            this.chatBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.chatBrowser.Name = "chatBrowser";
            this.chatBrowser.Size = new System.Drawing.Size(300, 342);
            this.chatBrowser.TabIndex = 0;
            this.chatBrowser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.chatBrowser_DocumentCompleted);
            // 
            // inputBox
            // 
            this.inputBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.inputBox.Location = new System.Drawing.Point(0, 342);
            this.inputBox.Name = "inputBox";
            this.inputBox.Size = new System.Drawing.Size(300, 25);
            this.inputBox.TabIndex = 1;
            this.inputBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.inputBox_KeyPress);
            // 
            // sendButton
            // 
            this.sendButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.sendButton.Location = new System.Drawing.Point(0, 367);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(300, 33);
            this.sendButton.TabIndex = 2;
            this.sendButton.Text = "发送";
            this.sendButton.UseVisualStyleBackColor = true;
            this.sendButton.Click += new System.EventHandler(this.sendButton_Click);
            // 
            // AIQAUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chatBrowser);
            this.Controls.Add(this.inputBox);
            this.Controls.Add(this.sendButton);
            this.Name = "AIQAUserControl";
            this.Size = new System.Drawing.Size(300, 400);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        // 将 private System.Windows.Forms.RichTextBox chatBox; 更改为 WebBrowser
        private System.Windows.Forms.WebBrowser chatBrowser;
        private System.Windows.Forms.TextBox inputBox;
        private System.Windows.Forms.Button sendButton;
    }
}