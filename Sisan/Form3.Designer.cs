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
            this.btn_alter_add = new System.Windows.Forms.Button();
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label7 = new System.Windows.Forms.Label();
            this.box_open_close = new System.Windows.Forms.CheckBox();
            this.lbl_notprob = new System.Windows.Forms.Label();
            this.txt_scale = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.Выбор = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.list_menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(646, 60);
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
            this.btn_save_all.Location = new System.Drawing.Point(818, 584);
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
            this.button_minimize.Location = new System.Drawing.Point(1315, -13);
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
            this.button_cross.Location = new System.Drawing.Point(1361, 0);
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
            this.label3.Location = new System.Drawing.Point(151, 317);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(395, 23);
            this.label3.TabIndex = 27;
            this.label3.Text = "Альтернативы для решения проблемы";
            // 
            // btn_alter_add
            // 
            this.btn_alter_add.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(118)))), ((int)(((byte)(168)))));
            this.btn_alter_add.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_alter_add.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn_alter_add.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkOrange;
            this.btn_alter_add.FlatAppearance.MouseOverBackColor = System.Drawing.Color.SandyBrown;
            this.btn_alter_add.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_alter_add.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_alter_add.Location = new System.Drawing.Point(204, 255);
            this.btn_alter_add.Name = "btn_alter_add";
            this.btn_alter_add.Size = new System.Drawing.Size(239, 40);
            this.btn_alter_add.TabIndex = 26;
            this.btn_alter_add.TabStop = false;
            this.btn_alter_add.Text = "Добавить альтернативу";
            this.btn_alter_add.UseVisualStyleBackColor = false;
            this.btn_alter_add.Click += new System.EventHandler(this.btn_alter_add_Click);
            // 
            // list_solution
            // 
            this.list_solution.ContextMenuStrip = this.list_menu;
            this.list_solution.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.list_solution.FormattingEnabled = true;
            this.list_solution.HorizontalScrollbar = true;
            this.list_solution.ItemHeight = 19;
            this.list_solution.Location = new System.Drawing.Point(32, 352);
            this.list_solution.Name = "list_solution";
            this.list_solution.Size = new System.Drawing.Size(604, 194);
            this.list_solution.TabIndex = 29;
            this.list_solution.TabStop = false;
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
            this.txt_solution.Location = new System.Drawing.Point(31, 219);
            this.txt_solution.Name = "txt_solution";
            this.txt_solution.Size = new System.Drawing.Size(604, 27);
            this.txt_solution.TabIndex = 30;
            this.txt_solution.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(190, 184);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(276, 23);
            this.label2.TabIndex = 31;
            this.label2.Text = "Добавление альтернативы";
            // 
            // comboBox_problems
            // 
            this.comboBox_problems.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboBox_problems.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_problems.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBox_problems.FormattingEnabled = true;
            this.comboBox_problems.Location = new System.Drawing.Point(244, 95);
            this.comboBox_problems.MaxDropDownItems = 10;
            this.comboBox_problems.Name = "comboBox_problems";
            this.comboBox_problems.Size = new System.Drawing.Size(856, 27);
            this.comboBox_problems.TabIndex = 32;
            this.comboBox_problems.TabStop = false;
            this.comboBox_problems.SelectedIndexChanged += new System.EventHandler(this.comboBox_problems_SelectedIndexChanged);
            // 
            // btn_add_problem
            // 
            this.btn_add_problem.BackColor = System.Drawing.SystemColors.Control;
            this.btn_add_problem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_add_problem.FlatAppearance.BorderSize = 0;
            this.btn_add_problem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_add_problem.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_add_problem.ForeColor = System.Drawing.Color.Green;
            this.btn_add_problem.Location = new System.Drawing.Point(989, 48);
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
            this.btn_problem_edit.FlatAppearance.BorderSize = 0;
            this.btn_problem_edit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_problem_edit.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_problem_edit.Location = new System.Drawing.Point(1025, 53);
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
            this.btn_problem_delete.FlatAppearance.BorderSize = 0;
            this.btn_problem_delete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_problem_delete.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_problem_delete.ForeColor = System.Drawing.Color.Red;
            this.btn_problem_delete.Location = new System.Drawing.Point(1070, 51);
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
            this.label4.Location = new System.Drawing.Point(35, 549);
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
            this.label_save_status.Location = new System.Drawing.Point(270, 600);
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
            this.label5.Size = new System.Drawing.Size(1400, 5);
            this.label5.TabIndex = 59;
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(580, 5);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(252, 29);
            this.label6.TabIndex = 60;
            this.label6.Text = "Работа с проблемами";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Выбор});
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridView1.Location = new System.Drawing.Point(684, 297);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(670, 239);
            this.dataGridView1.TabIndex = 61;
            this.dataGridView1.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(755, 255);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(533, 23);
            this.label7.TabIndex = 62;
            this.label7.Text = "Список экспертов, которые оцнивают альтернативы";
            // 
            // box_open_close
            // 
            this.box_open_close.AutoSize = true;
            this.box_open_close.Checked = true;
            this.box_open_close.CheckState = System.Windows.Forms.CheckState.Checked;
            this.box_open_close.Cursor = System.Windows.Forms.Cursors.Hand;
            this.box_open_close.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.box_open_close.Location = new System.Drawing.Point(38, 128);
            this.box_open_close.Name = "box_open_close";
            this.box_open_close.Size = new System.Drawing.Size(353, 27);
            this.box_open_close.TabIndex = 63;
            this.box_open_close.TabStop = false;
            this.box_open_close.Text = "Проблема ОТКРЫТА для оценивания";
            this.box_open_close.UseVisualStyleBackColor = true;
            // 
            // lbl_notprob
            // 
            this.lbl_notprob.AutoSize = true;
            this.lbl_notprob.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbl_notprob.ForeColor = System.Drawing.Color.Red;
            this.lbl_notprob.Location = new System.Drawing.Point(538, 143);
            this.lbl_notprob.Name = "lbl_notprob";
            this.lbl_notprob.Size = new System.Drawing.Size(251, 23);
            this.lbl_notprob.TabIndex = 69;
            this.lbl_notprob.Text = "Проблемы отсутствуют!";
            this.lbl_notprob.Visible = false;
            // 
            // txt_scale
            // 
            this.txt_scale.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txt_scale.Location = new System.Drawing.Point(1039, 177);
            this.txt_scale.Name = "txt_scale";
            this.txt_scale.Size = new System.Drawing.Size(69, 27);
            this.txt_scale.TabIndex = 70;
            this.txt_scale.TabStop = false;
            this.txt_scale.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(722, 166);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(311, 38);
            this.label8.TabIndex = 71;
            this.label8.Text = "Шкала для оценивания методом\r\n полного попарного сопоставления:";
            // 
            // Выбор
            // 
            this.Выбор.Frozen = true;
            this.Выбор.HeaderText = "Выбор";
            this.Выбор.Name = "Выбор";
            this.Выбор.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Выбор.Width = 50;
            // 
            // form3_analyst_add
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.CancelButton = this.button_back;
            this.ClientSize = new System.Drawing.Size(1400, 663);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txt_scale);
            this.Controls.Add(this.lbl_notprob);
            this.Controls.Add(this.box_open_close);
            this.Controls.Add(this.comboBox_problems);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label_save_status);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_solution);
            this.Controls.Add(this.list_solution);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btn_alter_add);
            this.Controls.Add(this.button_back);
            this.Controls.Add(this.button_minimize);
            this.Controls.Add(this.button_cross);
            this.Controls.Add(this.btn_save_all);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_problem_edit);
            this.Controls.Add(this.btn_problem_delete);
            this.Controls.Add(this.btn_add_problem);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "form3_analyst_add";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Редактирование проблем";
            this.Load += new System.EventHandler(this.form3_analyst_add_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.form3_analyst_add_MouseDown);
            this.list_menu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
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
        private System.Windows.Forms.Button btn_alter_add;
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
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox box_open_close;
        private System.Windows.Forms.Label lbl_notprob;
        private System.Windows.Forms.TextBox txt_scale;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Выбор;
    }
}