namespace system_analysis
{
    partial class Form_login
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
            this.button_back = new System.Windows.Forms.Button();
            this.button_cross = new System.Windows.Forms.Button();
            this.button_minimize = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_entry = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_password = new System.Windows.Forms.TextBox();
            this.comboBox_user = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label_analyst = new System.Windows.Forms.Label();
            this.label_error = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // button_back
            // 
            this.button_back.BackColor = System.Drawing.SystemColors.Control;
            this.button_back.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_back.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_back.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(238)))), ((int)(((byte)(240)))));
            this.button_back.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(238)))), ((int)(((byte)(240)))));
            this.button_back.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(238)))), ((int)(((byte)(240)))));
            this.button_back.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_back.Font = new System.Drawing.Font("Tahoma", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_back.ForeColor = System.Drawing.Color.Black;
            this.button_back.Location = new System.Drawing.Point(-1, -14);
            this.button_back.Name = "button_back";
            this.button_back.Size = new System.Drawing.Size(61, 40);
            this.button_back.TabIndex = 19;
            this.button_back.TabStop = false;
            this.button_back.Text = "←";
            this.button_back.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button_back.UseVisualStyleBackColor = false;
            this.button_back.Click += new System.EventHandler(this.button_back_Click);
            // 
            // button_cross
            // 
            this.button_cross.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_cross.BackColor = System.Drawing.SystemColors.Control;
            this.button_cross.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_cross.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(238)))), ((int)(((byte)(240)))));
            this.button_cross.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(238)))), ((int)(((byte)(240)))));
            this.button_cross.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(238)))), ((int)(((byte)(240)))));
            this.button_cross.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_cross.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_cross.ForeColor = System.Drawing.Color.Black;
            this.button_cross.Location = new System.Drawing.Point(547, -1);
            this.button_cross.Name = "button_cross";
            this.button_cross.Size = new System.Drawing.Size(40, 40);
            this.button_cross.TabIndex = 21;
            this.button_cross.TabStop = false;
            this.button_cross.Text = "✖";
            this.button_cross.UseVisualStyleBackColor = false;
            this.button_cross.Click += new System.EventHandler(this.button_cross_Click);
            // 
            // button_minimize
            // 
            this.button_minimize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_minimize.BackColor = System.Drawing.SystemColors.Control;
            this.button_minimize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_minimize.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(238)))), ((int)(((byte)(240)))));
            this.button_minimize.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(238)))), ((int)(((byte)(240)))));
            this.button_minimize.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(238)))), ((int)(((byte)(240)))));
            this.button_minimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_minimize.Font = new System.Drawing.Font("Tahoma", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_minimize.ForeColor = System.Drawing.Color.Black;
            this.button_minimize.Location = new System.Drawing.Point(501, -14);
            this.button_minimize.Name = "button_minimize";
            this.button_minimize.Size = new System.Drawing.Size(40, 40);
            this.button_minimize.TabIndex = 20;
            this.button_minimize.TabStop = false;
            this.button_minimize.Text = "-";
            this.button_minimize.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button_minimize.UseVisualStyleBackColor = false;
            this.button_minimize.Click += new System.EventHandler(this.button_minimize_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(249, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 33);
            this.label1.TabIndex = 22;
            this.label1.Text = "Вход";
            // 
            // btn_entry
            // 
            this.btn_entry.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(118)))), ((int)(((byte)(168)))));
            this.btn_entry.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_entry.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn_entry.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkOrange;
            this.btn_entry.FlatAppearance.MouseOverBackColor = System.Drawing.Color.SandyBrown;
            this.btn_entry.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_entry.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_entry.Location = new System.Drawing.Point(204, 258);
            this.btn_entry.Name = "btn_entry";
            this.btn_entry.Size = new System.Drawing.Size(172, 43);
            this.btn_entry.TabIndex = 23;
            this.btn_entry.TabStop = false;
            this.btn_entry.Text = "Войти";
            this.btn_entry.UseVisualStyleBackColor = false;
            this.btn_entry.Click += new System.EventHandler(this.btn_entry_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(87, 165);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 25);
            this.label3.TabIndex = 33;
            this.label3.Text = "Пароль:";
            // 
            // txt_password
            // 
            this.txt_password.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txt_password.Location = new System.Drawing.Point(183, 167);
            this.txt_password.MaxLength = 15;
            this.txt_password.Name = "txt_password";
            this.txt_password.PasswordChar = '*';
            this.txt_password.Size = new System.Drawing.Size(360, 27);
            this.txt_password.TabIndex = 34;
            this.txt_password.TabStop = false;
            this.txt_password.TextChanged += new System.EventHandler(this.txt_password_TextChanged);
            this.txt_password.MouseDown += new System.Windows.Forms.MouseEventHandler(this.txt_password_MouseDown);
            // 
            // comboBox_user
            // 
            this.comboBox_user.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_user.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBox_user.FormattingEnabled = true;
            this.comboBox_user.Location = new System.Drawing.Point(183, 114);
            this.comboBox_user.MaxDropDownItems = 5;
            this.comboBox_user.Name = "comboBox_user";
            this.comboBox_user.Size = new System.Drawing.Size(360, 27);
            this.comboBox_user.TabIndex = 35;
            this.comboBox_user.SelectedIndexChanged += new System.EventHandler(this.comboBox_user_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(23, 112);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(154, 25);
            this.label2.TabIndex = 32;
            this.label2.Text = "Пользователь:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_analyst
            // 
            this.label_analyst.AutoSize = true;
            this.label_analyst.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_analyst.ForeColor = System.Drawing.Color.Black;
            this.label_analyst.Location = new System.Drawing.Point(183, 112);
            this.label_analyst.Name = "label_analyst";
            this.label_analyst.Size = new System.Drawing.Size(104, 25);
            this.label_analyst.TabIndex = 36;
            this.label_analyst.Text = "Аналитик";
            // 
            // label_error
            // 
            this.label_error.AutoSize = true;
            this.label_error.Font = new System.Drawing.Font("Tahoma", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_error.ForeColor = System.Drawing.Color.Red;
            this.label_error.Location = new System.Drawing.Point(201, 221);
            this.label_error.Name = "label_error";
            this.label_error.Size = new System.Drawing.Size(175, 21);
            this.label_error.TabIndex = 38;
            this.label_error.Text = "Неверный пароль!";
            this.label_error.Visible = false;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBox1.Location = new System.Drawing.Point(414, 200);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(127, 20);
            this.checkBox1.TabIndex = 39;
            this.checkBox1.TabStop = false;
            this.checkBox1.Text = "Показать пароль";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // Form_login
            // 
            this.AcceptButton = this.btn_entry;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.CancelButton = this.button_back;
            this.ClientSize = new System.Drawing.Size(586, 328);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label_error);
            this.Controls.Add(this.label_analyst);
            this.Controls.Add(this.comboBox_user);
            this.Controls.Add(this.txt_password);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btn_entry);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_cross);
            this.Controls.Add(this.button_minimize);
            this.Controls.Add(this.button_back);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "Form_login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Вход";
            this.Load += new System.EventHandler(this.Form_login_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form_login_MouseDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_back;
        private System.Windows.Forms.Button button_cross;
        private System.Windows.Forms.Button button_minimize;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_entry;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_password;
        public System.Windows.Forms.ComboBox comboBox_user;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label label_analyst;
        private System.Windows.Forms.Label label_error;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}