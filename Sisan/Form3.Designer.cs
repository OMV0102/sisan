namespace system_analysis
{
    partial class form3_analyst_add
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_save_all = new System.Windows.Forms.Button();
            this.button_minimize = new System.Windows.Forms.Button();
            this.button_cross = new System.Windows.Forms.Button();
            this.button_back = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_save = new System.Windows.Forms.Button();
            this.list_solution = new System.Windows.Forms.ListBox();
            this.list_menu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.list_menu_edit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.list_menu_delete = new System.Windows.Forms.ToolStripMenuItem();
            this.txt_solution = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox_problems = new System.Windows.Forms.ComboBox();
            this.btn_add_problem = new System.Windows.Forms.Button();
            this.btn_problem_edit = new System.Windows.Forms.Button();
            this.btn_problem_delete = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label_save_status = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.list_menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(321, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 23);
            this.label1.TabIndex = 13;
            this.label1.Text = "Проблема";
            // 
            // btn_save_all
            // 
            this.btn_save_all.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(118)))), ((int)(((byte)(168)))));
            this.btn_save_all.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_save_all.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn_save_all.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkOrange;
            this.btn_save_all.FlatAppearance.MouseOverBackColor = System.Drawing.Color.SandyBrown;
            this.btn_save_all.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_save_all.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_save_all.Location = new System.Drawing.Point(540, 670);
            this.btn_save_all.Name = "btn_save_all";
            this.btn_save_all.Size = new System.Drawing.Size(165, 53);
            this.btn_save_all.TabIndex = 14;
            this.btn_save_all.TabStop = false;
            this.btn_save_all.Text = "Сохранить";
            this.btn_save_all.UseMnemonic = false;
            this.btn_save_all.UseVisualStyleBackColor = false;
            this.btn_save_all.Click += new System.EventHandler(this.btn_save_all_Click);
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
            this.button_minimize.Location = new System.Drawing.Point(710, -13);
            this.button_minimize.Name = "button_minimize";
            this.button_minimize.Size = new System.Drawing.Size(40, 40);
            this.button_minimize.TabIndex = 16;
            this.button_minimize.TabStop = false;
            this.button_minimize.Text = "-";
            this.button_minimize.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button_minimize.UseVisualStyleBackColor = false;
            this.button_minimize.Click += new System.EventHandler(this.button_minimize_Click);
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
            this.button_cross.Location = new System.Drawing.Point(756, 0);
            this.button_cross.Name = "button_cross";
            this.button_cross.Size = new System.Drawing.Size(40, 40);
            this.button_cross.TabIndex = 15;
            this.button_cross.TabStop = false;
            this.button_cross.Text = "✖";
            this.button_cross.UseVisualStyleBackColor = false;
            this.button_cross.Click += new System.EventHandler(this.button_cross_Click);
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
            this.button_back.Location = new System.Drawing.Point(1, -13);
            this.button_back.Name = "button_back";
            this.button_back.Size = new System.Drawing.Size(61, 40);
            this.button_back.TabIndex = 17;
            this.button_back.TabStop = false;
            this.button_back.Text = "←";
            this.button_back.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button_back.UseVisualStyleBackColor = false;
            this.button_back.Click += new System.EventHandler(this.button_back_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(299, 394);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(152, 23);
            this.label3.TabIndex = 27;
            this.label3.Text = "Альтернативы";
            // 
            // btn_save
            // 
            this.btn_save.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(118)))), ((int)(((byte)(168)))));
            this.btn_save.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_save.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn_save.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkOrange;
            this.btn_save.FlatAppearance.MouseOverBackColor = System.Drawing.Color.SandyBrown;
            this.btn_save.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_save.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_save.Location = new System.Drawing.Point(278, 331);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(239, 40);
            this.btn_save.TabIndex = 26;
            this.btn_save.TabStop = false;
            this.btn_save.Text = "Добавить альтернативу";
            this.btn_save.UseVisualStyleBackColor = false;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // list_solution
            // 
            this.list_solution.ContextMenuStrip = this.list_menu;
            this.list_solution.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.list_solution.FormattingEnabled = true;
            this.list_solution.HorizontalScrollbar = true;
            this.list_solution.ItemHeight = 19;
            this.list_solution.Location = new System.Drawing.Point(101, 437);
            this.list_solution.Name = "list_solution";
            this.list_solution.Size = new System.Drawing.Size(604, 194);
            this.list_solution.TabIndex = 29;
            this.list_solution.MouseDown += new System.Windows.Forms.MouseEventHandler(this.list_solution_MouseDown);
            // 
            // list_menu
            // 
            this.list_menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.list_menu_edit,
            this.toolStripSeparator1,
            this.list_menu_delete});
            this.list_menu.Name = "list_menu";
            this.list_menu.Size = new System.Drawing.Size(229, 54);
            // 
            // list_menu_edit
            // 
            this.list_menu_edit.Name = "list_menu_edit";
            this.list_menu_edit.Size = new System.Drawing.Size(228, 22);
            this.list_menu_edit.Text = "Редактировать альтернативу";
            this.list_menu_edit.Click += new System.EventHandler(this.list_menu_edit_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(225, 6);
            // 
            // list_menu_delete
            // 
            this.list_menu_delete.Name = "list_menu_delete";
            this.list_menu_delete.Size = new System.Drawing.Size(228, 22);
            this.list_menu_delete.Text = "Удалить альтернативу";
            this.list_menu_delete.Click += new System.EventHandler(this.list_menu_delete_Click);
            // 
            // txt_solution
            // 
            this.txt_solution.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txt_solution.Location = new System.Drawing.Point(101, 282);
            this.txt_solution.Name = "txt_solution";
            this.txt_solution.Size = new System.Drawing.Size(604, 27);
            this.txt_solution.TabIndex = 30;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(241, 235);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(276, 23);
            this.label2.TabIndex = 31;
            this.label2.Text = "Добавление альтернативы";
            // 
            // comboBox_problems
            // 
            this.comboBox_problems.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_problems.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBox_problems.FormattingEnabled = true;
            this.comboBox_problems.Location = new System.Drawing.Point(101, 88);
            this.comboBox_problems.MaxDropDownItems = 10;
            this.comboBox_problems.Name = "comboBox_problems";
            this.comboBox_problems.Size = new System.Drawing.Size(604, 27);
            this.comboBox_problems.TabIndex = 32;
            this.comboBox_problems.SelectedIndexChanged += new System.EventHandler(this.comboBox_problems_SelectedIndexChanged);
            // 
            // btn_add_problem
            // 
            this.btn_add_problem.BackColor = System.Drawing.SystemColors.Control;
            this.btn_add_problem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_add_problem.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(238)))), ((int)(((byte)(240)))));
            this.btn_add_problem.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(238)))), ((int)(((byte)(240)))));
            this.btn_add_problem.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(238)))), ((int)(((byte)(240)))));
            this.btn_add_problem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_add_problem.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_add_problem.ForeColor = System.Drawing.Color.Green;
            this.btn_add_problem.Location = new System.Drawing.Point(21, 73);
            this.btn_add_problem.Name = "btn_add_problem";
            this.btn_add_problem.Size = new System.Drawing.Size(30, 42);
            this.btn_add_problem.TabIndex = 33;
            this.btn_add_problem.TabStop = false;
            this.btn_add_problem.Text = "+";
            this.btn_add_problem.UseVisualStyleBackColor = false;
            this.btn_add_problem.Click += new System.EventHandler(this.btn_add_problem_Click);
            // 
            // btn_problem_edit
            // 
            this.btn_problem_edit.BackColor = System.Drawing.SystemColors.Control;
            this.btn_problem_edit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_problem_edit.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(238)))), ((int)(((byte)(240)))));
            this.btn_problem_edit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(238)))), ((int)(((byte)(240)))));
            this.btn_problem_edit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(238)))), ((int)(((byte)(240)))));
            this.btn_problem_edit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_problem_edit.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_problem_edit.Location = new System.Drawing.Point(54, 76);
            this.btn_problem_edit.Name = "btn_problem_edit";
            this.btn_problem_edit.Size = new System.Drawing.Size(39, 42);
            this.btn_problem_edit.TabIndex = 34;
            this.btn_problem_edit.TabStop = false;
            this.btn_problem_edit.Text = "✎";
            this.btn_problem_edit.UseVisualStyleBackColor = false;
            this.btn_problem_edit.Click += new System.EventHandler(this.btn_problem_edit_Click);
            // 
            // btn_problem_delete
            // 
            this.btn_problem_delete.BackColor = System.Drawing.SystemColors.Control;
            this.btn_problem_delete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_problem_delete.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(238)))), ((int)(((byte)(240)))));
            this.btn_problem_delete.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(238)))), ((int)(((byte)(240)))));
            this.btn_problem_delete.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(238)))), ((int)(((byte)(240)))));
            this.btn_problem_delete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_problem_delete.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_problem_delete.ForeColor = System.Drawing.Color.Red;
            this.btn_problem_delete.Location = new System.Drawing.Point(711, 78);
            this.btn_problem_delete.Name = "btn_problem_delete";
            this.btn_problem_delete.Size = new System.Drawing.Size(30, 42);
            this.btn_problem_delete.TabIndex = 35;
            this.btn_problem_delete.TabStop = false;
            this.btn_problem_delete.Text = "✖";
            this.btn_problem_delete.UseVisualStyleBackColor = false;
            this.btn_problem_delete.Click += new System.EventHandler(this.btn_problem_delete_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.ForeColor = System.Drawing.Color.DimGray;
            this.label4.Location = new System.Drawing.Point(98, 634);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(563, 18);
            this.label4.TabIndex = 36;
            this.label4.Text = "Нажмите ПКМ по альтернативе, чтобы редактировать или удалить ее.\r\n";
            // 
            // label_save_status
            // 
            this.label_save_status.AutoSize = true;
            this.label_save_status.Font = new System.Drawing.Font("Tahoma", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_save_status.ForeColor = System.Drawing.Color.Red;
            this.label_save_status.Location = new System.Drawing.Point(50, 686);
            this.label_save_status.Name = "label_save_status";
            this.label_save_status.Size = new System.Drawing.Size(475, 21);
            this.label_save_status.TabIndex = 37;
            this.label_save_status.Text = "Внесены изменения, необходимо сохранение!  ---->";
            this.label_save_status.Visible = false;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.BackColor = System.Drawing.SystemColors.ControlDark;
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label5.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(0, 40);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(795, 5);
            this.label5.TabIndex = 59;
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(265, 5);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(252, 29);
            this.label6.TabIndex = 60;
            this.label6.Text = "Работа с проблемами";
            // 
            // form3_analyst_add
            // 
            this.AcceptButton = this.btn_save;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.CancelButton = this.button_back;
            this.ClientSize = new System.Drawing.Size(795, 744);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label_save_status);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btn_problem_delete);
            this.Controls.Add(this.btn_problem_edit);
            this.Controls.Add(this.btn_add_problem);
            this.Controls.Add(this.comboBox_problems);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_solution);
            this.Controls.Add(this.list_solution);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.button_back);
            this.Controls.Add(this.button_minimize);
            this.Controls.Add(this.button_cross);
            this.Controls.Add(this.btn_save_all);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "form3_analyst_add";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Редактирование проблем";
            this.Load += new System.EventHandler(this.form3_analyst_add_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.form3_analyst_add_MouseDown);
            this.list_menu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_save_all;
        private System.Windows.Forms.Button button_minimize;
        private System.Windows.Forms.Button button_cross;
        private System.Windows.Forms.Button button_back;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.TextBox txt_solution;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ContextMenuStrip list_menu;
        private System.Windows.Forms.ToolStripMenuItem list_menu_edit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem list_menu_delete;
        public System.Windows.Forms.ListBox list_solution;
        private System.Windows.Forms.Button btn_add_problem;
        public System.Windows.Forms.ComboBox comboBox_problems;
        private System.Windows.Forms.Button btn_problem_edit;
        private System.Windows.Forms.Button btn_problem_delete;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label_save_status;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}