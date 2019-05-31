namespace system_analysis
{
    partial class Form_problem_add
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
            this.btn_edit_cancel = new System.Windows.Forms.Button();
            this.btn_edit_ok = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btn_edit_cancel
            // 
            this.btn_edit_cancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(118)))), ((int)(((byte)(168)))));
            this.btn_edit_cancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_edit_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_edit_cancel.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn_edit_cancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkOrange;
            this.btn_edit_cancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.SandyBrown;
            this.btn_edit_cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_edit_cancel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_edit_cancel.Location = new System.Drawing.Point(382, 103);
            this.btn_edit_cancel.Name = "btn_edit_cancel";
            this.btn_edit_cancel.Size = new System.Drawing.Size(100, 40);
            this.btn_edit_cancel.TabIndex = 34;
            this.btn_edit_cancel.TabStop = false;
            this.btn_edit_cancel.Text = "Отмена";
            this.btn_edit_cancel.UseVisualStyleBackColor = false;
            this.btn_edit_cancel.Click += new System.EventHandler(this.btn_edit_cancel_Click);
            // 
            // btn_edit_ok
            // 
            this.btn_edit_ok.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(118)))), ((int)(((byte)(168)))));
            this.btn_edit_ok.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_edit_ok.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn_edit_ok.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkOrange;
            this.btn_edit_ok.FlatAppearance.MouseOverBackColor = System.Drawing.Color.SandyBrown;
            this.btn_edit_ok.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_edit_ok.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_edit_ok.Location = new System.Drawing.Point(237, 103);
            this.btn_edit_ok.Name = "btn_edit_ok";
            this.btn_edit_ok.Size = new System.Drawing.Size(64, 40);
            this.btn_edit_ok.TabIndex = 33;
            this.btn_edit_ok.TabStop = false;
            this.btn_edit_ok.Text = "Ок";
            this.btn_edit_ok.UseVisualStyleBackColor = false;
            this.btn_edit_ok.Click += new System.EventHandler(this.btn_edit_ok_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(260, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 23);
            this.label3.TabIndex = 32;
            this.label3.Text = "Проблема";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(40, 61);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(571, 21);
            this.textBox1.TabIndex = 31;
            // 
            // Form_problem_add
            // 
            this.AcceptButton = this.btn_edit_ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_edit_cancel;
            this.ClientSize = new System.Drawing.Size(651, 164);
            this.Controls.Add(this.btn_edit_cancel);
            this.Controls.Add(this.btn_edit_ok);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.Name = "Form_problem_add";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Добавление/Редактирование формулировки проблемы";
            this.Load += new System.EventHandler(this.Form_problem_add_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_edit_cancel;
        private System.Windows.Forms.Button btn_edit_ok;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox textBox1;
    }
}