namespace NCToolsKidForJava.Forms
{
    partial class VOBuilderForm
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
            this.label4 = new System.Windows.Forms.Label();
            this.dbMdCode = new System.Windows.Forms.TextBox();
            this.loadMDDictionaryButton = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.resVOTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.ResTextBox = new System.Windows.Forms.TextBox();
            this.build_button = new System.Windows.Forms.Button();
            this.formattingButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(93, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "元数据";
            // 
            // dbMdCode
            // 
            this.dbMdCode.Location = new System.Drawing.Point(140, 12);
            this.dbMdCode.Name = "dbMdCode";
            this.dbMdCode.Size = new System.Drawing.Size(167, 21);
            this.dbMdCode.TabIndex = 8;
            // 
            // loadMDDictionaryButton
            // 
            this.loadMDDictionaryButton.Location = new System.Drawing.Point(12, 12);
            this.loadMDDictionaryButton.Name = "loadMDDictionaryButton";
            this.loadMDDictionaryButton.Size = new System.Drawing.Size(75, 23);
            this.loadMDDictionaryButton.TabIndex = 9;
            this.loadMDDictionaryButton.Text = "加载元数据对照";
            this.loadMDDictionaryButton.UseVisualStyleBackColor = true;
            this.loadMDDictionaryButton.Click += new System.EventHandler(this.loadMDDictionaryButton_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(15, 41);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(292, 397);
            this.dataGridView1.TabIndex = 12;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // resVOTextBox
            // 
            this.resVOTextBox.Location = new System.Drawing.Point(322, 39);
            this.resVOTextBox.Multiline = true;
            this.resVOTextBox.Name = "resVOTextBox";
            this.resVOTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.resVOTextBox.Size = new System.Drawing.Size(466, 211);
            this.resVOTextBox.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(320, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 14;
            this.label6.Text = "参数";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(320, 253);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 15;
            this.label7.Text = "结果";
            // 
            // ResTextBox
            // 
            this.ResTextBox.Location = new System.Drawing.Point(322, 268);
            this.ResTextBox.Multiline = true;
            this.ResTextBox.Name = "ResTextBox";
            this.ResTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.ResTextBox.Size = new System.Drawing.Size(466, 170);
            this.ResTextBox.TabIndex = 16;
            this.ResTextBox.WordWrap = false;
            // 
            // build_button
            // 
            this.build_button.Location = new System.Drawing.Point(713, 12);
            this.build_button.Name = "build_button";
            this.build_button.Size = new System.Drawing.Size(75, 23);
            this.build_button.TabIndex = 17;
            this.build_button.Text = "构建";
            this.build_button.UseVisualStyleBackColor = true;
            this.build_button.Click += new System.EventHandler(this.build_button_Click);
            // 
            // formattingButton
            // 
            this.formattingButton.Location = new System.Drawing.Point(632, 12);
            this.formattingButton.Name = "formattingButton";
            this.formattingButton.Size = new System.Drawing.Size(75, 23);
            this.formattingButton.TabIndex = 18;
            this.formattingButton.Text = "格式化";
            this.formattingButton.UseVisualStyleBackColor = true;
            this.formattingButton.Click += new System.EventHandler(this.formattingButton_Click);
            // 
            // VOBuilderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.formattingButton);
            this.Controls.Add(this.build_button);
            this.Controls.Add(this.ResTextBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.resVOTextBox);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.loadMDDictionaryButton);
            this.Controls.Add(this.dbMdCode);
            this.Controls.Add(this.label4);
            this.Name = "VOBuilderForm";
            this.Text = "VO构造器";
            this.Load += new System.EventHandler(this.VOBuilderForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox dbMdCode;
        private System.Windows.Forms.Button loadMDDictionaryButton;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox resVOTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox ResTextBox;
        private System.Windows.Forms.Button build_button;
        private System.Windows.Forms.Button formattingButton;
    }
}