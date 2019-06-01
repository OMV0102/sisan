namespace system_analysis
{
    partial class Form_change_pass
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
            this.button_cross = new System.Windows.Forms.Button();
            this.button_minimize = new System.Windows.Forms.Button();
            this.btn_save = new System.Windows.Forms.Button();
            this.button_back = new System.Windows.Forms.Button();
            this.label_error = new System.Windows.Forms.Label();
            this.txt_pass_curr = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_pass_new = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label_error2 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button_cross
            // 
            this.button_cross.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_cross.BackColor = System.Drawing.SystemColors.Control;
            this.button_cross.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_cross.FlatAppearance.BorderSize = 0;
            this.button_cross.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_cross.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_cross.ForeColor = System.Drawing.Color.Black;
            this.button_cross.Location = new System.Drawing.Point(515, -1);
            this.button_cross.Name = "button_cross";
            this.button_cross.Size = new System.Drawing.Size(40, 40);
            this.button_cross.TabIndex = 23;
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
            this.button_minimize.FlatAppearance.BorderSize = 0;
            this.button_minimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_minimize.Font = new System.Drawing.Font("Tahoma", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_minimize.ForeColor = System.Drawing.Color.Black;
            this.button_minimize.Location = new System.Drawing.Point(469, -14);
            this.button_minimize.Name = "button_minimize";
            this.button_minimize.Size = new System.Drawing.Size(40, 40);
            this.button_minimize.TabIndex = 22;
            this.button_minimize.TabStop = false;
            this.button_minimize.Text = "-";
            this.button_minimize.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button_minimize.UseVisualStyleBackColor = false;
            this.button_minimize.Click += new System.EventHandler(this.button_minimize_Click);
            // 
            // btn_save
            // 
            this.btn_save.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(118)))), ((int)(((byte)(168)))));
            this.btn_save.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_save.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn_save.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkOrange;
            this.btn_save.FlatAppearance.MouseOverBackColor = System.Drawing.Color.SandyBrown;
            this.btn_save.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_save.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_save.Location = new System.Drawing.Point(190, 278);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(172, 43);
            this.btn_save.TabIndex = 24;
            this.btn_save.TabStop = false;
            this.btn_save.Text = "Сохранить";
            this.btn_save.UseVisualStyleBackColor = false;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // button_back
            // 
            this.button_back.BackColor = System.Drawing.SystemColors.Control;
            this.button_back.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_back.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_back.FlatAppearance.BorderSize = 0;
            this.button_back.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_back.Font = new System.Drawing.Font("Tahoma", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_back.ForeColor = System.Drawing.Color.Black;
            this.button_back.Location = new System.Drawing.Point(-2, -14);
            this.button_back.Name = "button_back";
            this.button_back.Size = new System.Drawing.Size(61, 40);
            this.button_back.TabIndex = 25;
            this.button_back.TabStop = false;
            this.button_back.Text = "←";
            this.button_back.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button_back.UseVisualStyleBackColor = false;
            this.button_back.Click += new System.EventHandler(this.button_back_Click);
            // 
            // label_error
            // 
            this.label_error.AutoSize = true;
            this.label_error.Font = new System.Drawing.Font("Tahoma", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_error.ForeColor = System.Drawing.Color.Red;
            this.label_error.Location = new System.Drawing.Point(206, 64);
            this.label_error.Name = "label_error";
            this.label_error.Size = new System.Drawing.Size(175, 21);
            this.label_error.TabIndex = 41;
            this.label_error.Text = "Неверный пароль!";
            this.label_error.Visible = false;
            // 
            // txt_pass_curr
            // 
            this.txt_pass_curr.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txt_pass_curr.Location = new System.Drawing.Point(143, 103);
            this.txt_pass_curr.MaxLength = 15;
            this.txt_pass_curr.Name = "txt_pass_curr";
            this.txt_pass_curr.PasswordChar = '*';
            this.txt_pass_curr.Size = new System.Drawing.Size(334, 27);
            this.txt_pass_curr.TabIndex = 40;
            this.txt_pass_curr.TextChanged += new System.EventHandler(this.txt_pass_curr_TextChanged);
            this.txt_pass_curr.MouseDown += new System.Windows.Forms.MouseEventHandler(this.txt_pass_curr_MouseDown);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(38, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 61);
            this.label3.TabIndex = 39;
            this.label3.Text = "Текущий пароль:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_pass_new
            // 
            this.txt_pass_new.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txt_pass_new.Location = new System.Drawing.Point(143, 208);
            this.txt_pass_new.MaxLength = 15;
            this.txt_pass_new.Name = "txt_pass_new";
            this.txt_pass_new.Size = new System.Drawing.Size(334, 27);
            this.txt_pass_new.TabIndex = 43;
            this.txt_pass_new.TextChanged += new System.EventHandler(this.txt_pass_new_TextChanged);
            this.txt_pass_new.MouseDown += new System.Windows.Forms.MouseEventHandler(this.txt_pass_new_MouseDown);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(38, 188);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 61);
            this.label1.TabIndex = 42;
            this.label1.Text = "Новый пароль:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_error2
            // 
            this.label_error2.AutoSize = true;
            this.label_error2.Font = new System.Drawing.Font("Tahoma", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_error2.ForeColor = System.Drawing.Color.Red;
            this.label_error2.Location = new System.Drawing.Point(190, 176);
            this.label_error2.Name = "label_error2";
            this.label_error2.Size = new System.Drawing.Size(237, 21);
            this.label_error2.TabIndex = 44;
            this.label_error2.Text = "Новый пароль не введен!";
            this.label_error2.Visible = false;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.checkBox1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBox1.Location = new System.Drawing.Point(350, 132);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(127, 20);
            this.checkBox1.TabIndex = 45;
            this.checkBox1.Text = "Показать пароль";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.checkBox2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBox2.Location = new System.Drawing.Point(350, 241);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(127, 20);
            this.checkBox2.TabIndex = 46;
            this.checkBox2.Text = "Показать пароль";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(193, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(169, 29);
            this.label2.TabIndex = 47;
            this.label2.Text = "Смена пароля";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.BackColor = System.Drawing.SystemColors.ControlDark;
            this.label9.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label9.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(0, 40);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(554, 5);
            this.label9.TabIndex = 48;
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Form_change_pass
            // 
            this.AcceptButton = this.btn_save;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_back;
            this.ClientSize = new System.Drawing.Size(554, 350);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label_error2);
            this.Controls.Add(this.txt_pass_new);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label_error);
            this.Controls.Add(this.txt_pass_curr);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button_back);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.button_cross);
            this.Controls.Add(this.button_minimize);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "Form_change_pass";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Смена пароля";
            this.Load += new System.EventHandler(this.Form_change_pass_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form_change_pass_MouseDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_cross;
        private System.Windows.Forms.Button button_minimize;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.Button button_back;
        private System.Windows.Forms.Label label_error;
        private System.Windows.Forms.TextBox txt_pass_curr;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_pass_new;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label_error2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label9;
    }
}