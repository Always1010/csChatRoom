namespace client
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
            this.chatBox = new System.Windows.Forms.TextBox();
            this.IP = new System.Windows.Forms.TextBox();
            this.Port = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Connect = new System.Windows.Forms.Button();
            this.textSend = new System.Windows.Forms.TextBox();
            this.Send = new System.Windows.Forms.Button();
            this.Clear = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.connect_state = new System.Windows.Forms.Label();
            this.appExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // chatBox
            // 
            this.chatBox.Location = new System.Drawing.Point(11, 30);
            this.chatBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chatBox.Multiline = true;
            this.chatBox.Name = "chatBox";
            this.chatBox.Size = new System.Drawing.Size(462, 300);
            this.chatBox.TabIndex = 0;
            // 
            // IP
            // 
            this.IP.Location = new System.Drawing.Point(527, 30);
            this.IP.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.IP.Name = "IP";
            this.IP.Size = new System.Drawing.Size(152, 25);
            this.IP.TabIndex = 1;
            // 
            // Port
            // 
            this.Port.Location = new System.Drawing.Point(585, 68);
            this.Port.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Port.Name = "Port";
            this.Port.Size = new System.Drawing.Size(112, 25);
            this.Port.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(502, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "IP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(517, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "端口";
            // 
            // Connect
            // 
            this.Connect.Location = new System.Drawing.Point(585, 106);
            this.Connect.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Connect.Name = "Connect";
            this.Connect.Size = new System.Drawing.Size(95, 33);
            this.Connect.TabIndex = 5;
            this.Connect.Text = "连接";
            this.Connect.UseVisualStyleBackColor = true;
            this.Connect.Click += new System.EventHandler(this.Connect_Click);
            // 
            // textSend
            // 
            this.textSend.Location = new System.Drawing.Point(479, 241);
            this.textSend.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textSend.Multiline = true;
            this.textSend.Name = "textSend";
            this.textSend.Size = new System.Drawing.Size(148, 66);
            this.textSend.TabIndex = 6;
            this.textSend.TextChanged += new System.EventHandler(this.textSend_TextChanged);
            // 
            // Send
            // 
            this.Send.Location = new System.Drawing.Point(633, 273);
            this.Send.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Send.Name = "Send";
            this.Send.Size = new System.Drawing.Size(66, 34);
            this.Send.TabIndex = 7;
            this.Send.Text = "发送";
            this.Send.UseVisualStyleBackColor = true;
            this.Send.Click += new System.EventHandler(this.Send_Click);
            // 
            // Clear
            // 
            this.Clear.Location = new System.Drawing.Point(505, 168);
            this.Clear.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Clear.Name = "Clear";
            this.Clear.Size = new System.Drawing.Size(164, 34);
            this.Clear.TabIndex = 8;
            this.Clear.Text = "清空屏幕";
            this.Clear.UseVisualStyleBackColor = true;
            this.Clear.Click += new System.EventHandler(this.Clear_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(202, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 15);
            this.label3.TabIndex = 9;
            this.label3.Text = "简单聊天室";
            // 
            // connect_state
            // 
            this.connect_state.AutoSize = true;
            this.connect_state.Location = new System.Drawing.Point(502, 113);
            this.connect_state.Name = "connect_state";
            this.connect_state.Size = new System.Drawing.Size(52, 15);
            this.connect_state.TabIndex = 10;
            this.connect_state.Text = "未连接";
            // 
            // appExit
            // 
            this.appExit.Location = new System.Drawing.Point(616, 364);
            this.appExit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.appExit.Name = "appExit";
            this.appExit.Size = new System.Drawing.Size(84, 22);
            this.appExit.TabIndex = 11;
            this.appExit.Text = "退出登录";
            this.appExit.UseVisualStyleBackColor = true;
            this.appExit.Click += new System.EventHandler(this.appExit_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(711, 397);
            this.Controls.Add(this.appExit);
            this.Controls.Add(this.connect_state);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Clear);
            this.Controls.Add(this.Send);
            this.Controls.Add(this.textSend);
            this.Controls.Add(this.Connect);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Port);
            this.Controls.Add(this.IP);
            this.Controls.Add(this.chatBox);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form2";
            this.Text = "Form2";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox chatBox;
        private System.Windows.Forms.TextBox IP;
        private System.Windows.Forms.TextBox Port;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Connect;
        private System.Windows.Forms.TextBox textSend;
        private System.Windows.Forms.Button Send;
        private System.Windows.Forms.Button Clear;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label connect_state;
        private System.Windows.Forms.Button appExit;
    }
}