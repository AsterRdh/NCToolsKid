namespace NCToolsKidForJava.Forms
{
    partial class ConfigForm
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.TestBDConfigButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.dbPasswordTextBox = new System.Windows.Forms.TextBox();
            this.dbUsernameTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dbSIDTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dbIPTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.SaveCloseButton = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(776, 397);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.TestBDConfigButton);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.dbPasswordTextBox);
            this.tabPage1.Controls.Add(this.dbUsernameTextBox);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.dbSIDTextBox);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.dbIPTextBox);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(768, 371);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "数据库设置";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // TestBDConfigButton
            // 
            this.TestBDConfigButton.Location = new System.Drawing.Point(217, 114);
            this.TestBDConfigButton.Name = "TestBDConfigButton";
            this.TestBDConfigButton.Size = new System.Drawing.Size(75, 23);
            this.TestBDConfigButton.TabIndex = 8;
            this.TestBDConfigButton.Text = "测试";
            this.TestBDConfigButton.UseVisualStyleBackColor = true;
            this.TestBDConfigButton.Click += new System.EventHandler(this.TestBDConfigButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 89);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "密码";
            // 
            // dbPasswordTextBox
            // 
            this.dbPasswordTextBox.Location = new System.Drawing.Point(77, 86);
            this.dbPasswordTextBox.Name = "dbPasswordTextBox";
            this.dbPasswordTextBox.PasswordChar = '*';
            this.dbPasswordTextBox.Size = new System.Drawing.Size(216, 21);
            this.dbPasswordTextBox.TabIndex = 6;
            this.dbPasswordTextBox.UseSystemPasswordChar = true;
            // 
            // dbUsernameTextBox
            // 
            this.dbUsernameTextBox.Location = new System.Drawing.Point(77, 59);
            this.dbUsernameTextBox.Name = "dbUsernameTextBox";
            this.dbUsernameTextBox.Size = new System.Drawing.Size(216, 21);
            this.dbUsernameTextBox.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "用户名";
            // 
            // dbSIDTextBox
            // 
            this.dbSIDTextBox.Location = new System.Drawing.Point(77, 32);
            this.dbSIDTextBox.Name = "dbSIDTextBox";
            this.dbSIDTextBox.Size = new System.Drawing.Size(216, 21);
            this.dbSIDTextBox.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(48, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "SID";
            // 
            // dbIPTextBox
            // 
            this.dbIPTextBox.Location = new System.Drawing.Point(77, 6);
            this.dbIPTextBox.Name = "dbIPTextBox";
            this.dbIPTextBox.Size = new System.Drawing.Size(216, 21);
            this.dbIPTextBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "数据库地址";
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(768, 371);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // SaveCloseButton
            // 
            this.SaveCloseButton.Location = new System.Drawing.Point(709, 415);
            this.SaveCloseButton.Name = "SaveCloseButton";
            this.SaveCloseButton.Size = new System.Drawing.Size(75, 23);
            this.SaveCloseButton.TabIndex = 1;
            this.SaveCloseButton.Text = "确定";
            this.SaveCloseButton.UseVisualStyleBackColor = true;
            this.SaveCloseButton.Click += new System.EventHandler(this.SaveCloseButton_Click);
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(16, 415);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(75, 23);
            this.CloseButton.TabIndex = 2;
            this.CloseButton.Text = "取消";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(628, 415);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 3;
            this.SaveButton.Text = "应用";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.button4_Click);
            // 
            // ConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.SaveCloseButton);
            this.Controls.Add(this.tabControl1);
            this.Name = "ConfigForm";
            this.Text = "设置";
            this.Load += new System.EventHandler(this.ConfigForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button SaveCloseButton;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox dbPasswordTextBox;
        private System.Windows.Forms.TextBox dbUsernameTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox dbSIDTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox dbIPTextBox;
        private System.Windows.Forms.Button TestBDConfigButton;
        private System.Windows.Forms.Button SaveButton;
    }
}