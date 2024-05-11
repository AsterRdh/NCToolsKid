namespace NCToolsKidForJava
{
    partial class LanCaller
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SelfIPLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SelfPortBox = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.CreateRoomButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.RoomStateLabel = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.JoinRoomButton = new System.Windows.Forms.Button();
            this.HostPortBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.HostIpBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.LogTextBox = new System.Windows.Forms.TextBox();
            this.sendMessageButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(189, 12);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(599, 291);
            this.textBox1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "本机ID";
            // 
            // SelfIPLabel
            // 
            this.SelfIPLabel.AutoSize = true;
            this.SelfIPLabel.Location = new System.Drawing.Point(59, 9);
            this.SelfIPLabel.Name = "SelfIPLabel";
            this.SelfIPLabel.Size = new System.Drawing.Size(41, 12);
            this.SelfIPLabel.TabIndex = 2;
            this.SelfIPLabel.Text = "label2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "端口";
            // 
            // SelfPortBox
            // 
            this.SelfPortBox.Location = new System.Drawing.Point(59, 14);
            this.SelfPortBox.Name = "SelfPortBox";
            this.SelfPortBox.Size = new System.Drawing.Size(100, 21);
            this.SelfPortBox.TabIndex = 4;
            this.SelfPortBox.Text = "8001";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(189, 309);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox3.Size = new System.Drawing.Size(599, 129);
            this.textBox3.TabIndex = 5;
            // 
            // CreateRoomButton
            // 
            this.CreateRoomButton.Location = new System.Drawing.Point(6, 41);
            this.CreateRoomButton.Name = "CreateRoomButton";
            this.CreateRoomButton.Size = new System.Drawing.Size(149, 23);
            this.CreateRoomButton.TabIndex = 6;
            this.CreateRoomButton.Text = "创建房间";
            this.CreateRoomButton.UseVisualStyleBackColor = true;
            this.CreateRoomButton.Click += new System.EventHandler(this.CreateRoomButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.RoomStateLabel);
            this.groupBox1.Controls.Add(this.CreateRoomButton);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.SelfPortBox);
            this.groupBox1.Location = new System.Drawing.Point(14, 37);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(169, 85);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "创建房间";
            // 
            // RoomStateLabel
            // 
            this.RoomStateLabel.AutoSize = true;
            this.RoomStateLabel.Location = new System.Drawing.Point(10, 67);
            this.RoomStateLabel.Name = "RoomStateLabel";
            this.RoomStateLabel.Size = new System.Drawing.Size(41, 12);
            this.RoomStateLabel.TabIndex = 7;
            this.RoomStateLabel.Text = "未创建";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.JoinRoomButton);
            this.groupBox2.Controls.Add(this.HostPortBox);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.HostIpBox);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(14, 138);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(169, 100);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "加入房间";
            // 
            // JoinRoomButton
            // 
            this.JoinRoomButton.Location = new System.Drawing.Point(6, 69);
            this.JoinRoomButton.Name = "JoinRoomButton";
            this.JoinRoomButton.Size = new System.Drawing.Size(149, 23);
            this.JoinRoomButton.TabIndex = 7;
            this.JoinRoomButton.Text = "加入房间";
            this.JoinRoomButton.UseVisualStyleBackColor = true;
            this.JoinRoomButton.Click += new System.EventHandler(this.JoinRoomButton_Click);
            // 
            // HostPortBox
            // 
            this.HostPortBox.Location = new System.Drawing.Point(55, 42);
            this.HostPortBox.Name = "HostPortBox";
            this.HostPortBox.Size = new System.Drawing.Size(100, 21);
            this.HostPortBox.TabIndex = 3;
            this.HostPortBox.Text = "8001";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "端口";
            // 
            // HostIpBox
            // 
            this.HostIpBox.Location = new System.Drawing.Point(55, 14);
            this.HostIpBox.Name = "HostIpBox";
            this.HostIpBox.Size = new System.Drawing.Size(100, 21);
            this.HostIpBox.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "主机IP";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(14, 244);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(169, 88);
            this.listBox1.TabIndex = 9;
            // 
            // LogTextBox
            // 
            this.LogTextBox.Location = new System.Drawing.Point(12, 338);
            this.LogTextBox.Multiline = true;
            this.LogTextBox.Name = "LogTextBox";
            this.LogTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.LogTextBox.Size = new System.Drawing.Size(171, 130);
            this.LogTextBox.TabIndex = 10;
            // 
            // sendMessageButton
            // 
            this.sendMessageButton.Location = new System.Drawing.Point(712, 445);
            this.sendMessageButton.Name = "sendMessageButton";
            this.sendMessageButton.Size = new System.Drawing.Size(75, 23);
            this.sendMessageButton.TabIndex = 11;
            this.sendMessageButton.Text = "发送";
            this.sendMessageButton.UseVisualStyleBackColor = true;
            this.sendMessageButton.Click += new System.EventHandler(this.sendMessageButton_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(631, 445);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 12;
            this.button1.Text = "发送文件";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(189, 444);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(117, 23);
            this.button2.TabIndex = 13;
            this.button2.Text = "获取独立服务端";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // LanCaller
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 475);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.sendMessageButton);
            this.Controls.Add(this.LogTextBox);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.SelfIPLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Name = "LanCaller";
            this.Text = "多机合作助手";
            this.Load += new System.EventHandler(this.LanCaller_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label SelfIPLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox SelfPortBox;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Button CreateRoomButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button JoinRoomButton;
        private System.Windows.Forms.TextBox HostPortBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox HostIpBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label RoomStateLabel;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TextBox LogTextBox;
        private System.Windows.Forms.Button sendMessageButton;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}