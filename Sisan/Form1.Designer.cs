namespace system_analysis
{
    partial class form1_main
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.button_analyst = new System.Windows.Forms.Button();
            this.button_expert = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button_cross = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.button_minimize = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button_analyst
            // 
            this.button_analyst.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(118)))), ((int)(((byte)(168)))));
            this.button_analyst.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_analyst.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.button_analyst.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkOrange;
            this.button_analyst.FlatAppearance.MouseOverBackColor = System.Drawing.Color.SandyBrown;
            this.button_analyst.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_analyst.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_analyst.Location = new System.Drawing.Point(65, 164);
            this.button_analyst.Name = "button_analyst";
            this.button_analyst.Size = new System.Drawing.Size(200, 70);
            this.button_analyst.TabIndex = 0;
            this.button_analyst.TabStop = false;
            this.button_analyst.Text = "Аналитик";
            this.button_analyst.UseVisualStyleBackColor = false;
            this.button_analyst.Click += new System.EventHandler(this.button_analyst_Click);
            // 
            // button_expert
            // 
            this.button_expert.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(118)))), ((int)(((byte)(168)))));
            this.button_expert.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_expert.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.button_expert.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkOrange;
            this.button_expert.FlatAppearance.MouseOverBackColor = System.Drawing.Color.SandyBrown;
            this.button_expert.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_expert.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_expert.Location = new System.Drawing.Point(397, 164);
            this.button_expert.Name = "button_expert";
            this.button_expert.Size = new System.Drawing.Size(200, 70);
            this.button_expert.TabIndex = 1;
            this.button_expert.TabStop = false;
            this.button_expert.Text = "Эксперт";
            this.button_expert.UseVisualStyleBackColor = false;
            this.button_expert.Click += new System.EventHandler(this.button_expert_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(-361, 226);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 42);
            this.label1.TabIndex = 2;
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button_cross
            // 
            this.button_cross.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_cross.BackColor = System.Drawing.SystemColors.Control;
            this.button_cross.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_cross.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_cross.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(238)))), ((int)(((byte)(240)))));
            this.button_cross.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(238)))), ((int)(((byte)(240)))));
            this.button_cross.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(238)))), ((int)(((byte)(240)))));
            this.button_cross.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_cross.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_cross.ForeColor = System.Drawing.Color.Black;
            this.button_cross.Location = new System.Drawing.Point(633, 0);
            this.button_cross.Name = "button_cross";
            this.button_cross.Size = new System.Drawing.Size(40, 40);
            this.button_cross.TabIndex = 3;
            this.button_cross.TabStop = false;
            this.button_cross.Text = "✖";
            this.button_cross.UseVisualStyleBackColor = false;
            this.button_cross.Click += new System.EventHandler(this.button_cross_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(239, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(174, 39);
            this.label2.TabIndex = 4;
            this.label2.Text = "Войти как:";
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
            this.button_minimize.Location = new System.Drawing.Point(582, -10);
            this.button_minimize.Name = "button_minimize";
            this.button_minimize.Size = new System.Drawing.Size(40, 40);
            this.button_minimize.TabIndex = 5;
            this.button_minimize.TabStop = false;
            this.button_minimize.Text = "-";
            this.button_minimize.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button_minimize.UseVisualStyleBackColor = false;
            this.button_minimize.Click += new System.EventHandler(this.button_minimize_Click);
            // 
            // form1_main
            // 
            this.AllowDrop = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(672, 297);
            this.Controls.Add(this.button_minimize);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button_cross);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_expert);
            this.Controls.Add(this.button_analyst);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "form1_main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Экспертные оценки";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.form1_main_KeyDown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.form1_main_MouseDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_analyst;
        private System.Windows.Forms.Button button_expert;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_cross;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button_minimize;
    }
}

